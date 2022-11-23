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

    // ���ݿ� �ڷ�ƾ
    Coroutine _coAttack;

    // ���ݼӵ� �ڷ�ƾ ���Կ� 
    float _attackDelay;

    // ���� ����Ʈ��
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
        // ��������Ʈ ����
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
    // �ִϸ��̼��� ���� ����
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
        // ��� �� �̵�
        if(GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    private void Move()
    {
        // �̵� �� ���
        if(GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            _creatureState = CreatureState.Idle;
        }   
        _inputDir = GameManager.Ui._joyStickController.inputDirection;
        // Debug.Log("�÷��̾� : " + _inputDir);
        bool isMove = _inputDir.magnitude != 0;
        //if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        if(isMove)
        {
            // �̵�
            float x =_inputDir.x;
            float y =_inputDir.y;
            Vector3 tempVector = new Vector3(x, 0, y);
            tempVector = tempVector * Time.deltaTime * _moveSpeed;
            transform.position += tempVector;
            // ȸ��
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
        // �Ÿ�
        float distance = Vector3.Distance(GameManager.Obj._targetMonster.transform.position, transform.position);
        // �̵�
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Obj._targetMonster.transform.position, Time.deltaTime * _autoMoveSpeed);
        
        // ȸ��
        Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
        tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
        transform.rotation = Quaternion.LookRotation(tempDir.normalized);

        // �����Ÿ��̻� ��������� ����
        if(distance < 2.0f)
        {
            _creatureState = CreatureState.Attack;
        }
    }

    private void Attack()
    {
        // Ÿ�� �� ����
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            return;
        }

        if(GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            // ȸ��
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // �ڷ�ƾ�� �̿��� ���ݵ����� (����� ���)
            if(_coAttack == null)
            {
                _coAttack = StartCoroutine(CoAttackDelay(_attackDelay));
            }
        }
        // ���� �� �̵�
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    // ���� ������ ��� (�ִϸ��̼� �����̸�)
    IEnumerator CoAttackDelay(float _delay)
    {
        // ������
        yield return new WaitForSeconds(_delay);
        // ����� ���
        // GameManager.Ui._targetMonsterStat.Hp -= _playerStat.Atk - GameManager.Ui._targetMonsterStat.Def;
        // ����� ����� ���ͽ�ũ��Ʈ���� ó�� >> �÷��̾� ���ݷ¸� �Ѱ���
        // ��üũ
        if (GameManager.Obj._targetMonster == null)
        {
            yield break;
        }
        // �ڷ�ƾ �ʱ�ȭ
        _coAttack = null;
    }

    // ���� ����� ��� + ����� �ִϸ��̼� Ŭ�� Attack���� �̺�Ʈ�� ó��
    public void AttackEvent()
    {
        // ����� ���
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk);
        // ���� �߰�
        GameManager.Sound.SFXPlay("Punch1");
    }

    // �⺻���� ����Ʈ �ִϸ��̼� Ŭ�� Attack���� �̺�Ʈ�� ó��
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
        // Ÿ�� �� ����
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            return;
        }

        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            // ȸ��
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // �ڷ�ƾ�� �̿��� ���ݵ����� (����� ���)
            if (_coAttack == null)
            {
                _coAttack = StartCoroutine(CoAttackDelay(_attackDelay));
            }
        }
        // ���� �� �̵�
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    private void Dead()
    {
        // �÷��̾ ������ ������ ���� ���� ��� ��
        GameManager.Ui.GameOverUI();
        // ��� ������ ��
        GameManager.Ui.UISetActiveFalse();
        // ����� ����
        GameManager.Sound.BGMPlay("31");
        // ȿ���� ����
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
            // ����� �޴� �Լ�
            _playerStat.Hp -= monsterAtk - _playerStat.Def;
            if (_playerStat.Hp <= 0)
            {
                // HP�� -20,-40 �̷��� �̻��ϹǷ� ����
                _playerStat.Hp = 0;
                _creatureState = CreatureState.Dead;
            }
        }
    }
}
