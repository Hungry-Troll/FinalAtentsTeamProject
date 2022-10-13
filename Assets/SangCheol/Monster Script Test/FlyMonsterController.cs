using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEngine.UI;

public class FlyMonsterController : MonoBehaviour
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

    public GameObject _hpBarPrefab;
    Vector3 _hpBarOffset;
    private Canvas _uiCanvas;
    //private Slider _hpBarSlider;
    private Image _hpBarImage;
    public int _mobNum
    {
        get;
        set;
    } = 0;
    float _time;
    int _skillPersent;
    int _attackCount;
    void Awake()
    {
        _hpBarOffset = new Vector3(0, 5f, 0);
        _distance = 50.0f;
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
        SetHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        _PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //������Ʈ���� �÷��̾� ���� ��������� ���� Start()���� �ѹ��� ��������� ����
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

            switch (_state)
            {
                case CreatureState.Idle:
                    _ani.SetInteger("state", 0);
                    break;
                case CreatureState.Move:
                    _ani.SetInteger("state", 2);
                    break;
                case CreatureState.Attack:
                    //_ani.SetInteger("state", 2);
                    //_ani.SetTrigger("isAttack");
                    break;
                case CreatureState.Dead:
                    _ani.CrossFade("Dead", 0.3f);
                    break;
                case CreatureState.Skill:
                    //_ani.SetInteger("state", 3);
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
        _speed = 10.0f;
        if(_PosToPos <= _distance && _PosToPos > 10.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 10.0f, transform.position.z), 10f * Time.deltaTime);
            if (transform.position.y == 2f)
            {
                _ani.CrossFade("Pterodactyl_Flap", 0.3f);
            }
            else if (transform.position.y <= 10f)
            {
                _ani.SetInteger("state", 3);
                transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, _PlayerPos.transform.position - new Vector3(6,6,6), 0.1f), _speed * Time.deltaTime);
                Vector3 _lookRotation = _PlayerPos.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);
            }
        }
        else if (_PosToPos <= 7.0f && _PosToPos > _attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _PlayerPos.transform.position.y, transform.position.z), _speed * Time.deltaTime);
            if (transform.position.y >= _PlayerPos.transform.position.y - 2.0f)
            {
                _ani.SetInteger("state", 4);
            }
        }
        // ����
        else if(_PosToPos <= _attack)
        {
            _time += Time.deltaTime;
            if (_time > 4f)
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
        //if (_PosToPos <= _attack)
        //{
        //    // �ڷ�ƾ�� ���� �ƴϸ�
        //    if (_coAttack == null)
        //    {
        //        // �ڷ�ƾ�Լ� �����ϰ� �ڷ�ƾ ������ ����
        //        _coAttack = StartCoroutine(AttackDelay(3.0f));
        //    }
        //}
        //else
        //{
        //    //��ž �ڷ�ƾ�� �ʿ��Ѱ���?
        //    //StopCoroutine(AttackDelay(3.0f));
        //    Property_state = CreatureState.Move;
        //    return;
        //}
        Debug.Log("��!!!!!");
        Property_state = CreatureState.Move;
    }

    private void UpdateDead()
    {
        // ������Ʈ Ǯ�� ���� ����
        // ���ӸŴ����� ����� ���͸� ����Ʈ���� �����ؾߵ� 
        gameObject.SetActive(false);
    }

    private void UpdateSkill()
    {
        Debug.Log("��ų�ߵ�!!!!!!!");
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
        Debug.Log("����!!!!");
        yield return new WaitForSeconds(_delay);
        // ����� ����� �÷��̾� ��ũ��Ʈ���� ó�� >> ���ݷ¸� �Ѱ���
        //_playerController.OnDamaged(_monsterStat.Atk);
        // �ڷ�ƾ ���� �ʱ�ȭ 
        _coAttack = null;
    }

    IEnumerator CoolTime(float _coolTime)
    {
        Debug.Log("��Ÿ��!!");

        while(_coolTime > 1.0f)
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
        _hpBarImage.fillAmount = _monsterStat.Hp / _monsterStat.Max_Hp;
        //_hpBarSlider.value = _monsterStat.Hp;
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
        _uiCanvas = GameObject.Find("Ui Canvas").GetComponent<Canvas>();
        GameObject _hpbar = Instantiate<GameObject>(_hpBarPrefab, _uiCanvas.transform);
        _hpBarImage = _hpbar.GetComponentsInChildren<Image>()[1];

        var _HpBar = _hpbar.GetComponent<HpBar>();
        _HpBar._mobPos= this.gameObject.transform;
        _HpBar.offset = _hpBarOffset;
    }
}
