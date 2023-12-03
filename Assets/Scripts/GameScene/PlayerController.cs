using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;

    [Header("跳跃按键")]
    public KeyCode jumpKeyCode;
    [Header("切枪按键")]
    public KeyCode switchGunKeyCode;
    [Header("移动参数")]
    public float speed = 8f;

    float xVelocity;

    [Header("跳跃参数")]
    public float jumpForce = 6f;

    [Header("跳跃时移动受阻")]
    public float jumpLessMove = 0.1f;

    int jumpCount;//跳跃次数

    [Header("状态")]
    public bool isOnGround;

    [Header("环境检测")]
    public LayerMask groundLayer;

    [Header("拥有的枪")]
    public Gun[] guns;

    public bool canInput = true;
    //按键设置
    bool jumpPress;
    private int gunNum=0;
    private Vector3 mousePos;
    Animator animator;

    public Rigidbody2D Rb { get => rb;  }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        CameraFollow.Instance.Player = this.transform.Find("CameraPos");
        guns = GetComponentsInChildren<Gun>(true);
    }


    void Update()
    {
        if (!canInput) return;
        if (Input.GetKeyDown(jumpKeyCode) && jumpCount > 0)
        {
            jumpPress = true;
        }
        SwitchGun();
    }

    void FixedUpdate()
    {
        IsOnGroundCheck();
        if (!canInput) return;
        Move();
        Jump();
    }
    public Gun GetNowGun() => guns[gunNum];
    void IsOnGroundCheck()
    {
        ////判断角色碰撞器与地面图层发生接触
        if (coll.IsTouchingLayers(groundLayer))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    internal void Dead()
    {
        GameObject.Destroy(this);
        ResultPanelUI.Instance.Open();
    }

    void Move()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");

        var zuli = isOnGround ? 1 : jumpLessMove;
        rb.velocity = new Vector2(xVelocity * speed * zuli, rb.velocity.y);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if (xVelocity!=0&&isOnGround)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

    void Jump()
    {
        //在地面上
        if (isOnGround)
        {
            jumpCount = 1;
        }
        //在地面上跳跃
        if (jumpPress && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
        //在空中跳跃
        else if (jumpPress && jumpCount > 0 && !isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
    }
    void SwitchGun()
    {
        if (Input.GetKeyDown(switchGunKeyCode))
        {
            guns[gunNum].gameObject.SetActive(false);
            if (--gunNum < 0)
            {
                gunNum = guns.Length - 1;
            }
            guns[gunNum].gameObject.SetActive(true);
        }
    }
}
