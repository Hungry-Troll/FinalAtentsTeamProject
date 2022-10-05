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
                // 태그 설정은 프리팹이 아니라 프리팹 안의 rig을 이용할 것.
                if (RayStruct.collider.tag == "Wesley")
                {
                    WesleyAnimator.SetTrigger("MeetPlayer");
                    WesleyPanel.SetActive(true);
                    WesleyText.text = WesleyDialogsArr[0];
                    // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
                    // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.                   
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