using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.New_Scripts.UI
{
    public class PageMusicControl : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Audio snapshot controlling sound")]
        [SerializeField] private AudioMixerSnapshot _snapshot = null;

        private void OnEnable()
        {
            if (_snapshot != null) {
                _snapshot.TransitionTo(0.2f);
            }
        }
    }
}