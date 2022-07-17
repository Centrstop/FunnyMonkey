using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class ResetScore : MonoBehaviour
    {
        public void OnReset()
        {
            PlayerPrefs.SetInt("score", 0);
        }        
    }
}