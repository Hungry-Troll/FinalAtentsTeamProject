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
                    // 웨슬리 NPC 가상카메라 ON
                    GameManager.Cam.WeleyCamOn();
                    StartCoroutine(WesleyDialog1Coroutine(ArrOfWesleyDialog1));
                    // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
                    // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.                   
                    capsuleCollider.enabled = false;
                    // 모든 UI 끔
                    GameManager.Ui.UISetActiveFalse();
                    // 방향 화살표 UI 끔
                    // UISetActiveFalse() 에 포함시키지 않은 것은 별개로 작동해야 할 때가 있기 때문
                    GameManager.Ui._directionArrowController.OffAllArrows();
                    // 추가 대화를 추후 만들어야 됨1
                    // 무전기 추가 필요
                    //GameManager.Cine.vCam2.gameObject.SetActive(true);
                }
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
        // 웨슬리 방향 화살표 UI 켬
        GameManager.Ui._directionArrowController.OnArrow("ToWesley");
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

        // 비디오 시작하면 상인쪽 NPC 로 안내하는 화살표 생성
        GameManager.Ui._directionArrowController.OnArrow("ToVenice");
        // 마을로 가는 포탈 방향 화살표 생성
        // 일단 여기 만들지만 상인 퀘스트가 있다면 그쪽 이벤트에서 아래 코드 실행
        GameManager.Ui._directionArrowController.OnArrow("TutorialToVillage");

        // 웨슬리 NPC 가상카메라 OFF
        GameManager.Cam.WeleyCamOff();
    }

    IEnumerator WesleyDialog0Coroutine(char[] _Arr)
    {
        for (int i = 0; i < _Arr.Length; i++)
        {
            WesleyDialog0.text += _Arr[i];
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(WesleyDialog0Coroutine(_Arr));
    }

    IEnumerator WesleyDialog1Coroutine(char[] _Arr)
    {
        WesleyDialog1.text = string.Empty;
        //GameManager.Cine.vCam2.gameObject.SetActive(true);
        for (int i = 0; i < _Arr.Length; i++)
        {
            WesleyDialog1.text += _Arr[i];
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(WesleyDialog1Coroutine(_Arr));
    }
}