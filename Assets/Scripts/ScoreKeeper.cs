using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreField;
    [SerializeField] TextMeshProUGUI highScoreField;
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] GameObject highScoreUI;

    public static int Score { get; private set; }

    int topScore;

    int timerValue;
    bool isCountingDown = false;
    bool isTimerActivated = false;
    bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    //bool isFood = true;
    
    void Awake()
    {
        EventManager.Clicked.AddListener(AddScore);
    }

    void Start()
    {
        isGameOver = false;
        Score = 0;
        topScore = Score;
        timerValue = 10;

        playerName.text = "Hey, " + UIScript.PlayerName + ", you burned";
    }

    void AddScore(ClickEventData data)
    {
        if (isGameOver) return;
        
        bool isClicked = data.IsClicked;
        bool isCollided = data.IsCollided;
        int calories = data.Calories;

        //int calRandomizer = Random.Range(-calories/10, calories/10);

        Score += calories; // + calRandomizer;

        if (Score < 0) Score = 0;

        if (Score >= topScore)
        {
            topScore = Score;
            timerValue = 10;
            isTimerActivated = false;
            isCountingDown = false;
            highScoreField.text = topScore.ToString();
            Show1Line();
        }

        if (Score < topScore)
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

    void Show1Line() => scoreField.text = $"Burned: {Score.ToString()} cal.";
    void Show2Lines() => scoreField.text = $"Burned: {Score.ToString()} cal. \nYour best: {topScore.ToString()}";
    void Show3Lines() => scoreField.text = $"Burned: {Score.ToString()} cal. \nYour best: {topScore.ToString()} \nTimer: {timerValue.ToString()}";

    void Update()
    {
        if (timerValue <= 0)
        {
            isGameOver = true;
            isCountingDown = false;
            scoreField.gameObject.SetActive(false);
        }

        if (scoreField.gameObject.activeInHierarchy) 
        {
            highScoreUI.gameObject.SetActive(false);
        } 
        else
        {
            highScoreUI.gameObject.SetActive(true);
        }
    }

    IEnumerator Timer()
    {
        if (isGameOver)
        {
            isTimerActivated = false;
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

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
