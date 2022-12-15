using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BossMonsterControllerEX : MonsterControllerEX
{
    public GameObject[] MobPrefab;
    public override void Awake()
    {
        base.Awake();

        _time = 0;

        //MobPrefab = new GameObject[3];
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    //// Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override CreatureState Property_state
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;

            switch (_state)
            {
                case CreatureState.Idle:
                    _ani.SetInteger("state", 0);
                    break;
                case CreatureState.Move:
                    _ani.SetInteger("state", 1);
                    break;
                case CreatureState.Attack:
                    _ani.SetTrigger("isAttack");
                    break;
                case CreatureState.Dead:
                    _ani.CrossFade("Dead", 0.3f);
                    break;
                case CreatureState.Skill:
                    _ani.SetTrigger("isSkill");
                    break;
                case CreatureState.Skill2:
                    _ani.SetTrigger("isSkill2");
                    break;
            }
        }
    }

    public override void UpdateState()
    {
        //플레이어를 계속 찾는 것 수정
        //_PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        switch (Property_state)
        {
            case CreatureState.Idle:
                UpdateIdle();
                break;
            case CreatureState.Move:
                UpdateMoving();
                break;
            case CreatureState.Attack:
                UpdateAttack();
                break;
            case CreatureState.Dead:
                Invoke("UpdateDead", 0.5f);
                break;
            case CreatureState.Skill:
                UpdateSkill();
                break;
            case CreatureState.Skill2:
                UpdateSkill2();
                break;
        }
    }

    public override void UpdateIdle()
    {
        base.UpdateIdle();
    }

    public override void UpdateMoving()
    {
        _speed = 0.5f;
        _ani.SetInteger("state", 1);
        if (_PosToPos <= _distance && _PosToPos > 10f)
        {
            _ani.SetInteger("state", 2);
            Vector3 targetPos = new Vector3(2.5f, 0, 0);

            transform.position = Vector3.MoveTowards(transform.position, (_PlayerPos.transform.position - targetPos), _speed * Time.deltaTime);
            Vector3 _lookRotation = _PlayerPos.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

        }
        else if (_PosToPos <= 10f && _PosToPos > _attack)
        {
            _speed = 0.5f;
            _ani.SetInteger("state", 1);
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
            if (_time > 4f)
            {
                _skillPersent = Random.Range(0, 99);
                Debug.Log(_skillPersent);
                if (_skillPersent > 90)
                {
                    StartCoroutine(CoolTime(7f));
                    float hpPer = (_monsterStat.Hp / _monsterStat.Max_Hp * 100);
                    if (hpPer >= 70)
                    {
                        Property_state = CreatureState.Skill;
                    }
                    else
                    {
                        Property_state = CreatureState.Skill2;
                    }
                }
                else if (_skillPersent <= 90)
                {
                    Property_state = CreatureState.Attack;
                }
                _time = 0;
            }
        }
        // 대기
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
        Vector3 _pos = new Vector3(0, 0, 0);
        EffectManager.Instance.MonsterEffect(this.transform.position, Vector3.zero, this.gameObject.transform, EffectType.Roar);
        int _monsterNum = Random.Range(0, 2);
        RaycastHit hit;
        if (Physics.Raycast(_pos, -Vector3.up, out hit, Mathf.Infinity))
        {
            switch (_monsterNum)
            {
                case (0):
                    Instantiate(MobPrefab[0], hit.point, Quaternion.identity);
                    break;
                case (1):
                    Instantiate(MobPrefab[1], hit.point, Quaternion.identity);
                    break;
                case (2):
                    Instantiate(MobPrefab[2], hit.point, Quaternion.identity);
                    break;
            }
        }
        Property_state = CreatureState.Idle;
        return;
    }

    public override void UpdateSkill2()
    {
        Property_state = CreatureState.Idle;
        return;
    }

    /*    public override void OnDisable()
        {
            base.OnDisable();
        }*/

    //public override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}

    public override IEnumerator AttackDelay(float _delay)
    {
        yield return StartCoroutine(base.AttackDelay(_delay));
    }

    public override IEnumerator DeadDelay(float _delay)
    {
        yield return StartCoroutine(base.DeadDelay(_delay));
    }

    public override IEnumerator CoolTime(float _coolTime)
    {
        yield return StartCoroutine(base.CoolTime(_coolTime));
    }

    public override void OnDamaged(int playerAtk, int SkillDamagePercent)
    {
        base.OnDamaged(playerAtk, SkillDamagePercent);
    }
}
