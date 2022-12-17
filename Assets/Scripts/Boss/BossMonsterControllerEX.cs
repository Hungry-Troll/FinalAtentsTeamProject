using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BossMonsterControllerEX : MonsterControllerEX
{
    //public GameObject[] MobPrefab;
    // 보스 스킬 파티클
    ParticleSystem _attackEffect;
    ParticleSystem _skill1;
    ParticleSystem _skill2;
    Coroutine _coBossSkill1;
    Coroutine _coBossSkill2;

    // 소환 파티클
    ParticleSystem[] _sumEffect;
    public Transform[] _sumMonsterPos;
    // 소환하는 숫자 확인용
    List<MonsterControllerEX> _sumList;
    // 소환 시간 확인 변수
    float time;

    public override void Awake()
    {
        base.Awake();

        _time = 0;
        // 시작하면 소환 목표를 찾음(스킬1)
        _sumMonsterPos = new Transform[3];
        _sumMonsterPos[0] = GameManager.Obj._fieldManager._startPosSumBossMonster[0].transform;
        _sumMonsterPos[1] = GameManager.Obj._fieldManager._startPosSumBossMonster[1].transform;
        _sumMonsterPos[2] = GameManager.Obj._fieldManager._startPosSumBossMonster[2].transform;
        //tmp에 소환 이펙트를 넣고 보스가 관리
        _sumEffect = new ParticleSystem[3];
        // 소환 파티클 연결
        _sumEffect[0] = Util.FindChild("SumEffect1", transform).GetComponent<ParticleSystem>();
        _sumEffect[1] = Util.FindChild("SumEffect2", transform).GetComponent<ParticleSystem>();
        _sumEffect[2] = Util.FindChild("SumEffect3", transform).GetComponent<ParticleSystem>();

        time = 0;


        // 스킬 1 파티클 연결
        _skill1 = Util.FindChild("BossSkill1", transform).GetComponent<ParticleSystem>();
        // 파티클 off
        Skill1EffectOff();
        // 스킬 2 파티클 연결
        _skill2 = Util.FindChild("BossSkill2", transform).GetComponent<ParticleSystem>();
        // 파티클 off
        Skill2EffectOff();
        // 기본공격 파티클 연결
        _attackEffect = Util.FindChild("BossAttack", transform).GetComponent<ParticleSystem>();
        // 파티클 off
        AttackEffectOff();


        // 코루틴 초기화
        _coBossSkill1 = null;
        _coBossSkill2 = null;

        // 소환리스트
        _sumList = new List<MonsterControllerEX>();

    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _attack = 5.0f;
    }

    public override void FindMaterial()
    {
        Transform tmpTr = Util.FindChild("trex", transform);
        _material = tmpTr.GetComponent<SkinnedMeshRenderer>().material;
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
                    _ani.SetInteger("state", 2);
                    break;
                case CreatureState.Dead:
                    _ani.CrossFade("Dead", 0.3f);
                    break;
                case CreatureState.Skill:
                    _ani.SetInteger("state", 3);
                    break;
                case CreatureState.Skill2:
                    _ani.SetInteger("state", 4);
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

    // 대기 상태일때도  스스로 판단해서 공격이나 대기를 할 수 있도록...
    public override void UpdateIdle()
    {
        if (_PosToPos <= _distance)
        {
            Property_state = CreatureState.Move;
        }
        else if (_PosToPos > _distance)
        {
            Property_state = CreatureState.Idle;
        }
        else if (_PosToPos <= _attack)
        {
            Property_state = CreatureState.Attack;
        }
    }

    public override void UpdateMoving()
    {
        // 이동
        _speed = 2.0f;
        if (_PosToPos <= _distance && _PosToPos > 10f)
        {
            Vector3 targetPos = new Vector3(2.5f, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, (_PlayerPos.transform.position - targetPos), _speed * Time.deltaTime);
            Vector3 _lookRotation = _PlayerPos.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

        }
        // 이동
        else if (_PosToPos <= 10f && _PosToPos > _attack)
        {
            _speed = 2.0f;
            Vector3 targetPos = new Vector3(2.5f, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, (_PlayerPos.transform.position - targetPos), _speed * Time.deltaTime);
            Vector3 _lookRotation = _PlayerPos.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);
        }
        // 공격
        else if (_PosToPos <= _attack)
        {

            Property_state = CreatureState.Attack;

        }
        // 대기
        else if (_PosToPos > _distance)
        {
            Property_state = CreatureState.Idle;
            //return;
        }
    }

    // 공격 상태에서 스킬 사용 여부를 판단해야됨
    // 무빙 상태에서만 스킬 여부를 판단하면 대기 일때와 공격상태일때 다시 스킬을 사용할지 판단을 할 수 가 없음
    public override void UpdateAttack()
    {
        _skillPersent = Random.Range(0, 99);
        if (_skillPersent > 70)
        {
            float hpPer = (_monsterStat.Hp / _monsterStat.Max_Hp * 100);
            if (hpPer >= 70 && _sumList.Count == 0)
            {
                Property_state = CreatureState.Skill;
            }
            else
            {
                Property_state = CreatureState.Skill2;
            }
        }
        else
        {
            Property_state = CreatureState.Attack;
        }
    }

    public override void UpdateDead()
    {
        base.UpdateDead();
    }

    // 주변에 몬스터 소환 스킬
    public override void UpdateSkill()
    {
        if (_coBossSkill1 == null)
        {
            // 이펙트 온
            Skill1EffectOn();
            // 소환
            for (int i = 0; i < 3; i++)
            {
                MonsterControllerEX monster = GameManager.Create.CreateMonster(_sumMonsterPos[i].transform.position, "Velociraptor");
                monster.gameObject.name = monster.gameObject.name + "_" + i;
                _sumList.Add(monster);
            }

            _coBossSkill1 = StartCoroutine(coBossSkill1(4.5f));
        }
    }

    public override void UpdateSkill2()
    {
        if (_coBossSkill2 == null)
        {
            // 이펙트 온
            //Skill2EffectOn(); 이펙트는 애니메이션 클립에서 관리

            _coBossSkill1 = StartCoroutine(coBossSkill1(0.8f));

        }
    }

    /*    public override void OnDisable()
        {
            base.OnDisable();
        }*/

    //public override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}

    public IEnumerator coBossSkill1(float delay)
    {
        while (true)
        {
            time += Time.deltaTime;
            if (time > delay)
            {
                Property_state = CreatureState.Move;
                _coBossSkill1 = null;
                _coBossSkill2 = null;

                // 스킬 이펙트 오프
                Skill1EffectOff();
                // 시간 초기화
                time -= time;
                // 코루틴 정지
                yield break;
            }
            yield return null;
        }
    }

    public override IEnumerator AttackDelay(float _delay)
    {
        yield return StartCoroutine(base.AttackDelay(_delay));
    }

    public override void AttackEvent()
    {
        if (GameManager.Obj._playerController._creatureState != CreatureState.Dead)
        {
            // 대미지 계산
            _playerController.OnDamaged(_monsterStat.Atk);
            // 사운드 적용
            GameManager.Sound.SFXPlay("Dino-raptor");
        }
    }

    public override IEnumerator DeadDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // 오브젝트 풀링 추후 구현
        gameObject.SetActive(false);
    }

    public override IEnumerator CoolTime(float _coolTime)
    {
        yield return StartCoroutine(base.CoolTime(_coolTime));
    }

    public override void OnDamaged(int playerAtk, int SkillDamagePercent)
    {
        base.OnDamaged(playerAtk, SkillDamagePercent);
    }

    public void Skill1EffectOn()
    {
        _skill1.Play();
        _sumEffect[0].transform.SetParent(_sumMonsterPos[0].transform);
        _sumEffect[0].transform.position = _sumMonsterPos[0].transform.position;
        _sumEffect[0].Play();
        _sumEffect[1].transform.SetParent(_sumMonsterPos[1].transform);
        _sumEffect[1].transform.position = _sumMonsterPos[1].transform.position;
        _sumEffect[1].Play();
        _sumEffect[2].transform.SetParent(_sumMonsterPos[2].transform);
        _sumEffect[2].transform.position = _sumMonsterPos[2].transform.position;
        _sumEffect[2].Play();
    }

    public void Skill1EffectOff()
    {
        _skill1.Stop();
        _sumEffect[0].Stop();
        _sumEffect[1].Stop();
        _sumEffect[2].Stop();
    }

    public void Skill2EffectOn()
    {
        _skill2.Play();
    }

    public void Skill2EffectOff()
    {
        _skill2.Stop();
    }

    public void AttackEffectOn()
    {
        _attackEffect.Play();
    }

    public void AttackEffectOff()
    {
        _attackEffect.Stop();
    }
}
