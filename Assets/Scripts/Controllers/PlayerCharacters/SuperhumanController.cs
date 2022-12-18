using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// 플레이어 컨트롤러 상속받은 강화인간 컨트롤러

// 부모인 PlayerController의 MonoBehaviour(Awake, Start, Udate 등)를 사용하고 싶다면 
// -> 부모의 함수에 virtual 키워드 추가하고 자식의 함수에 override 키워드 붙여서 재정의 하면 된다.
// -> 참고로 부모 함수 호출은 base.함수명(); ((ex) base.Awake();)
// 자세한 내용) https://dragontory.tistory.com/307


public class SuperhumanController : PlayerController
{
    // 공격 이펙트용
    protected TrailRenderer _swordEffect;

    public override void JobStart()
    {
        //SuperHuman Skill1 파티클 연결
        _skill1SlashEffect2_1 = FindEffect("Skill1SlashEffect2_1", transform);
        _skill1SlashEffect2_2 = FindEffect("Skill1SlashEffect2_2", transform);
        _skill1SlashEffect2_3 = FindEffect("Skill1SlashEffect2_3", transform);
        _skill1SlashEffect2_4 = FindEffect("Skill1SlashEffect2_4", transform);
        _skill1SlashEffect2_5 = FindEffect("Skill1SlashEffect2_5", transform);
        // Skill2 파티클 연결
        Transform skill2Tr = Util.FindChild("Skill2WheelWindEffect", transform);
        _skill2WheelWindEffect = skill2Tr.GetComponent<ParticleSystem>();
        // Skill2 파티클 Stop;
        Skill2WheelWindOff();
        // Skill2 공격범위용 콜라이더
        _skill2BoxCollider = skill2Tr.GetComponent<BoxCollider>();
        //SuperHuman Skill3 파티클 연결
        _skill3GroundEffect = FindEffect("Skill3GroundEffect", transform);
        _skill3BoosterEffect = FindEffect("Skill3BoosterEffect", transform);
        // 공격이펙트 연결
        Transform tmp = Util.FindChild("SwordEffect", transform);
        _swordEffect = tmp.GetComponent<TrailRenderer>();
    }

    protected override void UpdateAnimation()
    {
        switch (_creatureState)
        {
            case CreatureState.Idle:
                _anim.SetInteger("playerStat", 0);
                break;
            case CreatureState.Move:
                _anim.SetInteger("playerStat", 1);
                break;
            case CreatureState.AutoMove:
                _anim.SetInteger("playerStat", 1);
                break;
            case CreatureState.Attack:
                _anim.SetInteger("playerStat", 2);
                break;
            case CreatureState.Dead:
                _anim.SetInteger("playerStat", 3);
                break;
            case CreatureState.Rolling:
                _anim.SetInteger("playerStat", 9);
                break;
            case CreatureState.Skill:
                break;
            case CreatureState.Skill2:
                _anim.SetInteger("playerStat", 6);
                break;
            case CreatureState.Skill3:
                break;
            case CreatureState.None:
                break;
        }
    }

    public override void Skill1()
    {
        if (GameManager.Obj._targetMonster == null)
        {
            GameManager.Ui._uiSceneAttackButton.Skill1Button(false);
            _creatureState = CreatureState.Idle;
            return;
        }
        float distance = Vector3.Distance(GameManager.Obj._targetMonster.transform.position, transform.position);
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse && distance < 2f)
        {
            // 회전
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // 코루틴을 이용한 공격딜레이 (대미지 계산)
            if (_coSkill1 == null)
            {
                //GameManager.Ui._uiSceneAttackButton.Skill1Button(true);
                _coSkill1 = StartCoroutine(CoSkill1());
            }
        }
    }

    protected IEnumerator CoSkill1()
    {
        _swordEffect.enabled = true;
        _anim.SetInteger("playerStat", 5);
        yield return new WaitForSeconds(0.2f);
        _anim.SetInteger("playerStat", 51);
        yield return new WaitForSeconds(0.2f);
        _anim.SetInteger("playerStat", 52);
        yield return new WaitForSeconds(0.7f);
        _anim.SetInteger("playerStat", 0);
        _swordEffect.enabled = false;
        _coSkill1 = null;
        _creatureState = CreatureState.Attack;
    }

    public void Skill1Event1()
    {
        switch (effectChange)
        {
            case 0:
                _skill1SlashEffect2_1.Play();
                break;
            case 1:
                _skill1SlashEffect2_2.Play();
                break;
            case 2:
                _skill1SlashEffect2_3.Play();
                break;
            case 3:
                _skill1SlashEffect2_4.Play();
                break;
        }
        effectChange++;
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 2);
    }

    public override void Skill2()
    {
        if (playerStat.Job == Job.Superhuman.ToString())
        {
            float skillDelay = 1.0f;
            float skillSpeed = 10.0f;
            //보고 있는 방향을 저장한다.
            Vector3 skillDir;
            skillDir = _tempDir.normalized;
            _tempVector = skillDir;
            _tempVector = _tempVector * Time.deltaTime * skillSpeed;
            transform.position += _tempVector;

            if (_coSkill2 == null)
            {
                _coSkill2 = StartCoroutine(CoSkill2(skillDelay));
            }
        }
    }
    protected IEnumerator CoSkill2(float _delay)
    {
        // 딜레이
        yield return new WaitForSeconds(_delay);

        _coSkill2 = null;
        _creatureState = CreatureState.Idle;
    }
    // Skill2 애니메이션 클립에서 관리
    // 스킬 이펙트 온
    public void Skill2WheelWindOn()
    {
        _skill2WheelWindEffect.Play();
        GameManager.Sound.SFXPlay("Skill2");
    }
    // Skill2 애니메이션 클립에서 관리
    // 스킬 이펙트 오프
    public void Skill2WheelWindOff()
    {
        _skill2WheelWindEffect.Stop();
        // 오브젝트 매니저에있는 스킬공격대상 리스트를 초기화함 >> 다음 스킬에 다시 계산 후 적용
        GameManager.Obj._targetMonstersControllerList = null;
    }

    // Skill2 애니메이션 클립에서 관리
    // 대미지 계산
    public void Skill2Event()
    {
        // 공격 대상을 찾음
        List<MonsterControllerEX> targetList = GameManager.Obj.FindMobListTargets();

        // 광역 스킬에 맞을 대상이 없으면 리턴
        if (targetList == null)
        {
            return;
        }
        else if (targetList.Count < 0)
        {
            return;
        }
        // 대상이 있으면
        else
        {
            // 대미지 계산
            for (int i = 0; i < targetList.Count; i++)
            {
                targetList[i].OnDamaged(_playerStat.Atk, 1);
            }
        }
    }

    public override void Skill3()
    {
        //if (_isSkill1 == false && _isSkill3 == false)
        //{
        if (_skill3Stat.Skill3Level != skill3Level)
        {
            Skill3DataLoad();
        }
        StartCoroutine(CoSkill3());
        //_sceneAttackButton = SceneAttackButton.None;
        _creatureState = CreatureState.Idle;
        //}
    }

    protected IEnumerator CoSkill3()
    {
        _anim.SetInteger("playerStat", 7);
        _skill3PlayerScale.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        _playerStat.Max_Hp += _skill3Stat.Skill3StatMaxHp;
        _playerStat.Hp += _skill3Stat.Skill3StatHp;
        _playerStat.Atk += _skill3Stat.Skill3StatAtk;
        _playerStat.Def += _skill3Stat.Skill3StatDef;
        _skill3GroundEffect.Play();
        _skill3BoosterEffect.Play();
        _isSkill3 = true;
        GameManager.Ui._uiSceneAttackButton.Skill3Button(1);
        yield return new WaitForSeconds(_skill3Stat.Duration);
        _playerStat.Max_Hp -= _skill3Stat.Skill3StatMaxHp;
        if (_playerStat.Max_Hp < _playerStat.Hp)
        {
            _playerStat.Hp = _playerStat.Max_Hp;
        }
        _playerStat.Atk -= _skill3Stat.Skill3StatAtk;
        _playerStat.Def -= _skill3Stat.Skill3StatDef;
        _skill3BoosterEffect.Stop();
        _skill3PlayerScale.transform.localScale = new Vector3(1f, 1f, 1f);
        _isSkill3 = false;
        GameManager.Ui._uiSceneAttackButton.Skill3Button(2);
    }

    protected override void Attack()
    {
        // 타겟 널 판정
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            // 공격상태인대 타겟이 없을 경우 소드 이펙트를 제거
            SwordEffectOff();
            return;
        }
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            // 회전
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // 코루틴을 이용한 공격딜레이 (대미지 계산)
            if (_coAttack == null)
            {
                _coAttack = StartCoroutine(CoAttackDelay(_attackDelay));
            }
        }
        // 공격 중 이동
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    protected override IEnumerator CoAttackDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // 대미지 계산
        // GameManager.Ui._targetMonsterStat.Hp -= _playerStat.Atk - GameManager.Ui._targetMonsterStat.Def;
        // 대미지 계산은 몬스터스크립트에서 처리 >> 플레이어 공격력만 넘겨줌
        // 널체크
        if (GameManager.Obj._targetMonster == null)
        {
            // 공격상태인대 타겟이 없을 경우 소드 이펙트를 제거
            SwordEffectOff();
            yield break;
        }
        // 코루틴 초기화
        _coAttack = null;
    }

    public override void AttackEvent()
    {
        // 대미지 계산
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // 사운드 추가
        GameManager.Sound.SFXPlay("Punch1");
    }

    // 기본공격 이펙트 애니메이션 클립 Attack에서 이벤트로 처리
    public void SwordEffectOn()
    {
        _swordEffect.enabled = true;
    }
    public void SwordEffectOff()
    {
        _swordEffect.enabled = false;
    }

}
