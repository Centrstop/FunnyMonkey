using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New_Scripts
{
    
    public class NumberDisplay : UIComponent
    {
        [Header("Settings")]
        [Tooltip("Tag display number")]
        [SerializeField] private string _tagDisplay = null;
        [Tooltip("The prefab to use to display the number")]
        [SerializeField] private Text _numberDisplay = null;



        public override void ProcessData(string operation, object value)
        {
            if(_tagDisplay != null)
            {
                if(_numberDisplay != null)
                {
                    if(operation == _tagDisplay)
                    {
                        if (value is int)
                        {
                            _numberDisplay.text = ((int)value).ToString();
                        }
                    }
                }
            }
        }

    }
}