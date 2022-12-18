using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// 플레이어 컨트롤러 상속받은 사이보그 컨트롤러

// 부모인 PlayerController의 MonoBehaviour(Awake, Start, Udate 등)를 사용하고 싶다면 
// -> 부모의 함수에 virtual 키워드 추가하고 자식의 함수에 override 키워드 붙여서 재정의 하면 된다.
// -> 참고로 부모 함수 호출은 base.함수명(); ((ex) base.Awake();)
// 자세한 내용) https://dragontory.tistory.com/307

public class CyborgController : PlayerController
{
    //폭탄이 레이캐스트 도착지점 계산값
    public float bombDistance;

    // 사이보그 논타겟 필드
    protected GameObject _skillGround;

    public override void JobStart()
    {
        //사이보그 Skill1 파티클 연결
        _skill1FlamethrowerEffect = FindEffect("Skill1FlamethrowerEffect", transform);
        // 사이보그 Skill1 공격범위용 콜라이더
        _skill2BoxCollider = _skill1FlamethrowerEffect.GetComponent<BoxCollider>();
        //사이보그 논타겟 스킬 범위
        _skillGround = Util.FindChild("SkillGround", transform).gameObject;
        //사이보그 Skill2 파티클 연결
        _cyborgSkill2 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/Skill2Effect"));
        _skill2FireExplosionEffect = FindEffect("FireExplosion", _cyborgSkill2.transform);
        _skill2FireBigEffect = FindEffect("FireBig", _cyborgSkill2.transform);
        //폭탄 위치
        _bomb = Util.FindChild("bomb", transform);
        _bombPosition = _bomb.localPosition;
        //사이보그 Skill3 파티클 연결
        _cyborgSkill3 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/Skill3Effect"));
        _skill3Explosion1Effect = FindEffect("Explosion1", _cyborgSkill3.transform);
        _skill3Explosion2Effect = FindEffect("Explosion2", _cyborgSkill3.transform);
        _skill3Explosion3Effect = FindEffect("Explosion3", _cyborgSkill3.transform);
        _skill3Explosion4Effect = FindEffect("Explosion4", _cyborgSkill3.transform);
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
        Vector3 _skill1CyborgDir;
        if (_coSkill1 == null)
        {
            _skill1CyborgDir = _tempDir.normalized;
            _tempDir = _skill1CyborgDir;
            //GameManager.Ui._uiSceneAttackButton.Skill1Button(true);
            _coSkill1 = StartCoroutine(CyborgCoSkill1());
        }
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
            _anim.SetInteger("playerStat", 5);
        }
    }

    protected IEnumerator CyborgCoSkill1()
    {
        // 콜라이더를 공용으로 사용 겟컴퍼는트를 줄이려면 미리 선언하고 각각 사용하는 방식이 있음
        _skill2BoxCollider = null;
        _skill2BoxCollider = _skill1FlamethrowerEffect.GetComponent<BoxCollider>();
        Skill1EffectOn();
        GameManager.Sound.SFXPlay("CyborgSkill2");
        yield return new WaitForSeconds(4f);
        Skill1EffectOff();
        _coSkill1 = null;
        _creatureState = CreatureState.Idle;
    }
    public void Skill1EffectOn()
    {
        _skill1FlamethrowerEffect.Play();
    }
    public void Skill1EffectOff()
    {
        _skill1FlamethrowerEffect.Stop();
    }

    public void Skill1Event2()
    {
        effectChange = 0;
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
                targetList[i].OnDamaged(_playerStat.Atk, 5);
            }
        }
    }

    public override void Skill2()
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
                // 로테이션 x값 튀는 것 보정
                //transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
                _anim.SetInteger("playerStat", 6);
                if (_bomb.gameObject.activeSelf == true)
                {
                    _bomb.position = Vector3.Slerp(_bomb.position, checkPoint, 0.05f);
                    bombDistance = Vector3.Distance(_bomb.position, checkPoint);
                }
                else
                {
                    return;
                }
                if (bombDistance < 0.5f)
                {
                    StartCoroutine(CyborgCoSkill2());
                    //_sceneAttackButton = SceneAttackButton.None;
                    _creatureState = CreatureState.Idle;
                }
            }
            else
            {
                _skillGround.SetActive(false);
                checkPoint = new Vector3(0, 0, 0);
                //_sceneAttackButton = SceneAttackButton.None;
                _creatureState = CreatureState.Idle;
            }
        }
    }

    IEnumerator CyborgCoSkill2()
    {
        _bomb.gameObject.SetActive(false);
        _cyborgSkill2.transform.position = checkPoint;
        checkPoint = new Vector3(0, 0, 0);
        _cyborgSkill2.gameObject.SetActive(true);
        _skill2FireBigEffect.Stop();
        _skill2FireExplosionEffect.Play();
        yield return new WaitForSeconds(0.5f);
        _skill2FireExplosionEffect.Stop();
        _skill2FireBigEffect.Play();
        yield return new WaitForSeconds(2f);
        _skill2FireBigEffect.Stop();
        _cyborgSkill2.gameObject.SetActive(false);
        _bomb.localPosition = _bombPosition;
    }

    //수류탄 던지는 애니메이터 이벤트
    //버그가 있어서 대미지 계산이 안됨
    //_bomb.gameObject에 콜라이더와 리지드 바디를 넣고 충돌 계산 처리
    public void CybogSkill2ThrowBombEvent()
    {
/*        _bomb.gameObject.SetActive(true);
        _skill2BoxCollider = null;
        _skill2BoxCollider = _cyborgSkill2.GetComponent<BoxCollider>();
        Skill1Event2();*/
    }

    public override void Skill3()
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
                _anim.SetInteger("playerStat", 7);
                StartCoroutine(CyborgCoSkill3());
                _sceneAttackButton = SceneAttackButton.None;
                _creatureState = CreatureState.Idle;
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

    IEnumerator CyborgCoSkill3()
    {
        yield return new WaitForSeconds(2f);
        _cyborgSkill3.gameObject.SetActive(true);
        _cyborgSkill3.transform.position = checkPoint;
        checkPoint = new Vector3(0, 0, 0);
        _skill3Explosion1Effect.Play();
        _skill3Explosion2Effect.Play();
        _skill3Explosion3Effect.Play();
        _skill3Explosion4Effect.Play();
        yield return new WaitForSeconds(1f);

        _cyborgSkill3.gameObject.SetActive(false);
    }

    public override void AttackEvent()
    {
        // 대미지 계산
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // 사운드 추가
        GameManager.Sound.SFXPlay("Gun1");
    }
}
