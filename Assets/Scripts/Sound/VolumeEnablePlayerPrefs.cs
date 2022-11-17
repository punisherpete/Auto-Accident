using UnityEngine;

namespace Sound
{
    public class VolumeEnablePlayerPrefs
    {
        private const string VolumeEnabledKey = "VolumeEnabledKey";
        private const int Disabled = 0;
        private const int Enabled = 1;

        public bool Get()
        {
            return PlayerPrefs.GetInt(VolumeEnabledKey) == Enabled;
        }

        public void Set(bool isVolumeEnabled)
        {
            PlayerPrefs.SetInt(VolumeEnabledKey, isVolumeEnabled ? Enabled : Disabled);
        }
    }
}