using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMController : MonoBehaviour
{
    public CinemachineVirtualCamera vCam1;  // 최초에 계속 따라가는 카메라
    public CinemachineVirtualCamera vCam2;  // 웨슬리를 클릭할 때 변경되는 카메라 위치
    public GameObject _player;
    void Start()
    {
        // 1. vCam1에다가 추적할 사용자를 지정해줘야 된다.
        //GameManager.Select._job;

        _player = GameObject.FindWithTag("Player");

        vCam1.Follow = _player.transform;
        vCam1.LookAt = _player.transform;

        // 2. 웨슬리 클릭하게 되면, 우선순위 변경


    }

   
}
