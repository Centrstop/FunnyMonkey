using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public abstract class BaseDamageHandler : MonoBehaviour
    {
        public abstract void Activate();

        public abstract void Deactivate();

    }
}