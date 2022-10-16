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
    MonsterControllerEX _monster;
    MonsterStat _monsterStat;

    // �÷��̾� ���� Ȯ�ο�
    public Define.Job _select_Job;
    // �� ���� Ȯ�ο�
    public Define.Pet _select_Pet;

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
                // Hp �� ����
                GameManager.Ui.PlayerHpBarCreate(player);
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
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPet = GameManager.Resource.GetPet(petName);
            if (temPet != null)
            {
                // ����
                GameObject pet = GameObject.Instantiate<GameObject>(temPet, hit.point, Quaternion.identity);
                // ������Ʈ ����
                _pet = pet.AddComponent<PetController>();
                // Obj �Ŵ������� �� Stat ���� (���� �Ŵ������� �����ؾߵ��� ���)
                GameManager.Obj._petStat = pet.AddComponent<PetStat>();
                // ���� ����
                GameManager.Stat.PetStatLoadJson(_select_Pet);
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

    public MonsterControllerEX CreateMonster(Vector3 origin, string monsterName)
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
                GameManager.Stat.MonsterStatLoadJson(tempName, _monsterStat);
                _monster.name = tempName;
                // Hp �� ����
                GameManager.Ui.HpBarCreate(monster);
                return _monster;
            }
        }
        return null;
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
