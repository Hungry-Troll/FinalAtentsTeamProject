using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ��ȭ�� ���̴� UI�� �ǳ� �ȿ� �ؽ�Ʈ�� ��ư�� �̸� ������ ������ ���� �����
// �װ��� �����ؼ� ��� ������ ���� ������ �� ����.
public class InteractWithWesley : MonoBehaviour
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
                    DialogsOfWesleyPanel[0].SetActive(true);
                    // �ݶ��̴��� ��Ȱ��ȭ�ϴ� ������ �� ���� �����
                    // ù ��° ��ȭâ�� �ִ� ���¿��� ������ Ŭ���غ��� �� �� ����.                   
                    capsuleCollider.enabled = false;
                };
            }
        }
    }

    public void EndToTalkWithWesley()
    {
        DialogsOfWesleyPanel[0].SetActive(false);
        capsuleCollider.enabled = true;
    }
}