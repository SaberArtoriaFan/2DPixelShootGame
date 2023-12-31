﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject explosionPrefab;
    new private Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(Vector2 direction)
    {
        rigidbody.velocity = direction * speed;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        if (other.gameObject.tag == "Effect"||other.gameObject.tag=="Player") return;
        if(other.gameObject.tag=="Enemy")
            other.gameObject.GetComponent<Enemy>().TakenDamage(10000);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;

        // Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }
}
