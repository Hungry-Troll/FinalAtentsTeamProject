using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// �÷��̾� ��Ʈ�ѷ� ��ӹ��� ��ȭ�ΰ� ��Ʈ�ѷ�

// �θ��� PlayerController�� MonoBehaviour(Awake, Start, Udate ��)�� ����ϰ� �ʹٸ� 
// -> �θ��� �Լ��� virtual Ű���� �߰��ϰ� �ڽ��� �Լ��� override Ű���� �ٿ��� ������ �ϸ� �ȴ�.
// -> ����� �θ� �Լ� ȣ���� base.�Լ���(); ((ex) base.Awake();)
// �ڼ��� ����) https://dragontory.tistory.com/307


public class SuperhumanController : PlayerController
{
    // ���� ����Ʈ��
    protected TrailRenderer _swordEffect;

    public override void JobStart()
    {
        //SuperHuman Skill1 ��ƼŬ ����
        _skill1SlashEffect2_1 = FindEffect("Skill1SlashEffect2_1", transform);
        _skill1SlashEffect2_2 = FindEffect("Skill1SlashEffect2_2", transform);
        _skill1SlashEffect2_3 = FindEffect("Skill1SlashEffect2_3", transform);
        _skill1SlashEffect2_4 = FindEffect("Skill1SlashEffect2_4", transform);
        _skill1SlashEffect2_5 = FindEffect("Skill1SlashEffect2_5", transform);
        // Skill2 ��ƼŬ ����
        Transform skill2Tr = Util.FindChild("Skill2WheelWindEffect", transform);
        _skill2WheelWindEffect = skill2Tr.GetComponent<ParticleSystem>();
        // Skill2 ��ƼŬ Stop;
        Skill2WheelWindOff();
        // Skill2 ���ݹ����� �ݶ��̴�
        _skill2BoxCollider = skill2Tr.GetComponent<BoxCollider>();
        //SuperHuman Skill3 ��ƼŬ ����
        _skill3GroundEffect = FindEffect("Skill3GroundEffect", transform);
        _skill3BoosterEffect = FindEffect("Skill3BoosterEffect", transform);
        // ��������Ʈ ����
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
            // ȸ��
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // �ڷ�ƾ�� �̿��� ���ݵ����� (����� ���)
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
            //���� �ִ� ������ �����Ѵ�.
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
        // ������
        yield return new WaitForSeconds(_delay);

        _coSkill2 = null;
        _creatureState = CreatureState.Idle;
    }
    // Skill2 �ִϸ��̼� Ŭ������ ����
    // ��ų ����Ʈ ��
    public void Skill2WheelWindOn()
    {
        _skill2WheelWindEffect.Play();
        GameManager.Sound.SFXPlay("Skill2");
    }
    // Skill2 �ִϸ��̼� Ŭ������ ����
    // ��ų ����Ʈ ����
    public void Skill2WheelWindOff()
    {
        _skill2WheelWindEffect.Stop();
        // ������Ʈ �Ŵ������ִ� ��ų���ݴ�� ����Ʈ�� �ʱ�ȭ�� >> ���� ��ų�� �ٽ� ��� �� ����
        GameManager.Obj._targetMonstersControllerList = null;
    }

    // Skill2 �ִϸ��̼� Ŭ������ ����
    // ����� ���
    public void Skill2Event()
    {
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
        // Ÿ�� �� ����
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            // ���ݻ����δ� Ÿ���� ���� ��� �ҵ� ����Ʈ�� ����
            SwordEffectOff();
            return;
        }
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            // ȸ��
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // �ڷ�ƾ�� �̿��� ���ݵ����� (����� ���)
            if (_coAttack == null)
            {
                _coAttack = StartCoroutine(CoAttackDelay(_attackDelay));
            }
        }
        // ���� �� �̵�
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    protected override IEnumerator CoAttackDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // ����� ���
        // GameManager.Ui._targetMonsterStat.Hp -= _playerStat.Atk - GameManager.Ui._targetMonsterStat.Def;
        // ����� ����� ���ͽ�ũ��Ʈ���� ó�� >> �÷��̾� ���ݷ¸� �Ѱ���
        // ��üũ
        if (GameManager.Obj._targetMonster == null)
        {
            // ���ݻ����δ� Ÿ���� ���� ��� �ҵ� ����Ʈ�� ����
            SwordEffectOff();
            yield break;
        }
        // �ڷ�ƾ �ʱ�ȭ
        _coAttack = null;
    }

    public override void AttackEvent()
    {
        // ����� ���
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // ���� �߰�
        GameManager.Sound.SFXPlay("Punch1");
    }

    // �⺻���� ����Ʈ �ִϸ��̼� Ŭ�� Attack���� �̺�Ʈ�� ó��
    public void SwordEffectOn()
    {
        _swordEffect.enabled = true;
    }
    public void SwordEffectOff()
    {
        _swordEffect.enabled = false;
    }

}
