using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartController : MonoBehaviour
{
    // 플레이어 콜라이더 확인용
    Collider _playerCollider;

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
    }

    public void OnTriggerEnter(Collider other)
    {
        // 플레이어 콜라이더와 충돌했다면
        // 보스 생성
        if(_bossSwan == false)
        {
            _startPos = _startPosBoss.transform.position;
            // 보스 생성
            GameManager.Create.CreateHumanBoss(_startPos, "BossHuman");
            // 퀘스트 시작
            GameManager.Quest.QuestProgressValueAdd();
            _bossSwan = true;
            // 조이스틱 멈춤
            GameManager.Quest.QuestJoystickStop();
        }
        // 보스 시네머신 작동
    }
}
