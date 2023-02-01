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

        //int calRandomizer = Random.Range(-calories/10, calories/10);

        score += calories; // + calRandomizer;

        if (score < 0) score = 0;

        if (score >= topScore)
        {
            topScore = score;
            timerValue = 10;
            isTimerActivated = false;
            isCountingDown = false;
            Show1Line();
        }

        if (score < topScore)
        {
            isCountingDown = true;
            Show2Lines();
        }

        if (isCountingDown)
        {
            Show3Lines();
            if (!isTimerActivated) StartCoroutine(Timer());
        }
    }

    void Show1Line() => tmpro.text = $"Burned: {score.ToString()} cal.";
    void Show2Lines() => tmpro.text = $"Burned: {score.ToString()} cal. \nYour best: {topScore.ToString()}";
    void Show3Lines() => tmpro.text = $"Burned: {score.ToString()} cal. \nYour best: {topScore.ToString()} \nTimer: {timerValue.ToString()}";

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
            Show2Lines();
            yield break;
        }

        isTimerActivated = true;

        for (int i = 10; i > 0; i--)
        {
            if (!isCountingDown) yield break;
            yield return new WaitForSeconds(1);
            timerValue--;
            Show3Lines();
        }
    }
}
