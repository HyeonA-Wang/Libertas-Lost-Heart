using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float moveDefaulPower = 12f;
    public float movePower = 0;

    public float dashDelay = 0;
    public float dashDelayLimit = 0.5f;
    public float dashTime = 0;
    public float dashLimitTime = 0.15f;
    public float dashPower = 60f;

    public int jumpCount = 0;
    public float jumpPower = 24f;
    public int jumpMax = 2;

    public bool isRight = false;
    public bool isLeft = false;
    public bool isJump = false;
    public bool isDash = false;
    public bool isDashDelay = false;
    public bool isFloat = false;

    [SerializeField]
    Rigidbody2D rigid;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if (isJump == false && isFloat == false)
        {
            rigid.velocity = Vector2.zero;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown("left shift"))
        {
            isDash = true;
        }


    }
    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D other) //�浹���� ��� ȣ��
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && rigid.velocity.y == 0) // �ٴڰ� �浹�ϸ� i�� 0�̵�
        {
            jumpCount = jumpMax;
            isJump = false;
            isFloat = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) // �ٴڰ� �������� isFloat = true
        {
            isFloat = true;
            Debug.Log(isFloat);
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

        if (isDash == true && isDashDelay == false)
        {
            movePower = dashPower;
            dashTime += Time.deltaTime;
            if (dashTime > dashLimitTime)
            {
                movePower = moveDefaulPower;
                isDash = false;
                dashTime = 0;
                isDashDelay = true;
            }
        }

        if (isDashDelay == true)
        {
            dashDelay += Time.deltaTime;
            if (dashDelay > dashDelayLimit)
            {
                isDashDelay = false;
                isDash = false;
                dashDelay = 0;
            }
        }


        transform.position += moveVelocity * movePower * Time.deltaTime;
        movePower = moveDefaulPower;

    }

    void Jump()
    {

        if (jumpCount > 0) //i�� 1�ʰ��̰ų� isJumping�� false�̸� ����
        {
            isJump = true;
            jumpCount--; //���� �� jumpCount�� ����
            rigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }
    }


}