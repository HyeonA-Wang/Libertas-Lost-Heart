using UnityEngine;

public class MovementCamera : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        // Negative left, a : -1
        // Positive right, d : 1
        // Non : 0
        float x = Input.GetAxisRaw("Horizontal"); // 좌우 이동
        // Negative down, s : -1
        // Positive up, w : 1
        // Non : 0


        // 이동방향 설정
        moveDirection = new Vector3(x, 0, 0);

        // 새로운 위치 = 현재 위치 + (방향 * 속도)
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}