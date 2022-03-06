using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "GameEvents", menuName = "Chars/GameEvents", order = 1)]
public class GameEvents : ScriptableObject
{
    //Anteriormente tenia que crear un evento y una funcion Trigger por cada uno
    //public static event Action OnGameOver;
    //public static event Action<int> OnAddScore;

    //public static void TriggerFadeInEvent(float value)
    //{
    //    OnFadeInEvent?.Invoke(value);
    //}

    //public static void TriggerFadeOut(float value)
    //{
    //    OnFadeOutEvent?.Invoke(value);
    //}

    //public static void TriggerGameOver()
    //{
    //    OnGameOver?.Invoke();
    //}

    //public static void TriggerAddScore(int value)
    //{
    //    OnAddScore?.Invoke(value);
    //}

}
