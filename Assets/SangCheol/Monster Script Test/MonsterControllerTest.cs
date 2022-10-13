using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
//50���� �� Image ����Ϸ��� UnityEngine.UI; �ʿ���
using UnityEngine.UI;

public class MonsterControllerTest : MonoBehaviour
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

    //HpBar ������ ����
    public GameObject _hpBarPrefab;
    //���� ���� HpBar �������� ������ ��ġ -> 62��° �ڵ�
    Vector3 _hpBarOffset;
    //Canvas
    private Canvas _uiCanvas;
    //HpBar �̹��� ���
    private Image _hpBarImage;

    public int _mobNum
    {
        get;
        set;
    } = 0;

    //���� ����/��ų ��Ÿ�� �� Ȯ��
    float _time;
    int _skillPersent;
    int _attackCount;

    void Awake()
    {
        //���� �� HpBar ��ġ
        _hpBarOffset = new Vector3(0, 6f, 0);
        //Resources ���� �ȿ� �ִ� HpBar ������ �ҷ�����
        _hpBarPrefab = Resources.Load<GameObject>("HpBar");
        _distance = 15.0f;
        _rotateSpeed = 90f;
        _attack = 3.0f;
        _mobNum = 0;
        _time = 0;
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
                    _ani.SetTrigger("isAttack");
                    break;
                case CreatureState.Dead:
                    _ani.CrossFade("Dead", 0.3f);
                    break;
                case CreatureState.Skill:
                    _ani.SetTrigger("isSkill");
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
                Invoke("UpdateDead", 0.5f);
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
        _speed = 7.0f;
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
            _ani.SetInteger("state", 0);
            _time += Time.deltaTime;
            if (_time > 3.5f)
            {
                _skillPersent = Random.Range(0, 99);
                Debug.Log(_skillPersent);
                if (_skillPersent > 97)
                {
                    Property_state = CreatureState.Skill;
                }
                else if (_skillPersent <= 97)
                {
                    Property_state = CreatureState.Attack;
                }
                _time = 0;
            }
        }
        // ���
        else if(_PosToPos > _distance)
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
            //    Property_state=CreatureState.Idle;
            //    return;
            //}
            this.transform.position = Vector3.MoveTowards(transform.position, MonsterManager.instance._mobPosList[_mobNum], _speed * Time.deltaTime);
            Vector3 _lookRotation = MonsterManager.instance._mobPosList[_mobNum] - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

            if (this.transform.position == MonsterManager.instance._mobPosList[_mobNum])
            {
                Property_state = CreatureState.Idle;
                return;
            }
        }
    }

    private void UpdateAttack()
    {
        //if(_PosToPos <= _attack)
        //{
        //    // �ڷ�ƾ�� ���� �ƴϸ�
        //    if(_coAttack == null)
        //    {
        //        // �ڷ�ƾ�Լ� �����ϰ� �ڷ�ƾ ������ ����
        //        _coAttack = StartCoroutine(AttackDelay(3.0f));
        //    }
        //}
        //else
        //{
        //    //��ž �ڷ�ƾ�� �ʿ��Ѱ���?
        //    StopCoroutine(AttackDelay(3.0f));
        //    Property_state = CreatureState.Move;
        //    return;
        //}
        // EffectManager���� Effect�Լ� ȣ��
        EffectManager.Instance.MonsterEffect((_PlayerPos.position + _hpBarOffset), Vector3.zero, this.gameObject.transform, EffectType.Roar);
        Property_state = CreatureState.Move;
    }

    private void UpdateDead()
    {
        // �̸����� ������Ʈ�Ŵ������� ã�Ƽ� ����
        GameManager.Obj.RemoveMobListTraget(gameObject.name);
        // �׾��� �� ũ�ν����̵� �ִϸ��̼� ������ ���� �߻� 
        // �״� �ð��� �ֱ����� �ڷ�ƾ
        if (_coDead == null)
        {
            _coDead = StartCoroutine(DeadDelay(1.0f));
        }
    }

    private void UpdateSkill()
    {
        StartCoroutine(CoolTime(7f));
        Property_state = CreatureState.Move;
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
        //�˱� ��� -> ��ġ�� �޾ƿ;� �Ǵµ� ���� �̱���
        //TrailEffect._instance.Use();
        // ��üũ
        if (_playerController == null)
        {
            yield break;
        }
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

    IEnumerator CoolTime(float _coolTime)
    {
        Debug.Log("��Ÿ��!!");

        while (_coolTime > 1.0f)
        {
            _coolTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("��Ÿ�� ��!!");
    }

    //���� ����� �޴� �Լ�
    public void OnDamaged(int playerAtk)
    {
        // ����� ���
        _monsterStat.Hp -= playerAtk - _monsterStat.Def;
        // �������� ���� Hp����
        _hpBarImage.fillAmount = _monsterStat.Hp / _monsterStat.Max_Hp;
        if (_monsterStat.Hp <= 0)
        {
            //MonsterManager�� ���ӸŴ����� �����ؼ� ���. MonsterManager >> GameManager.Mob
            GameManager.Mob.Property_isDie = true;
            Property_state = CreatureState.Dead;
            //_hp = 10.0f; // �����ذ� ������ �����... ������Ʈ Ǯ���뵵�� ���� ���
            return;
        }
    }

    void SetHpBar()
    {
        //Ui ĵ���� ã�Ƽ� HpBar�������� �ڽ����� ����
        _uiCanvas = GameObject.Find("Ui Canvas").GetComponent<Canvas>();
        GameObject _hpbar = Instantiate<GameObject>(_hpBarPrefab, _uiCanvas.transform);
        //HpBar�������� �̹����̱� ������ HpBar������ ù��° �ڽ�(HpBar ���� ���� �ȿ� �ִ� ü�°�����)�� �ҷ����� ���� [1] ���
        _hpBarImage = _hpbar.GetComponentsInChildren<Image>()[1];

        //HpBar ������ ��ġ ����
        var _HpBar = _hpbar.GetComponent<HpBar>();
        _HpBar._mobPos = this.gameObject.transform;
        _HpBar.offset = _hpBarOffset;
    }
}
