using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public bool isJump = false;
    public int jumpCount = 0;
    public float jumpPower = 20f;
    public int jumpMax = 2;
    [SerializeField]
    Rigidbody2D rigid;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (isJump == false)
        {
            rigid.velocity = Vector2.zero;
        }
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
            Jump();
        }
    }
    void OnCollisionEnter2D(Collision2D other) //충돌했을 경우 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) // 바닥과 충돌하면 i가 0이됨
        {
            isJump = false;
            jumpCount = jumpMax;
        }
    }
    void Jump()
    {
        if (jumpCount > 0) //i가 1초과이거나 isJumping이 false이면 종료
        {
            jumpCount--; //점프 시 jumpCount값 감소
            rigid.velocity = Vector2.zero;
            Debug.Log(jumpCount);
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }

    }
}