using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterControllBoss : BaseController
{
    void Awake()
    {
        EffectManager.Instance.MonsterEffect(this.transform.position, Vector3.zero, this.gameObject.transform, EffectType.Summon);
        //몬스터 위 HpBar 위치
        _hpBarOffset = new Vector3(0, 6f, 0);
        //Resources 폴더 안에 있는 HpBar 프리팹 불러오기
        _hpBarPrefab = Resources.Load<GameObject>("HpBar");
        _damageTextOffset = new Vector3(0, 8f, 0);
        hudDamageText = Resources.Load<GameObject>("DamageText");
        _distance = 9000f;
        _rotateSpeed = 90f;
        _attack = 3.0f;
        _mobNum = 0;
        _time = 0;

        _hp = 20;
        _fullHp = 20;
    }
    // Start is called before the first frame update
    void Start()
    {
        _monsterStat = GetComponent<MonsterStat>();
        //몬스터 애니메이터 가지고옴
        _ani = GetComponent<Animator>();
        // 게임매니저에서 플레이어 위치 가지고 옴
        //_PlayerPos = GameManager.Obj._playerController.transform;
        //// 게임매니저에서 플레이어 스크립트 가지고 옴
        //_playerController = GameManager.Obj._playerController;
        //HpBar함수 호출
        SetHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        _PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //업데이트에서 플레이어저 정보 가지고오는 것을 Start()에서 한번만 가지고오게 변경
        _PosToPos = Vector3.Distance(_PlayerPos.position, transform.position);
        UpdateState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void UpdateIdle()
    {
        base.UpdateIdle();
    }

    public override void UpdateMoving()
    {
        _speed = 7.0f;
        if (_PosToPos <= _distance && _PosToPos > _attack)
        {
            Vector3 targetPos = new Vector3(2.5f, 0, 0);

            transform.position = Vector3.MoveTowards(transform.position, (_PlayerPos.transform.position - targetPos), _speed * Time.deltaTime);
            Vector3 _lookRotation = _PlayerPos.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

        }
        // 공격
        else if (_PosToPos <= _attack)
        {
            _ani.SetInteger("state", 0);
            _time += Time.deltaTime;
            if (_time > 3f)
            {
                _skillPersent = Random.Range(0, 99);
                Debug.Log(_skillPersent);
                if (_skillPersent > 90)
                {
                    Property_state = CreatureState.Skill;
                }
                else if (_skillPersent <= 90)
                {
                    Property_state = CreatureState.Attack;
                }
                _time = 0;
            }
        }
        //대기
        else if (_PosToPos > _distance)
        {
             Property_state = CreatureState.Idle;
             return;
        }
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();
    }

    public override void UpdateDead()
    {
        base.UpdateDead();
    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override IEnumerator AttackDelay(float _delay)
    {
        yield return StartCoroutine(base.AttackDelay(_delay));
    }

    // 사망 딜레이 때문에 생기는 경고메세지 제거용
    public override IEnumerator DeadDelay(float _delay)
    {
        yield return StartCoroutine(base.DeadDelay(_delay));
    }

    public override IEnumerator CoolTime(float _coolTime)
    {
        yield return StartCoroutine(base.CoolTime(_coolTime));
    }

    //몬스터 대미지 받는 함수
    //public void OnDamaged(int playerAtk)
    //{
    //    // 대미지 계산
    //    _monsterStat.Hp -= playerAtk - _monsterStat.Def;
    //    // 데미지에 따른 Hp감소
    //    _hpBarImage.fillAmount = _monsterStat.Hp / _monsterStat.Max_Hp;
    //    if (_monsterStat.Hp <= 0)
    //    {
    //        //MonsterManager를 게임매니저에 연결해서 사용. MonsterManager >> GameManager.Mob
    //        GameManager.Mob.Property_isDie = true;
    //        Property_state = CreatureState.Dead;
    //        //_hp = 10.0f; // 말해준거 같은대 까먹음... 오브젝트 풀링용도면 추후 사용
    //        return;
    //    }
    //}

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public override void SetHpBar()
    {
        base.SetHpBar();
    }
}
