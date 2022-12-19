using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// �÷��̾� ��Ʈ�ѷ� ��ӹ��� ���̺��� ��Ʈ�ѷ�

// �θ��� PlayerController�� MonoBehaviour(Awake, Start, Udate ��)�� ����ϰ� �ʹٸ� 
// -> �θ��� �Լ��� virtual Ű���� �߰��ϰ� �ڽ��� �Լ��� override Ű���� �ٿ��� ������ �ϸ� �ȴ�.
// -> ����� �θ� �Լ� ȣ���� base.�Լ���(); ((ex) base.Awake();)
// �ڼ��� ����) https://dragontory.tistory.com/307

public class CyborgController : PlayerController
{
    //��ź�� ����ĳ��Ʈ �������� ��갪
    public float bombDistance;

    // ���̺��� ��Ÿ�� �ʵ�
    protected GameObject _skillGround;

    public override void JobStart()
    {
        //���̺��� Skill1 ��ƼŬ ����
        _skill1FlamethrowerEffect = FindEffect("Skill1FlamethrowerEffect", transform);
        // ���̺��� Skill1 ���ݹ����� �ݶ��̴�
        _skill2BoxCollider = _skill1FlamethrowerEffect.GetComponent<BoxCollider>();
        //���̺��� ��Ÿ�� ��ų ����
        _skillGround = Util.FindChild("SkillGround", transform).gameObject;
        //���̺��� Skill2 ��ƼŬ ����
        _cyborgSkill2 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/Skill2Effect"));
        _skill2FireExplosionEffect = FindEffect("FireExplosion", _cyborgSkill2.transform);
        _skill2FireBigEffect = FindEffect("FireBig", _cyborgSkill2.transform);
        //��ź ��ġ
        _bomb = Util.FindChild("bomb", transform);
        _bombPosition = _bomb.localPosition;
        _bomb.gameObject.SetActive(false);
        //���̺��� Skill3 ��ƼŬ ����
        _cyborgSkill3 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/Skill3Effect"));
        _skill3Explosion1Effect = FindEffect("Explosion1", _cyborgSkill3.transform);
        _skill3Explosion2Effect = FindEffect("Explosion2", _cyborgSkill3.transform);
        _skill3Explosion3Effect = FindEffect("Explosion3", _cyborgSkill3.transform);
        _skill3Explosion4Effect = FindEffect("Explosion4", _cyborgSkill3.transform);
        _coSkill1 = null;
        _coSkill2 = null;
        _coSkill3 = null;
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
        // �ݶ��̴��� �������� ��� �����۴�Ʈ�� ���̷��� �̸� �����ϰ� ���� ����ϴ� ����� ����
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
        // ���� ����� ã��
        List<MonsterControllerEX> targetList = GameManager.Obj.FindMobListTargets();

        // ���� ��ų�� ���� ����� ������ ����
        if (targetList == null)
        {
            return;
        }
        else if (targetList.Count < 0)
        {
            return;
        }
        // ����� ������
        else
        {
            // ����� ���
            for (int i = 0; i < targetList.Count; i++)
            {
                targetList[i].OnDamaged(_playerStat.Atk, 5);
            }
        }
    }

    public override void Skill2()
    {
        if (_coSkill2 != null)
            return;

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
                _anim.SetInteger("playerStat", 6);
                _bomb.gameObject.SetActive(true);
                _bomb.position = Vector3.Slerp(_bomb.position, checkPoint, 0.1f);
                bombDistance = Vector3.Distance(_bomb.position, checkPoint);
                /*                if (_bomb.gameObject.activeSelf == true)
                                {
                                    _bomb.position = Vector3.Slerp(_bomb.position, checkPoint, 0.1f);
                                    bombDistance = Vector3.Distance(_bomb.position, checkPoint);
                                }
                                else
                                {
                                    return;
                                }*/
                if (bombDistance < 0.5f)
                {
                    _coSkill2 = StartCoroutine(CyborgCoSkill2());
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
        // ���� �߰�
        GameManager.Sound.SFXPlay("CyborgSkill1");
        yield return new WaitForSeconds(0.5f);
        _skill2FireExplosionEffect.Stop();
        _skill2FireBigEffect.Play();
        yield return new WaitForSeconds(2f);
        _skill2FireBigEffect.Stop();
        _cyborgSkill2.gameObject.SetActive(false);
        _bomb.localPosition = _bombPosition;
        _bomb.gameObject.SetActive(false);
        _coSkill2 = null;
        _creatureState = CreatureState.Idle;
    }

    //����ź ������ �ִϸ����� �̺�Ʈ
    //���װ� �־ ����� ����� �ȵ�
    //_bomb.gameObject�� �ݶ��̴��� ������ �ٵ� �ְ� �浹 ��� ó��
    public void CybogSkill2ThrowBombEvent()
    {
        //_bomb.gameObject.SetActive(true);
/*        _skill2BoxCollider = null;
        _skill2BoxCollider = _cyborgSkill2.GetComponent<BoxCollider>();
        Skill1Event2();*/
    }

    public override void Skill3()
    {
        if (_coSkill3 != null)
            return;
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
                _coSkill3 = StartCoroutine(CyborgCoSkill3());
            }
            else
            {
                _skillGround.SetActive(false);
                checkPoint = new Vector3(0, 0, 0);
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
        GameManager.Sound.SFXPlay("CyborgSkill3_1");
        _skill3Explosion2Effect.Play();
        GameManager.Sound.SFXPlay("CyborgSkill3_2");
        _skill3Explosion3Effect.Play();
        GameManager.Sound.SFXPlay("CyborgSkill3_3");
        _skill3Explosion4Effect.Play();
        GameManager.Sound.SFXPlay("CyborgSkill3_4");
        _skill2BoxCollider = _skill3Explosion4Effect.GetComponent<BoxCollider>();        skill3Event();
        yield return new WaitForSeconds(1f);
        _creatureState = CreatureState.Idle;
        _cyborgSkill3.gameObject.SetActive(false);
        _skillGround.SetActive(false);
        _coSkill3 = null;
    }

    public void skill3Event()
    {
        effectChange = 0;
        // ���� ����� ã��
        List<MonsterControllerEX> targetList = GameManager.Obj.FindMobListTargets();

        // ���� ��ų�� ���� ����� ������ ����
        if (targetList == null)
        {
            return;
        }
        else if (targetList.Count < 0)
        {
            return;
        }
        // ����� ������
        else
        {
            // ����� ���
            for (int i = 0; i < targetList.Count; i++)
            {
                targetList[i].OnDamaged(_playerStat.Atk, 1);
            }
        }
    }

    public override void AttackEvent()
    {
        // ����� ���
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // ���� �߰�
        GameManager.Sound.SFXPlay("Gun1");
    }
}
