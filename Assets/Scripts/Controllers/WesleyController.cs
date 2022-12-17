using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Ui_Npc 프리팹 컨트롤러와 연계해서 퀘스트를 진행하게됨
// 
public class WesleyController : MonoBehaviour
{
    Animator WesleyAnimator;
    public GameObject WesleyPrefab;
    public GameObject DialogsOfWesleyPanel;
    CapsuleCollider capsuleCollider;

    // 대화창용 변수
    public Text WesleyDialog0;
    // 대화용 코루틴 변수 (버그제거용)
    public Coroutine _coTalk;

    ParticleSystem _heart;

    void Awake()
    {
        WesleyAnimator = WesleyPrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
        // 퀘스트 매니저에서 웨슬리 컨트롤러를 들고있음
        GameManager.Quest._wesleyController = GetComponent<WesleyController>();
        _coTalk = null;
        // 퀘스트 하트 연결
        _heart = Util.FindChild("QuestHeart", transform).GetComponent<ParticleSystem>();
        // 퀘스트 하트 끔
        QuestHeartOff();
    }

    private void Start()
    {
        // 씬에 따라서 퀘스트 시작이 다르게
        switch (GameManager.Scene._sceneNameEnum)
        {
            case Define.SceneName.Tutorial:
                GameManager.Quest.QuestStart(GameManager.QuestData._questLevel); //1레벨 퀘스트 시작
                break;
            case Define.SceneName.Village02:
                RadioQuest();
                break;
            case Define.SceneName.DunGeon:
                // 던전맵은 전투 후 퀘스트 진행이 필요함
                RadioQuest();
                // 퀘스트 값 가지고 옴
                // GameManager.Quest.QuestDataGetValue(GameManager.QuestData._questLevel);
                // 대입 // 퀘스트 목표량
                //GameManager.Quest.QuestInfoText();
                break;
        }
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
                // 타겟이 웨슬리이고 , 퀘스트 목표치를 달성했을 경우
                if (RayStruct.collider.tag == "Wesley" && GameManager.Quest._questProgressValue >= GameManager.Quest._questObjectiveScore)
                {
                    // 퀘스트 레벨에 따른 행동 분류
                    switch (GameManager.QuestData._questLevel)
                    {
                        // 퀘스트 레벨1
                        case 1:
                            TutorialQuest();
                            // 함수 끝나면 퀘스트 레벨2
                            // 첫 번째 몬스터 죽이고 웨슬리 클릭했을 때 웨슬리 방향 화살표 꺼주기
                            GameManager.Ui._directionArrowController.OffAllArrows();
                            break;
                        case 2:
                            TutorialQuest();
                            GameManager.Quest.TutorialDoorOpen();
                            // 함수 끝나면 퀘스트 레벨3
                            break;
                    }
                }
            }
        }
    }

    public void EndToTalkWithWesley()
    {
        capsuleCollider.enabled = true;
        // 모든 UI 킴
        GameManager.Ui.UISetActiveTrue();
        // 장소 팝업
        // 처음 게임 시작시에만 우선 나오게 설정
        if ((GameManager.QuestData._questLevel == 1))
        {
            GameManager.Ui.PopUpLocation("본 아일랜드");
            // 팝업 close
            StartCoroutine(GameManager.Ui.ClosePopUpLocation());
        }
        else
        {
            // 웨슬리 NPC 가상카메라 OFF
            GameManager.Cam.WeleyCamOff();
        }
    }

    // 회상씬 비디오
    public void OpeningVideoPlay()
    {
        capsuleCollider.enabled = true;
        GameObject tutorialVideo = GameManager.Create.CreateUi("UI_TutorialVideo", gameObject);
        // 스킵 버튼에 비디오 오브젝트 넘겨주기
        GameManager.Ui._skipButtonController.UI_TutorialVideo = tutorialVideo;

        // 웨슬리 NPC 가상카메라 OFF
        GameManager.Cam.WeleyCamOff();
    }

    public IEnumerator WesleyDialog0Coroutine(char[] _Arr)
    {
        for (int i = 0; i < _Arr.Length; i++)
        {
            WesleyDialog0.text += _Arr[i];
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(WesleyDialog0Coroutine(_Arr));
    }

    public void WeleyDialogStart(string conversationText)
    {
        // 이전 텍스트 초기화
        WesleyDialog0.text = string.Empty;
        if(_coTalk != null)
        {
            StopCoroutine(_coTalk);
        }
        // 텍스트 변환
        char[] ArrOfWesleyDialog0 = conversationText.ToCharArray();
        // 코루틴실행
        _coTalk = StartCoroutine(WesleyDialog0Coroutine(ArrOfWesleyDialog0));
    }

    // 듀토리얼맵 퀘스트
    public void TutorialQuest()
    {
        WesleyAnimator.SetTrigger("MeetPlayer");
        // 퀘스트 완료
        GameManager.Quest.QuestCompletion();
        // 새로운 퀘스트 시작
        GameManager.Quest.QuestStart(GameManager.QuestData._questLevel);
        // 웨슬리 NPC 가상카메라 ON
        GameManager.Cam.WeleyCamOn();
        // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
        // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.                   
        capsuleCollider.enabled = false;
        // 듀토리얼 2레벨 퀘스트 진행을 위한 임시 코드
        if (GameManager.QuestData._questLevel == 2)
        {
            GameManager.Quest.Property_QuestProgressValue++;
        }
    }

    // 무전기 퀘스트
    public void RadioQuest()
    {
        // 퀘스트 완료
        GameManager.Quest.QuestCompletion();
        // 새로운 퀘스트 시작
        GameManager.Quest.QuestStart(GameManager.QuestData._questLevel);
    }

    public void QuestHeartOn()
    {
        _heart.Play();
    }

    public void QuestHeartOff()
    {
        _heart.Stop();
    }
}