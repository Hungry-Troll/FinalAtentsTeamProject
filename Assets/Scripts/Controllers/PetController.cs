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
    ParticleSystem _particleSystem;
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
    bool isAttack;

    // ���ݼӵ� �ڷ�ƾ ���Կ� 
    float _attackDelay;
    float _skillDelay;
    private void Awake()
    {
        _pos = transform.position;
        _nav = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    void Start()
    {
        //  ������Ʈ�Ŵ������� ���� ������ �� ������ �÷��̾� ������ �̿��ؼ� ����
        _player = GameManager.Obj._playerController.transform;

        // ������Ʈ�Ŵ������� Ÿ���� ã�� �Լ� (������Ʈ �Ŵ������� ���� ���� ����Ʈ�� ������ ����)
        if (GameManager.Obj._targetMonster != null)
        {
            // Ÿ�� ����
            _target = GameManager.Obj._targetMonster.transform;
        }

        // �� ���� ������ ��
        _petStat = GetComponent<PetStat>();

        // ������ ���� ������
        _attackDelay = 2.0f;

        // ������ �� idle ���·� ������
        _creatureState = CreatureState.Idle;

        _rnd = Random.Range(0, 9);



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
        //�� ���� ������ ���ΰ�
        // �Ÿ��� Ÿ�ٿ� ���� ���� ��ȯ
        // �÷��̾�� �Ÿ��� 3�̻� ������ ������ �켱 �÷��̾�� �� + ���Ͱ� ���� ���

        if (distance >= 3f)
        {
            _target = _player;
            _creatureState = CreatureState.Move;
        }

        // ��üũ
        if (_target == null)
        {
            return;
        }

        // �÷��̾�� �Ÿ��� 5�����̰� ���Ϳ� �Ÿ��� 3�̻��̸� ������ ������
        if (GameManager.Obj._targetMonster != null && mondis > 3f && distance < 5f)
        {
            // ���
            _creatureState = CreatureState.Move;
        }

        // ���Ϳ� �Ÿ��� 5���ϸ� ���� -> ������ �÷��̾ ���ݹ�ư�� ������ ���� ����
        if (GameManager.Obj._targetMonster != null && mondis <= 5.0f)
        {
            // ����
            transform.LookAt(GameManager.Obj._targetMonster.transform);

            if (_rnd >= 6)
            {
                _creatureState = CreatureState.Skill;
            }
            else
            {
                _creatureState = CreatureState.Attack;
            }
        }
        // ���Ͱ� ���� �÷��̾�� �Ÿ��� 3�����̸� ���
        else if (distance < 2.2f)
        {
            _creatureState = CreatureState.Idle;
        }
    }
    // �ִϸ��̼Ǹ� ���� �����ϴ� �Լ�
    private void UpdateAnimation()
    {
        switch (_creatureState)
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
        if (target == null)
        {
            return;
        }
        _nav.SetDestination(target.transform.position);
        _creatureState = CreatureState.Move;
        transform.LookAt(target);
    }

    public void Attack()
    {
        transform.LookAt(_target);

        // Ÿ���� ������� ��üũ
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            return;
        }

        //�ð���� �ڷ�ƾ ����
        if (_coAttack == null && isAttack == false)
        {
            isAttack = true;
            _coAttack = StartCoroutine(CoAttackDelay(_attackDelay));
        }
    }

    public void Skill()
    {
        transform.LookAt(_target);

        // Ÿ���� ������� ��üũ
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            return;
        }

        //�ð���� �ڷ�ƾ ����
        if (_coAttack == null && isAttack == false)
        {
            isAttack = true;
            _coAttack = StartCoroutine(CoSkillDelay(_attackDelay));
        }
    }


    //public void PetSkill()
    //{����� ����ϸ� �����ϰ� ���������� ������� �ֱ� ������ ���
    //        _coSkill = StartCoroutine(CoSkillDelay(_attackDelay));
    //}

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
    //    transform.LookAt(_target);

    //        // ������Ʈ �Լ����� �ٷ� ��ų 

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
        //Debug.Log("�����");
        //GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk, 1);
        // �ڷ�ƾ �ʱ�ȭ
        _coAttack = null;
        _rnd = Random.Range(0, 9);
        isAttack = false;
        Debug.Log("����" + _rnd);
    }
    //��ų�� �ڷ�ƾ

    IEnumerator CoSkillDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // ������
        // ����� ����� ���ͽ�ũ��Ʈ���� ó�� >> �÷��̾� ���ݷ¸� �Ѱ���
        // �ӽ÷� ��ų������� ���� ����
        if (GameManager.Obj._targetMonster == null)
        {
            yield break;
        }
        // �ڷ�ƾ �ʱ�ȭ
        _coAttack = null;
        _rnd = Random.Range(0, 9);
        isAttack = false;
        Debug.Log("��ų" + _rnd);

    }


    #region �ִϸ��̼� �̺�Ʈ���� ó���� �Լ���
    public void PetAttack()
    {
        GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk, 1);
        transform.LookAt(_target);
    }

    public void PetSkill1() //Bra : �������� 2��� �� (��)
    {
        GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk * 2, 1);
        transform.LookAt(_target);
    }

    public void PetSkill2() //Tri : ������ ������ : �÷��̾ �޴� �������� 0 �� ��(��)
    {
        transform.LookAt(_target);
        PlayerController _playerController = GameManager.Obj._playerController;
        //MonsterControllerEX _monsterController = GameManager.Obj._targetMonsterController;
        _playerController.OnDamaged(0);

    }
    public void PetSkill3() //Pachy : ���ݼӵ� 2�� (��ų �ִϸ��̼��� 2�� ������ �۵���) (��)
    {
        GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk/2, 1);
        transform.LookAt(_target);
    }


    public void StartEffect()
    {
        _particleSystem.Play();
    }

    public void EndEffect()
    {
        _particleSystem.Stop();
    }

    #endregion












}


