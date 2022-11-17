using System;
using UnityEngine;

namespace Sound
{
    public class SoundControl : MonoBehaviour
    {
        [SerializeField] private AudioSwitchView _audioSwitch;

        private VolumeEnablePlayerPrefs _volumeEnabled = new VolumeEnablePlayerPrefs();
        private bool _isPlaying;

        public event Action<bool> IsSwitching;

        private void OnEnable()
        {
            _audioSwitch.OnClickIcon += Switching;
        }

        private void OnDisable()
        {
            _audioSwitch.OnClickIcon -= Switching;
        }

        private void Start()
        {
            _isPlaying = _volumeEnabled.Get();
            _audioSwitch.ChangeIcon(_isPlaying);
        }

        private void Switching()
        {
            _isPlaying = _isPlaying == true ? false : true;
            _audioSwitch.ChangeIcon(_isPlaying);
            _volumeEnabled.Set(_isPlaying);
            IsSwitching?.Invoke(_isPlaying);
        }
    }
}