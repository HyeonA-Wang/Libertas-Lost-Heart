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
    private LayerMask groundLayer;  // 바닥 체크를 위한 충돌 레이어
    private CapsuleCollider2D capsuleCollider2D; // 오브젝트의 충돌 범위 컴포넌트
    private bool isGrounded; // 바닥 체크 (바닥에 닿아있을 때 true)
    private Vector3 footPosition; // 발의 위치

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

        // 플레이어 오브젝트의 Collider2D min, center, max 위치 정보
        Bounds bounds = capsuleCollider2D.bounds;
        // 플레이어의 발 위치 설정
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        // 플레이어의 발 위치에 원을 생성하고, 원이 바닥과 닿아있으면 isGrounded = true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        // 플레이어의 발이 땅에 닿아있고 y축 속도가 0이하면 속도 횟수 초기화
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
        // 충돌 범위 확인용 함수
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

        if (jumpCount > 0) //i가 1초과이거나 isJumping이 false이면 종료
        {
            isJump = true;
            jumpCount--; //점프 시 jumpCount값 감소
            rigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }
    }


}