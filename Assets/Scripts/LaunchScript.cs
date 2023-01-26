using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LaunchScript : MonoBehaviour
{
    Rigidbody rb;

    float force = 10;

    public bool DoRotate;
    bool isClicked;
    bool isCollided;

    float randomizeForce;
    float rotationX;
    float rotationY;
    float rotationZ;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
       
        randomizeForce = Random.Range(5, 7);

        rotationX = Random.Range(-1, 1);
        rotationY = Random.Range(-5, 5);
        rotationZ = Random.Range(-1, 1);

        EventManager.Clicked.AddListener(StopRotation);
    }

    private void StopRotation(ClickEventData data)
    {
        isClicked = data.IsClicked;
        isCollided = data.IsCollided;
    }

    private void Start()
    {
        rb.AddForce(Vector3.up * force * randomizeForce, ForceMode.Impulse);
    }

    void Update()
    {
        if (transform.position.x < Launcher.LLCoord.x || transform.position.x > Launcher.URCoord.x)
        {
            Destroy(gameObject);
            return;
        }

        if (transform.position.y < Launcher.LLCoord.y - 1) 
        { 
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (isClicked || isCollided) return;
        
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ)));
    }
}
