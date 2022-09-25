using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager 
{
    // ���� ������Ʈ�� ������ ������ �ִ� �Ŵ���
    // �÷��̾� ����
    public PlayerController _playerController;
    public PlayerStat _playerStat;
    // ���� ����
    public List<MonsterController> _mobContList = new List<MonsterController>();
    public List<MonsterStat> _mobStatList = new List<MonsterStat>();
    // ������ ����
    public List<ItemController> _itemContList = new List<ItemController>();

}
