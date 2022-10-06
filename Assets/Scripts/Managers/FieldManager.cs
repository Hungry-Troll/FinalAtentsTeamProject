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
    _Pet_01 _pet;
    PetController _pet2;
    ItemController _item;
    MonsterController _monster;
    MonsterStat _monsterStat;

    // 플레이어 위치 설정용 / 비어있는 게임오브젝트에 플레이어 위치 대입
    public GameObject _startPosObject;
    public Vector3 _startPos;
    // 플레이어 직업 확인용
    public Define.Job _select_Job;
    // 펫 종류 확인용
    public Define.Pet _select_Pet;


    // Start is called before the first frame update
    void Start()
    {
        // 게임매니저에서 Ui매니저 Init(Awake 함수 대체)
        // Ui 불러옴
        GameManager.Ui.Init();
        // 플레이어 캐릭터 생성
        // 추후 플레이어 선택창에서 string 으로 이름만 받아오면 됨
        // 시작위치는 맵마다 다르게 해야 됨
        _startPos = _startPosObject.transform.position;
        // Obj매니저에서 플레이어스크립트를 들고있게함

        // 임시로 생성한 코드. 펫 스크립트 넘겨줌
        //GameManager.Obj._petController = CreatePet2(_startPos, "Triceratops");

        // Select매니저에서 어떤 직업을 선택했는지 확인
        _select_Job = GameManager.Select.SelectJobCheck();
        // 확인한 직업으로 플레이어 제작
        GameManager.Obj._playerController = CreatePlayerCharacter(_startPos, _select_Job.ToString());

        GameManager.Cam.Init();

        // 몬스터 생성용 테스트 코드
        for (int i = 0; i < GameManager.Resource._monster.Count-1; i++)
        {
            Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
            CreateMonster(_player.transform.position + tempPos, GameManager.Resource._monster[i].name);
        }

        // Select매니저에서 어떤 펫을 선택했는지 확인
        // 펫 완성되면 펫 생성에 연결할 것.
        _select_Pet = GameManager.Select.SelectPelCheck();

        // 펫 생성
        CreatePet(_startPos, "Fox");

        // 아이템 생성용 테스트 코드
        for (int i = 0; i < GameManager.Resource._fieldItem.Count; i++)
        {
            Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
            CreateFieldItem(_player.transform.position + tempPos, GameManager.Resource._fieldItem[i].name);
        }

        // Stat매니저에서 스텟정보 가지고 옴
        GameManager.Stat.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public PlayerController CreatePlayerCharacter(Vector3 origin , string playerName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPlayerChar = GameManager.Resource.GetCharacter(playerName);
            if (temPlayerChar != null)
            {
                // 생성
                GameObject player = GameObject.Instantiate<GameObject>(temPlayerChar,hit.point,Quaternion.identity);
                // 컴포넌트 부착
                _player = player.AddComponent<PlayerController>();
                // Obj 매니저에서 PlayerStat 관리
                GameManager.Obj._playerStat = player.AddComponent<PlayerStat>();
                return _player;
            }
        }
        return null;
    }
    public _Pet_01 CreatePet(Vector3 origin, string petName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPet = GameManager.Resource.GetPet(petName);
            if (temPet != null)
            {
                GameObject pet = GameObject.Instantiate<GameObject>(temPet, hit.point, Quaternion.identity);
                _pet = pet.AddComponent<_Pet_01>();
                return _pet;
            }
        }
        return null;
    }

    // 펫 컨트롤러 넘겨줄 임시 함수
    public PetController CreatePet2(Vector3 origin, string petName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPet = GameManager.Resource.GetPet(petName);
            if (temPet != null)
            {
                // 생성
                GameObject pet = GameObject.Instantiate<GameObject>(temPet, hit.point, Quaternion.identity);
                // 컴포넌트 부착
                _pet2 = pet.AddComponent<PetController>();
                // 오브젝트 매니저 관리
                GameManager.Obj._petStat = pet.AddComponent<PetStat>();
                return _pet2;
            }
        }
        return null;
    }

    // 아이템 타입에 따라 함수를 더 만들던지 아니면 조건문을 넣던지 // 함수를 하나 더 만드는게 편하긴 함
    public ItemController CreateFieldItem(Vector3 origin, string fieldItemName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temfieldItem = GameManager.Resource.GetfieldItem(fieldItemName);
            if (temfieldItem != null)
            {
                string tempName = temfieldItem.name;
                GameObject fieldItem = GameObject.Instantiate<GameObject>(temfieldItem, hit.point, Quaternion.identity);
                _item = fieldItem.AddComponent<ItemController>();
                // 임시코드 우선 나오는 모든 아이템은 무기로 지정
                _item._itemType = Define.ItemType.Weapon;
                GameManager.Obj._itemContList.Add(_item);
                _item.name = tempName;
                return _item;
            }
        }
        return null;
    }

    public MonsterController CreateMonster(Vector3 origin, string monsterName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temMonsterName = GameManager.Resource.GetMonster(monsterName);
            if (temMonsterName != null)
            {
                string tempName = temMonsterName.name;
                GameObject monster = GameObject.Instantiate<GameObject>(temMonsterName, hit.point, Quaternion.identity);
                _monster = monster.AddComponent<MonsterController>();
                GameManager.Obj._mobContList.Add(_monster);
                _monsterStat = monster.AddComponent<MonsterStat>();
                GameManager.Obj._mobStatList.Add(_monsterStat);
                _monster.name = tempName;
                return _monster;
            }
        }
        return null;
    }
}
