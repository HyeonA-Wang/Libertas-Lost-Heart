using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponeScript : MonoBehaviour
{
    public bool isResp = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            isResp = true;
        }
    }
    void FixedUpdate()
    {
        Respone();
    }
    void Respone()
    {
        if (isResp == true)
        {
            transform.position = Vector3.zero;
            isResp = false;
        }
    }
}