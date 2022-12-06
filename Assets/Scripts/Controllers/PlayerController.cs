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

    // ���ݿ� �ڷ�ƾ
    Coroutine _coAttack;

    // ���ݼӵ� �ڷ�ƾ ���Կ� 
    float _attackDelay;

    // ���� ����Ʈ��
    TrailRenderer _swordEffect;

    //Skill1 ��ƼŬ��
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
    //����Ʈ ��ȭ�� ����
    int effectChange;

    //Json ��ų����
    TextAsset _skillInfoJson;
    //Skill3 ���� �����
    Skill3Info _skill3Stat;
    public int skill3Level;
    //Skill3 �÷��̾� ũ�� Ʈ������ ã�� ����(�̴�Hp�� ������)
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
        // ��������Ʈ ����
        Transform tmp = Util.FindChild("SwordEffect", transform);
        _swordEffect = tmp.GetComponent<TrailRenderer>();
        // Skill1 ��ƼŬ ����
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
        // Skill3 ��ƼŬ ����
        _skill3GroundEffect = Util.FindChild("Skill3GroundEffect", transform).GetComponent<ParticleSystem>();
        _skill3BoosterEffect = Util.FindChild("Skill3BoosterEffect", transform).GetComponent<ParticleSystem>();
        //��ƼŬ ���� �ȵǰ�
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
        //��ų ����Ʈ ��ȭ�� ����
        effectChange = 0;
        //��ų �������
        _isRoll = false;
        _isSkill1 = false;
        _isSkill3 = false;
        //��ų���� ���̽����� �ҷ�����
        _skillInfoJson = Resources.Load<TextAsset>("Data/Json/Skill/Skills");
        //skill3 ��ü ����
        _skill3Stat = new Skill3Info();
        //��ų���� �ִ� ����
        skill3Level = 1;
        //Skill3 �÷��̾� ũ�� Ʈ������ ã�� ����(�̴�Hp�� ������)
        _skill3PlayerScale = Util.FindChild("Armature", transform).GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Ű���� onoff"+_KeyboardInputOnOff);
        Debug.Log("�Ϲ��ൿ" +_creatureState);
        Debug.Log("��ų�ൿ" +_sceneAttackButton);
        Debug.Log("���̽�ƽ"+GameManager.Ui._joyStickController._joystickState);
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
        // ��� �� �̵�
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
            // �̵� �� ���
            if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputFalse)
            {
                _creatureState = CreatureState.Idle;
            }
            _inputDir = GameManager.Ui._joyStickController.inputDirection;
            // Debug.Log("�÷��̾� : " + _inputDir);
            bool isMove = _inputDir.magnitude != 0;
            //if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
            if (isMove)
            {
                // �̵�
                float x = _inputDir.x;
                float y = _inputDir.y;
                _tempVector = new Vector3(x, 0, y);
                _tempVector = _tempVector * Time.deltaTime * _moveSpeed;
                transform.position += _tempVector;
                // ȸ��
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
        //���� ��ų�� ���´� ��Ÿ���� ����
        if (_isRoll == false)
        {
            //���� �ִ� ������ �����Ѵ�.
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
            // ȸ��
            Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
            tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
            transform.rotation = Quaternion.LookRotation(tempDir.normalized);
            // �ڷ�ƾ�� �̿��� ���ݵ����� (����� ���)
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
        // �Ÿ�
        float distance = Vector3.Distance(GameManager.Obj._targetMonster.transform.position, transform.position);
        // �̵�
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Obj._targetMonster.transform.position, Time.deltaTime * _autoMoveSpeed);

        // ȸ��
        Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
        tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
        transform.rotation = Quaternion.LookRotation(tempDir.normalized);

        // �����Ÿ��̻� ��������� ����
        if (distance < 2.0f)
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
            // ���ݻ����δ� Ÿ���� ���� ��� �ҵ� ����Ʈ�� ����
            SwordEffectOff();
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
            // ���ݻ����δ� Ÿ���� ���� ��� �ҵ� ����Ʈ�� ����
            SwordEffectOff();
            yield break;
        }
        // �ڷ�ƾ �ʱ�ȭ
        _coAttack = null;
    }

    // ���� ����� ��� + ����� �ִϸ��̼� Ŭ�� Attack���� �̺�Ʈ�� ó��
    public void AttackEvent()
    {
        // ����� ���
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
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
        if (_isRoll == true)
        {
            _playerStat.Hp -= monsterAtk * 0;
        }
        else
        {
            // ����� �޴� �Լ�
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
                // HP�� -20,-40 �̷��� �̻��ϹǷ� ����
                _playerStat.Hp = 0;
                _creatureState = CreatureState.Dead;
            }
        }
    }
}