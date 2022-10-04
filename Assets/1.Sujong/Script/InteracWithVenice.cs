using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteracWithVenice : MonoBehaviour
{
    Animator VeniceAnimator;
    public GameObject VenicePrefab;
    public GameObject[] MerDialogs;
    public GameObject man_guy_Rig;
    public GameObject Shop;

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
            if (Physics.Raycast(ray, out RayStruct, 7f))
            {
                // 태그 설정은 프리팹이 아니라 프리팹 안의 rig을 이용할 것.
                if (RayStruct.collider.tag == "Merchant")
                {
                    VeniceAnimator.SetTrigger("MeetPlayer");
                    MerDialogs[0].SetActive(true);
                    // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
                    // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.
                    CapsuleCollider collderOfMan_guy_Rig = 
                        man_guy_Rig.gameObject.GetComponent<CapsuleCollider>();
                    collderOfMan_guy_Rig.enabled = false;
                    VeniceAnimator.SetInteger("restoreInt", 1);
                };
            }
        }
    }

    public void TalkWithMer()
    {
        VeniceAnimator.SetTrigger("Talk");
        MerDialogs[0].SetActive(false);
        MerDialogs[1].SetActive(true);
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

    public void MoveToFirstCon()
    {
        MerDialogs[1].SetActive(false);
        MerDialogs[0].SetActive(true);
    }

    public void EndToTalkWithMer()
    {
        MerDialogs[0].SetActive(false);
        CapsuleCollider collderOfMan_guy_Rig = man_guy_Rig.gameObject.GetComponent<CapsuleCollider>();
        collderOfMan_guy_Rig.enabled = true;
    }
}
