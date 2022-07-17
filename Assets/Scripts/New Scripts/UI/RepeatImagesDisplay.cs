using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class RepeatImagesDisplay : UIComponent
    {
        [Header("Settings")]
        [Tooltip("Tag display number")]
        [SerializeField] private string _tagDisplay = null;
        [Tooltip("Images repeating in UI")]
        [SerializeField] private List<GameObject> _repeatImages = null;

        public override void ProcessData(string operation, object value)
        {
            if(operation == _tagDisplay)
            {
                if(value is int)
                {
                    int countImages = (int)value;
                    SetVisibleImagesCount(countImages);
                }
            }
        }

        public void SetVisibleImagesCount(int count)
        {
            if(_repeatImages != null)
            {
                for(int i = 0; i < _repeatImages.Count; ++i)
                {
                    if(i < count)
                    {
                        _repeatImages[i].SetActive(true);
                    }
                    else
                    {
                        _repeatImages[i].SetActive(false);
                    }
                }
            }
        }
    }
}