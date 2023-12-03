using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#region
//作者:Saber
#endregion
public class StartUI : MonoBehaviour
{
    public Button StartButton;

    private void Awake()
    {
        StartButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        //场景过小同步加载
        SceneManager.LoadScene(1);
    }
}
