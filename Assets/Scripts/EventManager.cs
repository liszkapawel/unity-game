using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static Action BossHitEvent;
    public static Action EndGameEvent;
    public static Action AddHeartEvent;

    public static void BossHit()
    {
        BossHitEvent?.Invoke();
    }

    public static void AddHeart()
    {
        AddHeartEvent?.Invoke();
    }

    public static void EndGame()
    {
        EndGameEvent?.Invoke();
    }

}
