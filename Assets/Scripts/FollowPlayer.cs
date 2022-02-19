using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public GameObject freed;  //player라는 GameObject변수 선언
    Transform playerT;
    void Start()
    {
        playerT = freed.transform;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(playerT.position.x, playerT.position.y, transform.position.z);
    }
}