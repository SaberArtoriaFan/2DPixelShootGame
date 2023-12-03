using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#region
//作者:Saber
#endregion
public class ExitBtn : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Application.Quit());
    }
}
