using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs = null;
    [SerializeField] Vector3 spawningPosition;

    ScoreKeeper scoreKeeper;

    bool isReady2Launch = true;
    float speedupTimer;

    public static Vector3 LLCoord { get; private set; } //ENCAPSULATION
    public static Vector3 URCoord { get; private set; } //ENCAPSULATION

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start ()
    {
        LLCoord = Camera.main.ViewportToWorldPoint(new Vector3 (0, 0, 0));
        URCoord = Camera.main.ViewportToWorldPoint(new Vector3 (1, 1, 1));

        speedupTimer = 100f;
    }

    void Update()
    {
        if (isReady2Launch)
        {
            isReady2Launch = false;
            spawningPosition = new Vector3(Random.Range(LLCoord.x + 0.2f, URCoord.x - 0.2f), LLCoord.y, 1);
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawningPosition, Quaternion.identity);
            StartCoroutine(TimeManagement());
        }
    }

    IEnumerator TimeManagement()
    {
        yield return new WaitForSeconds(100 / speedupTimer);
        isReady2Launch = true;

        if (scoreKeeper.IsGameOver) speedupTimer = 100f; else speedupTimer += 1f;
    }
}
