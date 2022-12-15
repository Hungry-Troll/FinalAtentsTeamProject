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
    public float _moveSpeed;
    public float _rotationSpeed;
    public float _rollSpeed;
    public float _autoMoveSpeed;
    public CreatureState _creatureState;
    public SceneAttackButton _sceneAttackButton;
    public Animator _anim;
    public PlayerStat _playerStat;
    protected bool _KeyboardInputOnOff;
    protected bool _isRoll;
    protected bool _isSkill1;
    protected bool _isSkill3;
    public GoldController _goldController;
    protected Define.Job _playerJob;
    // 레벨업 컨트롤러
    public LevelUpController _leveUpController;
    // 공격용 코루틴
    protected Coroutine _coAttack;

    // 공격속도 코루틴 대입용 
    protected float _attackDelay;

    // 공격 이펙트용
    protected TrailRenderer _swordEffect;

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
    //Skill2 공격범위 설정용
    public BoxCollider _skill2BoxCollider;

    //사이보그용 Skill1 파티클용
    ParticleSystem _skill1FlamethrowerEffect;
    //이펙트 변화용 변수
    int effectChange;

    //Json 스킬정보
    protected TextAsset _skillInfoJson;
    //Skill3 정보 저장용
    protected Skill3Info _skill3Stat;
    public int skill3Level;
    //Skill3 플레이어 크기 트렌스폼 찾는 변수(미니Hp바 때문에)
    protected Transform _skill3PlayerScale;
    //플레이어 직업 구분용
    PlayerStat playerStat;
    Job playerJob;
    // 사이보그 논타겟 필드
    GameObject _skillGround;
    // Start is called before the first frame update
    protected void Start()
    {
        _moveSpeed = 10.0f;
        _rotationSpeed = 10f;
        _rollSpeed = 5f;
        _creatureState = CreatureState.Idle;
        _sceneAttackButton = SceneAttackButton.None;

        _anim = GetComponent<Animator>();
        _autoMoveSpeed = _moveSpeed + 2.0f;
        _playerStat = GetComponent<PlayerStat>();
        _attackDelay = 1.0f;
        _goldController = GetComponent<GoldController>();
        _playerJob = GameManager.Select._job;
        // 레벨업 컨트롤러 연결
        _leveUpController = GetComponent<LevelUpController>();
        // 공격이펙트 연결
        Transform tmp = Util.FindChild("SwordEffect", transform);
        _swordEffect = tmp.GetComponent<TrailRenderer>();
        // Skill1 파티클 연결
        _skill1SlashEffect2_1 = Util.FindChild("Skill1SlashEffect2_1", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_2 = Util.FindChild("Skill1SlashEffect2_2", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_3 = Util.FindChild("Skill1SlashEffect2_3", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_4 = Util.FindChild("Skill1SlashEffect2_4", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_5 = Util.FindChild("Skill1SlashEffect2_5", transform).GetComponent<ParticleSystem>();
        // Skill2 파티클 연결
        Transform skill2Tr = Util.FindChild("Skill2WheelWindEffect", transform);
        _skill2WheelWindEffect = skill2Tr.GetComponent<ParticleSystem>();
        // Skill2 파티클 Stop;
        Skill2WheelWindOff();
        // Skill2 공격범위용 콜라이더
        _skill2BoxCollider = skill2Tr.GetComponent<BoxCollider>();
        // Skill3 파티클 연결
        _skill3GroundEffect = Util.FindChild("Skill3GroundEffect", transform).GetComponent<ParticleSystem>();
        _skill3BoosterEffect = Util.FindChild("Skill3BoosterEffect", transform).GetComponent<ParticleSystem>();
        //파티클 실행 안되게
        _skill1SlashEffect2_1.Stop();
        _skill1SlashEffect2_2.Stop();
        _skill1SlashEffect2_3.Stop();
        _skill1SlashEffect2_4.Stop();
        _skill1SlashEffect2_5.Stop();
        _skill3GroundEffect.Stop();
        _skill3BoosterEffect.Stop();

        //사이보그 Skill1 파티클 연결
        //_skill1FlamethrowerEffect = Util.FindChild("Skill1FlamethrowerEffect", transform).GetComponent<ParticleSystem>();
        //_skill1FlamethrowerEffect.Stop();
        // Skill2 공격범위용 콜라이더
        //_skill2BoxCollider = _skill1FlamethrowerEffect.GetComponent<BoxCollider>();
        //사이보그 논타겟 스킬 범위
        //_skillGround = Util.FindChild("SkillGround", transform).gameObject;
        //스킬 이펙트 변화용 변수
        effectChange = 0;
        //스킬 사용유무
        _isRoll = false;
        _isSkill1 = false;
        _isSkill3 = false;
        //스킬정보 제이슨으로 불러오기
        _skillInfoJson = Resources.Load<TextAsset>("Data/Json/Skill/Skills");
        //skill3 객체 생성
        _skill3Stat = new Skill3Info();
        //스킬레벨 넣는 변수
        skill3Level = 1;
        //Skill3 플레이어 크기 트렌스폼 찾는 변수(미니Hp바 때문에)
        _skill3PlayerScale = Util.FindChild("Armature", transform).GetComponent<Transform>();

        playerStat = transform.GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    protected void Update()
    {
        KeyboardInput();
        if (_sceneAttackButton == SceneAttackButton.None)
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
                case CreatureState.None:
                    break;
            }
            UpdateAnimation();
        }
        else
        {
            switch (_sceneAttackButton)
            {
                case SceneAttackButton.Rolling:
                    Roll();
                    break;
                case SceneAttackButton.Skill1:
                    Skill1();
                    break;
                case SceneAttackButton.Skill2:
                    Skill2();
                    break;
                case SceneAttackButton.Skill3:
                    Skill3();
                    break;
                case SceneAttackButton.None:
                    break;
            }
            SkillAnimation();
        }

    }
    // 애니메이션을 따로 관리
    protected void UpdateAnimation()
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
            case CreatureState.None:
                break;
        }
    }
    protected void SkillAnimation()
    {
        switch (_sceneAttackButton)
        {
            case SceneAttackButton.Rolling:
                _anim.SetInteger("playerStat", 9);
                break;
            case SceneAttackButton.Skill1:
                break;
            case SceneAttackButton.Skill2:
                _anim.SetInteger("playerStat", 6);
                break;
            case SceneAttackButton.Skill3:
                break;
            case SceneAttackButton.None:
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
        if (_KeyboardInputOnOff == true)
        {
            KeyboardMove();
        }
        else
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
                if (_isSkill1 == false)
                {
                    _tempDir = new Vector3(x, 0, y);
                    _tempDir = Vector3.RotateTowards(transform.forward, _tempDir, Time.deltaTime * _rotationSpeed, 0);
                }
                transform.rotation = Quaternion.LookRotation(_tempDir.normalized);
            }
        }
    }
    public void KeyboardInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _creatureState = CreatureState.Move;
            _KeyboardInputOnOff = true;
        }
        else
        {
            _KeyboardInputOnOff = false;
        }
    }
    public void KeyboardMove()
    {
        float x = 0f;
        float y = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            y += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += 1f;
        }
        _tempVector = new Vector3(x, 0, y);
        _tempVector = _tempVector * Time.deltaTime * _moveSpeed;
        transform.position += _tempVector;

        if (_isSkill1 == false)
        {
            _tempDir = new Vector3(x, 0, y);
            _tempDir = Vector3.RotateTowards(transform.forward, _tempDir, Time.deltaTime * _rotationSpeed, 0);
        }
        transform.rotation = Quaternion.LookRotation(_tempDir.normalized);
        if (_KeyboardInputOnOff == false)
        {
            _creatureState = CreatureState.Idle;
        }
    }
    public void Roll()
    {
        _creatureState = CreatureState.None;
        //현재 스킬의 상태는 나타내는 조건
        if (_isRoll == false)
        {
            //보고 있는 방향을 저장한다.
            _rollDir = _tempDir.normalized;
            _isRoll = true;
            GameManager.Ui._uiSceneAttackButton.RollingButton(true);
        }
        _tempVector = _rollDir;
        _tempVector = _tempVector * Time.deltaTime * _rollSpeed;
        transform.position += _tempVector;
        StartCoroutine(CoRoll());
    }
    protected IEnumerator CoRoll()
    {
        yield return new WaitForSeconds(1f);
        _isRoll = false;
        _sceneAttackButton = SceneAttackButton.None;
        _creatureState = CreatureState.Idle;
    }
    public void Skill1()
    {
        _creatureState = CreatureState.None;
        if (playerStat.Job == Job.Superhuman.ToString())
        {
            if (GameManager.Obj._targetMonster == null)
            {
                GameManager.Ui._uiSceneAttackButton.Skill1Button(false);
                _sceneAttackButton = SceneAttackButton.None;
                _creatureState = CreatureState.Idle;
                return;
            }
            float distance = Vector3.Distance(GameManager.Obj._targetMonster.transform.position, transform.position);
            if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse &&
                _KeyboardInputOnOff == false &&
                _isRoll == false &&
                distance < 2f)
            {
                // 회전
                Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
                tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
                transform.rotation = Quaternion.LookRotation(tempDir.normalized);
                // 코루틴을 이용한 공격딜레이 (대미지 계산)
                if (_coAttack == null && _isSkill1 == false)
                {
                    GameManager.Ui._uiSceneAttackButton.Skill1Button(true);
                    _isSkill1 = true;
                    _coAttack = StartCoroutine(CoSkill1());
                }
            }
            else
            {
                GameManager.Ui._uiSceneAttackButton.Skill1Button(false);
                _sceneAttackButton = SceneAttackButton.None;
                _creatureState = CreatureState.Move;
            }
        }
        else if (playerStat.Job == Job.Cyborg.ToString())
        {
            Vector3 _skill1CyborgDir;
            if (_coAttack == null && _isSkill1 == false)
            {
                _skill1CyborgDir = _tempDir.normalized;
                _tempDir = _skill1CyborgDir;
                _isSkill1 = true;
                GameManager.Ui._uiSceneAttackButton.Skill1Button(true);
                _coAttack = StartCoroutine(CyborgCoSkill1());
            }
            if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
            {
                Move();
                _anim.SetInteger("playerStat", 5);
            }
            else if (_KeyboardInputOnOff == true)
            {
                KeyboardMove();
                _anim.SetInteger("playerStat", 5);
            }
            else
            {
                _anim.SetInteger("playerStat", 2);
            }
        }
    }
    protected IEnumerator CyborgCoSkill1()
    {
        _skill2BoxCollider.enabled = true;
        _skill1FlamethrowerEffect.Play();
        _anim.SetInteger("playerStat", 2);
        yield return new WaitForSeconds(4f);
        _skill2BoxCollider.enabled = false;
        _skill1FlamethrowerEffect.Stop();
        _isSkill1 = false;
        _coAttack = null;
        _sceneAttackButton = SceneAttackButton.None;
        _creatureState = CreatureState.Attack;
    }
    protected IEnumerator CoSkill1()
    {
        _swordEffect.enabled = true;
        _anim.SetInteger("playerStat", 5);
        yield return new WaitForSeconds(0.2f);
        _anim.SetInteger("playerStat", 51);
        yield return new WaitForSeconds(0.2f);
        _anim.SetInteger("playerStat", 52);
        yield return new WaitForSeconds(0.7f);
        _anim.SetInteger("playerStat", 0);
        _swordEffect.enabled = false;
        _isSkill1 = false;
        _coAttack = null;
        _sceneAttackButton = SceneAttackButton.None;
        _creatureState = CreatureState.Attack;
    }
    public void Skill1Event1()
    {
        switch (effectChange)
        {
            case 0:
                _skill1SlashEffect2_1.Play();
                break;
            case 1:
                _skill1SlashEffect2_2.Play();
                break;
            case 2:
                _skill1SlashEffect2_3.Play();
                break;
            case 3:
                _skill1SlashEffect2_4.Play();
                break;

        }
        effectChange++;
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 2);
    }
    public void Skill1Event2()
    {
        _skill1SlashEffect2_5.Play();
        effectChange = 0;
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
    }
    /*public void Skill2()
    {
        _creatureState = CreatureState.None;
        if (playerStat.Job == Job.Cyborg.ToString())
        {
            // 타겟 널 판정
            if (GameManager.Obj._targetMonster == null)
            {
                _creatureState = CreatureState.Idle;
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
        else if (playerStat.Job == Job.Superhuman.ToString())
        {
            if (_isSkill1 == false)
            {
                _skillGround.SetActive(true);
                Transform checkPoint = RaycastInfo();
                Debug.Log(checkPoint);
                if (checkPoint != null)
                {
                    float distance = Vector3.Distance(checkPoint.position, transform.position);

                    Vector3 tempDir = checkPoint.position - transform.position;
                    tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
                    transform.rotation = Quaternion.LookRotation(tempDir.normalized);
                    if (distance < 10f)
                    {
                        _creatureState = CreatureState.Attack;
                    }
                }
            }
        }

    }*/
    IEnumerator CyborgCoSkill2()
    {
        yield return null;
    }
    public void Skill3()
    {
        _creatureState = CreatureState.None;
        if (playerStat.Job == Job.Superhuman.ToString())
        {
            if (_isSkill1 == false && _isSkill3 == false)
            {
                if (_skill3Stat.Skill3Level != skill3Level)
                {
                    Skill3DataLoad();
                }
                StartCoroutine(CoSkill3());
                _sceneAttackButton = SceneAttackButton.None;
                _creatureState = CreatureState.Idle;
            }
        }
    }
    protected IEnumerator CoSkill3()
    {
        _anim.SetInteger("playerStat", 7);
        _skill3PlayerScale.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        _playerStat.Max_Hp += _skill3Stat.Skill3StatMaxHp;
        _playerStat.Hp += _skill3Stat.Skill3StatHp;
        _playerStat.Atk += _skill3Stat.Skill3StatAtk;
        _playerStat.Def += _skill3Stat.Skill3StatDef;
        _skill3GroundEffect.Play();
        _skill3BoosterEffect.Play();
        _isSkill3 = true;
        GameManager.Ui._uiSceneAttackButton.Skill3Button(1);
        yield return new WaitForSeconds(_skill3Stat.Duration);
        _playerStat.Max_Hp -= _skill3Stat.Skill3StatMaxHp;
        if (_playerStat.Max_Hp < _playerStat.Hp)
        {
            _playerStat.Hp = _playerStat.Max_Hp;
        }
        _playerStat.Atk -= _skill3Stat.Skill3StatAtk;
        _playerStat.Def -= _skill3Stat.Skill3StatDef;
        _skill3BoosterEffect.Stop();
        _skill3PlayerScale.transform.localScale = new Vector3(1f, 1f, 1f);
        _isSkill3 = false;
        GameManager.Ui._uiSceneAttackButton.Skill3Button(2);
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

    protected void Attack()
    {
        // 타겟 널 판정
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            // 공격상태인대 타겟이 없을 경우 소드 이펙트를 제거
            SwordEffectOff();
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
    protected IEnumerator CoAttackDelay(float _delay)
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
            SwordEffectOff();
            yield break;
        }
        // 코루틴 초기화
        _coAttack = null;
    }

    // 공격 대미지 계산 + 사운드는 애니메이션 클립 Attack에서 이벤트로 처리
    public void AttackEvent()
    {
        // 대미지 계산
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // 사운드 추가
        GameManager.Sound.SFXPlay("Punch1");
    }

    // 기본공격 이펙트 애니메이션 클립 Attack에서 이벤트로 처리
    public void SwordEffectOn()
    {
        _swordEffect.enabled = true;
    }
    public void SwordEffectOff()
    {
        _swordEffect.enabled = false;
    }

    public void Skill2()
    {
        _creatureState = CreatureState.None;
        float skillDelay = 1.0f;
        float skillSpeed = 10.0f;
        //보고 있는 방향을 저장한다.
        Vector3 skillDir;
        skillDir = _tempDir.normalized;
        _tempVector = skillDir;
        _tempVector = _tempVector * Time.deltaTime * skillSpeed;
        transform.position += _tempVector;

        if (_coAttack == null)
        {
            _coAttack = StartCoroutine(CoSkill2(skillDelay));
        }
    }

    protected IEnumerator CoSkill2(float _delay)
    {
        // 딜레이
        yield return new WaitForSeconds(_delay);

        _coAttack = null;
        _sceneAttackButton = SceneAttackButton.None;
        _creatureState = CreatureState.Idle;
    }
    // Skill2 애니메이션 클립에서 관리
    // 스킬 이펙트 온
    public void Skill2WheelWindOn()
    {
        _skill2WheelWindEffect.Play();
        GameManager.Sound.SFXPlay("Skill2");
    }
    // Skill2 애니메이션 클립에서 관리
    // 스킬 이펙트 오프
    public void Skill2WheelWindOff()
    {
        _skill2WheelWindEffect.Stop();
        // 오브젝트 매니저에있는 스킬공격대상 리스트를 초기화함 >> 다음 스킬에 다시 계산 후 적용
        GameManager.Obj._targetMonstersControllerList = null;
    }
    // Skill2 애니메이션 클립에서 관리
    // 대미지 계산
    public void Skill2Event()
    {
        // 공격 대상을 찾음
        List<MonsterControllerEX> targetList = GameManager.Obj.FindMobListTargets();

        // 광역 스킬에 맞을 대상이 없으면 리턴
        if (targetList == null)
        {
            return;
        }
        else if (targetList.Count < 0)
        {
            return;
        }
        // 대상이 있으면
        else
        {
            // 대미지 계산
            for (int i = 0; i < targetList.Count; i++)
            {
                targetList[i].OnDamaged(_playerStat.Atk, 1);
            }
        }
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
        GameManager.Cam._Vcam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3f;
        yield return new WaitForSeconds(0.3f);
        GameManager.Cam._Vcam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;

    }

    public void ShakeCam(float a, float b)
    {
        CinemachineBasicMultiChannelPerlin perlin =
        GameManager.Cam._Vcam1.GetComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = a;
    }
    public Transform RaycastInfo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RaySkillLocation;
            if (Physics.Raycast(ray, out RaySkillLocation, Mathf.Infinity))
            {
                Debug.Log("레이저");
                if (RaySkillLocation.collider.tag == "SkillGround")
                {
                    Debug.Log("콜라이더");
                    return RaySkillLocation.transform;
                }
            }
        }
        return null;
    }
}