using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class PausePage : UIPage
    {

        public override void ProcessData(string operation, object value)
        {
            base.ProcessData(operation, value);
            if(operation == "pause")
            {
                Show();
            }else if(operation == "unpause"){
                Hide();
            }
        }

    }
}