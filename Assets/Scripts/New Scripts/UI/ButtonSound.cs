using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Sound Controller")]
    [SerializeField] private AudioSource _soundControl = null;
    [Tooltip("Click sound")]
    [SerializeField] private AudioClip _clickSound = null;
    [Tooltip("Navigate sound")]
    [SerializeField] private AudioClip _navigateSound = null;

    public void ButtonClick()
    {
        PlayAudioOneShot(_clickSound);
    }

    public void ButtonNavigate()
    {
        PlayAudioOneShot(_navigateSound);
    }

    private void PlayAudioOneShot(AudioClip sound)
    {
        if (_soundControl != null)
        {
            if (sound != null)
            {
                _soundControl.PlayOneShot(sound);
            }
        }
    }
}
