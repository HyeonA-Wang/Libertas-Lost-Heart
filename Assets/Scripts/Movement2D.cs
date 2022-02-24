using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float moveDefaulPower = 8f;
    public float movePower = 0;
    public float dashDelay = 10f;
    public float dashTime = 0;
    public float dashLimitTime = 0.1f;
    public float dashPower = 40f;
    public int jumpCount = 0;
    public int jumpMax = 2;

    public bool isRight = false;
    public bool isLeft = false;
    public bool isDash = false;


    [SerializeField]
    Rigidbody2D rigid;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown("left shift"))
        {
            isDash = true;
        }
        Debug.Log(isDash);


    }
    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D other) //충돌했을 경우 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) // 바닥과 충돌하면 i가 0이됨
        {
            jumpCount = jumpMax;
        }
    }

    void Move()

    {
        movePower = moveDefaulPower;
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            isRight = false;
            isLeft = true;
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            isLeft = false;
            isRight = true;
        }

        if (isDash == true)
        {
            movePower = dashPower;
            dashTime += Time.deltaTime;
            if (dashTime > dashLimitTime)
            {
                movePower = moveDefaulPower;
                isDash = false;
                dashTime = 0;
            }
        }


        transform.position += moveVelocity * movePower * Time.deltaTime;
        movePower = moveDefaulPower;

    }

}