using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class GameOverPage : UIPage
    {
        public override void ProcessData(string operation, object value)
        {
            base.ProcessData(operation, value);
            if(operation == "gameOver")
            {
                Show();
            }
        }
    }
}