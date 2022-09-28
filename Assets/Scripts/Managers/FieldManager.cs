using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    //게임 시작시 Awake , Start, Update 사용 용도 매니저

    // 플레이어 펫 아이템 몬스터 접근을 위한 변수 선언
    // 이후 각 변수에 대입해서 GameManager.Obj 를 통한 오브젝트 관리
    PlayerController _player;
    _Pet_01 _pet;
    ItemController _item;
    MonsterController _monster;
    MonsterStat _monsterStat;

    // 플레이어 위치 설정용 / 비어있는 게임오브젝트에 플레이어 위치 대입
    public GameObject _startPosObject;
    public Vector3 _startPos;

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
        GameManager.Obj._playerController = CreatePlayerCharacter(_startPos, "player");

        GameManager.Cam.Init();

        // 몬스터 생성용 테스트 코드
        for (int i = 0; i < GameManager.Resource._monster.Count; i++)
        {
            Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
            CreateMonster(_player.transform.position + tempPos, GameManager.Resource._monster[i].name);
        }

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
