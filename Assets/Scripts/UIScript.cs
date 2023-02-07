using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public static string PlayerName { get; private set; }

    TMP_InputField inputField;

    void Start()
    {
        inputField = GameObject.Find("Name Input Field").GetComponent<TMP_InputField>();
    }

    public void OnClick()
    {
        PlayerName = inputField.text;
        SceneManager.LoadScene("Scene01");
    }


    
}
