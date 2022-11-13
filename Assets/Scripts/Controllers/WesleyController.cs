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
    //다이얼로그 숫자 용도
    public int _dialogCount;
    public Text WesleyDialog0;
    public Text WesleyDialog1;
    string StrWesleyDialog0;
    string StrWesleyDialog1;
    char[] ArrOfWesleyDialog1;

    void Awake()
    {
        WesleyAnimator = WesleyPrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
    }

    private void Start()
    {
        WesleyDialog0.text = string.Empty;
        WesleyDialog1.text = string.Empty;
        StrWesleyDialog0 = "도와주세요!!!";
        StrWesleyDialog1 = "구해주셔서 고맙습니다.";
        char[] ArrOfWesleyDialog0 = StrWesleyDialog0.ToCharArray();
        ArrOfWesleyDialog1 = StrWesleyDialog1.ToCharArray();
        // 다이얼로그 숫자 변수 초기화
        _dialogCount = 0;
        // 모든 UI 끔
        GameManager.Ui.UISetActiveFalse();
        DialogsOfWesleyPanel[0].SetActive(true);
        _dialogCount++;
        StartCoroutine(WesleyDialog0Coroutine(ArrOfWesleyDialog0));
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
                if (RayStruct.collider.tag == "Wesley" && _dialogCount == 1)
                {
                    WesleyAnimator.SetTrigger("MeetPlayer");
                    DialogsOfWesleyPanel[_dialogCount].SetActive(true);
                    StartCoroutine(WesleyDialog1Coroutine(ArrOfWesleyDialog1));
                    // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
                    // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.                   
                    capsuleCollider.enabled = false;
                    // 모든 UI 끔
                    GameManager.Ui.UISetActiveFalse();
                    // 추가 대화를 추후 만들어야 됨
                    // 무전기 추가 필요
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
        // 장소 팝업
        GameManager.Ui.PopUpLocation("본 아일랜드");
        // 팝업 close
        StartCoroutine(GameManager.Ui.ClosePopUpLocation());
    }

    // 회상씬 비디오
    public void OpeningVideoPlay()
    {
        for (int i = 0; i < DialogsOfWesleyPanel.Length; i++)
        {
            DialogsOfWesleyPanel[i].SetActive(false);
        }
        capsuleCollider.enabled = true;
        GameManager.Create.CreateUi("UI_TutorialVideo", gameObject);
    }

    IEnumerator WesleyDialog0Coroutine(char[] _Arr)
    {
        for (int i = 0; i < _Arr.Length; i++)
        {
            WesleyDialog0.text += _Arr[i];
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator WesleyDialog1Coroutine(char[] _Arr)
    {
        WesleyDialog1.text = string.Empty;
        for (int i = 0; i < _Arr.Length; i++)
        {
            WesleyDialog1.text += _Arr[i];
            yield return new WaitForSeconds(0.2f);
        }
    }
}