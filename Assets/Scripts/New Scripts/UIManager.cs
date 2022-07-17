using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class UIManager : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("List with all ui pages")]
        [SerializeField] private List<UIComponent> _listPages = null;
        public void DataProcess(string operation, object value)
        {
            if(_listPages != null)
            {
                foreach(var page in _listPages)
                {
                    page.ProcessData(operation, value);
                }
            }
        }
    }
}