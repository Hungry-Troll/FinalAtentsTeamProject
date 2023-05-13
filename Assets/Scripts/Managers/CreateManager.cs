using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ʵ忡 �ִ� ũ������ �Լ����� ���⼭ ó��
// ���� �ٸ� �������� �ٽ� �÷��̾�, ��, ���͸� ������ �Ǳ� ������ �ʵ�Ŵ����� �ΰ� ����� �� ����
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

    // �÷��̾� ���� Ȯ�ο�
    public Define.Job _select_Job;
    // �� ���� Ȯ�ο�
    public Define.Pet _select_Pet;

    public PlayerController CreatePlayerCharacter(Vector3 origin, string playerName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
        origin.y += 100f;
        RaycastHit hit;
        if(Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPlayerChar = GameManager.Resource.GetCharacter(playerName);
            if(temPlayerChar != null)
            {
                // ����
                GameObject player = GameObject.Instantiate<GameObject>(temPlayerChar, hit.point, Quaternion.identity);
                
                // ������ ������Ʈ ����
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
                
                // Obj �Ŵ������� PlayerStat ����
                GameManager.Obj._playerStat = player.AddComponent<PlayerStat>();
                // ���� ����
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

                // ���� ĳ����â ����
                GameManager.Ui.InventoryStatUpdate();
                // Hp �� ����
                GameManager.Ui.PlayerHpBarCreate(player);

                // ��� ����
                GameManager.Obj._goldController = player.AddComponent<GoldController>();
                GameManager.Ui.PlayerGoldDisplayCreate(player);
                // ��������Ʈ�ѷ�
                player.AddComponent<LevelUpController>();
                return _player;
            }
        }
        return null;
    }

    public PetController CreatePet(Vector3 origin, string petName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
        origin.y += 100f;
        RaycastHit hit;
        if(Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPet = GameManager.Resource.GetPet(petName);
            if(temPet != null)
            {
                // ����
                GameObject pet = GameObject.Instantiate<GameObject>(temPet, hit.point, Quaternion.identity);
                // ������Ʈ ����
                _pet = pet.AddComponent<PetController>();
                // Obj �Ŵ������� �� Stat ���� (���� �Ŵ������� �����ؾߵ��� ���)
                GameManager.Obj._petStat = pet.AddComponent<PetStat>();
                // ���� ����
                //GameManager.Stat.PetStatLoadJson(_select_Pet);
                GameManager.Parse.FindPetObjData(petName);
                // Hp �� ����
                GameManager.Ui.HpBarCreate(pet);
                return _pet;
            }
        }
        return null;
    }

    // ������ Ÿ�Կ� ���� �Լ��� �� ������� �ƴϸ� ���ǹ��� �ִ��� // �Լ��� �ϳ� �� ����°� ���ϱ� ��
    public ItemController CreateFieldItem(Vector3 origin, string fieldItemName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
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
                // �ӽ��ڵ� �켱 ������ ��� �������� ����� ���� >> itemStatEx�� �ִ� ������ �������� ����ص� �� �� ����
                _item._itemType = Define.ItemType.Weapon;
                GameManager.Obj._itemContList.Add(_item);
                // ���� ��ũ��Ʈ�� �ְ�
                _itemStatEX = fieldItem.AddComponent<ItemStatEX>();
                // ������Ʈ �Ŵ����� ����
                GameManager.Obj._itemStatList.Add(_itemStatEX);
                // ���� ��ũ��Ʈ�� json ���� ���� ����
                GameManager.Stat.ItemStatLoadJson(tempName, _itemStatEX);
                _item.name = tempName;
                return _item;
            }
        }
        return null;
    }

    // �Ϲ� ���� ����
    public MonsterControllerEX CreateMonster(Vector3 origin, string monsterName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
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
                // ���� ��ũ��Ʈ�� json ���� ���� ����
                //GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                GameManager.Parse.FindMonsterObjData(Define.Monster.Velociraptor, _monsterStat);
                _monster.name = tempName;
                // Hp �� ����
                GameManager.Ui.HpBarCreate(monster);
                return _monster;
            }
        }
        return null;
    }

    // ����Ʈ ���� ����
    public MonsterControllerEX CreateQuestMonster(Vector3 origin, string monsterName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
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
                // ���� ��ũ��Ʈ�� json ���� ���� ����
                //GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                GameManager.Parse.FindMonsterObjData(Define.Monster.Velociraptor, _monsterStat);
                _monster.name = tempName;
                // Hp �� ����
                GameManager.Ui.HpBarCreate(monster);
                // ����Ʈ ���� ����
                _monster._isQuest = true;
                return _monster;
            }
        }
        return null;
    }
    // ����Ʈ ���� �ΰ��� ����
    public BossChangeEX CreateHumanBoss(Vector3 origin, string monsterName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
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

    // ����Ʈ �������� ����
    public BossMonsterControllerEX CreateBoss(Vector3 origin, string monsterName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
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
                // ���� ��ũ��Ʈ�� json ���� ���� ����
                //GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                GameManager.Parse.FindMonsterObjData(Define.Monster.Boss, GameManager.Obj._bossStat);
                _boss.name = tempName;
                // Hp �� ����
                GameManager.Ui._bossHpbar = GameManager.Create.CreateUi("UI_BossHpBar", _boss.gameObject);
                // ����Ʈ ���� ����
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

    // �κ��丮�� ������ �����ϴ� �Լ�
    public void CreateInventoryItem(string invenItemName)
    {
        // ���ҽ��Ŵ������� ã�Ƽ� �÷��̾� ������ �κ��丮�� �Ѱ� �־���
        GameObject tmp = GameManager.Resource.GetfieldItem(invenItemName);
        GameObject Item = Util.Instantiate(tmp);
        ItemStatEX itemStatEX = Item.AddComponent<ItemStatEX>();
        // ���� ��ũ��Ʈ�� json ���� ���� ����
        GameManager.Stat.ItemStatLoadJson(tmp.name, itemStatEX);
        GameManager.Item.InventoryItemAdd(Item, false);
    }

    //UI ���� �Լ� uiRoot�� UI�Ŵ��� go
    public GameObject CreateUi(string uiName, GameObject uiRoot) 
    {
        GameObject tmpUi = GameManager.Resource.GetUi(uiName);
        GameObject tmepUi = GameObject.Instantiate<GameObject>(tmpUi);
        tmepUi.transform.SetParent(uiRoot.transform);
        return tmepUi;
    }
}
