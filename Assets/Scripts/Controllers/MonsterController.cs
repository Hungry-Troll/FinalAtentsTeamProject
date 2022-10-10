using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : MonoBehaviour
{
    //Define ����ü ����
    CreatureState _state = CreatureState.Idle;
    //�÷��̾� Ÿ��
    Transform _PlayerPos;
    //���� ������Ʈ ��ǥ
    public Vector3 _mobpos;
    //���� �̵��ӵ�
    [SerializeField] float _speed = 2.0f;
    //���� ȸ���ӵ�
    float _rotateSpeed;
    //���� ���ݹݰ�
    float _attack;
    //�ִϸ�����
    Animator _ani;

    //���� �״°� �׽�Ʈ
    //float _damage = 5.0f;
    //float _hp = 10.0f;
    //���� ����
    MonsterStat _monsterStat;

    //���� ���� ����
    float _distance;
    //�÷��̾� ĳ���� ��ġ�� ���� ��ġ ������ �Ÿ��� ���
    float _PosToPos;
    //�÷��̾� ��ũ��Ʈ
    PlayerController _playerController;
    //���� �⺻���ݿ� �ڷ�ƾ ����
    Coroutine _coAttack;
    //���� ����� �ڷ�ƾ ����
    Coroutine _coDead;
    public int _mobNum
    {
        get;
        set;
    } = 0;

    void Awake()
    {
        _distance = 15.0f;
        _rotateSpeed = 90f;
        _attack = 3.0f;
        _mobNum = 0;
    }
    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        //������Ʈ���� �÷��̾��� ���� ��������� ���� Start()���� �ѹ��� ��������� ����
        _PosToPos = Vector3.Distance(_PlayerPos.position, transform.position);
        UpdateState();
    }

    public CreatureState Property_state
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

    public void UpdateState()
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
                Invoke("UpdateDead", 5f);
                break;
            case CreatureState.Skill:
                UpdateSkill();
                break;
        }
    }
    
    public void UpdateIdle()
    {
        if (_PosToPos <= _distance)
        {
            Property_state = CreatureState.Move;
            return;
        }
        else
        {
            Property_state = CreatureState.Idle;
            return;
        }
    }
    
    public void UpdateMoving()
    {
        _speed = 2.0f;
        if(_PosToPos <= _distance && _PosToPos > _attack)
        {
            Vector3 targetPos = new Vector3(2.5f, 0, 0);

            transform.position = Vector3.MoveTowards(transform.position, (_PlayerPos.transform.position - targetPos), _speed * Time.deltaTime);
            Vector3 _lookRotation = _PlayerPos.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);
            
        }
        // ����
        else if(_PosToPos <= _attack)
        {
            Property_state = CreatureState.Attack;
            return;
        }
        // ���
        else if(_PosToPos > _distance)
        {
            //this.gameObject.transform.position = MonsterManager.instance._mobPosList[_mobNum];
            //MonsterManager�� ���ӸŴ����� �����ؼ� ���. MonsterManager >> GameManager.Mob
            // GameManager.Mob._mobPosList[_mobNum] �̺κ��� ���� ���� ���͸Ŵ���EX�� ������ ������� �ʱ� ������... 
            // ������Ʈ Ǯ�� ������ ���
            this.transform.position = Vector3.MoveTowards(transform.position, GameManager.Mob._mobPosList[_mobNum], _speed * Time.deltaTime);
            Vector3 _lookRotation = GameManager.Mob._mobPosList[_mobNum] - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

            if(this.transform.position == GameManager.Mob._mobPosList[_mobNum])
            {
                Property_state=CreatureState.Idle;
                return;
            }
        }
    }

    private void UpdateAttack()
    {
        if(_PosToPos <= _attack)
        {
            // �ڷ�ƾ�� ���� �ƴϸ�
            if(_coAttack == null)
            {
                // �ڷ�ƾ�Լ� �����ϰ� �ڷ�ƾ ������ ����
                _coAttack = StartCoroutine(AttackDelay(3.0f));
            }
        }
        else
        {
            //��ž �ڷ�ƾ�� �ʿ��Ѱ���?
            StopCoroutine(AttackDelay(3.0f));
            Property_state = CreatureState.Move;
            return;
        }
    }

    private void UpdateDead()
    {
        // �׾��� �� ũ�ν����̵� �ִϸ��̼� ������ ���� �߻� 
        // �״� �ð��� �ֱ����� �ڷ�ƾ
        if(_coDead == null)
        {
            _coDead = StartCoroutine(DeadDelay(3.0f));
        }
        // �̸����� ������Ʈ�Ŵ������� ã�Ƽ� ����
        GameManager.Obj.RemoveMobListTraget(gameObject.name);
    }

    private void UpdateSkill()
    {

    }

    private void OnDisable()
    {
        Property_state = CreatureState.Idle;
    }

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

    IEnumerator AttackDelay(float _delay)
    {
        _speed = 0;
        Property_state = CreatureState.Attack;
        yield return new WaitForSeconds(_delay);
        // ����� ����� �÷��̾� ��ũ��Ʈ���� ó�� >> ���ݷ¸� �Ѱ���
        _playerController.OnDamaged(_monsterStat.Atk);
        // �ڷ�ƾ ���� �ʱ�ȭ 
        _coAttack = null;
    }
    // ��� ������ ������ ����� ���޼��� ���ſ�
    IEnumerator DeadDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // ������Ʈ Ǯ�� ���� ����
        //gameObject.SetActive(false);
        Destroy(gameObject);
        _coDead = null;
    }

    //���� ����� �޴� �Լ�
    public void OnDamaged(int playerAtk)
    {
        // ����� ���
        _monsterStat.Hp -= playerAtk - _monsterStat.Def;
        if (_monsterStat.Hp <= 0)
        {
            //MonsterManager�� ���ӸŴ����� �����ؼ� ���. MonsterManager >> GameManager.Mob
            GameManager.Mob.Property_isDie = true;
            Property_state = CreatureState.Dead;
            //_hp = 10.0f; // �����ذ� ������ �����... ������Ʈ Ǯ���뵵�� ���� ���
            return;
        }
    }
}
