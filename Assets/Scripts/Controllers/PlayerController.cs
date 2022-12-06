using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using SimpleJSON;

public class PlayerController : MonoBehaviour
{
    Vector2 _inputDir;
    Vector3 _tempVector;
    Vector3 _tempDir;
    Vector3 _rollVecter;
    Vector3 _rollDir;
    public float _moveSpeed;
    public float _rotationSpeed;
    public float _rollSpeed;
    public float _autoMoveSpeed;
    public CreatureState _creatureState;
    public SceneAttackButton _sceneAttackButton;
    public Animator _anim;
    public PlayerStat _playerStat;
    bool _KeyboardInputOnOff;
    bool _isRoll;
    bool _isSkill1;
    bool _isSkill3;

    // 공격용 코루틴
    Coroutine _coAttack;

    // 공격속도 코루틴 대입용 
    float _attackDelay;

    // 공격 이펙트용
    TrailRenderer _swordEffect;

    //Skill1 파티클용
    ParticleSystem _skill1SlashEffect1_1;
    ParticleSystem _skill1SlashEffect1_2;
    ParticleSystem _skill1SlashEffect1_3;
    ParticleSystem _skill1SlashEffect1_4;
    ParticleSystem _skill1SlashEffect1_5;
    ParticleSystem _skill1SlashEffect2_1;
    ParticleSystem _skill1SlashEffect2_2;
    ParticleSystem _skill1SlashEffect2_3;
    ParticleSystem _skill1SlashEffect2_4;
    ParticleSystem _skill1SlashEffect2_5;
    ParticleSystem _skill3GroundEffect;
    ParticleSystem _skill3BoosterEffect;
    //이펙트 변화용 변수
    int effectChange;

    //Json 스킬정보
    TextAsset _skillInfoJson;
    //Skill3 정보 저장용
    Skill3Info _skill3Stat;
    public int skill3Level;
    //Skill3 플레이어 크기 트렌스폼 찾는 변수(미니Hp바 때문에)
    Transform _skill3PlayerScale;
    // Start is called before the first frame update
    private void Start()
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
        // 공격이펙트 연결
        Transform tmp = Util.FindChild("SwordEffect", transform);
        _swordEffect = tmp.GetComponent<TrailRenderer>();
        // Skill1 파티클 연결
        _skill1SlashEffect1_1 = Util.FindChild("Skill1SlashEffect1_1", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect1_2 = Util.FindChild("Skill1SlashEffect1_2", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect1_3 = Util.FindChild("Skill1SlashEffect1_3", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect1_4 = Util.FindChild("Skill1SlashEffect1_4", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect1_5 = Util.FindChild("Skill1SlashEffect1_5", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_1 = Util.FindChild("Skill1SlashEffect2_1", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_2 = Util.FindChild("Skill1SlashEffect2_2", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_3 = Util.FindChild("Skill1SlashEffect2_3", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_4 = Util.FindChild("Skill1SlashEffect2_4", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_5 = Util.FindChild("Skill1SlashEffect2_5", transform).GetComponent<ParticleSystem>();
        // Skill3 파티클 연결
        _skill3GroundEffect = Util.FindChild("Skill3GroundEffect", transform).GetComponent<ParticleSystem>();
        _skill3BoosterEffect = Util.FindChild("Skill3BoosterEffect", transform).GetComponent<ParticleSystem>();
        //파티클 실행 안되게
        _skill1SlashEffect1_1.Stop();
        _skill1SlashEffect1_2.Stop();
        _skill1SlashEffect1_3.Stop();
        _skill1SlashEffect1_4.Stop();
        _skill1SlashEffect1_5.Stop();
        _skill1SlashEffect2_1.Stop();
        _skill1SlashEffect2_2.Stop();
        _skill1SlashEffect2_3.Stop();
        _skill1SlashEffect2_4.Stop();
        _skill1SlashEffect2_5.Stop();
        _skill3GroundEffect.Stop();
        _skill3BoosterEffect.Stop();
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
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("키보드 onoff"+_KeyboardInputOnOff);
        Debug.Log("일반행동" +_creatureState);
        Debug.Log("스킬행동" +_sceneAttackButton);
        Debug.Log("조이스틱"+GameManager.Ui._joyStickController._joystickState);
        if (_sceneAttackButton == SceneAttackButton.None)
        {
            KeyboardInput();
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
    private void UpdateAnimation()
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
    private void SkillAnimation()
    {
        switch (_sceneAttackButton)
        {
            case SceneAttackButton.Rolling:
                _anim.SetInteger("playerStat", 9);
                break;
            case SceneAttackButton.Skill1:
                break;
            case SceneAttackButton.Skill2:
                break;
            case SceneAttackButton.Skill3:
                break;
            case SceneAttackButton.None:
                break;
        }
    }

    private void Idle()
    {
        // 대기 중 이동
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    private void Move()
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
                _tempDir = new Vector3(x, 0, y);
                _tempDir = Vector3.RotateTowards(transform.forward, _tempDir, Time.deltaTime * _rotationSpeed, 0);
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
        _tempDir = new Vector3(x, 0, y);

        _tempVector = _tempVector * Time.deltaTime * _moveSpeed;
        transform.position += _tempVector;

        _tempDir = Vector3.RotateTowards(transform.forward, _tempDir, Time.deltaTime * _rotationSpeed, 0);
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
    IEnumerator CoRoll()
    {
        yield return new WaitForSeconds(1f);
        _isRoll = false;
        _sceneAttackButton = SceneAttackButton.None;
        _creatureState = CreatureState.Idle;
    }
    public void Skill1()
    {
        _creatureState = CreatureState.None;
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
    IEnumerator CoSkill1()
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
    public void Skill3()
    {
        _creatureState = CreatureState.None;
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
    IEnumerator CoSkill3()
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
    private void AutoMove()
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
        if (distance < 2.0f)
        {
            _creatureState = CreatureState.Attack;
        }
    }

    private void Attack()
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
    IEnumerator CoAttackDelay(float _delay)
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

    private void Dead()
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
        }
    }
}