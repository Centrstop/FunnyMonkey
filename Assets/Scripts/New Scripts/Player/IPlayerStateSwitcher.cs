using System.Collections;
using UnityEngine;
using Assets.Scripts.New_Scripts;
public interface IPlayerStateSwitcher
{
    void SwitchState<T>() where T : BaseStatePlayer;
}