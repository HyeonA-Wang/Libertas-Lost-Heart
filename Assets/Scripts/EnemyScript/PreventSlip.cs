using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventSlip : MonoBehaviour
{
    public bool isFloat = false;

    [SerializeField]
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFloat == true)
        {
            rigid.velocity = Vector2.zero;
            
        }
    }

    void OnCollisionEnter2D(Collision2D other) //충돌했을 경우 호출
    {

        isFloat = true;
        Debug.Log("isFloat true");

    }

    private void OnCollisionExit(Collision other)
    {

        isFloat = false;
        Debug.Log("isFloat false");

    }
}