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

    void Awake()
    {
        WesleyAnimator = WesleyPrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
    }

    private void Start()
    {       
        // ���̾�α� ���� ���� �ʱ�ȭ
        _dialogCount = 0;
        // ��� UI ��
        GameManager.Ui.UISetActiveFalse();
        DialogsOfWesleyPanel[0].SetActive(true);
        _dialogCount++;
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
}