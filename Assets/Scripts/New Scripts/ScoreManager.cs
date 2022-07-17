using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class ScoreManager : MonoBehaviour, ISubscribeScore
    {
        private event Action<int> _addScoreDelegate;

        private List<ISubscribeScore> _childList = null;

        void Start()
        {
            _childList = new List<ISubscribeScore>();
            FindAllEnemy();
            SubscribeAllAddScore();
        }

        private void FindAllEnemy()
        {
            for(int index = 0; index < transform.childCount; ++index)
            {
                if(transform.GetChild(index).TryGetComponent<ISubscribeScore>(out ISubscribeScore childObj))
                {
                    _childList.Add(childObj);
                }
            }
        }

        private void SubscribeAllAddScore()
        {
            foreach (ISubscribeScore child in _childList)
            {
                child.SubscribeAddScore(AddScore);
            }
        }

        private void AddScore(int scoreAmount)
        {
            _addScoreDelegate?.Invoke(scoreAmount);
        }

        public void SubscribeAddScore(Action<int> addScoreDelegate)
        {
            _addScoreDelegate += addScoreDelegate;
        }

        public void UnsubscribeAddScore(Action<int> addScoreDelegate)
        {
            _addScoreDelegate -= addScoreDelegate;
        }
    }
}