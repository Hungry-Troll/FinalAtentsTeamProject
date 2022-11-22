using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ��ȭ�� ���̴� UI�� �ǳ� �ȿ� �ؽ�Ʈ�� ��ư�� �̸� ������ ������ ���� �����
// �װ��� �����ؼ� ��� ������ ���� ������ �� ����.
public class VeniceController : MonoBehaviour
{
    Animator VeniceAnimator;
    public GameObject VenicePrefab;
    public GameObject Shop;
    public GameObject[] DialogOfVenicePanels;
    CapsuleCollider capsuleCollider;
    public Text VeniceDialog0;
    public Text VeniceDialog1;
    string StrVeniceDialog0;
    string StrVeniceDialog1;
    char[] ArrOfStrVeniceDialog0;
    char[] ArrOfStrVeniceDialog1;


    void Awake()
    {
        VeniceAnimator = VenicePrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
    }

    private void Start()
    {
        StrVeniceDialog0 = string.Empty;
        StrVeniceDialog1 = string.Empty;
        StrVeniceDialog0 = "�ȳ��ϼ���. ������ ���͵帱���?";
        StrVeniceDialog1 = "�ֺ��� ������ ������ ���ƿ�. �����ϼ���.";
        ArrOfStrVeniceDialog0 = StrVeniceDialog0.ToCharArray();
        ArrOfStrVeniceDialog1 = StrVeniceDialog1.ToCharArray();
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
                    StartCoroutine(VeniceDialog0Coroutine(ArrOfStrVeniceDialog0));
                    capsuleCollider.enabled = false;
                    VeniceAnimator.SetInteger("restoreInt", 1);
                    // ��� UI ��
                    GameManager.Ui.UISetActiveFalse();
                };
            }
        }
    }

    IEnumerator VeniceDialog0Coroutine(char[] _Arr)
    {
        VeniceDialog0.text = string.Empty;
        for (int i = 0; i < _Arr.Length; i++)
        {            
            VeniceDialog0.text += _Arr[i];
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(VeniceDialog0Coroutine(_Arr));
    }

    IEnumerator VeniceDialog1Coroutine(char[] _Arr)
    {
        VeniceDialog1.text = string.Empty;
        for (int i = 0; i < _Arr.Length; i++)
        {             
            VeniceDialog1.text += _Arr[i];
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(VeniceDialog1Coroutine(_Arr));
    }

    // �� �����δ� ȸ�Ƿ� ��� ���� ���� �����ص� �Ǵ� �κ�
    public void TalkWithVenice()
    {
        DialogOfVenicePanels[0].SetActive(false);
        VeniceAnimator.SetTrigger("Talk");
        DialogOfVenicePanels[1].SetActive(true);
        StartCoroutine(VeniceDialog1Coroutine(ArrOfStrVeniceDialog1));
        VeniceAnimator.SetInteger("restoreInt", 1);
    }
    public void BackToDialog0()
    {
        DialogOfVenicePanels[1].SetActive(false);
        DialogOfVenicePanels[0].SetActive(true);
        StartCoroutine(VeniceDialog0Coroutine(ArrOfStrVeniceDialog0));
    }
    public void OpenShop()
    {
        Shop.SetActive(true);
        // �κ��丮 Ŵ
        GameManager.Ui.InventoryOpen();
    }
    public void CloseShop()
    {
        Shop.SetActive(false);
        GameManager.Ui.InventoryClose();
    }
    public void EndToTalkWithVenice()
    {
        DialogOfVenicePanels[0].SetActive(false);
        capsuleCollider.enabled = true;

        // ��� UI Ŵ
        GameManager.Ui.UISetActiveTrue();

        CloseShop();
    }
}
