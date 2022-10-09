using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ʵ忡 �ִ� ũ������ �Լ����� ���⼭ ó��
// ���� �ٸ� �������� �ٽ� �÷��̾�, ��, ���͸� ������ �Ǳ� ������ �ʵ�Ŵ����� �ΰ� ����� �� ����
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
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPlayerChar = GameManager.Resource.GetCharacter(playerName);
            if (temPlayerChar != null)
            {
                // ����
                GameObject player = GameObject.Instantiate<GameObject>(temPlayerChar, hit.point, Quaternion.identity);
                // ������Ʈ ����
                _player = player.AddComponent<PlayerController>();
                // Obj �Ŵ������� PlayerStat ����
                GameManager.Obj._playerStat = player.AddComponent<PlayerStat>();
                // ���� ����
                GameManager.Stat.PlayerStatLoadJson(1, GameManager.Select._job);
                // ���� ĳ����â ����
                GameManager.Ui.InventoryStatUpdate();
                return _player;
            }
        }
        return null;
    }
    public _Pet_01 CreatePet(Vector3 origin, string petName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
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

    // �� ��Ʈ�ѷ� �Ѱ��� �ӽ� �Լ�
    public PetController CreatePet2(Vector3 origin, string petName)
    {
        // ������ ���̸� ���� ���� ���̿� ���� ĳ���� ���� �ڵ�
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPet = GameManager.Resource.GetPet(petName);
            if (temPet != null)
            {
                // ����
                GameObject pet = GameObject.Instantiate<GameObject>(temPet, hit.point, Quaternion.identity);
                // ������Ʈ ����
                _pet2 = pet.AddComponent<PetController>();
                // ������Ʈ �Ŵ��� ����
                GameManager.Obj._petStat = pet.AddComponent<PetStat>();
                return _pet2;
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
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temfieldItem = GameManager.Resource.GetfieldItem(fieldItemName);
            if (temfieldItem != null)
            {
                string tempName = temfieldItem.name;
                GameObject fieldItem = GameObject.Instantiate<GameObject>(temfieldItem, hit.point, Quaternion.identity);
                _item = fieldItem.AddComponent<ItemController>();
                // �ӽ��ڵ� �켱 ������ ��� �������� ����� ����
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

    public MonsterController CreateMonster(Vector3 origin, string monsterName)
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
                _monster = monster.AddComponent<MonsterController>();
                GameManager.Obj._mobContList.Add(_monster);
                _monsterStat = monster.AddComponent<MonsterStat>();
                GameManager.Obj._mobStatList.Add(_monsterStat);
                // ���� ��ũ��Ʈ�� json ���� ���� ����
                GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                _monster.name = tempName;
                return _monster;
            }
        }
        return null;
    }
}
