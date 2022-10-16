using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ��ȭ�� ���̴� UI�� �ǳ� �ȿ� �ؽ�Ʈ�� ��ư�� �̸� ������ ������ ���� �����
// �װ��� �����ؼ� ��� ������ ���� ������ �� ����.
public class InteracWithVenice : MonoBehaviour
{
    Animator VeniceAnimator;
    public GameObject VenicePrefab;
    public GameObject Shop;
    public GameObject[] DialogOfVenicePanels;
    CapsuleCollider capsuleCollider;
    
    void Awake()
    {
        VeniceAnimator = VenicePrefab.GetComponent<Animator>();
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
                // �±� ������ �������� �ƴ϶� ������ ���� man-samurai-black_Rig�� �̿��� ��.
                if (RayStruct.collider.tag == "Merchant")
                {
                    VeniceAnimator.SetTrigger("MeetPlayer");
                    // �ݶ��̴��� ��Ȱ��ȭ�ϴ� ������ �� ���� �����
                    // ù ��° ��ȭâ�� �ִ� ���¿��� ������ Ŭ���غ��� �� �� ����.                   
                    DialogOfVenicePanels[0].SetActive(true);
                    capsuleCollider.enabled = false;
                    VeniceAnimator.SetInteger("restoreInt", 1);
                };
            }
        }
    }

    // �� �����δ� ȸ�Ƿ� ��� ���� ���� �����ص� �Ǵ� �κ�
    public void TalkWithVenice()
    {
        DialogOfVenicePanels[0].SetActive(false);
        VeniceAnimator.SetTrigger("Talk");
        DialogOfVenicePanels[1].SetActive(true);
        VeniceAnimator.SetInteger("restoreInt", 1);
    }
    public void BackToDialog0()
    {
        DialogOfVenicePanels[1].SetActive(false);
        DialogOfVenicePanels[0].SetActive(true);
    }
    public void OpenShop()
    {
        Shop.SetActive(true);
    }
    public void CloseShop()
    {
        Shop.SetActive(false);
    }
    public void EndToTalkWithVenice()
    {
        DialogOfVenicePanels[0].SetActive(false);
        capsuleCollider.enabled = true;
    }
}
