using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class FieldManager : MonoBehaviour
{
    //게임 시작시 Awake , Start, Update 사용 용도 매니저

    // 플레이어 펫 아이템 몬스터 접근을 위한 변수 선언
    // 이후 각 변수에 대입해서 GameManager.Obj 를 통한 오브젝트 관리
    PlayerController _player;
    PetController _pet;
    ItemController _item;
    ItemStatEX _itemStatEX;
    MonsterController _monster;
    MonsterStat _monsterStat;

    // 플레이어 위치 설정용 / 비어있는 게임오브젝트에 플레이어 위치 대입
    public GameObject _startPosObject;
    public Vector3 _startPos;

    // 임시 몬스터 위치 설정용 / 비어있는 게임오브젝트에 플레이어 위치 대입
    public GameObject _startPosMonster;
    public Vector3 _startPosMob;


    // Start is called before the first frame update
    void Awake()
    {
        // 각 씬에 맞는 Awake 함수 호출
        SceneCheck();
    }

    // 각 씬에 맞는 Awake 함수 호출용 함수
    private void SceneCheck()
    {
        switch (GameManager.Scene._sceneNameEnum)
        {
            case Define.SceneName.Tutorial:
                TutorialAwake();
                break;
            case Define.SceneName.Village02:
                Village02Awake();
                break;
            case Define.SceneName.DunGeon:
                DunGeonAwake();
                break;
        }
    }

    private void TutorialAwake()
    {
        // 게임매니저에서 Ui매니저 Init(Awake 함수 대체)
        // Ui 불러옴
        GameManager.Ui.Init();
        // Select 매니저에서 어떤 캐릭터랑 펫을 선택했는지 확인
        GameManager.Select.Init();
        // 스텟 매니저에서 스텟 데이터 불러옴
        GameManager.Stat.Init();
        // 카메라 생성
        GameManager.Cam.Init();

        // 시작위치는 맵마다 다르게 해야 됨
        _startPos = _startPosObject.transform.position;

        //플레이어 제작
        GameManager.Obj._playerController = GameManager.Create.CreatePlayerCharacter(_startPos, GameManager.Select._job.ToString());

        // 리소스매니저에서 찾아서 플레이어 아이템 인벤토리에 한개 넣어줌
        GameManager.Create.CreateInventoryItem("sword1");

        // 몬스터 시작 위치
        _startPosMob = _startPosMonster.transform.position;
        // 몬스터 생성 코드
        for (int i = 0; i < GameManager.Resource._monster.Count - 1; i++)
        {
            GameManager.Create.CreateMonster(_startPosMob, GameManager.Resource._monster[i].name);
        }

        // 펫 생성 코드
        Vector3 temPos = new Vector3(Random.Range(3, 5), Random.Range(3, 5), Random.Range(3, 5));
        GameManager.Obj._petController = GameManager.Create.CreatePet(_startPos + temPos, GameManager.Select._pet.ToString());

        // 아이템 생성용 테스트 코드
        // 추후 제거
        for (int i = 0; i < GameManager.Resource._fieldItem.Count; i++)
        {
            Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
            GameManager.Create.CreateFieldItem(_startPos + tempPos, GameManager.Resource._fieldItem[i].name);
        }

        // BGM 변경
        GameManager.Sound.BGMPlay("Sketch 3 - Against All Odds 1");
    }

    private void Village02Awake()
    {
      
        // 오브젝트 매니저에서 기존 몬스터 리스트 초기화
        GameManager.Obj.RemoveAllMobList();
        // Ui 불러옴
        GameManager.Ui.Init();
        // 데이터 로드
        GameManager.Data.LoadData(false);
        // Select 매니저에서 어떤 캐릭터랑 펫을 선택했는지 확인
        GameManager.Select.Init();
        // 스텟 매니저에서 스텟 데이터 불러옴
        GameManager.Stat.Init();
        // 카메라 생성
        GameManager.Cam.Init();

        // 시작위치는 맵마다 다르게 해야 됨
        _startPos = _startPosObject.transform.position;
        //플레이어 제작
        GameManager.Obj._playerController = GameManager.Create.CreatePlayerCharacter(_startPos, GameManager.Select._job.ToString());
        // 무기 착용
        GameManager.Data.EquipWeaponLoad();

        // 펫 생성 코드
        Vector3 temPos = new Vector3(Random.Range(3, 5), Random.Range(3, 5), Random.Range(3, 5));
        GameManager.Obj._petController = GameManager.Create.CreatePet(_startPos + temPos, GameManager.Select._pet.ToString());
    }

    private void DunGeonAwake()
    {
        // 오브젝트 매니저에서 기존 몬스터 리스트 초기화
        GameManager.Obj.RemoveAllMobList();
        // Ui 불러옴
        GameManager.Ui.Init();
        // Select 매니저에서 어떤 캐릭터랑 펫을 선택했는지 확인
        GameManager.Select.Init();
        // 스텟 매니저에서 스텟 데이터 불러옴
        GameManager.Stat.Init();
        // 카메라 생성
        GameManager.Cam.Init();
    }

}
