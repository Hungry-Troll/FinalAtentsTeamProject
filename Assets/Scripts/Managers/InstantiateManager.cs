using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 필드에 있는 크리에잇 함수들을 여기서 처리
// 추후 다른 씬에서도 다시 플레이어, 펫, 몬스터를 만들어야 되기 때문에 필드매니저에 두고 사용할 수 없음
public class InstatiateManager : MonoBehaviour
{
    PlayerController _player;
    _Pet_01 _pet;
    PetController _pet2;
    ItemController _item;
    ItemStatEX _itemStatEX;
    MonsterController _monster;
    MonsterStat _monsterStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlayerController CreatePlayerCharacter(Vector3 origin, string playerName)
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
                GameObject player = GameObject.Instantiate<GameObject>(temPlayerChar, hit.point, Quaternion.identity);
                // 컴포넌트 부착
                _player = player.AddComponent<PlayerController>();
                // Obj 매니저에서 PlayerStat 관리
                GameManager.Obj._playerStat = player.AddComponent<PlayerStat>();
                // 스텟 적용
                GameManager.Stat.PlayerStatLoadJson(1, GameManager.Select._job);
                // 스텟 캐릭터창 적용
                GameManager.Ui.InventoryStatUpdate();
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
                // 스텟 스크립트를 넣고
                _itemStatEX = fieldItem.AddComponent<ItemStatEX>();
                // 오브젝트 매니저에 저장
                GameManager.Obj._itemStatList.Add(_itemStatEX);
                // 스텟 스크립트에 json 파일 스텟 적용
                GameManager.Stat.ItemStatLoadJson(tempName, _itemStatEX);
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
                // 스텟 스크립트에 json 파일 스텟 적용
                GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                _monster.name = tempName;
                return _monster;
            }
        }
        return null;
    }
}
