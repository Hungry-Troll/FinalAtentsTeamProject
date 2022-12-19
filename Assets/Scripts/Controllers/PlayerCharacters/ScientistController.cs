using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// 플레이어 컨트롤러 상속받은 사이언티스트 컨트롤러

// 부모인 PlayerController의 MonoBehaviour(Awake, Start, Udate 등)를 사용하고 싶다면 
// -> 부모의 함수에 virtual 키워드 추가하고 자식의 함수에 override 키워드 붙여서 재정의 하면 된다.
// -> 참고로 부모 함수 호출은 base.함수명(); ((ex) base.Awake();)
// 자세한 내용) https://dragontory.tistory.com/307

public class ScientistController : PlayerController
{
    // 사이보그 논타겟 필드
    protected GameObject _skillGround;
    ParticleSystem _attackEffect
        ;
    public void Awake()
    {
        //기본공격 효과
        GameObject tmp = Resources.Load<GameObject>("Prefabs/Particle_Prefab/Attack");
        GameObject _baseAttack = Util.Instantiate(tmp);
        _attackEffect = Util.FindChild("Attack", _baseAttack.transform).GetComponent<ParticleSystem>();
        AttackEffectOff();
    }

    public override void JobStart()
    {
        //논타겟 스킬 범위 변수
        _skillGround = Util.FindChild("SkillGround", transform).gameObject;
        _scientistSkill1 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/ScientistSkill1"));
        _poison = FindEffect("Poison", _scientistSkill1.transform);
        _explosion = FindEffect("Explosion", _scientistSkill1.transform);
        _scientistSkill2 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/ScientistSkill2"));
        _powerDraw = FindEffect("PowerDraw", _scientistSkill2.transform);
        _electricity = FindEffect("Electricity", _scientistSkill2.transform);
        _scientistSkill3 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/ScientistSkill3"));
        _powerBeam = FindEffect("PowerBeam", _scientistSkill3.transform);
        _poisonPortion = Util.FindChild("potion", transform);
        _portionPosition = _poisonPortion.localPosition;
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
                _anim.SetInteger("playerStat", 5);
                break;
            case CreatureState.Skill2:
                // 누를때 애니메이션 재생하는것으로
                //_anim.SetInteger("playerStat", 6);
                break;
            case CreatureState.Skill3:
                break;
            case CreatureState.None:
                break;
        }
    }

    public override void Skill1()
    {

        _skillGround.SetActive(true);
        if (checkPoint == new Vector3(0, 0, 0))
        {
            checkPoint = RaycastInfo();
        }
        else
        {
            float distance = Vector3.Distance(checkPoint, transform.position);
            if (distance < 5f)
            {
                _skillGround.SetActive(false);
                Vector3 tempDir = checkPoint - transform.position;
                transform.rotation = Quaternion.LookRotation(tempDir.normalized);
                _anim.SetInteger("playerStat", 5);
                if (_poisonPortion.gameObject.activeSelf == true)
                {
                    _poisonPortion.position = Vector3.Slerp(_poisonPortion.position, checkPoint, 0.05f);
                    _portionDistance = Vector3.Distance(_poisonPortion.position, checkPoint);
                }
                else
                {
                    return;
                }
                if (_portionDistance < 0.5f)
                {
                    _isSkill1 = true;
                    StartCoroutine(ScientistCoSkill1());
                    _creatureState = CreatureState.Idle;
                }
            }
            else
            {
                _skillGround.SetActive(false);
                checkPoint = new Vector3(0, 0, 0);
                _creatureState = CreatureState.Idle;
            }
        }

    }

    IEnumerator ScientistCoSkill1()
    {
        _poisonPortion.gameObject.SetActive(false);
        _scientistSkill1.transform.position = checkPoint;
        checkPoint = new Vector3(0, 0, 0);
        _scientistSkill1.gameObject.SetActive(true);
        _poison.Play();
        _explosion.Stop();
        for (int i = 0; i < 12; i++)
        {
            if (Vector3.Distance(_scientistSkill1.transform.position,
                                 _scientistSkill2.transform.position) < 5f)
            {
                _explosion.Play();
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
        _poison.Stop();
        _scientistSkill1.gameObject.SetActive(false);
        _poisonPortion.localPosition = _portionPosition;
        _isSkill1 = false;
    }
    // 대미지 계산은 트리거 충돌 처리로 함
    public void ScientistSkill1ThrowPortionEvent()
    {
        _poisonPortion.gameObject.SetActive(true);
    }

    public override void Skill2()
    {
        _skillGround.SetActive(true);
        if (checkPoint == new Vector3(0, 0, 0))
        {
            Debug.Log(checkPoint);
            checkPoint = RaycastInfo();
        }
        else
        {
            float distance = Vector3.Distance(checkPoint, transform.position);
            if (distance < 5f)
            {
                _skillGround.SetActive(false);
                Vector3 tempDir = checkPoint - transform.position;
                transform.rotation = Quaternion.LookRotation(tempDir.normalized);
                _anim.SetInteger("playerStat", 6);
                if (_scientistSkill2.gameObject.activeSelf == true)
                {
                    _isSkill2 = true;
                    _scientistSkill2Point = transform.position + (tempDir.normalized * 10);
                    Vector3 tmp = _scientistSkill2Point;
                    tmp.y += 1f;
                    _scientistSkill2Point = tmp;
                    StartCoroutine(ScientistCoSkill2());
                    _sceneAttackButton = SceneAttackButton.None;
                    _creatureState = CreatureState.Idle;
                }
            }
            else
            {
                _skillGround.SetActive(false);
                checkPoint = new Vector3(0, 0, 0);
                _sceneAttackButton = SceneAttackButton.None;
                _creatureState = CreatureState.Idle;
            }
        }
    }

    IEnumerator ScientistCoSkill2()
    {
        _scientistSkill2.transform.position = transform.position;
        Vector3 tmp = _scientistSkill2.transform.position;
        tmp.y += 1f;
        _scientistSkill2.transform.position = tmp;
        _powerDraw.Play();
        _electricity.Play();
        while (true)
        {
            _scientistSkill2.transform.position = Vector3.MoveTowards(_scientistSkill2.transform.position, _scientistSkill2Point, Time.deltaTime * 4f);
            if (Vector3.Distance(_scientistSkill2.transform.position, _scientistSkill2Point) < 0.5f)
            {
                break;
            }
            yield return null;
        }
        _scientistSkill2Point = new Vector3(0, 0, 0);
        _powerDraw.Stop();
        _electricity.Stop();
        _scientistSkill2.gameObject.SetActive(false);
        checkPoint = new Vector3(0, 0, 0);
        _isSkill2 = false;
    }

    public void ScientistSkill2Event()
    {
        _scientistSkill2.gameObject.SetActive(true);
    }

    public void AttackEffectOn()
    {
        _attackEffect.Play();
        if(GameManager.Obj._targetMonsterController != null)
        {
            _attackEffect.transform.position = GameManager.Obj._targetMonsterController.transform.localPosition;
            // 높이 보정
            Vector3 tmp = _attackEffect. transform.position;
            tmp.y = _attackEffect.transform.position.y + 1.0f;
            _attackEffect.transform.position = tmp;
        }
    }

    public void AttackEffectOff()
    {
        _attackEffect.Stop();
        //_attackEffect.transform.parent = GameManager.Obj._playerController.transform;
    }

    // 공격 딜레이 계산 (애니메이션 딜레이만)
    protected override IEnumerator CoAttackDelay(float _delay)
    {
        yield return base.CoAttackDelay(_delay);
    }

    // 공격 대미지 계산 + 사운드는 애니메이션 클립 Attack에서 이벤트로 처리
    public override void AttackEvent()
    {
        // 대미지 계산
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // 사운드 추가
        GameManager.Sound.SFXPlay("ScientistAttack");
        AttackEffectOn();
    }
}
