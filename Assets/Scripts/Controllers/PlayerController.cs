using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using SimpleJSON;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    protected Vector2 _inputDir;
    protected Vector3 _tempVector;
    protected Vector3 _tempDir;
    protected Vector3 _rollVecter;
    protected Vector3 _rollDir;
    protected Vector3 _scientistSkill2Point;
    protected Vector3 checkPoint;
    protected Vector3 _bombPosition;
    protected Vector3 _portionPosition;
    public float _moveSpeed;
    public float _rotationSpeed;
    public float _rollSpeed;
    public float _autoMoveSpeed;

    //포션이 레이캐스트 도착지점 계산값
    public float _portionDistance;

    public CreatureState _creatureState;
    public SceneAttackButton _sceneAttackButton;
    public Animator _anim;
    public PlayerStat _playerStat;

    protected bool _isRoll;
    protected bool _isSkill1;
    protected bool _isSkill2;
    protected bool _isSkill3;
    public GoldController _goldController;
    protected Define.Job _playerJob;
    // 레벨업 컨트롤러
    public LevelUpController _leveUpController;
    // 공격용 코루틴
    protected Coroutine _coAttack;
    protected Coroutine _coSkill1;
    protected Coroutine _coSkill2;
    protected Coroutine _coSkill3;
    protected Coroutine _coRoll;

    // 공격속도 코루틴 대입용 
    protected float _attackDelay;

    //Skill2 공격범위 설정용
    public BoxCollider _skill2BoxCollider;

    //Skill1 파티클용
    protected ParticleSystem _skill1SlashEffect2_1;
    protected ParticleSystem _skill1SlashEffect2_2;
    protected ParticleSystem _skill1SlashEffect2_3;
    protected ParticleSystem _skill1SlashEffect2_4;
    protected ParticleSystem _skill1SlashEffect2_5;
    protected ParticleSystem _skill3GroundEffect;
    protected ParticleSystem _skill3BoosterEffect;
    //Skill2 파티클용
    protected ParticleSystem _skill2WheelWindEffect;

    //사이보그용 Skill1 파티클용
    protected ParticleSystem _skill1FlamethrowerEffect;
    //사이보그용 Skill2 파티클용
    public GameObject _cyborgSkill2;
    public ParticleSystem _skill2FireExplosionEffect;
    public ParticleSystem _skill2FireBigEffect;
    //사이보그용 Skill2 폭탄 오브젝트
    protected Transform _bomb;
    //사이보그용 Skill3 파티클용
    public GameObject _cyborgSkill3;
    protected ParticleSystem _skill3Explosion1Effect;
    protected ParticleSystem _skill3Explosion2Effect;
    protected ParticleSystem _skill3Explosion3Effect;
    protected ParticleSystem _skill3Explosion4Effect;
    //과학자 리소스로 불러올 스킬 오브젝트
    public GameObject _scientistSkill1;
    protected ParticleSystem _poison;
    protected ParticleSystem _explosion;
    public GameObject _scientistSkill2;
    protected ParticleSystem _powerDraw;
    protected ParticleSystem _electricity;
    public GameObject _scientistSkill3;
    protected ParticleSystem _powerBeam;
    //과학자 스킬1 포션 오브젝트
    protected Transform _poisonPortion;

    //이펙트 변화용 변수
    protected int effectChange;

    //Json 스킬정보
    protected TextAsset _skillInfoJson;
    //Skill3 정보 저장용
    protected Skill3InfoTemp _skill3Stat;
    public int skill3Level;
    //Skill3 플레이어 크기 트렌스폼 찾는 변수(미니Hp바 때문에)
    protected Transform _skill3PlayerScale;
    //플레이어 직업 구분용
    protected PlayerStat playerStat;
    Job playerJob;

    // Start is called before the first frame update
    protected void Start()
    {
        Init();

        JobStart();
    }

    // Init함수는 스타트 함수 공통내용
    public void Init()
    {
        _moveSpeed = 10.0f;
        _rotationSpeed = 10f;
        _rollSpeed = 10.0f;
        _creatureState = CreatureState.Idle;
        //_sceneAttackButton = SceneAttackButton.None;
        checkPoint = new Vector3(0, 0, 0);

        _anim = GetComponent<Animator>();
        _autoMoveSpeed = _moveSpeed + 2.0f;
        _playerStat = GetComponent<PlayerStat>();
        _attackDelay = 1.0f;
        _goldController = GetComponent<GoldController>();
        _playerJob = GameManager.Select._job;
        // 레벨업 컨트롤러 연결
        _leveUpController = GetComponent<LevelUpController>();

        //스킬 이펙트 변화용 변수
        effectChange = 0;
        //스킬 사용유무
        _isRoll = false;
        _isSkill1 = false;
        _isSkill2 = false;
        _isSkill3 = false;
        //스킬정보 제이슨으로 불러오기
        _skillInfoJson = Resources.Load<TextAsset>("Data/Json/Skill/Skills");
        //skill3 객체 생성
        _skill3Stat = new Skill3InfoTemp();
        //스킬레벨 넣는 변수
        skill3Level = 1;
        //Skill3 플레이어 크기 트렌스폼 찾는 변수(미니Hp바 때문에)
        _skill3PlayerScale = Util.FindChild("Armature", transform).GetComponent<Transform>();

        playerStat = transform.GetComponent<PlayerStat>();
    }
    // JobStart함수는 직업별로 다른 Start 함수
    public virtual void JobStart(){ }

    // Update is called once per frame
    protected void Update()
    {

        switch (_creatureState)
        {
            case CreatureState.Idle:
                Idle();
                break;
            case CreatureState.Move:
                Move();
                break;
            case CreatureState.AutoMove:
                AutoMove();
                break;
            case CreatureState.Attack:
                Attack();
                break;
            case CreatureState.Dead:
                Dead();
                break;
            case CreatureState.Rolling:
                Roll();
                break;
            case CreatureState.Skill:
                Skill1();
                break;
            case CreatureState.Skill2:
                Skill2();
                break;
            case CreatureState.Skill3:
                Skill3();
                break;
            case CreatureState.None:
                break;
        }
        UpdateAnimation();
        
    }
    // 애니메이션을 따로 관리
    protected virtual void UpdateAnimation()
    {
        switch (_creatureState)
        {
            case CreatureState.Idle:
                _anim.SetInteger("playerStat", 0);
                break;
            case CreatureState.Move:
                _anim.SetInteger("playerStat", 1);
                break;
            case CreatureState.AutoMove:
                _anim.SetInteger("playerStat", 1);
                break;
            case CreatureState.Attack:
                _anim.SetInteger("playerStat", 2);
                break;
            case CreatureState.Dead:
                _anim.SetInteger("playerStat", 3);
                break;
            case CreatureState.Rolling:
                _anim.SetInteger("playerStat", 9);
                break;
            case CreatureState.Skill:
                break;
            case CreatureState.Skill2:
                _anim.SetInteger("playerStat", 6);
                break;
            case CreatureState.Skill3:
                break;
            case CreatureState.None:
                break;
        }
    }

    protected void Idle()
    {
        // 대기 중 이동
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    protected void Move()
    {
        // 이동 중 대기
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            _creatureState = CreatureState.Idle;
        }
        _inputDir = GameManager.Ui._joyStickController.inputDirection;
        // Debug.Log("플레이어 : " + _inputDir);
        bool isMove = _inputDir.magnitude != 0;
        //if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        if (isMove)
        {
            // 이동
            float x = _inputDir.x;
            float y = _inputDir.y;
            _tempVector = new Vector3(x, 0, y);
            _tempVector = _tempVector * Time.deltaTime * _moveSpeed;
            transform.position += _tempVector;
            // 회전
            if (playerStat.Job == Job.Cyborg.ToString() && _isSkill1 == true)
            {
                transform.rotation = Quaternion.LookRotation(_tempDir.normalized);
            }
            else
            {
                _tempDir = new Vector3(x, 0, y);
                _tempDir = Vector3.RotateTowards(transform.forward, _tempDir, Time.deltaTime * _rotationSpeed, 0);
                transform.rotation = Quaternion.LookRotation(_tempDir.normalized);
            }
        }
    }

    public void Roll()
    {
        _rollDir = _tempDir.normalized;
        // 쿨타임은 추후 
        //GameManager.Ui._uiSceneAttackButton.RollingButton(true);
        _tempVector = _rollDir;
        _tempVector = _tempVector * Time.deltaTime * _rollSpeed;
        transform.position += _tempVector;

        if (_coRoll == null)
        {
            //보고 있는 방향을 저장한다.

            _coRoll = StartCoroutine(CoRoll());
        }
    }
    protected IEnumerator CoRoll()
    {
        yield return new WaitForSeconds(1f);
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            _creatureState = CreatureState.Idle;
        }
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
        _coRoll = null;
    }
    public virtual void Skill1()
    {
       
    }

    public virtual void Skill2()
    {

    }

    public virtual void Skill3()
    {

    }

    public void Skill3DataLoad()
    {
        TextAsset _skill3Info = Resources.Load<TextAsset>("Data/Json/Skill/Skill3Info");
        JSONNode _root = JSON.Parse(_skill3Info.text);
        JSONNode _skillInfo = _root[skill3Level - 1];
        if (_skillInfo == null)
        {
            return;
        }
        _skill3Stat.Skill3Level = int.Parse(_skillInfo["_Level"].Value);
        _skill3Stat.Skill3StatMaxHp = int.Parse(_skillInfo["_MaxHp"].Value);
        _skill3Stat.Skill3StatHp = int.Parse(_skillInfo["_MaxHp"].Value);
        _skill3Stat.Skill3StatAtk = int.Parse(_skillInfo["_Atk"].Value);
        _skill3Stat.Skill3StatDef = int.Parse(_skillInfo["_Def"].Value);
        _skill3Stat.Duration = float.Parse(_skillInfo["_Duration"].Value);
    }
    protected void AutoMove()
    {
        if (GameManager.Obj._targetMonster == null)
            return;
        // 거리
        float distance = Vector3.Distance(GameManager.Obj._targetMonster.transform.position, transform.position);
        // 이동
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Obj._targetMonster.transform.position, Time.deltaTime * _autoMoveSpeed);

        // 회전
        Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
        tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
        transform.rotation = Quaternion.LookRotation(tempDir.normalized);

        // 일정거리이상 가까워지면 공격
        // 플레이어 스탯에 기본 사정거리 추가? 팀과 상의 후 결정
        float defaultDistance = 0;
        switch (_playerJob)
        {
            case Define.Job.Superhuman:
                defaultDistance = 2.0f;
                break;
            case Define.Job.Cyborg:
                defaultDistance = 10.0f;
                break;
            case Define.Job.Scientist:
                defaultDistance = 5.0f;
                break;
            default:
                defaultDistance = 2.0f;
                break;
        }
        if (distance < defaultDistance)
        {
            _creatureState = CreatureState.Attack;
        }
    }

    protected virtual void Attack()
    {
        // 타겟 널 판정
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            // 공격상태인대 타겟이 없을 경우 소드 이펙트를 제거
            //SwordEffectOff();
            return;
        }
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            // 회전
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // 코루틴을 이용한 공격딜레이 (대미지 계산)
            if (_coAttack == null)
            {
                _coAttack = StartCoroutine(CoAttackDelay(_attackDelay));
            }
        }
        // 공격 중 이동
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    // 공격 딜레이 계산 (애니메이션 딜레이만)
    protected virtual IEnumerator CoAttackDelay(float _delay)
    {
        // 딜레이
        yield return new WaitForSeconds(_delay);
        // 대미지 계산
        // GameManager.Ui._targetMonsterStat.Hp -= _playerStat.Atk - GameManager.Ui._targetMonsterStat.Def;
        // 대미지 계산은 몬스터스크립트에서 처리 >> 플레이어 공격력만 넘겨줌
        // 널체크
        if (GameManager.Obj._targetMonster == null)
        {
            // 공격상태인대 타겟이 없을 경우 소드 이펙트를 제거
            //SwordEffectOff();
            yield break;
        }
        // 코루틴 초기화
        _coAttack = null;
    }

    // 공격 대미지 계산 + 사운드는 애니메이션 클립 Attack에서 이벤트로 처리
    public virtual void AttackEvent()
    {
        // 대미지 계산
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // 사운드 추가
        GameManager.Sound.SFXPlay("Punch1");
    }

    protected void Dead()
    {
        // 플레이어가 죽으면 유다이 같은 것을 띄울 것
        GameManager.Ui.GameOverUI();
        // 모든 유아이 끔
        GameManager.Ui.UISetActiveFalse();
        // 배경음 변경
        GameManager.Sound.BGMPlay("31");
        // 효과음 제거
        GameManager.Sound.SFXPlayOff();
    }

    public void OnDamaged(int monsterAtk)
    {
        if (_isRoll == true)
        {
            _playerStat.Hp -= monsterAtk * 0;
        }
        else
        {
            // 대미지 받는 함수
            if (monsterAtk > _playerStat.Def)
            {
                _playerStat.Hp -= monsterAtk - _playerStat.Def;
            }
            else
            {
                _playerStat.Hp -= 1;
            }

            if (_playerStat.Hp <= 0)
            {
                // HP가 -20,-40 이러면 이상하므로 고정
                _playerStat.Hp = 0;
                _creatureState = CreatureState.Dead;
            }
            StartCoroutine(ShakeCam());
        }
    }
    // 경험치 증가 함수
    public void ExpAdd(int MonsterExp)
    {
        // 경험치를 레벨업컨트롤러에서 관리
        _leveUpController.ExpAmount = MonsterExp;
    }

    //Vector3 beforePosition;
    protected float currentTime = 0;
    protected float ShakeRange = 0.5f;
    protected float ShakeTime = 0.4f;

    protected IEnumerator ShakeCam()
    {
        // 카메라 흔들리면서 화면 색상까지 변경하는 코드 
        GameManager.Ui.OnDamagedUI(true);
        GameManager.Cam._Vcam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3f;
        yield return new WaitForSeconds(0.3f);
        GameManager.Ui.OnDamagedUI(false);
        GameManager.Cam._Vcam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }

    public void ShakeCam(float a, float b)
    {
        CinemachineBasicMultiChannelPerlin perlin =
        GameManager.Cam._Vcam1.GetComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = a;
    }
    public Vector3 RaycastInfo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RaySkillLocation;
            if (Physics.Raycast(ray, out RaySkillLocation, Mathf.Infinity))
            {
                return RaySkillLocation.point;
            }
        }
        return new Vector3(0, 0, 0);
    }
    public ParticleSystem FindEffect(string SName, Transform TName)
    {
        GameObject effectObject = Util.FindChild(SName, TName).gameObject;
        if (effectObject != null)
        {
            ParticleSystem effect = effectObject.GetComponent<ParticleSystem>();
            effectObject.SetActive(true);
            effect.Stop();
            return effect;
        }
        return null;
    }
}