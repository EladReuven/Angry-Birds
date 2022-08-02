using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static int shotsRemaining;
    public Text scoreText;
    public Text shotsRemaingingText;
    public GameObject gameOverObj;

    [SerializeField] int score;

    private void Awake()
    {
        shotsRemaining = 3;
        score = 0;
        gameOverObj.SetActive(false);
    }
    private void Start()
    {
        EventManager.Scored += GainPoint;
        EventManager.Shot += Shot;
        EventManager.GameOver += GameOver;
        scoreText.text = "Score: " + score;
        shotsRemaingingText.text = "Shots Remaining: " + shotsRemaining;
    }
    public void GainPoint()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }
    public void Shot()
    {
        shotsRemaining--;
        shotsRemaingingText.text = "Shots Remaining: " + shotsRemaining;
    }

    public void GameOver()
    {
        if(gameOverObj != null)
        {
            gameOverObj.SetActive(true);
        }
    }
}
