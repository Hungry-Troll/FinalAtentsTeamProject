using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossStartController : MonoBehaviour
{
    // 플레이어 콜라이더 확인용
    Collider _playerCollider;
    //CinemachineVirtualCamera _vCam1;    //맨 처음 플레이어를 비추는 카메라
    //CinemachineVirtualCamera _vCam2;    //그 다음 작은 보스를 비추는 카메라
    //CinemachineVirtualCamera _vCam3;    //그 다음 큰 보스를 비추는 카메라

    // 임시 몬스터 위치 설정용 / 비어있는 게임오브젝트에 플레이어 위치 대입
    public GameObject _startPosBoss;
    public Vector3 _startPos;
    bool _bossSwan;

    // Start is called before the first frame update
    void Start()
    {
        // OnTriggerEnter 사용을 위한 플레이어 콜라이더를 가지고 옴
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
        _bossSwan = false;
        //_vCam1 = GetComponent<CinemachineVirtualCamera>();
        //_vCam2 = GetComponent<CinemachineVirtualCamera>();
        //_vCam3 = GetComponent<CinemachineVirtualCamera>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // 플레이어 콜라이더와 충돌했다면

        // 2. 보스 시네머신1 작동
        GameManager.Cam.BossCamOn1();
        
        // 3. 퀘스트 시작
        GameManager.Quest.QuestProgressValueAdd();

        // 1. 조이스틱 멈춤
        GameManager.Quest.QuestJoystickStop();

        StartCoroutine(BossCreator());

        //보스 시네머신3 끄기
        StartCoroutine(TurnOffCam());



    }

    IEnumerator BossCreator()
    {
        yield return new WaitForSeconds(5f);
        if (_bossSwan == false)
        {   //보스 시네머신2 작동
            GameManager.Cam.BossCamOff1();
            GameManager.Cam.BossCamOn2();
            _startPos = _startPosBoss.transform.position;
            // 보스 생성
            GameManager.Create.CreateHumanBoss(_startPos, "BossHuman");
            
            _bossSwan = true;
            yield return new WaitForSeconds(4f);
            GameManager.Cam.BossCamOff2();
            GameManager.Cam.BossCamOn3();
        }
    }

    IEnumerator TurnOffCam()
    {
        yield return new WaitForSeconds(3f);
        //마지막 캠 끄기
        GameManager.Cam.BossCamOff3();
    }
}

        


