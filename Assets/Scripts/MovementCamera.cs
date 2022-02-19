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
        float x = Input.GetAxisRaw("Horizontal"); // �¿� �̵�
        // Negative down, s : -1
        // Positive up, w : 1
        // Non : 0


        // �̵����� ����
        moveDirection = new Vector3(x, 0, 0);

        // ���ο� ��ġ = ���� ��ġ + (���� * �ӵ�)
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}