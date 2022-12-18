using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    // ���� ������Ʈ�� ������ ������ �ִ� �Ŵ���
    // �ʵ� �Ŵ��� ����
    public FieldManager _fieldManager;

    // �÷��̾� ����
    public PlayerController _playerController;
    public PlayerStat _playerStat;
    // �� ����
    public PetController _petController;
    public PetStat _petStat;
    // ���� ����
    public List<MonsterControllerEX> _monsterContList = new List<MonsterControllerEX>();
    public List<MonsterStat> _monsterStatList = new List<MonsterStat>();
    // ���� ����
    public BossMonsterControllerEX _boss;
    public MonsterStat _bossStat;

    // ���Ͻ�(����) ����
    public VeniceController _veniceController;

    // ��� ����
    public GoldController _goldController;

    // ���� Ÿ�� ���� // ã�� ���� // ���� ���ݿ�
    public GameObject _targetMonster;
    public MonsterControllerEX _targetMonsterController;
    public MonsterStat _targetMonsterStat;

    // ���� Ÿ�� ���� // ���� ���ݿ�
    public List<MonsterControllerEX> _targetMonstersControllerList;

    // �ʵ� ������ ����
    public List<ItemController> _itemContList = new List<ItemController>();
    public List<ItemStatEX> _itemStatList = new List<ItemStatEX>();

    // �κ��丮 �������� ������Ʈ Ǯ�� ������� ���� >> Util.Instaiate���� ����ϴ� ��� ��� ������Ʈ Ǯ ����
    // ���̾��Űâ ������ �� ���ӿ�����Ʈ
    public GameObject _go;
    // Util.Instantiate���� ����ϴ� ����
    public List<GameObject> _objPool = new List<GameObject>();

    public void Init()
    {
        _go = new GameObject();
        _go.name = "@ObjPool_Root";
        // ���� �� ������ƮǮ ����Ʈ �ʱ�ȭ
        _objPool.Clear();
    }

    // ���� �������� Ÿ�� ���͸� ã�� �Լ� ��ã�� x
    public void FindMobListTarget()
    {
        // ��üũ
        if(GameManager.Obj._monsterContList.Count <= 0 || GameManager.Obj._monsterContList == null)
        {
            return;
        }

        List<float> targetDistance = new List<float>();
        float distance = 0;
        _targetMonster = null;

        for(int i = 0; i < GameManager.Obj._monsterContList.Count; i++)
        {
            // ������Ʈ���� ���� �ִ��� üũ
            if(GameManager.Obj._monsterContList.Count > 0)
            {
                targetDistance.Add(Vector3.Distance(GameManager.Obj._monsterContList[i].transform.position, GameManager.Obj._playerController.transform.position));
            }
            else
            {
                // ������Ʈ�� ���� ������ ����
                return;
            }

            if (distance == 0)
            {
                distance = targetDistance[i];
                _targetMonster = GameManager.Obj._monsterContList[i].gameObject;
                //_targetMonsterStat = GameManager.Obj._monsterStatList[i];
                _targetMonsterController = GameManager.Obj._monsterContList[i];
            }

            if (distance > targetDistance[i])
            {
                distance = targetDistance[i];
                _targetMonster = GameManager.Obj._monsterContList[i].gameObject;
                //_targetMonsterStat = GameManager.Obj._monsterStatList[i];
                _targetMonsterController = GameManager.Obj._monsterContList[i];
            }
        }
    }
    // ���� �������� Ÿ�� ���͸� ã�� �Լ� ��ã�� x (���� ���� ã��)
    public List<MonsterControllerEX> FindMobListTargets()
    {
        // ���� Ÿ��ó�� ����� �ٷ� ������Ʈ�Ŵ����� �����ؼ� ó���Ϸ��ϱ� ���װ� �߻��ؼ�
        // �������� ����Ʈ�� �����ϰ� ����� ����Ʈ�� �Ѱ���
        List<MonsterControllerEX> targetMonstersControllerList = new List<MonsterControllerEX>();
        // ��üũ
        if (GameManager.Obj._monsterContList.Count <= 0 || GameManager.Obj._monsterContList == null)
        {
            return null;
        }
        for (int i = 0; i < GameManager.Obj._monsterContList.Count; i++)
        {
            // �÷��̾� �ݶ��̴��� ���� �ݶ��̴��� �ٿ���� Ȯ��
            if (GameManager.Obj._playerController._skill2BoxCollider.bounds.Intersects(GameManager.Obj._monsterContList[i]._mobBoxCollider.bounds))
            {
                targetMonstersControllerList.Add(GameManager.Obj._monsterContList[i]);
            }
        }
        // Ÿ�� ������ ���� ���
        if(targetMonstersControllerList.Count <= 0 || targetMonstersControllerList == null)
        {
            return null;
        }
        // ���� ���
        return targetMonstersControllerList;
    }

    // ���� �������� Ÿ�� ���͸� �����ϴ� �Լ�
    public void RemoveMobListTraget(string MobName)
    {
        for(int i = 0; i < _monsterContList.Count; i++)
        {
            // �̸����� ���� ���ӿ�����Ʈ���� Ȯ����
            if(MobName == _monsterContList[i].gameObject.name)
            {
                // ���� �̸��� ���� ��Ʈ�ѷ� ����
                _monsterContList.RemoveAt(i);
                // Ÿ�� ���� ����
                _targetMonster = null;
            }
        }
    }
    // �� �̵� �� ���� ���� �ʱ�ȭ �ϴ� �Լ� (�� �̵� �� ���� ���� ������ ������ �ȵ� )
    public void RemoveAllMobList()
    {
        _monsterContList.Clear();
        _monsterStatList.Clear();
    }
}
