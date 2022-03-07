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
        if (Input.GetKeyDown("g"))
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
        Vector3 worldspone = new Vector3 (-20, 2.8f, 2);
        if (isResp == true)
        {
            transform.position = worldspone;
            isResp = false;
        }
    }
}