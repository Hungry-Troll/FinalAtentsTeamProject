using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    // ���� ������Ʈ�� ������ ������ �ִ� �Ŵ���
    // �÷��̾� ����
    public PlayerController _playerController;
    public PlayerStat _playerStat;
    // �� ����
    public PetController _petController;
    public PetStat _petStat;
    // ���� ����
    public List<MonsterController> _monsterContList = new List<MonsterController>();
    public List<MonsterStat> _monsterStatList = new List<MonsterStat>();

    // ���� Ÿ�� ���� // ã�� ����
    public GameObject _targetMonster;
    public MonsterController _targetMonsterController;
    public MonsterStat _targetMonsterStat;


    // ������ ����
    public List<ItemController> _itemContList = new List<ItemController>();
    public List<ItemStatEX> _itemStatList = new List<ItemStatEX>();

    // ���� �������� Ÿ�� ���͸� ã�� �Լ� ��ã�� x
    public void FindMobListTarget()
    {
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

            if(distance < targetDistance[i])
            {
                distance = targetDistance[i];
                _targetMonster = GameManager.Obj._monsterContList[i].gameObject;
                _targetMonsterStat = GameManager.Obj._monsterStatList[i];
                _targetMonsterController = GameManager.Obj._monsterContList[i];
            }
        }
    }
    // ���� �������� Ÿ�� ���͸� �����ϴ� �Լ�
    public void RemoveMobListTraget(string MobName)
    {
        for(int i = 0; i < GameManager.Obj._monsterContList.Count; i++)
        {
            // �̸����� ���� ���ӿ�����Ʈ���� Ȯ����
            if(MobName == GameManager.Obj._monsterContList[i].gameObject.name)
            {
                // ���� �̸��� ���� ��Ʈ�ѷ� ����
                GameManager.Obj._monsterContList.RemoveAt(i);
            }
        }
    }


}
