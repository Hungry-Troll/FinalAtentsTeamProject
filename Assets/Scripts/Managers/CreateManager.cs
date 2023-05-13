using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 필드에 있는 크리에잇 함수들을 여기서 처리
// 추후 다른 씬에서도 다시 플레이어, 펫, 몬스터를 만들어야 되기 때문에 필드매니저에 두고 사용할 수 없음
public class CreateManager
{
    PlayerController _player;
    PetController _pet;
    ItemController _item;
    ItemStatEX _itemStatEX;
    MonsterStat _monsterStat;
    MonsterControllerEX _monster;
    BossChangeEX _bossHuman;
    BossMonsterControllerEX _boss;

    // 플레이어 직업 확인용
    public Define.Job _select_Job;
    // 펫 종류 확인용
    public Define.Pet _select_Pet;

    public PlayerController CreatePlayerCharacter(Vector3 origin, string playerName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if(Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPlayerChar = GameManager.Resource.GetCharacter(playerName);
            if(temPlayerChar != null)
            {
                // 생성
                GameObject player = GameObject.Instantiate<GameObject>(temPlayerChar, hit.point, Quaternion.identity);
                
                // 직업별 컴포넌트 부착
                _select_Job = Util.SortJob(playerName);
                switch(_select_Job)
                {
                    case Define.Job.Superhuman:
                        _player = player.AddComponent<SuperhumanController>();
                        break;
                    case Define.Job.Cyborg:
                        _player = player.AddComponent<CyborgController>();
                        break;
                    case Define.Job.Scientist:
                        _player = player.AddComponent<ScientistController>();
                        break;
                    default:
                        _player = player.AddComponent<PlayerController>();
                        break;
                }
                
                // Obj 매니저에서 PlayerStat 관리
                GameManager.Obj._playerStat = player.AddComponent<PlayerStat>();
                // 스텟 적용
                TempStatEX tempStat = GameManager.Parse.FindPlayerObjData2(1, GameManager.Select._job);

                GameManager.Obj._playerStat.Atk = tempStat.Atk;
                GameManager.Obj._playerStat.Def = tempStat.Def;
                GameManager.Obj._playerStat.Hp = tempStat.Hp;
                GameManager.Obj._playerStat.Job = tempStat.Job;
                GameManager.Obj._playerStat.Lv = tempStat.Lv;
                GameManager.Obj._playerStat.Max_Hp = tempStat.Max_Hp;
                GameManager.Obj._playerStat.Name = tempStat.Name;
                GameManager.Obj._playerStat.Exp = tempStat.Exp;
                GameManager.Obj._playerStat.Lv_Exp = tempStat.Lv_Exp;
                GameManager.Obj._playerStat.Gold = tempStat.Gold;

                // 스텟 캐릭터창 적용
                GameManager.Ui.InventoryStatUpdate();
                // Hp 바 적용
                GameManager.Ui.PlayerHpBarCreate(player);

                // 골드 적용
                GameManager.Obj._goldController = player.AddComponent<GoldController>();
                GameManager.Ui.PlayerGoldDisplayCreate(player);
                // 레벨업컨트롤러
                player.AddComponent<LevelUpController>();
                return _player;
            }
        }
        return null;
    }

    public PetController CreatePet(Vector3 origin, string petName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if(Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPet = GameManager.Resource.GetPet(petName);
            if(temPet != null)
            {
                // 생성
                GameObject pet = GameObject.Instantiate<GameObject>(temPet, hit.point, Quaternion.identity);
                // 컴포넌트 부착
                _pet = pet.AddComponent<PetController>();
                // Obj 매니저에서 펫 Stat 관리 (스텟 매니저에서 관리해야될지 고민)
                GameManager.Obj._petStat = pet.AddComponent<PetStat>();
                // 스텟 적용
                //GameManager.Stat.PetStatLoadJson(_select_Pet);
                GameManager.Parse.FindPetObjData(petName);
                // Hp 바 적용
                GameManager.Ui.HpBarCreate(pet);
                return _pet;
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
        if(Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temfieldItem = GameManager.Resource.GetfieldItem(fieldItemName);
            if(temfieldItem != null)
            {
                string tempName = temfieldItem.name;
                GameObject fieldItem = GameObject.Instantiate<GameObject>(temfieldItem, hit.point, Quaternion.identity);
                _item = fieldItem.AddComponent<ItemController>();
                // 임시코드 우선 나오는 모든 아이템은 무기로 지정 >> itemStatEx에 있는 아이템 구분으로 사용해도 될 것 같음
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

    // 일반 몬스터 생성
    public MonsterControllerEX CreateMonster(Vector3 origin, string monsterName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if(Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temMonsterName = GameManager.Resource.GetMonster(monsterName);
            if(temMonsterName != null)
            {
                string tempName = temMonsterName.name;
                GameObject monster = GameObject.Instantiate<GameObject>(temMonsterName, hit.point, Quaternion.identity);
                _monster = monster.AddComponent<MonsterControllerEX>();
                GameManager.Obj._monsterContList.Add(_monster);
                _monsterStat = monster.AddComponent<MonsterStat>();
                GameManager.Obj._monsterStatList.Add(_monsterStat);
                // 스텟 스크립트에 json 파일 스텟 적용
                //GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                GameManager.Parse.FindMonsterObjData(Define.Monster.Velociraptor, _monsterStat);
                _monster.name = tempName;
                // Hp 바 적용
                GameManager.Ui.HpBarCreate(monster);
                return _monster;
            }
        }
        return null;
    }

    // 퀘스트 몬스터 생성
    public MonsterControllerEX CreateQuestMonster(Vector3 origin, string monsterName)
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
                _monster = monster.AddComponent<MonsterControllerEX>();
                GameManager.Obj._monsterContList.Add(_monster);
                _monsterStat = monster.AddComponent<MonsterStat>();
                GameManager.Obj._monsterStatList.Add(_monsterStat);
                // 스텟 스크립트에 json 파일 스텟 적용
                //GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                GameManager.Parse.FindMonsterObjData(Define.Monster.Velociraptor, _monsterStat);
                _monster.name = tempName;
                // Hp 바 적용
                GameManager.Ui.HpBarCreate(monster);
                // 퀘스트 변수 적용
                _monster._isQuest = true;
                return _monster;
            }
        }
        return null;
    }
    // 퀘스트 보스 인간형 생성
    public BossChangeEX CreateHumanBoss(Vector3 origin, string monsterName)
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
                _bossHuman = monster.AddComponent<BossChangeEX>();
                _bossHuman.name = tempName;
                return _bossHuman;
            }
        }
        return null;
    }

    // 퀘스트 보스몬스터 생성
    public BossMonsterControllerEX CreateBoss(Vector3 origin, string monsterName)
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
                _boss = monster.AddComponent<BossMonsterControllerEX>();
                GameManager.Obj._monsterContList.Add(_boss);
                _monsterStat = monster.AddComponent<MonsterStat>();
                GameManager.Obj._bossStat = _monsterStat;
                GameManager.Obj._boss = _boss;
                // 스텟 스크립트에 json 파일 스텟 적용
                //GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                GameManager.Parse.FindMonsterObjData(Define.Monster.Boss, GameManager.Obj._bossStat);
                _boss.name = tempName;
                // Hp 바 적용
                GameManager.Ui._bossHpbar = GameManager.Create.CreateUi("UI_BossHpBar", _boss.gameObject);
                // 퀘스트 변수 적용
                _boss._isQuest = true;
                return _boss;
            }
        }
        return null;
    }
    public GameObject CreateQuestDoor(Vector3 origin, string doorName)
    {
        GameObject door = GameManager.Resource.GetDoor(doorName);
        GameObject questDoor = GameObject.Instantiate<GameObject>(door);
        return questDoor;
    }

    // 인벤토리에 아이템 생성하는 함수
    public void CreateInventoryItem(string invenItemName)
    {
        // 리소스매니저에서 찾아서 플레이어 아이템 인벤토리에 한개 넣어줌
        GameObject tmp = GameManager.Resource.GetfieldItem(invenItemName);
        GameObject Item = Util.Instantiate(tmp);
        ItemStatEX itemStatEX = Item.AddComponent<ItemStatEX>();
        // 스텟 스크립트에 json 파일 스텟 적용
        GameManager.Stat.ItemStatLoadJson(tmp.name, itemStatEX);
        GameManager.Item.InventoryItemAdd(Item, false);
    }

    //UI 생성 함수 uiRoot는 UI매니저 go
    public GameObject CreateUi(string uiName, GameObject uiRoot) 
    {
        GameObject tmpUi = GameManager.Resource.GetUi(uiName);
        GameObject tmepUi = GameObject.Instantiate<GameObject>(tmpUi);
        tmepUi.transform.SetParent(uiRoot.transform);
        return tmepUi;
    }
}
