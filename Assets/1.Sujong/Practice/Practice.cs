using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Practice : MonoBehaviour
{
    Animator VeniceAnimator;
    public GameObject VenicePrefab;
    public string[] VeniceDialogs;
    public GameObject man_guy_Rig;
    public Text DialogText;
    public GameObject VenicePanel;

    void Awake()
    {
        VeniceAnimator = VenicePrefab.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayStruct;
            if (Physics.Raycast(ray, out RayStruct, 10f))
            {
                // �±� ������ �������� �ƴ϶� ������ ���� rig�� �̿��� ��.
                if (RayStruct.collider.tag == "Merchant")
                {
                    VeniceAnimator.SetTrigger("MeetPlayer");
                    VenicePanel.SetActive(true);
                    DialogText.text = VeniceDialogs[0];
                    // �ݶ��̴��� ��Ȱ��ȭ�ϴ� ������ �� ���� �����
                    // ù ��° ��ȭâ�� �ִ� ���¿��� ������ Ŭ���غ��� �� �� ����.
                    CapsuleCollider collderOfMan_guy_Rig =
                        man_guy_Rig.gameObject.GetComponent<CapsuleCollider>();
                    collderOfMan_guy_Rig.enabled = false;
                    VeniceAnimator.SetInteger("restoreInt", 1);
                };
            }
        }
    }
}
