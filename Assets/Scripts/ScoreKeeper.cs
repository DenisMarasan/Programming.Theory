using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpro;
    int score;
    int topScore;

    int valueCountDown = 10;
    bool isCountingDown = false;

    bool isGameOver = false;

    //bool isFood = true;
    
    void Awake()
    {
        EventManager.Clicked.AddListener(AddScore);
        topScore = score;
    }

    void AddScore(ClickEventData data)
    {
        bool isClicked = data.IsClicked;
        bool isCollided = data.IsCollided;
        int calories = data.Calories;

        score += calories;

        if (score < 0)
        {
            score = 0;
        }

        if (score > topScore)
        {
            topScore = score;
            valueCountDown = 10;
        }

        if (score < topScore)
        {
            isCountingDown = true;
            tmpro.text = "Score: " + score.ToString() + " Best: " + topScore.ToString();
            tmpro.text = "Timer: " + valueCountDown.ToString();
        }
    }

    void Update()
    {
        if (valueCountDown == 0)
        {
            isGameOver = true;
            isCountingDown = false;
            return;
        }

        if (isCountingDown && !isGameOver)
        {
            Timer();
            valueCountDown--;
        }
    }

    IEnumerator Timer()
    {
        isCountingDown = false;
        yield return new WaitForSeconds(1);
        isCountingDown = true;
    }
}
