using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer2 : MonoBehaviour
{
    float moveSpeed;

    void Awake()
    {
        moveSpeed = 2.5f;
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 tmp = transform.position;
        tmp.x += x * Time.deltaTime * moveSpeed;
        tmp.z += z * Time.deltaTime * moveSpeed;
        transform.position = tmp;
    }
}

