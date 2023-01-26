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
        
        calories = enemy.Calories;
    }

    void OnMouseDown()
    {
        isClicked = true;

        INeedABetterNameForThis();

        StartCoroutine(WaitAndDestroy());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isVisible) isCollided = true;

        calories *= -1;

        INeedABetterNameForThis();

        StartCoroutine(WaitAndDestroy());
    }

    void Update()
    {
        CheckVisibility();
    }

    void CheckVisibility() //ABSTRACTION
    {
        if (transform.position.y > Launcher.URCoord.y)
        {
            isVisible = true; //Don't punish a player for collisions beyond the screen limits
        }
    }

    void INeedABetterNameForThis() //ABSTRACTION
    {
        EventManager.Clicked.Invoke(new ClickEventData(isClicked, isFood, isCollided, calories));

        mr.enabled = false;
        col.enabled = false;
        launchScript.DoRotate = false;
        rb.isKinematic = true;
        transform.rotation = Quaternion.identity;
        ps.Play();
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
