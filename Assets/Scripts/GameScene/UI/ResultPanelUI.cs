using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#region
//作者:Saber
#endregion
public class ResultPanelUI : Singleton<ResultPanelUI>
{
    public Button GameOverBtn;


    protected override void Awake()
    {
        base.Awake();
        GameOverBtn.onClick.AddListener(Restart);

    }
    private void Restart()
    {
        GameOverBtn.onClick.RemoveAllListeners();
        SceneManager.LoadScene(1);
    }
    public void Open()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
