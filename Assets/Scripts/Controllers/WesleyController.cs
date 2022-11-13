using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ��ȭ�� ���̴� UI�� �ǳ� �ȿ� �ؽ�Ʈ�� ��ư�� �̸� ������ ������ ���� �����
// �װ��� �����ؼ� ��� ������ ���� ������ �� ����.
public class WesleyController : MonoBehaviour
{
    Animator WesleyAnimator;
    public GameObject WesleyPrefab;
    public GameObject[] DialogsOfWesleyPanel;
    CapsuleCollider capsuleCollider;
    //���̾�α� ���� �뵵
    public int _dialogCount;
    public Text WesleyDialog0;
    public Text WesleyDialog1;
    string StrWesleyDialog0;
    string StrWesleyDialog1;
    char[] ArrOfWesleyDialog1;

    void Awake()
    {
        WesleyAnimator = WesleyPrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
    }

    private void Start()
    {
        WesleyDialog0.text = string.Empty;
        WesleyDialog1.text = string.Empty;
        StrWesleyDialog0 = "�����ּ���!!!";
        StrWesleyDialog1 = "�����ּż� �����ϴ�.";
        char[] ArrOfWesleyDialog0 = StrWesleyDialog0.ToCharArray();
        ArrOfWesleyDialog1 = StrWesleyDialog1.ToCharArray();
        // ���̾�α� ���� ���� �ʱ�ȭ
        _dialogCount = 0;
        // ��� UI ��
        GameManager.Ui.UISetActiveFalse();
        DialogsOfWesleyPanel[0].SetActive(true);
        _dialogCount++;
        StartCoroutine(WesleyDialog0Coroutine(ArrOfWesleyDialog0));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayStruct;
            if (Physics.Raycast(ray, out RayStruct, Mathf.Infinity))
            {
                // �±� ������ �������� �ƴ϶� ������ ���� woman-metalhead_Rig�� �̿��� ��.
                if (RayStruct.collider.tag == "Wesley" && _dialogCount == 1)
                {
                    WesleyAnimator.SetTrigger("MeetPlayer");
                    DialogsOfWesleyPanel[_dialogCount].SetActive(true);
                    StartCoroutine(WesleyDialog1Coroutine(ArrOfWesleyDialog1));
                    // �ݶ��̴��� ��Ȱ��ȭ�ϴ� ������ �� ���� �����
                    // ù ��° ��ȭâ�� �ִ� ���¿��� ������ Ŭ���غ��� �� �� ����.                   
                    capsuleCollider.enabled = false;
                    // ��� UI ��
                    GameManager.Ui.UISetActiveFalse();
                    // �߰� ��ȭ�� ���� ������ ��
                    // ������ �߰� �ʿ�
                };
            }
        }
    }

    public void EndToTalkWithWesley()
    {
        for (int i = 0; i < DialogsOfWesleyPanel.Length; i++)
        {
            DialogsOfWesleyPanel[i].SetActive(false);
        }
        capsuleCollider.enabled = true;
        // ��� UI Ŵ
        GameManager.Ui.UISetActiveTrue();
        // ��� �˾�
        GameManager.Ui.PopUpLocation("�� ���Ϸ���");
        // �˾� close
        StartCoroutine(GameManager.Ui.ClosePopUpLocation());
    }

    // ȸ��� ����
    public void OpeningVideoPlay()
    {
        for (int i = 0; i < DialogsOfWesleyPanel.Length; i++)
        {
            DialogsOfWesleyPanel[i].SetActive(false);
        }
        capsuleCollider.enabled = true;
        GameManager.Create.CreateUi("UI_TutorialVideo", gameObject);
    }

    IEnumerator WesleyDialog0Coroutine(char[] _Arr)
    {
        for (int i = 0; i < _Arr.Length; i++)
        {
            WesleyDialog0.text += _Arr[i];
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator WesleyDialog1Coroutine(char[] _Arr)
    {
        WesleyDialog1.text = string.Empty;
        for (int i = 0; i < _Arr.Length; i++)
        {
            WesleyDialog1.text += _Arr[i];
            yield return new WaitForSeconds(0.2f);
        }
    }
}