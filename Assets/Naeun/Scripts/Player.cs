using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 targetPos;
    public float moveSpeed;
    public float rotSpeed;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.CompareTag("Terrain"))
                {
                    targetPos = hitInfo.point;
                }
            }
        }
        //이동코드 
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        // 회전코드 
        Vector3 dir = targetPos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, dir.normalized, Time.deltaTime * rotSpeed, 0);
        transform.rotation = Quaternion.LookRotation(newDir.normalized);
    }
}
