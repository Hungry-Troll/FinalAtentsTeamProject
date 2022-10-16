using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 대화에 쓰이는 UI는 판넬 안에 텍스트와 버튼을 미리 제작한 원본을 따로 만들고
// 그것을 복제해서 써야 문제가 없이 진행할 수 있음.
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
                // 태그 설정은 프리팹이 아니라 프리팹 안의 man-samurai-black_Rig를 이용할 것.
                if (RayStruct.collider.tag == "Merchant")
                {
                    VeniceAnimator.SetTrigger("MeetPlayer");
                    // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
                    // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.                   
                    DialogOfVenicePanels[0].SetActive(true);
                    capsuleCollider.enabled = false;
                    VeniceAnimator.SetInteger("restoreInt", 1);
                };
            }
        }
    }

    // 이 밑으로는 회의로 어떻게 만들 건지 결정해도 되는 부분
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
