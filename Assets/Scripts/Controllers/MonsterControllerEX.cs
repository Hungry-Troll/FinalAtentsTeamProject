using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
//50번쨰 줄 Image 사용하려면 UnityEngine.UI; 필요함
using UnityEngine.UI;

public class MonsterControllerEX : MonoBehaviour
{
    //Define 열거체 참조
    CreatureState _state = CreatureState.Idle;
    //플레이어 타겟
    Transform _PlayerPos;
    //몬스터 오브젝트 좌표
    public Vector3 _mobpos;
    //몬스터 이동속도
    [SerializeField] float _speed = 2.0f;
    //몬스터 회전속도
    float _rotateSpeed;
    //몬스터 공격반경
    float _attack;
    //애니메이터
    Animator _ani;

    //몬스터 죽는거 테스트
    //float _damage = 5.0f;
    //float _hp = 10.0f;
    //몬스터 스텟
    MonsterStat _monsterStat;

    //몬스터 추적 범위
    float _distance;
    //플레이어 캐릭터 위치와 몬스터 위치 사이의 거리를 계산
    float _PosToPos;
    //플레이어 스크립트
    PlayerController _playerController;
    //몬스터 기본공격용 코루틴 변수
    Coroutine _coAttack;
    //몬스터 사망용 코루틴 변수
    Coroutine _coDead;

    //HpBar 프리팹 생성
    public GameObject _hpBarPrefab;
    //몬스터 위에 HpBar 프리팹이 생성될 위치 -> 62번째 코드
    Vector3 _hpBarOffset;
    //Canvas
    private Canvas _uiCanvas;
    //HpBar 이미지 사용
    private Image _hpBarImage;

    public int _mobNum
    {
        get;
        set;
    } = 0;

    void Awake()
    {
        //몬스터 위 HpBar 위치
        _hpBarOffset = new Vector3(0, 6f, 0);
        //Resources 폴더 안에 있는 HpBar 프리팹 불러오기
        _hpBarPrefab = Resources.Load<GameObject>("HpBar");
        _distance = 15.0f;
        _rotateSpeed = 90f;
        _attack = 3.0f;
        _mobNum = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 아래 정보들은 Awake에서 가지고 올 경우 에러 발생 가능성 존재
        //몬스터 스텟 정보 가지고 옴
        _monsterStat = GetComponent<MonsterStat>();
        //몬스터 애니메이터 가지고옴
        _ani = GetComponent<Animator>();
        // 게임매니저에서 플레이어 위치 가지고 옴
        _PlayerPos = GameManager.Obj._playerController.transform;
        // 게임매니저에서 플레이어 스크립트 가지고 옴
        _playerController = GameManager.Obj._playerController;
    }

    // Update is called once per frame
    void Update()
    {
        //업데이트에서 플레이어저 정보 가지고오는 것을 Start()에서 한번만 가지고오게 변경
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
            //상태가 바뀔때마다 GetComponent호출하는것을 Start()함수에서 한번 호출로 변경
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
        // 공격
        else if(_PosToPos <= _attack)
        {
            Property_state = CreatureState.Attack;
            return;
        }
        // 대기
        else if(_PosToPos > _distance)
        {
            //this.gameObject.transform.position = MonsterManager.instance._mobPosList[_mobNum];
            //MonsterManager를 게임매니저에 연결해서 사용. MonsterManager >> GameManager.Mob
            // GameManager.Mob._mobPosList[_mobNum] 이부분은 추후 수정 몬스터매니저EX를 당장은 사용하지 않기 때문에... 
            // 오브젝트 풀링 구현시 사용
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
            // 코루틴이 널이 아니면
            if(_coAttack == null)
            {
                // 코루틴함수 시작하고 코루틴 변수에 대입
                _coAttack = StartCoroutine(AttackDelay(2.0f));
            }
        }
        else
        {
            //스탑 코루틴이 필요한가용?
            //StopCoroutine(AttackDelay(2.0f)); 
            Property_state = CreatureState.Move;
            return;
        }
    }

    private void UpdateDead()
    {
        // 이름으로 오브젝트매니저에서 찾아서 제거
        GameManager.Obj.RemoveMobListTraget(gameObject.name);
        // 죽었을 때 크로스페이드 애니메이션 때문에 에러 발생 
        // 죽는 시간을 주기위한 코루틴
        if (_coDead == null)
        {
            _coDead = StartCoroutine(DeadDelay(1.0f));
        }
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
    //        // 몬스터 스텟으로 임시 변수 대체
    //        _hp -= _damage;
    //        Debug.Log("현재 몬스터 체력 = " + _hp);
    //        if (_hp <= 0)
    //        {
    //            //MonsterManager를 게임매니저에 연결해서 사용. MonsterManager >> GameManager.Mob
    //            GameManager.Mob.Property_isDie = true;
    //            Debug.Log("몬스터 죽음!!");
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
        // 대미지 계산은 플레이어 스크립트에서 처리 >> 공격력만 넘겨줌
        // 널체크
        if (_playerController == null)
        {
            yield break;
        }
        // 플레이어가 살아있으면 
        if(GameManager.Obj._playerController._creatureState != CreatureState.Dead)
        {
            // 대미지 계산
            _playerController.OnDamaged(_monsterStat.Atk);
            // 사운드 적용
            GameManager.Sound.SFXPlay("Dino-raptor");
        }

        // 코루틴 변수 초기화 
        _coAttack = null;
    }


    // 사망 딜레이 때문에 생기는 경고메세지 제거용
    IEnumerator DeadDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        // 오브젝트 풀링 추후 구현
        //gameObject.SetActive(false);
        Destroy(gameObject);
        _coDead = null;
    }

    //몬스터 대미지 받는 함수
    public void OnDamaged(int playerAtk, int SkillDamagePercent)
    {
        //대미지 텍스트 생성
        GameObject tmp = GameManager.Create.CreateUi("UI_DamageText", gameObject);
        tmp.transform.SetParent(this.gameObject.transform);
        DamageTextEX damageText = tmp.GetComponent<DamageTextEX>();

        // 대미지 계산
        if (playerAtk > _monsterStat.Def)
        {
            _monsterStat.Hp -= (playerAtk - _monsterStat.Def) / SkillDamagePercent;
            damageText._damage = (playerAtk - _monsterStat.Def) / SkillDamagePercent;
        }
        else
        {
            _monsterStat.Hp -= 1;
            damageText._damage = 1;
        }

        if (_monsterStat.Hp <= 0)
        {
            //MonsterManager를 게임매니저에 연결해서 사용. MonsterManager >> GameManager.Mob
            GameManager.Mob.Property_isDie = true;
            Property_state = CreatureState.Dead;
            //_hp = 10.0f; // 말해준거 같은대 까먹음... 오브젝트 풀링용도면 추후 사용
            return;
        }
    }
}
