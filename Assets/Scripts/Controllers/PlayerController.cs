using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    Vector2 _inputDir;
    public float _moveSpeed;
    public float _autoMoveSpeed;
    public CreatureState _creatureState;
    public Animator _anim;
    // Start is called before the first frame update
    private void Start()
    {
        _moveSpeed = 2.5f;
        _creatureState = CreatureState.Idle;
        _anim = GetComponent<Animator>();
        _autoMoveSpeed = _moveSpeed + 2.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        switch(_creatureState)
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
                break;
            case CreatureState.None:
                break;
        }
    }

    private void Idle()
    {
        _anim.SetInteger("playerStat", 0);
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
            _anim.SetInteger("playerStat", 1);
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

    private void AutoMove()
    {
        _anim.SetInteger("playerStat", 1);
        // 거리
        float distance = Vector3.Distance(GameManager.Ui.targetMonster.transform.position, transform.position);
        // 이동
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Ui.targetMonster.transform.position, Time.deltaTime * _autoMoveSpeed);
        
        // 회전
        Vector3 tempDir = GameManager.Ui.targetMonster.transform.position - transform.position;
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
        if(GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
        {
            _anim.SetInteger("playerStat", 2);
            // 회전
            Vector3 tempDir = GameManager.Ui.targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
        }
        // 공격 중 이동
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }
}
