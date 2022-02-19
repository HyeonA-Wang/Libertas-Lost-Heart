using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float jumpForce = 9.0f;
    private Rigidbody2D rigid2D;
    [HideInInspector]
    //public bool isLongJump = false; // 낮은 점프, 높은 점프 체크

    [SerializeField]
    private LayerMask groundLayer;  // 바닥 체크를 위한 충돌 레이어
    private CapsuleCollider2D capsuleCollider2D; // 오브젝트의 충돌 범위 컴포넌트
    private bool isGrounded; // 바닥 체크 (바닥에 닿아있을 때 true)
    private Vector3 footPosition; // 발의 위치

    [SerializeField]
    private int maxJumpCount = 1;  // 최대 가능 점프 횟수
    private int currentJumpCount = 0;  // 현재 가능 점프 횟수

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        // 낮은 점프, 높은 점프 구현
        /*
        if ( isLongJump && rigid2D.velocity.y > 0 )
        {
            rigid2D.gravityScale = 1.0f;
        }
        else
        {
            rigid2D.gravityScale = 2.5f;
        }
        */
        // 플레이어 오브젝트의 Collider2D min, center, max 위치 정보
        Bounds bounds = capsuleCollider2D.bounds;
        // 플레이어의 발 위치 설정
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        // 플레이어의 발 위치에 원을 생성하고, 원이 바닥과 닿아있으면 isGrounded = true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        // 플레이어의 발이 땅에 닿아있고 y축 속도가 0이하면 속도 횟수 초기화
        if ( isGrounded == true && rigid2D.velocity.y <= 0 )
        {
            currentJumpCount = maxJumpCount;
        }
    }

    private void OnDrawGizmos()
    {
        // 충돌 범위 확인용 함수
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, 0.1f);
    }

    public void Move(float x)
    {
        // x축 이동은 x * speed로, y축 이동은 기존의 속력 값(현재는 중력)
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }

    public void Jump()
    {
        if ( currentJumpCount > 0 )
        {
            // jumpForce의 크기만큼 윗쪽 방향으로 속력 설정
            rigid2D.velocity = Vector2.up * jumpForce;

            currentJumpCount--;
        }
        
    }
}