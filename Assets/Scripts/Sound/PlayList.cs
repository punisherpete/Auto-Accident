using UnityEngine;

namespace Sound
{
    public class PlayList : MonoBehaviour
    {
        [SerializeField] private AudioSource _sound;
        [SerializeField] private SoundControl _soundControl;

        private VolumeEnablePlayerPrefs _volumeEnable = new VolumeEnablePlayerPrefs();
        private bool _isPlaying;

        private void OnEnable()
        {
            _soundControl.IsSwitching += SwitchSound;
        }

        private void OnDisable()
        {
            _soundControl.IsSwitching -= SwitchSound;
        }
        
        private void Start()
        {
            _isPlaying = _volumeEnable.Get();
            EnableSoundMainMenu();
        }
        
        private void EnableSoundMainMenu()
        {
            if (!_isPlaying)
            {
                _sound.Stop();
                return;
            }
            _sound.Play();
        }
        
        private void SwitchSound(bool isPlaying)
        {
            _isPlaying = isPlaying;
            EnableSoundMainMenu();
        }
    }
}