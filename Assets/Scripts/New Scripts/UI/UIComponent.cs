using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public abstract class UIComponent : MonoBehaviour
    {
        public abstract void ProcessData(string operation, object value);
    }
}