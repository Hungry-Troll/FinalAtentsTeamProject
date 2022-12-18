using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    // 게임 오브젝트의 정보를 가지고 있는 매니저
    // 필드 매니저 정보
    public FieldManager _fieldManager;

    // 플레이어 정보
    public PlayerController _playerController;
    public PlayerStat _playerStat;
    // 펫 정보
    public PetController _petController;
    public PetStat _petStat;
    // 몬스터 정보
    public List<MonsterControllerEX> _monsterContList = new List<MonsterControllerEX>();
    public List<MonsterStat> _monsterStatList = new List<MonsterStat>();
    // 보스 정보
    public BossMonsterControllerEX _boss;
    public MonsterStat _bossStat;

    // 베니스(상인) 정보
    public VeniceController _veniceController;

    // 골드 정보
    public GoldController _goldController;

    // 공격 타겟 몬스터 // 찾은 몬스터 // 단일 공격용
    public GameObject _targetMonster;
    public MonsterControllerEX _targetMonsterController;
    public MonsterStat _targetMonsterStat;

    // 공격 타겟 몬스터 // 광역 공격용
    public List<MonsterControllerEX> _targetMonstersControllerList;

    // 필드 아이템 정보
    public List<ItemController> _itemContList = new List<ItemController>();
    public List<ItemStatEX> _itemStatList = new List<ItemStatEX>();

    // 인벤토리 아이템을 오브젝트 풀링 방식으로 관리 >> Util.Instaiate에서 사용하는 모든 대상 오브젝트 풀 관리
    // 하이어라키창 정리용 빈 게임오브젝트
    public GameObject _go;
    // Util.Instantiate에서 사용하는 변수
    public List<GameObject> _objPool = new List<GameObject>();

    public void Init()
    {
        _go = new GameObject();
        _go.name = "@ObjPool_Root";
        // 시작 시 오브젝트풀 리스트 초기화
        _objPool.Clear();
    }

    // 몬스터 정보에서 타겟 몬스터를 찾는 함수 길찾기 x
    public void FindMobListTarget()
    {
        // 널체크
        if(GameManager.Obj._monsterContList.Count <= 0 || GameManager.Obj._monsterContList == null)
        {
            return;
        }

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
    // 몬스터 정보에서 타겟 몬스터를 찾는 함수 길찾기 x (여러 몬스터 찾기)
    public List<MonsterControllerEX> FindMobListTargets()
    {
        // 단일 타겟처럼 대상을 바로 오브젝트매니저에 접근해서 처리하려니까 버그가 발생해서
        // 지역변수 리스트를 선언하고 대상을 리스트로 넘겨줌
        List<MonsterControllerEX> targetMonstersControllerList = new List<MonsterControllerEX>();
        // 널체크
        if (GameManager.Obj._monsterContList.Count <= 0 || GameManager.Obj._monsterContList == null)
        {
            return null;
        }
        for (int i = 0; i < GameManager.Obj._monsterContList.Count; i++)
        {
            // 플레이어 콜라이더와 몬스터 콜라이더를 바운즈로 확인
            if (GameManager.Obj._playerController._skill2BoxCollider.bounds.Intersects(GameManager.Obj._monsterContList[i]._mobBoxCollider.bounds))
            {
                targetMonstersControllerList.Add(GameManager.Obj._monsterContList[i]);
            }
        }
        // 타겟 대상들이 없을 경우
        if(targetMonstersControllerList.Count <= 0 || targetMonstersControllerList == null)
        {
            return null;
        }
        // 있을 경우
        return targetMonstersControllerList;
    }

    // 몬스터 정보에서 타겟 몬스터를 제거하는 함수
    public void RemoveMobListTraget(string MobName)
    {
        for(int i = 0; i < _monsterContList.Count; i++)
        {
            // 이름으로 같은 게임오브젝트인지 확인함
            if(MobName == _monsterContList[i].gameObject.name)
            {
                // 같은 이름의 몬스터 컨트롤러 제거
                _monsterContList.RemoveAt(i);
                // 타겟 몬스터 제거
                _targetMonster = null;
            }
        }
    }
    // 씬 이동 시 몬스터 정보 초기화 하는 함수 (씬 이동 시 기존 몬스터 정보가 있으면 안됨 )
    public void RemoveAllMobList()
    {
        _monsterContList.Clear();
        _monsterStatList.Clear();
    }
}
