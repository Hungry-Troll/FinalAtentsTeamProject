using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    public CinemachineVirtualCamera vCam1;  // 최초에 계속 따라가는 카메라
    public CinemachineVirtualCamera vCam2;  // 웨슬리를 클릭할 때 변경되는 카메라 위치
    public CinemachineVirtualCamera vCam3;  // 베니스를 클릭할 떄 변경되는 카메라 위치
    public CinemachineVirtualCamera vCam4;  // 베니스를 클릭할 떄 변경되는 카메라 위치
    public CinemachineVirtualCamera vCam5;  // 베니스를 클릭할 떄 변경되는 카메라 위치
    public CinemachineVirtualCamera vCam6;  // 베니스를 클릭할 떄 변경되는 카메라 위치
    public GameObject _player;
    void Start()
    {
        // 1. vCam1에다가 추적할 사용자를 지정해줘야 된다.
        //GameManager.Select._job;

        vCam1 = GameManager.Cam._Vcam1;
        vCam2 = GameManager.Cam._Vcam2;
        vCam3 = GameManager.Cam._Vcam3;
        vCam4 = GameManager.Cam._Vcam4;
        vCam5 = GameManager.Cam._Vcam5;


        _player = GameObject.FindWithTag("Player");

        vCam1.Follow = _player.transform;
        vCam1.LookAt = _player.transform;

        // 2. 웨슬리 클릭하게 되면, 우선순위 변경

    }


    void Update()
    {
        
    }
}
