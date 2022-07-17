using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class WinPage : UIPage
    {

        public override void ProcessData(string operation, object value)
        {
            base.ProcessData(operation, value);
            if(operation == "levelCompleted")
            {
                Show();
            }
        }

    }
}