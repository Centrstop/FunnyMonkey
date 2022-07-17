using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class GamePage : UIPage
    {

        public override void ProcessData(string operation, object value)
        {
            base.ProcessData(operation, value);
            if(operation == "pause")
            {
                Hide();
            }else if(operation == "unpause")
            {
                Show();
            }else if((operation == "gameOver")||(operation == "levelCompleted"))
            {
                Hide();
            }
        }
    }
}