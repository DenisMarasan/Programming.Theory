using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpro;
    int score;
    int topScore;

    int timerValue = 10;
    bool isCountingDown = false;
    bool isTimerActivated = false;
    bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    //bool isFood = true;
    
    void Awake()
    {
        EventManager.Clicked.AddListener(AddScore);
        topScore = score;
    }

    void AddScore(ClickEventData data)
    {
        if (isGameOver) return;
        
        bool isClicked = data.IsClicked;
        bool isCollided = data.IsCollided;
        int calories = data.Calories;

        score += calories;

        if (score < 0) score = 0;

        if (score >= topScore)
        {
            topScore = score;
            timerValue = 10;
            isTimerActivated = false;
            isCountingDown = false;
            Show1();
        }

        if (score < topScore)
        {
            isCountingDown = true;
            Show2();
        }

        if (isCountingDown)
        {
            Show3();
            if (!isTimerActivated) StartCoroutine(Timer());
        }

    }

    void Show1() => tmpro.text = $"Score: {score.ToString()} cal.";
    void Show2() => tmpro.text = $"Score: {score.ToString()} cal. \nYour best: {topScore.ToString()}";
    void Show3() => tmpro.text = $"Score: {score.ToString()} cal. \nYour best: {topScore.ToString()} \nTimer: {timerValue.ToString()}";

    void Update()
    {
        if (timerValue == 0)
        {
            isGameOver = true;
            isCountingDown = false;
            return;
        }
    }

    IEnumerator Timer()
    {
        if (isGameOver)
        {
            isTimerActivated = false;
            Show2();
            yield break;
        }

        isTimerActivated = true;

        for (int i = 10; i > 0; i--)
        {
            if (!isCountingDown) yield break;
            yield return new WaitForSeconds(1);
            timerValue--;
            Show3();
        }
    }
}
