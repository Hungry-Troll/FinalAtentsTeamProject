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
    // ������ ��Ʈ�ѷ�
    public LevelUpController _leveUpController;
    // ���ݿ� �ڷ�ƾ
    protected Coroutine _coAttack;

    // ���ݼӵ� �ڷ�ƾ ���Կ� 
    protected float _attackDelay;

    // ���� ����Ʈ��
    protected TrailRenderer _swordEffect;

    //Skill1 ��ƼŬ��
    protected ParticleSystem _skill1SlashEffect2_1;
    protected ParticleSystem _skill1SlashEffect2_2;
    protected ParticleSystem _skill1SlashEffect2_3;
    protected ParticleSystem _skill1SlashEffect2_4;
    protected ParticleSystem _skill1SlashEffect2_5;
    protected ParticleSystem _skill3GroundEffect;
    protected ParticleSystem _skill3BoosterEffect;
    //Skill2 ��ƼŬ��
    protected ParticleSystem _skill2WheelWindEffect;
    //Skill2 ���ݹ��� ������
    public BoxCollider _skill2BoxCollider;

    //���̺��׿� Skill1 ��ƼŬ��
    ParticleSystem _skill1FlamethrowerEffect;
    //����Ʈ ��ȭ�� ����
    int effectChange;

    //Json ��ų����
    protected TextAsset _skillInfoJson;
    //Skill3 ���� �����
    protected Skill3Info _skill3Stat;
    public int skill3Level;
    //Skill3 �÷��̾� ũ�� Ʈ������ ã�� ����(�̴�Hp�� ������)
    protected Transform _skill3PlayerScale;
    //�÷��̾� ���� ���п�
    PlayerStat playerStat;
    Job playerJob;
    // ���̺��� ��Ÿ�� �ʵ�
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
        // ������ ��Ʈ�ѷ� ����
        _leveUpController = GetComponent<LevelUpController>();
        // ��������Ʈ ����
        Transform tmp = Util.FindChild("SwordEffect", transform);
        _swordEffect = tmp.GetComponent<TrailRenderer>();
        // Skill1 ��ƼŬ ����
        _skill1SlashEffect2_1 = Util.FindChild("Skill1SlashEffect2_1", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_2 = Util.FindChild("Skill1SlashEffect2_2", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_3 = Util.FindChild("Skill1SlashEffect2_3", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_4 = Util.FindChild("Skill1SlashEffect2_4", transform).GetComponent<ParticleSystem>();
        _skill1SlashEffect2_5 = Util.FindChild("Skill1SlashEffect2_5", transform).GetComponent<ParticleSystem>();
        // Skill2 ��ƼŬ ����
        Transform skill2Tr = Util.FindChild("Skill2WheelWindEffect", transform);
        _skill2WheelWindEffect = skill2Tr.GetComponent<ParticleSystem>();
        // Skill2 ��ƼŬ Stop;
        Skill2WheelWindOff();
        // Skill2 ���ݹ����� �ݶ��̴�
        _skill2BoxCollider = skill2Tr.GetComponent<BoxCollider>();
        // Skill3 ��ƼŬ ����
        _skill3GroundEffect = Util.FindChild("Skill3GroundEffect", transform).GetComponent<ParticleSystem>();
        _skill3BoosterEffect = Util.FindChild("Skill3BoosterEffect", transform).GetComponent<ParticleSystem>();
        //��ƼŬ ���� �ȵǰ�
        _skill1SlashEffect2_1.Stop();
        _skill1SlashEffect2_2.Stop();
        _skill1SlashEffect2_3.Stop();
        _skill1SlashEffect2_4.Stop();
        _skill1SlashEffect2_5.Stop();
        _skill3GroundEffect.Stop();
        _skill3BoosterEffect.Stop();

        //���̺��� Skill1 ��ƼŬ ����
        //_skill1FlamethrowerEffect = Util.FindChild("Skill1FlamethrowerEffect", transform).GetComponent<ParticleSystem>();
        //_skill1FlamethrowerEffect.Stop();
        // Skill2 ���ݹ����� �ݶ��̴�
        //_skill2BoxCollider = _skill1FlamethrowerEffect.GetComponent<BoxCollider>();
        //���̺��� ��Ÿ�� ��ų ����
        //_skillGround = Util.FindChild("SkillGround", transform).gameObject;
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
    // �ִϸ��̼��� ���� ����
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
        // ��� �� �̵�
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
        // �Ÿ�
        float distance = Vector3.Distance(GameManager.Obj._targetMonster.transform.position, transform.position);
        // �̵�
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Obj._targetMonster.transform.position, Time.deltaTime * _autoMoveSpeed);

        // ȸ��
        Vector3 tempDir = GameManager.Obj._targetMonster.transform.position - transform.position;
        tempDir = Vector3.RotateTowards(transform.forward, tempDir.normalized, Time.deltaTime * _moveSpeed, 0);
        transform.rotation = Quaternion.LookRotation(tempDir.normalized);

        // �����Ÿ��̻� ��������� ����
        // �÷��̾� ���ȿ� �⺻ �����Ÿ� �߰�? ���� ���� �� ����
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
    protected IEnumerator CoAttackDelay(float _delay)
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
        _creatureState = CreatureState.None;
        float skillDelay = 1.0f;
        float skillSpeed = 10.0f;
        //���� �ִ� ������ �����Ѵ�.
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
        // ������
        yield return new WaitForSeconds(_delay);

        _coAttack = null;
        _sceneAttackButton = SceneAttackButton.None;
        _creatureState = CreatureState.Idle;
    }
    // Skill2 �ִϸ��̼� Ŭ������ ����
    // ��ų ����Ʈ ��
    public void Skill2WheelWindOn()
    {
        _skill2WheelWindEffect.Play();
        GameManager.Sound.SFXPlay("Skill2");
    }
    // Skill2 �ִϸ��̼� Ŭ������ ����
    // ��ų ����Ʈ ����
    public void Skill2WheelWindOff()
    {
        _skill2WheelWindEffect.Stop();
        // ������Ʈ �Ŵ������ִ� ��ų���ݴ�� ����Ʈ�� �ʱ�ȭ�� >> ���� ��ų�� �ٽ� ��� �� ����
        GameManager.Obj._targetMonstersControllerList = null;
    }
    // Skill2 �ִϸ��̼� Ŭ������ ����
    // ����� ���
    public void Skill2Event()
    {
        // ���� ����� ã��
        List<MonsterControllerEX> targetList = GameManager.Obj.FindMobListTargets();

        // ���� ��ų�� ���� ����� ������ ����
        if (targetList == null)
        {
            return;
        }
        else if (targetList.Count < 0)
        {
            return;
        }
        // ����� ������
        else
        {
            // ����� ���
            for (int i = 0; i < targetList.Count; i++)
            {
                targetList[i].OnDamaged(_playerStat.Atk, 1);
            }
        }
    }

    protected void Dead()
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
            StartCoroutine(ShakeCam());
        }
    }
    // ����ġ ���� �Լ�
    public void ExpAdd(int MonsterExp)
    {
        // ����ġ�� ��������Ʈ�ѷ����� ����
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
            Debug.Log("���콺");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RaySkillLocation;
            if (Physics.Raycast(ray, out RaySkillLocation, Mathf.Infinity))
            {
                Debug.Log("������");
                if (RaySkillLocation.collider.tag == "SkillGround")
                {
                    Debug.Log("�ݶ��̴�");
                    return RaySkillLocation.transform;
                }
            }
        }
        return null;
    }
}