using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// �÷��̾� ��Ʈ�ѷ� ��ӹ��� ���̾�Ƽ��Ʈ ��Ʈ�ѷ�

// �θ��� PlayerController�� MonoBehaviour(Awake, Start, Udate ��)�� ����ϰ� �ʹٸ� 
// -> �θ��� �Լ��� virtual Ű���� �߰��ϰ� �ڽ��� �Լ��� override Ű���� �ٿ��� ������ �ϸ� �ȴ�.
// -> ����� �θ� �Լ� ȣ���� base.�Լ���(); ((ex) base.Awake();)
// �ڼ��� ����) https://dragontory.tistory.com/307

public class ScientistController : PlayerController
{
    // ���̺��� ��Ÿ�� �ʵ�
    protected GameObject _skillGround;
    ParticleSystem _attackEffect
        ;
    public void Awake()
    {
        //�⺻���� ȿ��
        GameObject tmp = Resources.Load<GameObject>("Prefabs/Particle_Prefab/Attack");
        GameObject _baseAttack = Util.Instantiate(tmp);
        _attackEffect = Util.FindChild("Attack", _baseAttack.transform).GetComponent<ParticleSystem>();
        AttackEffectOff();
    }

    public override void JobStart()
    {
        //��Ÿ�� ��ų ���� ����
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
                //_anim.SetInteger("playerStat", 5);
                break;
            case CreatureState.Skill2:
                // ������ �ִϸ��̼� ����ϴ°�����
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
                    _poisonPortion.position = Vector3.Slerp(_poisonPortion.position, checkPoint, 0.1f);
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
        Vector3 tmp = _scientistSkill1.transform.position;
        tmp.y += 1f;
        _scientistSkill1.transform.position = tmp;
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
                _explosion.gameObject.SetActive(true);
                yield return new WaitForSeconds(1.0f);
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
        _poison.Stop();
        _explosion.gameObject.SetActive(false);
        _scientistSkill1.gameObject.SetActive(false);
        _poisonPortion.localPosition = _portionPosition;
        _isSkill1 = false;
    }
    // ����� ����� Ʈ���� �浹 ó���� ��
    public void ScientistSkill1ThrowPortionEvent()
    {
        GameManager.Sound.SFXPlay("ScientistSkill1");
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
        GameManager.Sound.SFXPlay("ScientistSkill2");
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

    public override void Skill3()
    {
        if (_coSkill3 != null)
            return;

        if (GameManager.Obj._targetMonster == null)
        {
            _sceneAttackButton = SceneAttackButton.None;
            _creatureState = CreatureState.Idle;
            return;
        }
        float distance = Vector3.Distance(GameManager.Obj._targetMonster.transform.position, transform.position);
        if (distance < 5f)
        {
            // ȸ��
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            _anim.SetInteger("playerStat", 7);
            if (_scientistSkill3.gameObject.activeSelf == true)
            {
                _coSkill3 = StartCoroutine(ScientistCoSkil3());
                _creatureState = CreatureState.Idle;
            }

        }
        else
        {
            _creatureState = CreatureState.Idle;
        }
    }

    IEnumerator ScientistCoSkil3()
    {
        _powerBeam.Play();
        float skill3Check = 0;
        float skill3DamageCheck = 0;
        while (true)
        {
            _scientistSkill3.transform.position = GameManager.Obj._targetMonster.transform.position;
            Vector3 tmp = _scientistSkill3.transform.position;
            tmp.y += 10f;
            _scientistSkill3.transform.position = tmp;
            yield return null;
            skill3Check += Time.deltaTime;
            skill3DamageCheck += Time.deltaTime;
            if (GameManager.Obj._targetMonster == null)
            {
                break;
            }
            // �� 9�� ������� �ִ� ���
            if (skill3DamageCheck > 0.1f)
            {
                Skill3Event();
                skill3DamageCheck -= skill3DamageCheck;
            }
            if (skill3Check > 10f)
            {
                skill3Check = 0f;
                GameManager.Sound.SFXPlayOff();
                break;
            }
        }
        _powerBeam.Stop();
        _scientistSkill3.gameObject.SetActive(false);
        _coSkill3 = null;
    }
    // ����� ��� �Լ�
    public void Skill3Event()
    { 
        // ����� ���
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 10);
        // ���� �߰�
        GameManager.Sound.SFXPlay("ScientistSkill3");
    }

    public void ScientistSkill3Event()
    {
        _scientistSkill3.gameObject.SetActive(true);
    }

    public void AttackEffectOn()
    {
        _attackEffect.Play();
        if(GameManager.Obj._targetMonsterController != null)
        {
            _attackEffect.transform.position = GameManager.Obj._targetMonsterController.transform.localPosition;
            // ���� ����
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

    // ���� ������ ��� (�ִϸ��̼� �����̸�)
    protected override IEnumerator CoAttackDelay(float _delay)
    {
        yield return base.CoAttackDelay(_delay);
    }

    // ���� ����� ��� + ����� �ִϸ��̼� Ŭ�� Attack���� �̺�Ʈ�� ó��
    public override void AttackEvent()
    {
        // ����� ���
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // ���� �߰�
        GameManager.Sound.SFXPlay("ScientistAttack");
        AttackEffectOn();
    }
}
