using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    // 게임 오브젝트의 정보를 가지고 있는 매니저
    // 플레이어 정보
    public PlayerController _playerController;
    public PlayerStat _playerStat;
    // 펫 정보
    public PetController _petController;
    public PetStat _petStat;
    // 몬스터 정보
    public List<MonsterController> _monsterContList = new List<MonsterController>();
    public List<MonsterStat> _monsterStatList = new List<MonsterStat>();

    // 공격 타겟 몬스터 // 찾은 몬스터
    public GameObject _targetMonster;
    public MonsterController _targetMonsterController;
    public MonsterStat _targetMonsterStat;


    // 아이템 정보
    public List<ItemController> _itemContList = new List<ItemController>();
    public List<ItemStatEX> _itemStatList = new List<ItemStatEX>();

    // 몬스터 정보에서 타겟 몬스터를 찾는 함수 길찾기 x
    public void FindMobListTarget()
    {
        List<float> targetDistance = new List<float>();
        float distance = 0;
        _targetMonster = null;

        for(int i = 0; i < GameManager.Obj._monsterContList.Count; i++)
        {
            // 몹리스트에서 몹이 있는지 체크
            if(GameManager.Obj._monsterContList.Count > 0)
            {
                targetDistance.Add(Vector3.Distance(GameManager.Obj._monsterContList[i].transform.position, GameManager.Obj._playerController.transform.position));
            }
            else
            {
                // 몹리스트에 몹이 없으면 리턴
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
    // 몬스터 정보에서 타겟 몬스터를 제거하는 함수
    public void RemoveMobListTraget(string MobName)
    {
        for(int i = 0; i < GameManager.Obj._monsterContList.Count; i++)
        {
            // 이름으로 같은 게임오브젝트인지 확인함
            if(MobName == GameManager.Obj._monsterContList[i].gameObject.name)
            {
                // 같은 이름의 몬스터 컨트롤러 제거
                GameManager.Obj._monsterContList.RemoveAt(i);
            }
        }
    }


}
