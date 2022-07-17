using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New_Scripts
{
    public class ToggleButton : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The button image component")]
        [SerializeField] private Image _imageView = null;
        [Tooltip("The button enabled")]
        [SerializeField] private Sprite _imageButtonOn = null;
        [Tooltip("The button enabled")]
        [SerializeField] private Sprite _imageButtonOff = null;
        private bool _enable = true;

        public void Toggle()
        {
            if (!_enable)
            {
                SetOn();
            }
            else
            {
                SetOff();
            }
        }

        private void SetOn()
        {
            if (_imageButtonOn != null)
            {
                _enable = false;
                _imageView.sprite = _imageButtonOn;
            }
        }

        private void SetOff()
        {
            if(_imageButtonOff != null)
            {
                _enable = true;
                _imageView.sprite = _imageButtonOff;
            }
        }

        public void ChangeVolume(float volume)
        {
            if (Mathf.Approximately(volume, -80.0f))
            {
                SetOff();
            }else
            {
                SetOn();
            }
        }
    }
}