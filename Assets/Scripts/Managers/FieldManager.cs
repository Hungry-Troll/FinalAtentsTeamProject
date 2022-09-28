using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    //���� ���۽� Awake , Start, Update ��� �뵵 �Ŵ���

    // �÷��̾� �� ������ ���� ������ ���� ���� ����
    // ���� �� ������ �����ؼ� GameManager.Obj �� ���� ������Ʈ ����
    PlayerController _player;
    _Pet_01 _pet;
    ItemController _item;
    MonsterController _monster;
    MonsterStat _monsterStat;

    // �÷��̾� ��ġ ������ / ����ִ� ���ӿ�����Ʈ�� �÷��̾� ��ġ ����
    public GameObject _startPosObject;
    public Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        // ���ӸŴ������� Ui�Ŵ��� Init(Awake �Լ� ��ü)
        // Ui �ҷ���
        GameManager.Ui.Init();
        // �÷��̾� ĳ���� ����
        // ���� �÷��̾� ����â���� string ���� �̸��� �޾ƿ��� ��
        // ������ġ�� �ʸ��� �ٸ��� �ؾ� ��
        _startPos = _startPosObject.transform.position;
        // Obj�Ŵ������� �÷��̾ũ��Ʈ�� ����ְ���
        GameManager.Obj._playerController = CreatePlayerCharacter(_startPos, "player");

        GameManager.Cam.Init();

        // ���� ������ �׽�Ʈ �ڵ�
        for (int i = 0; i < GameManager.Resource._monster.Count; i++)
        {
            Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
            CreateMonster(_player.transform.position + tempPos, GameManager.Resource._monster[i].name);
        }

        // �� ����
        CreatePet(_startPos, "Fox");

        // ������ ������ �׽�Ʈ �ڵ�
        for (int i = 0; i < GameManager.Resource._fieldItem.Count; i++)
        {
            Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
            CreateFieldItem(_player.transform.position + tempPos, GameManager.Resource._fieldItem[i].name);
        }

        // Stat�Ŵ������� �������� ������ ��
        GameManager.Stat.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public PlayerController CreatePlayerCharacter(Vector3 origin , string playerName)
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
                GameObject player = GameObject.Instantiate<GameObject>(temPlayerChar,hit.point,Quaternion.identity);
                // ������Ʈ ����
                _player = player.AddComponent<PlayerController>();
                // Obj �Ŵ������� PlayerStat ����
                GameManager.Obj._playerStat = player.AddComponent<PlayerStat>();
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
                GameManager.Obj._itemContList.Add(_item);
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
                _monster.name = tempName;
                return _monster;
            }
        }
        return null;
    }
}
