using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destroy : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    int calories;
    
    ParticleSystem ps;
    MeshRenderer mr;
    Rigidbody rb;
    Collider col;
    LaunchScript launchScript;
    
    ScoreKeeper scoreKeeper;
    Vector3 lastFramePos;
    Vector3 thisFramePos;

    bool isVisible = false;

    [SerializeField] bool isClicked = false;
    bool isFood = true;
    bool isCollided = false;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        launchScript = GetComponent<LaunchScript>();

        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        calories = enemy.Calories;
    }

    void OnMouseDown()
    {
        isClicked = true;

        INeedABetterNameForThis();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isVisible) isCollided = true;

        calories *= -1;

        INeedABetterNameForThis();
    }

    void Update()
    {
        CheckVisibility();
        DestroyWhenGameOver(); //ABSTRACTION
    }

    private void DestroyWhenGameOver() //ABSTRACTION
    {
        if (!scoreKeeper.IsGameOver) return;

        thisFramePos = transform.position;

        if ((thisFramePos - lastFramePos).normalized == Vector3.down)
        {
            StartCoroutine(PauseAndDestroyOnGameOver());
        }

        lastFramePos = transform.position;
    }

    void CheckVisibility()
    {
        if (transform.position.y > Launcher.URCoord.y)
        {
            isVisible = true; //Don't punish a player for collisions beyond the screen limits
        }
    }

    void INeedABetterNameForThis()
    {
        if (!scoreKeeper.IsGameOver) EventManager.Clicked.Invoke(new ClickEventData(isClicked, isFood, isCollided, calories));

        mr.enabled = false;
        col.enabled = false;
        launchScript.DoRotate = false;
        rb.isKinematic = true;
        transform.rotation = Quaternion.identity;
        ps.Play();

        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    IEnumerator PauseAndDestroyOnGameOver()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 1f));
        INeedABetterNameForThis();
    }
}
