using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public GameObject freed;  //player��� GameObject���� ����
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