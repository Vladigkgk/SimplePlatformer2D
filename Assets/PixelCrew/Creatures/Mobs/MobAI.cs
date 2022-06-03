using System.Collections;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using PixelCrew.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;
        [SerializeField] private float _agroDelay = 0.5f;
        [SerializeField] private float _coolDownAttack = 1f;
        [SerializeField] private float _coolDownMiss = 1f;

        private static readonly int isDeadKey = Animator.StringToHash("is-dead");

        private bool isDead;

        private Creature _creater;
        private SpawnListComponent _particles;
        private Animator _animator;
        private Patrol _patrol;

        private Coroutine _coroutine;
        private GameObject _target;

        private void Awake()
        {
            _creater = GetComponent<Creature>();
            _particles = GetComponent<SpawnListComponent>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        private void StartState(IEnumerator coroutine)
        {
            _creater.SetDirection(Vector2.zero);
            if (_coroutine != null) 
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(coroutine);

        }


        public void HeroInOnVision(GameObject go)
        {
            if (isDead) return;

            _target = go;

            StartState(AgroHero());
        }

        private IEnumerator AgroHero()
        {
            _particles.Spawn("Exclamation");
            var diretion = UpdateDiretion();
            _creater.UpdateSpriteDirection(diretion);
            yield return new WaitForSeconds(_agroDelay);

            StartState(GoToHero());
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDiretionToHero();
                }

                yield return null;
            }

            _particles.Spawn("MissHero");
            _creater.SetDirection(Vector2.zero);
            yield return new WaitForSeconds(_coolDownMiss);

            StartState(_patrol.DoPatrol());

        }

        private void SetDiretionToHero()
        {
            var diretion = UpdateDiretion();
            _creater.SetDirection(diretion);
        }

        private Vector2 UpdateDiretion()
        {
            var diretion = _target.transform.position - transform.position;
            diretion.y = 0;
            return diretion.normalized;
        }

        private IEnumerator Attack()
        {
            _creater.Attack();
            yield return new WaitForSeconds(_coolDownAttack);

            StartState(GoToHero());
        }

        public void IsDead()
        {
            _creater.SetDirection(Vector2.zero);
            isDead = true;
            _animator.SetBool(isDeadKey, isDead);
            StopAllCoroutines();
        }
    }
}