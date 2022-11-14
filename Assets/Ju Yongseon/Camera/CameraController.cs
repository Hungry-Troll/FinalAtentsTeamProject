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
        // �̵����� ���� ī�޶��� �ٶ󺸴� ������ �����մϴ�.
        transform.rotation = Quaternion.Euler(ymove, xmove, 0);
        // ī�޶� �ٶ󺸴� �չ����� Z ���Դϴ�. �̵����� ���� Z ������� ���͸� ���մϴ�.
        reverseDistance = new Vector3(0, 0, distance);
        // �÷��̾��� ��ġ���� ī�޶� �ٶ󺸴� ���⿡ ���Ͱ��� ������ ��� ��ǥ�� �����մϴ�.
        transform.position = player.transform.position - transform.rotation * reverseDistance + cameraHeight;
    }
    void xyDrag()
    {
        //������ ���콺�� �����̴� ���� �� ������ �־��ش�.
        if (Input.GetMouseButton(0))
        {
            xmove += Input.GetAxis("Mouse X") * dragSpeed;
            ymove -= Input.GetAxis("Mouse Y") * dragSpeed;
        }
        //y��ǥ �ּ� �ִ밪
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
        //�巡�� �Ҷ����� �� ���� �Ÿ������� �־��ش�.
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
    //ī�޶� ���� ������ �÷��̾��� ����
    void MoveLookAt()
    {
        //ī�޶� ���� �ִ� x,z���� ���� �÷��̾��� ����
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

//���� ����Ʈ https://itadventure.tistory.com/399?category=862463
//ȸ�� ���� https://www.youtube.com/watch?v=IccPDnM4lpw 
//���� ���� https://gall.dcinside.com/mgallery/board/view/?id=game_dev&no=20184