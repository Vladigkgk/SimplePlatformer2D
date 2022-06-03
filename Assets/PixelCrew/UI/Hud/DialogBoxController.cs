using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Utils;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions.Localization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PixelCrew.UI.Hud
{
    public class DialogBoxController : MonoBehaviour
    {
        //[SerializeField] private Text _text;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;
        private static readonly int isOpen = Animator.StringToHash("is-open");

        [Space] [SerializeField] private float _textSpeed = 0.09f;

        private AudioSource _source;

        private DialogData _data;
        private int _currentSentence;
        private UnityEvent _onComplete;

        protected Sentence CurrentSentence => _data.Sentence[_currentSentence];

        private Coroutine _typingTextRoutine;

        [Header("AudioClips")]
        [SerializeField] private AudioClip _type;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _hide;

        [Space] [SerializeField] protected DialogContent _content;

        protected virtual DialogContent CurrentContent => _content;


        private void Start()
        {
            _source = AudioUtils.FindAudioSource();
            _animator = GetComponent<Animator>();
        }

        public void ShowDialogBox(DialogData data, UnityEvent OnComplete)
        {
            _data = data;

            _onComplete = OnComplete;

            _currentSentence = 0;

            CurrentContent.Text.text = string.Empty;

            _container.SetActive(true);
            if (_source != null)
            {
                _source.PlayOneShot(_open);
            }

            _animator.SetBool(isOpen, true);
        }

        private IEnumerator TypeText()
        {
            CurrentContent.Text.text = string.Empty;
            var sentence = CurrentSentence;

            CurrentContent.TrySetImage(sentence.Icon);

            var text = LocalizationManager.I.Localize(sentence.Valued);

            foreach (var letter in text)
            {
                CurrentContent.Text.text += letter;
                if (_source != null)
                {
                    _source.PlayOneShot(_type);
                }
                yield return new WaitForSeconds(_textSpeed);
            }
            _typingTextRoutine = null;
        }

        public void OnSkip()
        {
            if (_typingTextRoutine == null) return;
            StopTypeAnimation();
            var key = _data.Sentence[_currentSentence].Valued;
            CurrentContent.Text.text = LocalizationManager.I.Localize(key);

        }

        public void OnContinue()
        {
            StopTypeAnimation();
            _currentSentence++;

            var isDialogComplete = _currentSentence >= _data.Sentence.Length;
            if (isDialogComplete)
            {
                HideDialogBox();
            }
            else
            {
                OnStartAnimationDialog();
            }
        }

        private void HideDialogBox()
        {
            _onComplete?.Invoke();
            _animator.SetBool(isOpen, false);
            if (_source != null)
            {
                _source.PlayOneShot(_hide);
            }
        }

        private void StopTypeAnimation()
        {
            if (_typingTextRoutine != null) StopCoroutine(_typingTextRoutine);
            _typingTextRoutine = null;
        }

        protected virtual void OnStartAnimationDialog()
        {
            _typingTextRoutine = StartCoroutine(TypeText());
        }

        private void OnCloseAnimationComplete()
        {

        }
    }
}
