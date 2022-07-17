using System;
using System.Collections;
using UnityEngine;

public interface ISubscribeScore
{
    public void SubscribeAddScore(Action<int> addScoreDelegate);
    public void UnsubscribeAddScore(Action<int> addScoreDelegate);

}