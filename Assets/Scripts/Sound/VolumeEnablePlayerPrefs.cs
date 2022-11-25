using UnityEngine;

namespace Sound
{
    public class VolumeEnablePlayerPrefs
    {
        private const string VolumeEnabledKey = "VolumeEnabledKey";
        private const int Disabled = 1;
        private const int Enabled = 0;

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