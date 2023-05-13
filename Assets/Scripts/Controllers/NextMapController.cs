using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 맵 이동 관련 컨트롤러
public class NextMapController : MonoBehaviour
{
    // 새로운 맵으로 넘어가는 컨트롤러 매니저로 만들어야 될 수도 있음
    // 플레이어 콜라이더 확인용
    Collider _playerCollider;

    void Start()
    {
        // OnTriggerEnter 사용을 위한 플레이어 콜라이더를 가지고 옴
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // 플레이어 콜라이더와 충돌했다면
        if (other == _playerCollider)
        {
            // 현재 씬 체크
            SceneCheck();
        }
    }
    // 현재 씬 체크 후 각각 맞는 함수 실행
    private void SceneCheck()
    {
        switch (GameManager.Scene._sceneNameEnum)
        {
            case Define.SceneName.Tutorial:
                MoveToVillage02();
                break;
            case Define.SceneName.Village02:
                MoveToDunGeon();
                break;
        }
    }
    // 마을로 씬 이동하는 함수
    public void MoveToVillage02()
    {   
         GameManager.Data.SaveData_1();
         GameManager.Scene.LoadScene("Village02");
         // 현재 데이터를 저장한다.
         // 다음 씬으로 넘어간다.
         // 저장한 데이터를 불러온다.
    }   
    // 던전으로 씬 이동하는 함수
    public void MoveToDunGeon()
    {
        GameManager.Data.SaveData_1();
        GameManager.Scene.LoadScene("DunGeon");
    }

}
