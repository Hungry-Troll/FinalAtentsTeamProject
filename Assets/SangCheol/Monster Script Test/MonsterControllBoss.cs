using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterControllBoss : BaseController
{
    void Awake()
    {
        EffectManager.Instance.MonsterEffect(this.transform.position, Vector3.zero, this.gameObject.transform, EffectType.Summon);
        //���� �� HpBar ��ġ
        _hpBarOffset = new Vector3(0, 6f, 0);
        //Resources ���� �ȿ� �ִ� HpBar ������ �ҷ�����
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
        //���� �ִϸ����� �������
        _ani = GetComponent<Animator>();
        // ���ӸŴ������� �÷��̾� ��ġ ������ ��
        //_PlayerPos = GameManager.Obj._playerController.transform;
        //// ���ӸŴ������� �÷��̾� ��ũ��Ʈ ������ ��
        //_playerController = GameManager.Obj._playerController;
        //HpBar�Լ� ȣ��
        SetHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        _PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //������Ʈ���� �÷��̾��� ���� ��������� ���� Start()���� �ѹ��� ��������� ����
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
        // ����
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
        //���
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

    // ��� ������ ������ ����� ���޼��� ���ſ�
    public override IEnumerator DeadDelay(float _delay)
    {
        yield return StartCoroutine(base.DeadDelay(_delay));
    }

    public override IEnumerator CoolTime(float _coolTime)
    {
        yield return StartCoroutine(base.CoolTime(_coolTime));
    }

    //���� ����� �޴� �Լ�
    //public void OnDamaged(int playerAtk)
    //{
    //    // ����� ���
    //    _monsterStat.Hp -= playerAtk - _monsterStat.Def;
    //    // �������� ���� Hp����
    //    _hpBarImage.fillAmount = _monsterStat.Hp / _monsterStat.Max_Hp;
    //    if (_monsterStat.Hp <= 0)
    //    {
    //        //MonsterManager�� ���ӸŴ����� �����ؼ� ���. MonsterManager >> GameManager.Mob
    //        GameManager.Mob.Property_isDie = true;
    //        Property_state = CreatureState.Dead;
    //        //_hp = 10.0f; // �����ذ� ������ �����... ������Ʈ Ǯ���뵵�� ���� ���
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
