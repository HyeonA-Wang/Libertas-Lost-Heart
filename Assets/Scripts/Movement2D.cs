using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float jumpForce = 9.0f;
    private Rigidbody2D rigid2D;
    [HideInInspector]
    //public bool isLongJump = false; // ���� ����, ���� ���� üũ

    [SerializeField]
    private LayerMask groundLayer;  // �ٴ� üũ�� ���� �浹 ���̾�
    private CapsuleCollider2D capsuleCollider2D; // ������Ʈ�� �浹 ���� ������Ʈ
    private bool isGrounded; // �ٴ� üũ (�ٴڿ� ������� �� true)
    private Vector3 footPosition; // ���� ��ġ

    [SerializeField]
    private int maxJumpCount = 1;  // �ִ� ���� ���� Ƚ��
    private int currentJumpCount = 0;  // ���� ���� ���� Ƚ��

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        // ���� ����, ���� ���� ����
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
        // �÷��̾� ������Ʈ�� Collider2D min, center, max ��ġ ����
        Bounds bounds = capsuleCollider2D.bounds;
        // �÷��̾��� �� ��ġ ����
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        // �÷��̾��� �� ��ġ�� ���� �����ϰ�, ���� �ٴڰ� ��������� isGrounded = true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        // �÷��̾��� ���� ���� ����ְ� y�� �ӵ��� 0���ϸ� �ӵ� Ƚ�� �ʱ�ȭ
        if ( isGrounded == true && rigid2D.velocity.y <= 0 )
        {
            currentJumpCount = maxJumpCount;
        }
    }

    private void OnDrawGizmos()
    {
        // �浹 ���� Ȯ�ο� �Լ�
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, 0.1f);
    }

    public void Move(float x)
    {
        // x�� �̵��� x * speed��, y�� �̵��� ������ �ӷ� ��(����� �߷�)
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }

    public void Jump()
    {
        if ( currentJumpCount > 0 )
        {
            // jumpForce�� ũ�⸸ŭ ���� �������� �ӷ� ����
            rigid2D.velocity = Vector2.up * jumpForce;

            currentJumpCount--;
        }
        
    }
}