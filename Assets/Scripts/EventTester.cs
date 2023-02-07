using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{
    public event EventHandler OnSpacePress;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePress?.Invoke(this, EventArgs.Empty);
        }
    }
}
