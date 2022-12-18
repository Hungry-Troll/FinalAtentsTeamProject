using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
//50���� �� Image ����Ϸ��� UnityEngine.UI; �ʿ���
using UnityEngine.UI;
using System;

public class MonsterControllerEX : MonoBehaviour
{
    //Define ����ü ����
    public CreatureState _state = CreatureState.Idle;
    //�÷��̾� Ÿ��
    public Transform _PlayerPos;
    //���� ������Ʈ ��ǥ
    public Vector3 _mobpos;
    //���� �̵��ӵ�
    public float _speed = 2.0f;
    //���� ȸ���ӵ�
    public float _rotateSpeed;
    //���� ���ݹݰ�
    public float _attack;
    //�ִϸ�����
    public Animator _ani;

    //���� ����
    public MonsterStat _monsterStat;

    //���� ���� ����
    public float _distance;
    //�÷��̾� ĳ���� ��ġ�� ���� ��ġ ������ �Ÿ��� ���
    public float _PosToPos;
    //�÷��̾� ��ũ��Ʈ
    public PlayerController _playerController;
    //���� �⺻���ݿ� �ڷ�ƾ ����
    public Coroutine _coAttack;
    //���� ����� �ڷ�ƾ ����
    public Coroutine _coDead;

    //����Ʈ�� ���� ���� ����
    public bool _isQuest;

    // ���� ���� �ǰݿ� �ڽ��ݶ��̴�
    public BoxCollider _mobBoxCollider;
    // �ǰ� ���¿�
    public Material _material;


    // ���� �޴� �����
    public int _damage;

    //���� ����/��ų ��Ÿ�� �� Ȯ��
    public float _time;
    public int _skillPersent;
    public int _attackCount;

    // ���� ��ų�� �ڷ�ƾ
    public Coroutine _coSkill1;
    public Coroutine _coSkill2;

    public int _mobNum
    {
        get;
        set;
    } = 0;

    public virtual void Awake()
    {
        _distance = 15.0f;
        _rotateSpeed = 45f;
        _attack = 2.0f;
        _mobNum = 0;
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        // �Ʒ� �������� Awake���� ������ �� ��� ���� �߻� ���ɼ� ����
        //���� ���� ���� ������ ��
        _monsterStat = GetComponent<MonsterStat>();
        //���� �ִϸ����� �������
        _ani = GetComponent<Animator>();
        // ���ӸŴ������� �÷��̾� ��ġ ������ ��
        _PlayerPos = GameManager.Obj._playerController.transform;
        // ���ӸŴ������� �÷��̾� ��ũ��Ʈ ������ ��
        _playerController = GameManager.Obj._playerController;
        // ���� �ǰݿ� �ڽ��ݶ��̴� (���� ��ų �ǰݿ�)
        _mobBoxCollider = GetComponent<BoxCollider>();
        // �ǰݻ��¿� ���׸��� ã��
        FindMaterial();
        // ��ų �ʱ�ȭ
        _coSkill1 = null;
        _coSkill2 = null;
    }

    public virtual void FindMaterial()
    {
        Transform tmpTr = Util.FindChild("velociraptor", transform);
        _material = tmpTr.GetComponent<SkinnedMeshRenderer>().material;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        //������Ʈ���� �÷��̾��� ���� ��������� ���� Start()���� �ѹ��� ��������� ����
        _PosToPos = Vector3.Distance(_PlayerPos.position, transform.position);
        UpdateState();
    }

    public virtual CreatureState Property_state
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            //���°� �ٲ𶧸��� GetComponentȣ���ϴ°��� Start()�Լ����� �ѹ� ȣ��� ����
            //Animator ani = GetComponent<Animator>();

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
                    break;
            }
        }
    }

    public virtual void UpdateState()
    {
        //�÷��̾ ��� ã�� �� ����
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
        }
    }

    public virtual void UpdateIdle()
    {
        if (_PosToPos <= _distance)
        {
            Property_state = CreatureState.Move;
            return;
        }
        /* else
         {
             Property_state = CreatureState.Idle;
             return;
         }*/
    }

    public virtual void UpdateMoving()
    {
        _speed = 2.0f;
        if (_PosToPos <= _distance && _PosToPos > _attack)
        {
            Vector3 targetPos = new Vector3(2.5f, 0, 0);

            transform.position = Vector3.MoveTowards(transform.position, (_PlayerPos.transform.position - targetPos), _speed * Time.deltaTime);
            Vector3 _lookRotation = _PlayerPos.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

        }
        // ����
        else if (_PosToPos <= _attack)
        {
            Property_state = CreatureState.Attack;
            return;
        }
        // ���
        else if (_PosToPos > _distance)
        {
            //this.gameObject.transform.position = MonsterManager.instance._mobPosList[_mobNum];
            //MonsterManager�� ���ӸŴ����� �����ؼ� ���. MonsterManager >> GameManager.Mob
            // GameManager.Mob._mobPosList[_mobNum] �̺κ��� ���� ���� ���͸Ŵ���EX�� ������ ������� �ʱ� ������... 
            // ������Ʈ Ǯ�� ������ ���
            //this.transform.position = Vector3.MoveTowards(transform.position, GameManager.Mob._mobPosList[_mobNum], _speed * Time.deltaTime);
            //Vector3 _lookRotation = GameManager.Mob._mobPosList[_mobNum] - this.transform.position;
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

            //if(this.transform.position == GameManager.Mob._mobPosList[_mobNum])
            //{
            Property_state = CreatureState.Idle;
            return;
            //}
        }
    }

    public virtual void UpdateAttack()
    {
        if (_PosToPos <= _attack)
        {
            // �ڷ�ƾ�� ���� �ƴϸ�
            if (_coAttack == null)
            {
                // �ڷ�ƾ�Լ� �����ϰ� �ڷ�ƾ ������ ����
                _coAttack = StartCoroutine(AttackDelay(2.0f));
            }
        }
        else
        {
            //��ž �ڷ�ƾ�� �ʿ��Ѱ���?
            //StopCoroutine(AttackDelay(2.0f)); 
            Property_state = CreatureState.Move;
            return;
        }
    }

    public virtual void UpdateDead()
    {
        // �̸����� ������Ʈ�Ŵ������� ã�Ƽ� ����
        GameManager.Obj.RemoveMobListTraget(gameObject.name);
        // �׾��� �� ũ�ν����̵� �ִϸ��̼� ������ ���� �߻� 
        // �״� �ð��� �ֱ����� �ڷ�ƾ
        if (_coDead == null)
        {
            // ���Ͱ� ����Ʈ �������� Ȯ��
            if (_isQuest)
            {
                // ����Ʈ �����̸� ����Ʈ ���ຯ���� ����
                GameManager.Quest.QuestProgressValueAdd();
                _isQuest = false;
            }
            _coDead = StartCoroutine(DeadDelay(1.0f));
        }
    }

    public virtual void UpdateSkill()
    {

    }

    public virtual void UpdateSkill2()
    {

    }

    /*    public virtual void OnDisable()
        {
            Property_state = CreatureState.Idle;
        }*/

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag.Equals("Player"))
    //    {
    //        // ���� �������� �ӽ� ���� ��ü
    //        _hp -= _damage;
    //        Debug.Log("���� ���� ü�� = " + _hp);
    //        if (_hp <= 0)
    //        {
    //            //MonsterManager�� ���ӸŴ����� �����ؼ� ���. MonsterManager >> GameManager.Mob
    //            GameManager.Mob.Property_isDie = true;
    //            Debug.Log("���� ����!!");
    //            Property_state = CreatureState.Dead;
    //            _hp = 10.0f;
    //            return;
    //        }
    //    }
    //}

    public virtual IEnumerator AttackDelay(float _delay)
    {
        _speed = 0;
        Property_state = CreatureState.Attack;
        yield return new WaitForSeconds(_delay);
        // ����� ����� �÷��̾� ��ũ��Ʈ���� ó�� >> ���ݷ¸� �Ѱ���
        // ��üũ
        if (_playerController == null)
        {
            yield break;
        }
        // �ڷ�ƾ ���� �ʱ�ȭ 
        _coAttack = null;
    }

    // �ִϸ��̼� Ŭ������ ����� ���
    public virtual void AttackEvent()
    {
        if (GameManager.Obj._playerController._creatureState != CreatureState.Dead)
        {
            // ����� ���
            _playerController.OnDamaged(_monsterStat.Atk);
            // ���� ����
            GameManager.Sound.SFXPlay("Dino-raptor");
        }
    }

    // ��� ������ ������ ����� ���޼��� ���ſ�
    public virtual IEnumerator DeadDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // ������Ʈ Ǯ�� ���� ����
        gameObject.SetActive(false);
        //Destroy(gameObject);
        //_coDead = null;
        // ������ ����ġ�� �÷��̾�� ����
        GameManager.Obj._playerController.ExpAdd(_monsterStat.Exp);
    }

    public virtual IEnumerator CoolTime(float _coolTime)
    {
        Debug.Log("��Ÿ��!!");

        while (_coolTime > 2.0f)
        {
            _coolTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("��Ÿ�� ��!!");
    }

    //���� ����� �޴� �Լ�
    public virtual void OnDamaged(int playerAtk, int SkillDamagePercent)
    {
        // �ǰݽ� ���� ����
        StartCoroutine(OnDamagedColor());

        _damage = 0;
        // ����� ���
        if (playerAtk > _monsterStat.Def)
        {
            _monsterStat.Hp -= (playerAtk - _monsterStat.Def) / SkillDamagePercent;
            _damage = (playerAtk - _monsterStat.Def) / SkillDamagePercent;
        }
        else
        {
            _monsterStat.Hp -= 1;
            _damage = 1;
        }
        //����� �ؽ�Ʈ ���� // �θ� ���� ���ӿ�����Ʈ��
        GameObject tmp = GameManager.DamText.DamageTextStart(this.gameObject, _damage);

        if (_monsterStat.Hp <= 0)
        {
            //MonsterManager�� ���ӸŴ����� �����ؼ� ���. MonsterManager >> GameManager.Mob
            GameManager.Mob.Property_isDie = true;
            Property_state = CreatureState.Dead;
            //_hp = 10.0f; // �����ذ� ������ �����... ������Ʈ Ǯ���뵵�� ���� ���
            return;
        }
    }

    IEnumerator OnDamagedColor()
    {
        float time = 0;
        Color tmpColor;
        tmpColor.r = 1;
        tmpColor.g = 0.5f;
        tmpColor.b = 0.5f;
        tmpColor.a = 1;
        _material.color = tmpColor;

        while (true)
        {
            time += Time.deltaTime;
            if (time > 0.2)
            {
                tmpColor.r = 1;
                tmpColor.g = 1f;
                tmpColor.b = 1f;
                tmpColor.a = 1;
                _material.color = tmpColor;
                yield break;
            }
            yield return null;
        }
    }
}
