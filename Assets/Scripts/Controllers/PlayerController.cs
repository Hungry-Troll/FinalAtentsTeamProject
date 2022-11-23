using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    Vector2 _inputDir;
    Vector3 rollVecter;
    Vector3 rollDir;
    public float _moveSpeed;
    public float _rollSpeed;
    public float _autoMoveSpeed;
    public CreatureState _creatureState;
    public Animator _anim;
    public PlayerStat _playerStat;
    bool KeyboardInputOnOff;
    bool isRoll;

    // 공격용 코루틴
    Coroutine _coAttack;

    // 공격속도 코루틴 대입용 
    float _attackDelay;

    // 공격 이펙트용
    TrailRenderer _swordEffect;

    // Start is called before the first frame update
    private void Start()
    {
        _moveSpeed = 2.5f;
        _rollSpeed = 5f;
        _creatureState = CreatureState.Idle;
        _anim = GetComponent<Animator>();
        _autoMoveSpeed = _moveSpeed + 2.0f;
        _playerStat = GetComponent<PlayerStat>();
        _attackDelay = 1.0f;
        // 공격이펙트 연결
        Transform tmp = Util.FindChild("SwordEffect", transform);
        //_swordEffect = tmp.GetComponent<TrailRenderer>();
        isRoll = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(_creatureState);
        if (CreatureState.Roll != _creatureState)
        {
            KeyboardInput();
        }

        switch (_creatureState)
        {
            case CreatureState.Idle:
                Idle();
                break;
            case CreatureState.Move:
                Move();
                break;
            case CreatureState.KeyboardMove:
                KeyboardMove(false);
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
            case CreatureState.Roll:
                Debug.Log(100);
                KeyboardMove(true);
                StartCoroutine(Roll());
                break;
            case CreatureState.None:
                break;
        }
        UpdateAnimation();
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
            case CreatureState.KeyboardMove:
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
            case CreatureState.Roll:
                _anim.SetInteger("playerStat", 9);
                break;
            case CreatureState.None:
                break;
        }
    }

    private void Idle()
    {
        // 대기 중 이동
        if(GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    private void Move()
    {
        // 이동 중 대기
        if(GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            _creatureState = CreatureState.Idle;
        }   
        _inputDir = GameManager.Ui._joyStickController.inputDirection;
        // Debug.Log("플레이어 : " + _inputDir);
        bool isMove = _inputDir.magnitude != 0;
        //if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        if(isMove)
        {
            // 이동
            float x =_inputDir.x;
            float y =_inputDir.y;
            Vector3 tempVector = new Vector3(x, 0, y);
            tempVector = tempVector * Time.deltaTime * _moveSpeed;
            transform.position += tempVector;
            // 회전
            Vector3 tempDir = new Vector3(x, 0, y);
            tempDir = Vector3.RotateTowards(transform.forward, tempDir, Time.deltaTime * _moveSpeed,0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
        }
    }
    public void KeyboardInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _creatureState = CreatureState.KeyboardMove;
            KeyboardInputOnOff = true;
        }
        else
        {
            KeyboardInputOnOff = false;
        }
    }
    public void KeyboardMove(bool rollSkill)
    {
        Debug.Log(101);
        if (KeyboardInputOnOff == true || rollSkill == true)
        {
            Debug.Log(102);
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
            Vector3 tempVector = new Vector3(x, 0, y);
            Vector3 tempDir = new Vector3(x, 0, y);
            if (rollSkill == true)
            {
                Debug.Log(103);
                if (isRoll == false)
                {
                    Debug.Log(104);
                    rollVecter = tempVector.normalized;
                    rollDir = tempDir.normalized;
                    _moveSpeed = 5f;
                    isRoll = true;
                }
                tempVector = rollVecter;
                tempDir = rollDir;
            }
            tempVector = tempVector * Time.deltaTime * _moveSpeed;
            transform.position += tempVector;

            tempDir = Vector3.RotateTowards(transform.forward, tempDir, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
        }
        else if (KeyboardInputOnOff == false || rollSkill == false)
        {
            _creatureState = CreatureState.Idle;
        }

    }
    IEnumerator Roll()
    {
        Debug.Log(105);
        yield return new WaitForSeconds(1f);
        _moveSpeed = 2.5f;
        isRoll = false;
        _creatureState = CreatureState.Idle;
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
        if(distance < 2.0f)
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
            return;
        }

        if(GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            // 회전
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // 코루틴을 이용한 공격딜레이 (대미지 계산)
            if(_coAttack == null)
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
            yield break;
        }
        // 코루틴 초기화
        _coAttack = null;
    }

    // 공격 대미지 계산 + 사운드는 애니메이션 클립 Attack에서 이벤트로 처리
    public void AttackEvent()
    {
        // 대미지 계산
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk);
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
        if (isRoll == true)
        {
            _playerStat.Hp -= monsterAtk * 0;
        }
        else
        {
            // 대미지 받는 함수
            _playerStat.Hp -= monsterAtk - _playerStat.Def;
            if (_playerStat.Hp <= 0)
            {
                // HP가 -20,-40 이러면 이상하므로 고정
                _playerStat.Hp = 0;
                _creatureState = CreatureState.Dead;
            }
        }
    }
}
