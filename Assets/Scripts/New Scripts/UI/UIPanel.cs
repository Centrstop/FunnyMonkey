using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class UIPanel : UIPage
    {
        [Header("Settings")]
        [Tooltip("Panel name settings")]
        [SerializeField] private string _panelName = null;
        public override void ProcessData(string operation, object value)
        {
            base.ProcessData(operation, value);
            if (_panelName != null)
            {
                if(operation == _panelName)
                {
                    if(value is bool)
                    {
                        bool tempValue = ((bool)value);
                        if(tempValue)
                        {
                            Show();
                        }
                        else
                        {
                            Hide();
                        }
                    }
                }
            }
        }
    }
}