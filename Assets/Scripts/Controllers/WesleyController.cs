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

    void Awake()
    {
        WesleyAnimator = WesleyPrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
    }

    private void Start()
    {
        // ��� UI ��
        GameManager.Ui.UISetActiveFalse();
        DialogsOfWesleyPanel[0].SetActive(true);
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
                if (RayStruct.collider.tag == "Wesley")
                {
                    WesleyAnimator.SetTrigger("MeetPlayer");
                    DialogsOfWesleyPanel[1].SetActive(true);

                    // BGM ����
                    GameManager.Sound.BGMPlay("-kpop_release-");
                    // �ݶ��̴��� ��Ȱ��ȭ�ϴ� ������ �� ���� �����
                    // ù ��° ��ȭâ�� �ִ� ���¿��� ������ Ŭ���غ��� �� �� ����.                   
                    capsuleCollider.enabled = false;
                    // ��� UI ��
                    GameManager.Ui.UISetActiveFalse();
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
}