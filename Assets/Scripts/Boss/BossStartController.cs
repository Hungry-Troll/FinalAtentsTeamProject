using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossStartController : MonoBehaviour
{
    // 플레이어 콜라이더 확인용
    Collider _playerCollider;
    BoxCollider mine;

    // 임시 몬스터 위치 설정용 / 비어있는 게임오브젝트에 플레이어 위치 대입
    public GameObject _startPosBoss;
    public Vector3 _startPos;
    bool _bossSwan;

    void Start()
    {
        // OnTriggerEnter 사용을 위한 플레이어 콜라이더를 가지고 옴
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
        _bossSwan = false;
        mine = GetComponent<BoxCollider>();
        
    }

    public void OnTriggerEnter(Collider other)
    {
        mine.enabled = false;
        // 플레이어 콜라이더와 한번 충돌했다면 끄기 => 안끄면 움직일때마다 계속 호출
        // 1. 조이스틱 멈춤
        GameManager.Quest.QuestJoystickStop();
        
        // 2. 보스 시네머신1 작동
        GameManager.Cam.BossCamOn1();
        
        // 3. 퀘스트 시작
        GameManager.Quest.QuestProgressValueAdd();
        GameManager.Ui.UISetActiveFalse();
        // 4. 3초 뒤에 카메라 돌아감
        StartCoroutine(BossCreator());
    }

    IEnumerator BossCreator()
    {
        yield return new WaitForSeconds(3f);
        if (_bossSwan == false)
            GameManager.Cam.BossCamOff1();
        {   //보스 시네머신2 작동
            GameManager.Cam.BossCamOn2();
            _startPos = _startPosBoss.transform.position;
            // 보스 생성 
            GameManager.Create.CreateHumanBoss(_startPos, "BossHuman");
            _bossSwan = true;
            //18초동안 보스가 물약먹고 뻗고 변신함..
            yield return new WaitForSeconds(17f);
            GameManager.Cam.BossCamOff2();
            GameManager.Cam.BossCamOn3();
            yield return StartCoroutine(TurnOffCam());
        }
    }

    IEnumerator TurnOffCam()
    {
        yield return new WaitForSeconds(5f);
        //마지막 캠 끄기
        GameManager.Cam.BossCamOff3();
        GameManager.Ui.UISetActiveTrue();
    }
}

        


