using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer2 : MonoBehaviour
{
    float moveSpeed;
    Animator manAnimator;

    void Awake()
    {
        moveSpeed = 2.5f;
        manAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 tmp = transform.position;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        tmp.x += x * Time.deltaTime * moveSpeed;
        tmp.z += z * Time.deltaTime * moveSpeed;
        transform.position = tmp;
        if ((tmp.x != 0) || (tmp.z != 0))
        {
            manAnimator.SetInteger("Moving", 1);
        }
    }
}

