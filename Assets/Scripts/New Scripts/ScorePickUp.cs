using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class ScorePickUp : PickUpBase, ISubscribeScore
    {
        [Header("Score Settings")]
        [Tooltip("Amount of score to add when picked up"), Range(1,100)]
        [SerializeField] private int _scoreAmount = 1;
        private event Action<int> _addScoreDelegate;
        public void SubscribeAddScore(Action<int> addScoreDelegate)
        {
            _addScoreDelegate += addScoreDelegate;
        }

        public void UnsubscribeAddScore(Action<int> addScoreDelegate)
        {
            _addScoreDelegate -= addScoreDelegate;
        }

        protected override void DoOnPickUp(GameObject collisionGameObject)
        {
            if(collisionGameObject.TryGetComponent<Player>(out Player player))
            {
                player.ScoreEffect();
                _addScoreDelegate?.Invoke(_scoreAmount);
                gameObject.SetActive(false);
            }
        }
    }
}