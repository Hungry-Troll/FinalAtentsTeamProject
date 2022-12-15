using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI : UI ��� ����� ���� ��
using UnityEngine.UI;
// SceneManagement : �� ��ȯ�� ���� ��
using UnityEngine.SceneManagement;

public class TurnOnTheStage_Pet : MonoBehaviour
{
    // pet �ִϸ��̼� ���� List
    //private List<Animator> _animatorList1;
    private Animator[] _animatorList;

    // �� �� �ִϸ��̼�
    public Animator _pet1Ani;
    public Animator _pet2Ani;
    public Animator _pet3Ani;

    // �� �� ���� ���� �迭
    private GameObject[] _petInfoArr;

    private bool _bTurnLeft = false;
    private bool _bTurnRight = false;
    private Quaternion _turn = Quaternion.identity;

    // ����
    public static int _characterNum = 0;
    int _value = 0;

    private void Awake()
    {
        // �ִϸ����� ����Ʈ, �ִϸ����� �ʱ�ȭ
        //_animatorList1 = new List<Animator>();
        _animatorList = new Animator[3];

        // �ִϸ����� ����Ʈ�� �� �ִϸ����� ���� �߰�
        _animatorList[0] = _pet1Ani;
        _animatorList[1] = _pet2Ani;
        _animatorList[2] = _pet3Ani;

        _petInfoArr = GameObject.FindGameObjectsWithTag("Pet_Info");
    }

    private void Start()
    {
        // ���� �ʱ�ȭ�մϴ�.
        _turn.eulerAngles = new Vector3(0, _value, 0);
    }


    private void Update()
    {
        if (_bTurnLeft)
        {
            Debug.Log("Left");
            _characterNum++;

            // ���� 4 -> 3
            if (_characterNum == 3)
            {
                _characterNum = 0;
            }

            // ���� -> 120
            // ������ 90�� ���ϴ�.
            _value -= 120;

            // �ο� ������ ����մϴ�.
            _bTurnLeft = false;
        }

        if (_bTurnRight)
        {
            Debug.Log("Right");
            _characterNum--;

            if (_characterNum == -1)
            {
                // ���� 3 -> 2
                _characterNum = 2;
            }

            // ���� -> 120
            // ������ 90�� ���մϴ�.
            _value += 120;

            // �ο� ������ ����մϴ�.
            _bTurnRight = false;
        }

        // ������ ��ϴ�.
        _turn.eulerAngles = new Vector3(0, _value, 0);

        // �����ϴ�.
        transform.rotation = Quaternion.Slerp(transform.rotation, _turn, Time.deltaTime * 5.0f);

        // ������ �길 �����̵��� �������� �ִϸ��̼� off
        // ������ �� ������ �������� SetActive
        for (int i = 0; i < 3; i++)
        {
            if (i == _characterNum)
            {
                Debug.Log(_animatorList[i].name);
                //�� ���ÿ� string �Ѱ���

                GameManager.Select._petName = _animatorList[i].name;

                // ���õ� ���� active true
                _petInfoArr[i].SetActive(true);

                // ���õ� �� �ִϸ��̼� Idle ���·� ��ȯ
                _animatorList[i].SetInteger("state", 1);
            }
            else
            {
                // ���� x ����� active false
                _petInfoArr[i].SetActive(false);

                // ���� x ��� �ִϸ��̼� x
                _animatorList[i].SetInteger("state", 0);

            }

        }
    }

    public void TurnLeft()
    {
        // �ٸ� ��ư�� �������� ��Ʈ��
        _bTurnLeft = true;
        _bTurnRight = false;
    }

    public void TurnRight()
    {
        // �ٸ� ��ư�� �������� ��Ʈ��
        _bTurnRight = true;
        _bTurnLeft = false;
    }

    public void TurnStage()
    {
        // �������� ��ȯ�� ���� �Լ�
        // SceneManager.LoadScene("OnTheStage");
        GameManager.Scene.LoadScene("Tutorial");
    }
}
