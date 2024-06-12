using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class menuUiScript : MonoBehaviour
{
    public TMP_Text NameText;
    public TMP_InputField _InputFieldEditor;

    private void OnEnable()
    {
        LoadData();
    }

    public void LoadData()
    {
        NameText.text = "Best Score:" + MenuManager.instance.PlayerName + ":"+ MenuManager.instance.HighestScore;
    }

    public void OnStartPlay()
    {
        MenuManager.instance.currentPlayerName = _InputFieldEditor.text;
        //MenuManager.instance.HighestScore = 0;
        SceneManager.LoadScene("main");
    }

    public void OnExitPlay()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
