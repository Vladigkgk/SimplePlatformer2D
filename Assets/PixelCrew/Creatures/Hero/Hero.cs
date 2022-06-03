using System;
using System.Collections;
using PixelCrew.Components;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Utils;
using PixelCrew.Components.GoBased;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repositories.Items;
using PixelCrew.UI.Perks;
using PixelCrew.Model.Data.Definitions.Player;
using PixelCrew.Effect.CameraRelate;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PixelCrew.Creatures.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [SerializeField] private ProbabilityDropComponent _coinsDown;


        [SerializeField] private float _slamDownVelocity;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private Cooldown _speedPotion;
        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _disarmed;

        [Header("SuperThrow")]
        [SerializeField] private Cooldown _coolDawnSuperThrow;
        [SerializeField] private int _particleCountThrow;
        [SerializeField] private float _superThrowDelay;
        [SerializeField] private SpawnComponent _throwItem;

        [Header("Perks")]
        [SerializeField] private float _secondsShield = 5;
        [SerializeField] private GameObject _shield;
        [SerializeField] private GameObject _candle;

        private bool _superThrowEnabled;

        [Space] [Header("Particles")] [SerializeField]

        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private bool _allowDoubleJump;

        private HealthComponent _health;
        private CameraShakeEffect _cameraShake;
        private GameSession _session;

        private const string SwordId = "Sword";

        private int CoinsCount => _session.Data.Inventory.Count("Coin");
        private int SwordCount => _session.Data.Inventory.Count(SwordId);
        private int HealthPotionCount => _session.Data.Inventory.Count("HealthPotion");

        private int SpeedPotionCount => _session.Data.Inventory.Count("SpeedPotion");

        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _health = GetComponent<HealthComponent>();
            _cameraShake = FindObjectOfType<CameraShakeEffect>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _session.StatsModel.OnUpgraded += OnHeroUpgraded;

            _health.SetHealth(_session.Data.Hp.Value);
            UpdateHeroWeapon();
        }

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId) return SwordCount > 1;

                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
            _session.StatsModel.OnUpgraded -= OnHeroUpgraded;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                UpdateHeroWeapon();
        }

        private void OnHeroUpgraded(StatId id)
        {
           switch (id)
            {
                case StatId.Health:
                    var health = (int)_session.StatsModel.GetValue(id);
                    _session.Data.Hp.Value = health;
                    _health.SetHealth(health);
                    break;
            } 
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp.Value = currentHealth;
        }

        protected override float CalculateXVelocity()
        {
            var modifySpeed = _speedPotion.IsReady ? 1f : 2f;
            var defaultSpeed = Direction.x * _session.StatsModel.GetValue(StatId.Speed);
            
            return defaultSpeed * modifySpeed;
        }

        protected override float CalculateYVelocity()
        {

            if (IsGrounded)
            {
                _allowDoubleJump = true;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded && _allowDoubleJump)
            {
                JumpingSfx();
                _allowDoubleJump = _session.PerkModel.IsFlyingSupported;
                return _jumpSpeed;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            _cameraShake.Shack();
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);

            _coinsDown.SetCount(numCoinsToDispose);
            _coinsDown.CalculateDrop();
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _slamDownVelocity)
                {
                    _particles.Spawn("SlamDown");
                }
            }
        }

        public override void Attack()
        {
            if (SwordCount <= 0) return;
            var modifyDamage = _attackRange.GetComponent<ModifyHealthComponent>();
            modifyDamage.SetHpDelta(-ModifyDamageByCritical(1));
            base.Attack();
        }

        private int ModifyDamageByCritical(int damage)
        {
            var critChange = _session.StatsModel.GetValue(StatId.CriticalDamage);
            if (Random.value * 100 <= critChange )
            {
                return damage * 5;
            }
            return damage;
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        }

        public void StartingThrow()
        {
            _coolDawnSuperThrow.Reset();
        }


        public void CancledThrow()
        {
            if (!CanThrow || !_throwCooldown.IsReady) return;
            if (_coolDawnSuperThrow.IsReady)
            {
                _superThrowEnabled = true;
            }
            Animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();

        }

        public void Throw()
        {
            if (_superThrowEnabled && _session.PerkModel.IsSuperThrowSupported)
            {
                var throwableCount = _session.Data.Inventory.Count(SelectedItemId);
                var possibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;
                var numSword = Mathf.Min(_particleCountThrow, possibleCount);
                StartCoroutine(SuperThrow(numSword));
                _superThrowEnabled = false;
            }
            else
            {
                ThrowAndRemove();
            }
        }

        private IEnumerator SuperThrow(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ThrowAndRemove();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }

        private void ThrowAndRemove()
        {
            var throwableId = _session.QuickInventory.SelectedItem.Id;
            var projectTile = DefsFacade.I.Throwable.Get(throwableId);
            _throwItem.SetPrefab(projectTile.Projectile);
            var throwabledItem = _throwItem.InstanceSpawn();
            var modifyHelth = throwabledItem.GetComponent<ModifyHealthComponent>();
            var rangeDamage = (int)_session.StatsModel.GetValue(StatId.RangeDamage);
            modifyHelth.SetHpDelta(-rangeDamage);

            Sounds.Play("Range");
            _session.Data.Inventory.Remove(throwableId, 1);
        }

        public void UsePotion()
        {
            if (CanThrow && _session.Data.Inventory.Count(SelectedItemId) <= 0) return;

            if ("HealthPotion" == SelectedItemId)
            {
                OnDoHeal();
            }
            else if ("SpeedPotion" == SelectedItemId)
            {
                OnDoSpeeding();
            }
            _session.Data.Inventory.Remove(SelectedItemId, 1);
        }

        public void OnDoHeal()
        {
            _health.ModifyHealth(5);
            _health.SetHealth(_session.Data.Hp.Value);
        }
        public void OnDoSpeeding()
        {
            _speedPotion.Reset();
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }

        public void UseCandle()
        {
            var active = _candle.activeSelf ? true : false;
            _candle.SetActive(!active);
        }

        public void UseActivePerk()
        {
            if (_session.PerkModel.IsShieldSupported)
                OnUseShield();
        }

        private void OnUseShield()
        {
            _shield.SetActive(true);
            _health.Immun.Retain(this);
            StartCoroutine(TimerShield(_secondsShield));
        }

        private IEnumerator TimerShield(float time)
        {
            yield return new WaitForSeconds(time);
            _shield.SetActive(!true);
            _health.Immun.Release(this);
            _session.PerkModel.cooldown.Reset();
        }

    }
}