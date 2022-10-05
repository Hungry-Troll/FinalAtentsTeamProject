using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteracWithVenice : MonoBehaviour
{
    Animator VeniceAnimator;
    public GameObject VenicePrefab;
    public string[] VeniceDialogs;
    public GameObject Shop;
    public GameObject VenicePanel;
    CapsuleCollider capsuleCollider;
    public TextMeshProUGUI VeniceText;
    
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
            if (Physics.Raycast(ray, out RayStruct, 7f))
            {
                // 태그 설정은 프리팹이 아니라 프리팹 안의 rig을 이용할 것.
                if (RayStruct.collider.tag == "Merchant")
                {
                    VeniceAnimator.SetTrigger("MeetPlayer");
                    VenicePanel.SetActive(true);
                    VeniceText.text = VeniceDialogs[0];
                    // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
                    // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.                   
                    capsuleCollider.enabled = false;
                    VeniceAnimator.SetInteger("restoreInt", 1);
                };
            }
        }
    }

    public void TalkWithVenice()
    {
        VeniceAnimator.SetTrigger("Talk");
        VeniceText.text = VeniceDialogs[1];
        VeniceAnimator.SetInteger("restoreInt", 1);
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
        VenicePanel.SetActive(false);
        capsuleCollider.enabled = true;
    }
}
