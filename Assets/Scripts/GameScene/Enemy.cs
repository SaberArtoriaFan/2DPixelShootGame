using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Transform target;
    [SerializeField] private float maxHp;
    public float hp;

    [Header("Hurt")]
    private SpriteRenderer sp;
    public float hurtLength;//MARKER 效果持续多久
    private float hurtCounter;//MARKER 相当于计数器

    private Rigidbody2D rb;

    private void Start()
    {
        hp = maxHp;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        hurtCounter -= Time.deltaTime;
        if (hurtCounter <= 0)
            sp.material.SetFloat("_FlashAmount", 0);
    }
    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (hurtCounter > 0) return;
        //transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        var dir = target.position.x - transform.position.x>0;
        var velocity = dir ? Vector2.right : Vector2.left;
        rb.velocity = new Vector2(velocity.x*moveSpeed, rb.velocity.y);
        

    }

    public void TakenDamage(float _amount)
    {
        if (hp <= 0) return;
        hp -= _amount;
        HurtShader();
        if (hp <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision?.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>()?.Dead();
        }
    }
    private void HurtShader()
    {
        sp.material.SetFloat("_FlashAmount", 1);
        hurtCounter = hurtLength;
    }
    IEnumerator Dead()
    {
        yield return new WaitUntil(()=>hurtCounter <= 0);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}
