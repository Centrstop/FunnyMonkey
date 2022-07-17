using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.New_Scripts
{
    public class VolumeControl : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Audio mixer controlling sound")]
        [SerializeField] private AudioMixerGroup _audioGroup = null;
        [Tooltip("Audio parametr controlling sound")]
        [SerializeField] private string _changeParametr = "MasterVolume";
        [Tooltip("Sound slider")]
        [SerializeField] private Slider _slider = null;
        private bool _enabled = true;
        

        public void ChangeVolume(float volume)
        {
            if (_audioGroup != null)
            {
                if (_changeParametr != null)
                {
                    _audioGroup.audioMixer.SetFloat(_changeParametr, volume);
                }
            }
        }

        public void Toggle()
        {
            if (_slider != null)
            {
                _enabled = (Mathf.Approximately(_slider.value, -80.0f)) ? false : true;
                _enabled = !_enabled;
                if (_enabled)
                {
                    _slider.value = 0.0f;
                }
                else
                {
                    _slider.value = -80.0f;
                }
            }
        }
    }
}