using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action Scored;
    public static event Action Shot;
    public static event Action GameOver;

    public static void ScoredAPoint()
    {
        Scored.Invoke();
    }

    public static void Shoot()
    {
        Shot.Invoke();
    }

    public static void GameOverM()
    {
        GameOver.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
