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



    // 타겟 확인용
    float distance;
    float mondis;

    // 펫 스텟 스크립트
    PetStat _petStat;

    // 공격용 코루틴
    Coroutine _coAttack;
    // 스킬용 코루틴
    Coroutine _coSkill;
    // 스킬용 변수들
    int _rnd;
    private float _timer;
    bool isAttack;

    // 공격속도 코루틴 대입용 
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
        //  오브젝트매니저에서 게임 시작할 때 생성한 플레이어 정보를 이용해서 연결
        _player = GameManager.Obj._playerController.transform;

        // 오브젝트매니저에서 타겟을 찾는 함수 (오브젝트 매니저에서 만든 몬스터 리스트를 가지고 있음)
        if (GameManager.Obj._targetMonster != null)
        {
            // 타겟 지정
            _target = GameManager.Obj._targetMonster.transform;
        }

        // 펫 스텟 가지고 옴
        _petStat = GetComponent<PetStat>();

        // 공격을 위한 딜레이
        _attackDelay = 2.0f;

        // 시작할 때 idle 상태로 시작함
        _creatureState = CreatureState.Idle;

        _rnd = Random.Range(0, 9);



    }

    void Update()
    {
        // 타겟을 찾는 함수
        FindTarget();
        // 찾은 타겟을 이용해서 상태를 변환
        PetAISystem();

        // 변환된 상태를 가지고 아래 함수 구현
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
        // 애니메이션을 따로 관리
    }
    // 타겟 찾는 함수
    private void FindTarget()
    {
        // 플레이어와 거리계산
        distance = Vector3.Distance(transform.position, _player.transform.position);

        // 타겟이 없을경우 널체크
        if (GameManager.Obj._targetMonster == null)
        {
            return;
        }
        // 오브젝트매니저에서 타겟이 널이 아니면
        if (GameManager.Obj._targetMonster.transform != null)
        {
            // 타겟 대입
            _target = GameManager.Obj._targetMonster.transform;
            // 타겟과 거리 계산
            mondis = Vector3.Distance(transform.position, _target.transform.position);
        }
    }
    // 펫 인공지능?
    private void PetAISystem()
    {
        //즉 언제 공격할 것인가
        // 거리나 타겟에 따른 상태 변환
        // 플레이어와 거리가 3이상 떨어져 있으면 우선 플레이어에게 감 + 몬스터가 없을 경우

        if (distance >= 3f)
        {
            _target = _player;
            _creatureState = CreatureState.Move;
        }

        // 널체크
        if (_target == null)
        {
            return;
        }

        // 플레이어와 거리가 5이하이고 몬스터와 거리가 3이상이면 적에게 움직임
        if (GameManager.Obj._targetMonster != null && mondis > 3f && distance < 5f)
        {
            // 대기
            _creatureState = CreatureState.Move;
        }

        // 몬스터와 거리가 5이하면 공격 -> 실제로 플레이어가 공격버튼을 누르면 같이 공격
        if (GameManager.Obj._targetMonster != null && mondis <= 5.0f)
        {
            // 공격
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
        // 몬스터가 없고 플레이어와 거리가 3이하이면 대기
        else if (distance < 2.2f)
        {
            _creatureState = CreatureState.Idle;
        }
    }
    // 애니메이션만 따로 관리하는 함수
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
        // 널체크
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

        // 타겟이 없을경우 널체크
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            return;
        }

        //시간재는 코루틴 시작
        if (_coAttack == null && isAttack == false)
        {
            isAttack = true;
            _coAttack = StartCoroutine(CoAttackDelay(_attackDelay));
        }
    }

    public void Skill()
    {
        transform.LookAt(_target);

        // 타겟이 없을경우 널체크
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            return;
        }

        //시간재는 코루틴 시작
        if (_coAttack == null && isAttack == false)
        {
            isAttack = true;
            _coAttack = StartCoroutine(CoSkillDelay(_attackDelay));
        }
    }


    //public void PetSkill()
    //{대미지 계산하면 무한하게 연속적으로 대미지를 주기 때문에 사용
    //        _coSkill = StartCoroutine(CoSkillDelay(_attackDelay));
    //}

    public void Dead()
    {
        _nav.enabled = false;
        StartCoroutine(coDead());
    }

    // 대미지 받는 함수
    public void OnDamaged(int monsterAtk)
    {
        _petStat.Hp -= monsterAtk - _petStat.Def;
        if (_petStat.Hp <= 0)
        {
            _creatureState = CreatureState.Dead;
        }
    }
    //    transform.LookAt(_target);

    //        // 업데이트 함수에서 바로 스킬 

    IEnumerator coDead()
    {// 함수 이름이 겹쳐서 수정 Dead >> coDead

        yield return new WaitForSeconds(5f);
        Debug.Log("비활성화");
        gameObject.SetActive(false);

        //StopCoroutine("Dead");
        gameObject.SetActive(true);
        StartCoroutine(Revival());
    }
    IEnumerator Revival()
    {
        //StopCoroutine("Dead");
        yield return new WaitForSeconds(2f);

        Debug.Log("활성화");
        _animator.SetInteger("Pet_ani", 0);
        _nav.enabled = true;
        _isChase = true;
        StopCoroutine(Revival());

    }

    /// <summary>
    /// 이하 김용현 작성 코루틴
    /// </summary>

    // 일반 공격용 코루틴

    IEnumerator CoAttackDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);

        // 대미지 계산은 몬스터스크립트에서 처리 >> 플레이어 공격력만 넘겨줌
        // 타겟이 있을때만 대미지 계산 널 체크
        if (GameManager.Obj._targetMonster == null)
        {
            yield break;
        }
        //Debug.Log("펫공격");
        //GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk, 1);
        // 코루틴 초기화
        _coAttack = null;
        _rnd = Random.Range(0, 9);
        isAttack = false;
        Debug.Log("공격" + _rnd);
    }
    //스킬용 코루틴

    IEnumerator CoSkillDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // 딜레이
        // 대미지 계산은 몬스터스크립트에서 처리 >> 플레이어 공격력만 넘겨줌
        // 임시로 스킬대미지를 직접 구현
        if (GameManager.Obj._targetMonster == null)
        {
            yield break;
        }
        // 코루틴 초기화
        _coAttack = null;
        _rnd = Random.Range(0, 9);
        isAttack = false;
        Debug.Log("스킬" + _rnd);

    }


    #region 애니메이션 이벤트에서 처리할 함수들
    public void PetAttack()
    {
        GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk, 1);
        transform.LookAt(_target);
    }

    public void PetSkill1() //Bra : 데미지가 2배로 들어감 (완)
    {
        GameManager.Obj._targetMonsterController.OnDamaged(_petStat.Atk * 2, 1);
        transform.LookAt(_target);
    }

    public void PetSkill2() //Tri : 도발을 시전함 : 플레이어가 받는 데미지가 0 이 됨(완)
    {
        transform.LookAt(_target);
        PlayerController _playerController = GameManager.Obj._playerController;
        //MonsterControllerEX _monsterController = GameManager.Obj._targetMonsterController;
        _playerController.OnDamaged(0);

    }
    public void PetSkill3() //Pachy : 공격속도 2배 (스킬 애니메이션이 2번 빠르게 작동함) (완)
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


