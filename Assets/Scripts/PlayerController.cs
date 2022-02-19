using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement2D;
    // Start is called before the first frame update
    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //좌우 이동 방향 제어
        // left or a = -1 / right or d = 1
        float x = Input.GetAxisRaw("Horizontal");
        movement2D.Move(x);

        // 플레이어 점프 (스페이스 키를 누르면 점프)
        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            movement2D.Jump();
        }

        // 낮은 점프, 높은 점프 구현
        /*
        if ( Input.GetKey(KeyCode.Space) )
        {
            movement2D.isLongJump = true;
        }

        else if ( Input.GetKeyUp(KeyCode.Space) )
        {
            movement2D.isLongJump = false;
        }
        */
    }
}
