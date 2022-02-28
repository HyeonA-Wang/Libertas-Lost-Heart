using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMove : MonoBehaviour
{
    public float moveDefaulPower = 8f;
    public float movePower = 0;

    public float dashDelay = 10f;
    public float dashTime = 0;
    public float dashLimitTime = 0.1f;
    public float dashPower = 40f;

    public int jumpCount = 0;
    public float jumpPower = 20f;
    public int jumpMax = 2;

    [SerializeField]
    private LayerMask groundLayer;  // �ٴ� üũ�� ���� �浹 ���̾�
    private CapsuleCollider2D capsuleCollider2D; // ������Ʈ�� �浹 ���� ������Ʈ
    private bool isGrounded; // �ٴ� üũ (�ٴڿ� ������� �� true)
    private Vector3 footPosition; // ���� ��ġ

    public bool isRight = false;
    public bool isLeft = false;
    public bool isJump = false;
    public bool isDash = false;

    [SerializeField]
    Rigidbody2D rigid;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if (isJump == false)
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

        // �÷��̾� ������Ʈ�� Collider2D min, center, max ��ġ ����
        Bounds bounds = capsuleCollider2D.bounds;
        // �÷��̾��� �� ��ġ ����
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        // �÷��̾��� �� ��ġ�� ���� �����ϰ�, ���� �ٴڰ� ��������� isGrounded = true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        // �÷��̾��� ���� ���� ����ְ� y�� �ӵ��� 0���ϸ� �ӵ� Ƚ�� �ʱ�ȭ
        if (isGrounded == true && rigid.velocity.y <= 0)
        {
            jumpCount = jumpMax;
        }
    }


    void FixedUpdate()
    {
        Move();
    }


    private void OnDrawGizmos()
    {
        // �浹 ���� Ȯ�ο� �Լ�
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, 0.1f);
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