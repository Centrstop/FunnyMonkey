using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class UIPage : UIComponent
    {

        protected List<UIComponent> _childrenUI = new List<UIComponent>(); 
        public override void ProcessData(string operation, object value)
        {
            foreach (UIComponent component in this._childrenUI)
            {
                component.ProcessData(operation, value);
            }
        }

        private void FindAllChildren()
        {
            for(int index = 0; index < transform.childCount; ++index)
            {
                if(transform.GetChild(index).TryGetComponent<UIComponent>(out UIComponent component))
                {
                    _childrenUI.Add(component);
                }
            }
        }

        protected virtual void Awake()
        {
            FindAllChildren();
        }

        protected virtual void Start()
        {
            FindAllChildren();
        }

        protected void Show()
        {
            gameObject.SetActive(true);
        }
        
        protected void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}