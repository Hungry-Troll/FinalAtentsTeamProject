using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

public class PetController : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _nav;
    Transform _player;
    public Transform _target;
    Vector3 _pos;
    public bool _isChase;
    public float _repeatRate = 3f;

   
    CreatureState _creatureState;


    // Ÿ�� Ȯ�ο�
    float distance;
    float mondis;

    // �� ���� ��ũ��Ʈ
    PetStat _petStat;

    // ���ݿ� �ڷ�ƾ
    Coroutine _coAttack;
    // ��ų�� �ڷ�ƾ
    Coroutine _coSkill;
    // ��ų�� ������
    int _rnd;
    private float _timer;

    // �ð��� �ڷ�ƾ
    Coroutine _coTime;

    // ���ݼӵ� �ڷ�ƾ ���Կ� 
    float _attackDelay;

    private void Awake()
    {
        _pos = transform.position;
        _nav = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        //  ������Ʈ�Ŵ������� ���� ������ �� ������ �÷��̾� ������ �̿��ؼ� ����
        _player= GameManager.Obj._playerController.transform;
        //player = GameObject.Find("Player").transform;

        // ������Ʈ�Ŵ������� Ÿ���� ã�� �Լ� (������Ʈ �Ŵ������� ���� ���� ����Ʈ�� ������ ����)
        //GameManager.Obj.FindMobListTarget();

        if(GameManager.Obj._targetMonster != null)
        {
            // Ÿ�� ����
            _target = GameManager.Obj._targetMonster.transform;
        }

        //target = GameObject.FindGameObjectWithTag("Monster").transform;

        // �� ���� ������ ��
        _petStat = GetComponent<PetStat>();

        // ������ ���� ������
        _attackDelay = 1.0f;

        _creatureState = CreatureState.Idle;

    }

    void Update()
    {
        // Ÿ���� ã�� �Լ�
        FindTarget();
        // ã�� Ÿ���� �̿��ؼ� ���¸� ��ȯ
        PetAISystem();

        // ��ȯ�� ���¸� ������ �Ʒ� �Լ� ����
        switch (_creatureState)
        {
            case CreatureState.Idle:
                Idle();
                break;
            case CreatureState.Move:
                Move(_target);
                break;
            case CreatureState.Attack:
                Attack();
                break;
            case CreatureState.Skill:
                Skill();
                break;
            case CreatureState.Dead:
                Dead();
                break;
        }
        UpdateAnimation();
        // �ִϸ��̼��� ���� ����
    }
    // Ÿ�� ã�� �Լ�
    private void FindTarget()
    {
        // �÷��̾�� �Ÿ����
        distance = Vector3.Distance(transform.position, _player.transform.position);

        // Ÿ���� ������� ��üũ
        if (GameManager.Obj._targetMonster == null)
        {
            return;
        }

        // ������Ʈ�Ŵ������� Ÿ���� ���� �ƴϸ�
        if (GameManager.Obj._targetMonster.transform != null)
        {
            // Ÿ�� ����
            _target = GameManager.Obj._targetMonster.transform;
            // Ÿ�ٰ� �Ÿ� ���
            mondis = Vector3.Distance(transform.position, _target.transform.position);
        }
    }
    // �� �ΰ�����?
    private void PetAISystem()
    {
        // �Ÿ��� Ÿ�ٿ� ���� ���� ��ȯ
        // �÷��̾�� �Ÿ��� 5�̻� ������ ������ �켱 �÷��̾�� �� + ���Ͱ� ���� ���
        if(distance >= 3f)
        {
            _target = _player;
            _creatureState = CreatureState.Move;
        }

        // ��üũ
        if(_target == null)
        {
            return;
        }

        // �÷��̾�� �Ÿ��� 5�����̰� ���Ϳ� �Ÿ��� 3�̻��̸� ������ ������
        if(GameManager.Obj._targetMonster != null && mondis > 3f && distance < 6f)
        {
            // ���
            _creatureState = CreatureState.Move;
        }

        // ���Ϳ� �Ÿ��� 2.5���ϸ� ����
        if(GameManager.Obj._targetMonster != null && mondis <= 10.0f)
        {
            // ����
            transform.LookAt(GameManager.Obj._targetMonster.transform);
            _creatureState = CreatureState.Attack;
        }
        // ���Ͱ� ���� �÷��̾�� �Ÿ��� 3�����̸� ���
        else if (distance < 3f)
        {
            _creatureState = CreatureState.Idle;
        }
    }
    // �ִϸ��̼Ǹ� ���� �����ϴ� �Լ�
    private void UpdateAnimation()
    {
        switch(_creatureState)
        {
            case CreatureState.Idle:
                _animator.SetInteger("Pet_ani", 0);
                break;
            case CreatureState.Move:
                _animator.SetInteger("Pet_ani", 1);
                break;
            case CreatureState.Attack:
                _animator.SetInteger("Pet_ani", 2);
                break;
            case CreatureState.Skill:
                _animator.SetInteger("Pet_ani", 3);
                break;
            case CreatureState.Dead:
                _animator.SetTrigger("isDead");
                break;
        }
    }

    public void Idle()
    {
        transform.LookAt(_player);
    }

    public void Move(Transform target)
    {
        // ��üũ
        if(target == null)
        {
            return;
        }
        _nav.SetDestination(target.transform.position);
        transform.LookAt(target);
    }

    public void Attack()
    {
        // Ÿ���� ������� ��üũ
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            return;
        }

        transform.LookAt(_target);

        // �ð���� �ڷ�ƾ ����
        if (_coTime == null)
        {
            _coTime = StartCoroutine(CoTimer(_repeatRate));
        }
        // ��ų ���ǿ� �����ϸ� ��ų ���
        if (_timer > 0 && _rnd > 50)
        {
            Skill();
        }

        if (_coAttack == null)
        {
            // ������Ʈ �Լ����� �ٷ� ���� ����� ����ϸ� �����ϰ� ���������� ������� �ֱ� ������ ���
            _coAttack = StartCoroutine(CoAttackDelay(_attackDelay));
        }
    }

    public void Skill()
    {
        transform.LookAt(_target);
        if (_coSkill == null)
        {
            // ������Ʈ �Լ����� �ٷ� ��ų ����� ����ϸ� �����ϰ� ���������� ������� �ֱ� ������ ���
            _coSkill = StartCoroutine(CoSkillDelay(_attackDelay));
        }
    }

    public void Dead()
    {
        _nav.enabled = false;
        StartCoroutine(coDead());
    }

    // ����� �޴� �Լ�
    public void OnDamaged(int monsterAtk)
    {
        _petStat.Hp -= monsterAtk - _petStat.Def;
        if (_petStat.Hp <= 0)
        {
            _creatureState = CreatureState.Dead;
        }
    }

    IEnumerator coDead()
    {// �Լ� �̸��� ���ļ� ���� Dead >> coDead

        yield return new WaitForSeconds(5f);
        Debug.Log("��Ȱ��ȭ");
        gameObject.SetActive(false);
        
        //StopCoroutine("Dead");
        gameObject.SetActive(true);
        StartCoroutine(Revival());
    }
    IEnumerator Revival()
    {   
        //StopCoroutine("Dead");
        yield return new WaitForSeconds(2f);
        
        Debug.Log("Ȱ��ȭ");
        _animator.SetInteger("Pet_ani", 0);
        _nav.enabled = true;
        _isChase = true;
        StopCoroutine(Revival());

    }


    /// <summary>
    /// ���� ����� �ۼ� �ڷ�ƾ
    /// </summary>


    // �Ϲ� ���ݿ� �ڷ�ƾ
    IEnumerator CoAttackDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // ����� ����� ���ͽ�ũ��Ʈ���� ó�� >> �÷��̾� ���ݷ¸� �Ѱ���
        // Ÿ���� �������� ����� ��� �� üũ
        if (GameManager.Obj._targetMonster == null)
        {
            yield break;
        }
        GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk);
        // �ڷ�ƾ �ʱ�ȭ
        _coAttack = null;
    }
    // ��ų�� �ڷ�ƾ
    IEnumerator CoSkillDelay(float _delay)
    {
        // ������
        yield return new WaitForSeconds(_delay);
        // ����� ����� ���ͽ�ũ��Ʈ���� ó�� >> �÷��̾� ���ݷ¸� �Ѱ���
        // �ӽ÷� ��ų������� ���� ����
        GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk * 2);
        // �ڷ�ƾ �ʱ�ȭ
        _coSkill = null;
    }

    // �ð��� ��� �Լ�
    IEnumerator CoTimer(float second)
    {
        _timer = 0f;
        while (_timer > second)
        {
            _timer += Time.deltaTime;
            int rnd = Random.Range(0, 100);
            yield return null;
        }
        _coTime = null;
    }



}


