using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMController : MonoBehaviour
{
    public CinemachineVirtualCamera vCam1;  // ���ʿ� ��� ���󰡴� ī�޶�
    public CinemachineVirtualCamera vCam2;  // �������� Ŭ���� �� ����Ǵ� ī�޶� ��ġ
    public GameObject _player;
    void Start()
    {
        // 1. vCam1���ٰ� ������ ����ڸ� ��������� �ȴ�.
        //GameManager.Select._job;

        _player = GameObject.FindWithTag("Player");

        vCam1.Follow = _player.transform;
        vCam1.LookAt = _player.transform;

        // 2. ������ Ŭ���ϰ� �Ǹ�, �켱���� ����


    }

   
}
