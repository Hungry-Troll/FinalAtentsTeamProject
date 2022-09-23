using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //GetKey함수는 키를 누르고 있는 동안의 의미
            transform.position -= new Vector3(moveSpeed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0f, moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0.0f, 0f, moveSpeed);
        }
        /* Vector3 tmp = transform.position;
        tmp.x += dx * Time.deltaTime * moveSpeed;
        tmp.z += dz * Time.deltaTime * moveSpeed;
        transform.position = tmp; */
    }
}
