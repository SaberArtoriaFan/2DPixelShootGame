using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region
//作者:Saber
#endregion
public class EnemySpawnPos : MonoBehaviour
{
    public bool isOpen = true;
    public float spawnInval = 1f;
    float timer = 0;


    [SerializeField]
    public GameObject EnemyModel;
    private void Update()
    {
        if (isOpen)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInval)
            {
                timer = 0;
                Spawn();
            }
        }
        if (Input.GetKeyDown(Global.Instance.SpawnKeyCode))
            isOpen = !isOpen;
    }

    private void Spawn()
    {
        var enemy = GameObject.Instantiate(EnemyModel);
        enemy.transform.position = this.transform.position;
    }
}
