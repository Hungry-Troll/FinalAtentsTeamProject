using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithWesley : MonoBehaviour
{
    Animator WesleyAnimator;
    public GameObject WesleyPrefab;
    public string[] WesleyDialogsArr;
    public GameObject WesleyPanel;
    CapsuleCollider capsuleCollider;
    public TextMeshProUGUI WesleyText;

    void Awake()
    {
        WesleyAnimator = WesleyPrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayStruct;
            if (Physics.Raycast(ray, out RayStruct, 7f))
            {
                // �±� ������ �������� �ƴ϶� ������ ���� rig�� �̿��� ��.
                if (RayStruct.collider.tag == "Wesley")
                {
                    WesleyAnimator.SetTrigger("MeetPlayer");
                    WesleyPanel.SetActive(true);
                    WesleyText.text = WesleyDialogsArr[0];
                    // �ݶ��̴��� ��Ȱ��ȭ�ϴ� ������ �� ���� �����
                    // ù ��° ��ȭâ�� �ִ� ���¿��� ������ Ŭ���غ��� �� �� ����.                   
                    capsuleCollider.enabled = false;
                };
            }
        }
    }

    public void TalkWithWesley()
    {
        WesleyAnimator.SetTrigger("Talk");
        WesleyText.text = WesleyDialogsArr[1];
    }

    public void EndToTalkWithWesley()
    {
        WesleyPanel.SetActive(false);
        capsuleCollider.enabled = true;
    }
}