using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 대화에 쓰이는 UI는 판넬 안에 텍스트와 버튼을 미리 제작한 원본을 따로 만들고
// 그것을 복제해서 써야 문제가 없이 진행할 수 있음.
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
        // 모든 UI 끔
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
                // 태그 설정은 프리팹이 아니라 프리팹 안의 woman-metalhead_Rig를 이용할 것.
                if (RayStruct.collider.tag == "Wesley")
                {
                    WesleyAnimator.SetTrigger("MeetPlayer");
                    DialogsOfWesleyPanel[1].SetActive(true);

                    // BGM 변경
                    GameManager.Sound.BGMPlay("-kpop_release-");
                    // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
                    // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.                   
                    capsuleCollider.enabled = false;
                    // 모든 UI 끔
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
        // 모든 UI 킴
        GameManager.Ui.UISetActiveTrue();
    }
}