using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager 
{
    // 게임 오브젝트의 정보를 가지고 있는 매니저
    // 플레이어 정보
    public PlayerController _playerController;
    public PlayerStat _playerStat;
    // 몬스터 정보
    public List<MonsterController> _mobContList = new List<MonsterController>();
    public List<MonsterStat> _mobStatList = new List<MonsterStat>();
    // 아이템 정보
    public List<ItemController> _itemContList = new List<ItemController>();

}
