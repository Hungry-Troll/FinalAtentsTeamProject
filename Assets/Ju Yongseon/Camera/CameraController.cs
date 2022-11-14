using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Renderer ObstacleRenderer;
    public float dragSpeed;
    Vector3 cameraHeight;
    Vector3 reverseDistance;
    Vector3 Direcction;
    float xmove;
    float ymove;
    float distance;
    void Start()
    {
        cameraHeight = new Vector3(0, 1f, 0);
        dragSpeed = 3f;
        distance = 5f;
    }
    void LateUpdate()
    {
        xyDrag();
        cameraDistance();
        MoveLookAt();
        ObjectAlbedo();
        // 이동량에 따라 카메라의 바라보는 방향을 조정합니다.
        transform.rotation = Quaternion.Euler(ymove, xmove, 0);
        // 카메라가 바라보는 앞방향은 Z 축입니다. 이동량에 따른 Z 축방향의 벡터를 구합니다.
        reverseDistance = new Vector3(0, 0, distance);
        // 플레이어의 위치에서 카메라가 바라보는 방향에 벡터값을 적용한 상대 좌표를 차감합니다.
        transform.position = player.transform.position - transform.rotation * reverseDistance + cameraHeight;
    }
    void xyDrag()
    {
        //눌러서 마우스가 움직이는 값을 각 변수에 넣어준다.
        if (Input.GetMouseButton(0))
        {
            xmove += Input.GetAxis("Mouse X") * dragSpeed;
            ymove -= Input.GetAxis("Mouse Y") * dragSpeed;
        }
        //y좌표 최소 최대값
        if (ymove < 0)
        {
            ymove = 0;
        }
        else if (ymove > 60f)
        {
            ymove = 60f;
        }
    }
    void cameraDistance()
    {
        //드래그 할때마다 그 값을 거리변수에 넣어준다.
        distance -= Input.GetAxis("Mouse ScrollWheel") * dragSpeed;
        if (distance < 3f)
        {
            distance = 3f;
        }
        else if (distance > 10f)
        {
            distance = 10f;
        }
    }
    //카메라가 보는 시점이 플레이어의 시점
    void MoveLookAt()
    {
        //카메라가 보고 있는 x,z축의 값이 플레이어의 시점
        Vector3 playerRotate = Vector3.Scale(transform.forward, new Vector3(1, 0, 1));
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * dragSpeed);
    }
    void ObjectAlbedo()
    {
        RaycastHit hitInfo;
        Direcction = (player.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, Direcction, out hitInfo, distance))
        {
            Debug.Log(distance);
            Debug.DrawRay(transform.position, Direcction, Color.red, distance);
            if (hitInfo.collider.CompareTag("MapObject"))
            {
                ObstacleRenderer = hitInfo.collider.gameObject.GetComponentInChildren<Renderer>();
                if (ObstacleRenderer != null)
                {
                    Material Mat = ObstacleRenderer.material;
                    Color matColor = Mat.color;
                    matColor.a = 0.2f;
                    Mat.color = matColor;
                }
            }
            else
            {
                if (ObstacleRenderer != null)
                {
                    Material Mat = ObstacleRenderer.material;
                    Color matColor = Mat.color;
                    matColor.a = 1f;
                    Mat.color = matColor;
                }
            }
        }
    }
}

//참고 사이트 https://itadventure.tistory.com/399?category=862463
//회전 참고 https://www.youtube.com/watch?v=IccPDnM4lpw 
//투명 참고 https://gall.dcinside.com/mgallery/board/view/?id=game_dev&no=20184