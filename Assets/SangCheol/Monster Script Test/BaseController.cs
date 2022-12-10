using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{

    //Define 열거체 참조
    public CreatureState _state = CreatureState.Idle;
    //플레이어 타겟
    public Transform _PlayerPos;
    //몬스터 오브젝트 좌표
    public Vector3 _mobpos;
    //몬스터 이동속도
    public float _speed = 2.0f;
    //몬스터 회전속도
    public float _rotateSpeed;
    //몬스터 공격반경
    public float _attack;
    //애니메이터
    public Animator _ani;

    //몬스터 죽는거 테스트
    //float _damage = 5.0f;
    //float _hp = 10.0f;
    //몬스터 스텟
    public MonsterStat _monsterStat;

    //몬스터 추적 범위
    public float _distance;
    //플레이어 캐릭터 위치와 몬스터 위치 사이의 거리를 계산
    public float _PosToPos;
    //플레이어 스크립트
    public PlayerController _playerController;
    //몬스터 기본공격용 코루틴 변수
    public Coroutine _coAttack;
    //몬스터 사망용 코루틴 변수
    public Coroutine _coDead;

    //HpBar 프리팹 생성
    public GameObject _hpBarPrefab;
    //몬스터 위에 HpBar 프리팹이 생성될 위치 -> 62번째 코드
    public Vector3 _hpBarOffset;
    //Canvas
    public Canvas _uiCanvas;
    //HpBar 이미지 사용
    public Image _hpBarImage;

    public int _hp;
    public int _fullHp;
    public int _damage;
    public float hpPer;

    public Vector3 _damageTextOffset;
    public GameObject hudDamageText;
    //public Transform hudPos;

    public int _mobNum
    {
        get;
        set;
    } = 0;

    //몬스터 공격/스킬 쿨타임 및 확률
    public float _time;
    public int _skillPersent;
    public int _attackCount;

    // Start is called before the first frame update
    void Start()
    {
        _hp = 20;
        _fullHp = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            //상태가 바뀔때마다 GetComponent호출하는것을 Start()함수에서 한번 호출로 변경

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

    public virtual void UpdateState()
    {
        //플레이어를 계속 찾는 것 수정
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
        else
        {
            Property_state = CreatureState.Idle;
            return;
        }
    }

    public virtual void UpdateMoving()
    {
        _speed = 7.0f;
        if (_PosToPos <= _distance && _PosToPos > _attack)
        {
            Vector3 targetPos = new Vector3(2.5f, 0, 0);

            transform.position = Vector3.MoveTowards(transform.position, (_PlayerPos.transform.position - targetPos), _speed * Time.deltaTime);
            Vector3 _lookRotation = _PlayerPos.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

        }
        // 공격
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
        //대기
        else if (_PosToPos > _distance)
        {
            //this.gameObject.transform.position = MonsterManager.instance._mobPosList[_mobNum];
            //MonsterManager를 게임매니저에 연결해서 사용. MonsterManager >> GameManager.Mob
            // GameManager.Mob._mobPosList[_mobNum] 이부분은 추후 수정 몬스터매니저EX를 당장은 사용하지 않기 때문에... 
            // 오브젝트 풀링 구현시 사용
            //this.transform.position = Vector3.MoveTowards(transform.position, GameManager.Mob._mobPosList[_mobNum], _speed * Time.deltaTime);
            //Vector3 _lookRotation = GameManager.Mob._mobPosList[_mobNum] - this.transform.position;
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lookRotation), Time.deltaTime * _rotateSpeed);

            //if(this.transform.position == GameManager.Mob._mobPosList[_mobNum])aa
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

    public virtual void UpdateAttack()
    {
        //if(_PosToPos <= _attack)
        //{
        //    // 코루틴이 널이 아니면
        //    if(_coAttack == null)
        //    {
        //        // 코루틴함수 시작하고 코루틴 변수에 대입
        //        _coAttack = StartCoroutine(AttackDelay(3.0f));
        //    }
        //}
        //else
        //{
        //    //스탑 코루틴이 필요한가용?
        //    StopCoroutine(AttackDelay(3.0f));
        //    Property_state = CreatureState.Move;
        //    return;
        //}
        // EffectManager에서 Effect함수 호출
        EffectManager.Instance.MonsterEffect((_PlayerPos.position + _hpBarOffset), Vector3.zero, this.gameObject.transform, EffectType.Hit);
        Property_state = CreatureState.Idle;
        return;
    }

    public virtual void UpdateDead()
    {
        // 이름으로 오브젝트매니저에서 찾아서 제거
        //GameManager.Obj.RemoveMobListTraget(gameObject.name);
        // 죽었을 때 크로스페이드 애니메이션 때문에 에러 발생 
        // 죽는 시간을 주기위한 코루틴
        if (_coDead == null)
        {
            _coDead = StartCoroutine(DeadDelay(1.0f));
        }
    }

    public virtual void UpdateSkill()
    {
        StartCoroutine(CoolTime(7f));
        Property_state = CreatureState.Move;
    }

    public virtual void OnDisable()
    {
        Property_state = CreatureState.Idle;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _damage = Random.Range(1, 5);
            // 몬스터 스텟으로 임시 변수 대체
            _hp -= _damage;
            _hpBarImage.fillAmount = _hp / _fullHp;
            hpPer = (_hp / _fullHp * 100);
            TakeDamage(_damage);
            Debug.Log("현재 몬스터 체력 = " + _hp);
            Debug.Log("남은 체력 비율 = " + hpPer);
            if (_hp <= 0)
            {
                //MonsterManager를 게임매니저에 연결해서 사용. MonsterManager >> GameManager.Mob
                //GameManager.Mob.Property_isDie = true;
                Debug.Log("몬스터 죽음!!");
                Property_state = CreatureState.Dead;
                _hp = 20;
                return;
            }
        }
    }

    public virtual IEnumerator AttackDelay(float _delay)
    {
        _speed = 0;
        //Property_state = CreatureState.Attack;
        yield return new WaitForSeconds(_delay);
        // 대미지 계산은 플레이어 스크립트에서 처리 >> 공격력만 넘겨줌
        //검기 사용 -> 위치를 받아와야 되는데 아직 미구현
        //TrailEffect._instance.Use();
        // 널체크
        if (_playerController == null)
        {
            yield break;
        }
        _playerController.OnDamaged(_monsterStat.Atk);
        // 코루틴 변수 초기화 
        _coAttack = null;
    }
    // 사망 딜레이 때문에 생기는 경고메세지 제거용
    public virtual IEnumerator DeadDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // 오브젝트 풀링 추후 구현
        //gameObject.SetActive(false);
        Destroy(_hpBarPrefab);
        Destroy(gameObject);
        _coDead = null;
    }

    public virtual IEnumerator CoolTime(float _coolTime)
    {
        Debug.Log("쿨타임!!");

        while (_coolTime > 1.0f)
        {
            _coolTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("쿨타임 끝!!");
    }

    //몬스터 대미지 받는 함수
    //public void OnDamaged(int playerAtk)
    //{
    //    // 대미지 계산
    //    _monsterStat.Hp -= playerAtk - _monsterStat.Def;
    //    // 데미지에 따른 Hp감소
    //    _hpBarImage.fillAmount = _monsterStat.Hp / _monsterStat.Max_Hp;
    //    if (_monsterStat.Hp <= 0)
    //    {
    //        //MonsterManager를 게임매니저에 연결해서 사용. MonsterManager >> GameManager.Mob
    //        GameManager.Mob.Property_isDie = true;
    //        Property_state = CreatureState.Dead;
    //        //_hp = 10.0f; // 말해준거 같은대 까먹음... 오브젝트 풀링용도면 추후 사용
    //        return;
    //    }
    //}

    public virtual void TakeDamage(int damage)
    {
        _uiCanvas = GameObject.Find("Ui Canvas").GetComponent<Canvas>();
        GameObject hudText = Instantiate<GameObject>(hudDamageText, _uiCanvas.transform); // 생성할 텍스트 오브젝트
        hudText.GetComponent<DamageText>().damage = damage; // 데미지 전달
        var _DamageText = hudText.GetComponent<DamageText>();
        _DamageText._mobPos = this.gameObject.transform;
        _DamageText.offset = _damageTextOffset;
    }

    public virtual void SetHpBar()
    {
        //Ui 캔버스 찾아서 HpBar프리팹을 자식으로 넣음
        _uiCanvas = GameObject.Find("Ui Canvas").GetComponent<Canvas>();
        GameObject _hpbar = Instantiate<GameObject>(_hpBarPrefab, _uiCanvas.transform);
        //HpBar프리팹이 이미지이기 떄문에 HpBar프리팹 첫번째 자식(HpBar 바탕 말고 안에 있는 체력게이지)를 불러오기 위해 [1] 사용
        _hpBarImage = _hpbar.GetComponentsInChildren<Image>()[1];
        //_uiCanvas.worldCamera = Camera.main;
        //HpBar 프리팹 위치 설정
        var _HpBar = _hpbar.GetComponent<HpBar>();
        _HpBar._mobPos = this.gameObject.transform;
        _HpBar.offset = _hpBarOffset;
    }
}
