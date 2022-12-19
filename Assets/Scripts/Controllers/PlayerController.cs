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

    //������ ����ĳ��Ʈ �������� ��갪
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
    // ������ ��Ʈ�ѷ�
    public LevelUpController _leveUpController;
    // ���ݿ� �ڷ�ƾ
    protected Coroutine _coAttack;
    protected Coroutine _coSkill1;
    protected Coroutine _coSkill2;
    protected Coroutine _coSkill3;
    protected Coroutine _coRoll;

    // ���ݼӵ� �ڷ�ƾ ���Կ� 
    protected float _attackDelay;

    //Skill2 ���ݹ��� ������
    public BoxCollider _skill2BoxCollider;

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

    //���̺��׿� Skill1 ��ƼŬ��
    protected ParticleSystem _skill1FlamethrowerEffect;
    //���̺��׿� Skill2 ��ƼŬ��
    public GameObject _cyborgSkill2;
    public ParticleSystem _skill2FireExplosionEffect;
    public ParticleSystem _skill2FireBigEffect;
    //���̺��׿� Skill2 ��ź ������Ʈ
    protected Transform _bomb;
    //���̺��׿� Skill3 ��ƼŬ��
    public GameObject _cyborgSkill3;
    protected ParticleSystem _skill3Explosion1Effect;
    protected ParticleSystem _skill3Explosion2Effect;
    protected ParticleSystem _skill3Explosion3Effect;
    protected ParticleSystem _skill3Explosion4Effect;
    //������ ���ҽ��� �ҷ��� ��ų ������Ʈ
    public GameObject _scientistSkill1;
    protected ParticleSystem _poison;
    protected ParticleSystem _explosion;
    public GameObject _scientistSkill2;
    protected ParticleSystem _powerDraw;
    protected ParticleSystem _electricity;
    public GameObject _scientistSkill3;
    protected ParticleSystem _powerBeam;
    //������ ��ų1 ���� ������Ʈ
    protected Transform _poisonPortion;

    //����Ʈ ��ȭ�� ����
    protected int effectChange;

    //Json ��ų����
    protected TextAsset _skillInfoJson;
    //Skill3 ���� �����
    protected Skill3InfoTemp _skill3Stat;
    public int skill3Level;
    //Skill3 �÷��̾� ũ�� Ʈ������ ã�� ����(�̴�Hp�� ������)
    protected Transform _skill3PlayerScale;
    //�÷��̾� ���� ���п�
    protected PlayerStat playerStat;
    Job playerJob;

    // Start is called before the first frame update
    protected void Start()
    {
        Init();

        JobStart();
    }

    // Init�Լ��� ��ŸƮ �Լ� ���볻��
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
        // ������ ��Ʈ�ѷ� ����
        _leveUpController = GetComponent<LevelUpController>();

        //��ų ����Ʈ ��ȭ�� ����
        effectChange = 0;
        //��ų �������
        _isRoll = false;
        _isSkill1 = false;
        _isSkill2 = false;
        _isSkill3 = false;
        //��ų���� ���̽����� �ҷ�����
        _skillInfoJson = Resources.Load<TextAsset>("Data/Json/Skill/Skills");
        //skill3 ��ü ����
        _skill3Stat = new Skill3InfoTemp();
        //��ų���� �ִ� ����
        skill3Level = 1;
        //Skill3 �÷��̾� ũ�� Ʈ������ ã�� ����(�̴�Hp�� ������)
        _skill3PlayerScale = Util.FindChild("Armature", transform).GetComponent<Transform>();

        playerStat = transform.GetComponent<PlayerStat>();
    }
    // JobStart�Լ��� �������� �ٸ� Start �Լ�
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
    // �ִϸ��̼��� ���� ����
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
        // ��� �� �̵�
        if (GameManager.Ui._joyStickController._joystickState == JoystickState.InputTrue)
        {
            _creatureState = CreatureState.Move;
        }
    }

    protected void Move()
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
        // ��Ÿ���� ���� 
        //GameManager.Ui._uiSceneAttackButton.RollingButton(true);
        _tempVector = _rollDir;
        _tempVector = _tempVector * Time.deltaTime * _rollSpeed;
        transform.position += _tempVector;

        if (_coRoll == null)
        {
            //���� �ִ� ������ �����Ѵ�.

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

    protected virtual void Attack()
    {
        // Ÿ�� �� ����
        if (GameManager.Obj._targetMonster == null)
        {
            _creatureState = CreatureState.Idle;
            // ���ݻ����δ� Ÿ���� ���� ��� �ҵ� ����Ʈ�� ����
            //SwordEffectOff();
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
    protected virtual IEnumerator CoAttackDelay(float _delay)
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
            //SwordEffectOff();
            yield break;
        }
        // �ڷ�ƾ �ʱ�ȭ
        _coAttack = null;
    }

    // ���� ����� ��� + ����� �ִϸ��̼� Ŭ�� Attack���� �̺�Ʈ�� ó��
    public virtual void AttackEvent()
    {
        // ����� ���
        GameManager.Obj._targetMonsterController.OnDamaged(_playerStat.Atk, 1);
        // ���� �߰�
        GameManager.Sound.SFXPlay("Punch1");
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
        // ī�޶� ��鸮�鼭 ȭ�� ������� �����ϴ� �ڵ� 
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
            Debug.Log("���콺");
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