using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sound
{
    [RequireComponent(typeof(Button))]
    public class AudioSwitchView : MonoBehaviour
    {
        [SerializeField] private Sprite _switchOn, _switchOff;
        [SerializeField] private Image _image;

        private Button _button;

        public event Action OnClickIcon;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(ClickHandler);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ClickHandler);
        }

        public void ChangeIcon(bool isOn)
        {
            _image.sprite = isOn ? _switchOn : _switchOff;
        }
        
        private void ClickHandler()
        {
            OnClickIcon?.Invoke();
        }
        
    }
}