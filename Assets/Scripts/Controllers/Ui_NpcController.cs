using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class Ui_NpcController : MonoBehaviour
{
    public Button _WesleyButton;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Quest._ui_NpcController = GetComponent<Ui_NpcController>();

        // 게임오브젝트
        //Transform goTr = Util.FindChild("WesleyObjects", transform);
        //GameObject whesleyObject = goTr.gameObject;

        // 웨슬리 대화창 연결
        Transform dialogTr = Util.FindChild("WesleyDialog0", transform);
        GameManager.Quest._conversationInfo = dialogTr.gameObject;

        // 웨슬리 대화창 이름 연결
        Transform NameTr = Util.FindChild("NameText", transform);
        GameManager.Quest._conversationCharacterNameText = NameTr.GetComponent<Text>();

        // 웨슬리 대화창 내용 텍스트 연결
        Transform ContentTr = Util.FindChild("ContentText", transform);
        GameManager.Quest._conversationText = ContentTr.GetComponent<Text>();

        // 대화창 버튼 연결
        Transform buttonTr = Util.FindChild("WesleyButton", transform);
        _WesleyButton = buttonTr.GetComponent<Button>();
        _WesleyButton.onClick.AddListener(NextConversation);
    }


    // 대화 버튼 누를경우
    public void NextConversation()
    {
        // 조건
        // 만약 다른 _conversationCount 이 남아있으면 계속 대화를 해야되고
        // _conversationCount이 없으면 대화를 그만 하고 UI를 켜야됨
        // 대화 종료 되었는지 확인할 변수
        bool isConversationEnd = false;
        if (GameManager.Quest._questStroy.Count <= GameManager.Quest._conversationCount)
        {
            isConversationEnd = true;
            GameManager.Quest.QuestConversationEnd();
        }
        // 조건 동영상 재생을 위한 레벨은? 2레벨
        switch(GameManager.QuestData._questLevel)
        {
            case 1 :
                // 화살표 끄기
                GameManager.Ui._directionArrowController.OffAllArrows();
                // 대화 종료 후 
                if(isConversationEnd)
                {
                    // 방향 화살표 생성(웨슬리로 향하는 화살표)
                    GameManager.Ui._directionArrowController.OnArrow("ToWesley");
                }
                break;
            case 2 :
                // 하트 이펙트 끄기
                GameManager.Quest._wesleyController.QuestHeartOff();
                // 화살표 끄기... 사실 WesleyController 에서 이미 꺼놨을 것. 삭제해도 되는 코드.
                GameManager.Ui._directionArrowController.OffAllArrows();
                // 비디오 재생
                GameManager.Quest._wesleyController.OpeningVideoPlay();
                if (isConversationEnd)
                {
                    // 하트 이펙트온
                    GameManager.Quest._wesleyController.QuestHeartOn();
                }
                break;
            case 3 :
                // 화살표 끄기
                GameManager.Ui._directionArrowController.OffAllArrows();
                // 대화 종료 후 
                if(isConversationEnd)
                {
                    // 하트 이펙트 끄기
                    GameManager.Quest._wesleyController.QuestHeartOff();
                    // 방향 화살표 생성(마을 맵 포탈로 향하는 화살표)
                    GameManager.Ui._directionArrowController.OnArrow("ToVenice");
                    GameManager.Ui._directionArrowController.OnArrow("TutorialToVillage");
                    // 베니스 하트 이펙트 켜기
                    GameManager.Quest._veniceController.QuestHeartOn();
                }
                break;
            case 4 :
                // 화살표 끄기
                GameManager.Ui._directionArrowController.OffAllArrows();
                // 대화 종료 후
                if(isConversationEnd)
                {
                    // 방향 화살표 생성(던전 맵 포탈로 향하는 화살표)
                    GameManager.Ui._directionArrowController.OnArrow("VillageToDungeon");
                    // 장소 이름 팝업(마을)
                    GameManager.Ui.PopUpLocation("도망자들의 마을");
                    StartCoroutine(GameManager.Ui.ClosePopUpLocation());
                }
                break;
            case 5 :
                // 화살표 끄기
                GameManager.Ui._directionArrowController.OffAllArrows();
                // 대화 종료 후
                if(isConversationEnd)
                {
                    // 방향 화살표 생성(설원 맵 몬스터들로 향하는 화살표)
                    GameManager.Ui._directionArrowController.OnArrow("ToIceField");
                    // 장소 이름 팝업(던전)
                    GameManager.Ui.PopUpLocation("트라이 산맥");
                    StartCoroutine(GameManager.Ui.ClosePopUpLocation());
                }
                break;
            case 6:
                // 화살표 끄기
                GameManager.Ui._directionArrowController.OffAllArrows();
                // 대화 종료 후
                if (isConversationEnd)
                {
                    // 방향 화살표 생성(초원 맵 몬스터들로 향하는 화살표)
                    GameManager.Ui._directionArrowController.OnArrow("ToGreenField");
                }
                break;
            case 7:
                // 화살표 끄기
                GameManager.Ui._directionArrowController.OffAllArrows();
                // 대화 종료 후
                if (isConversationEnd)
                {
                    // 방향 화살표 생성(화산지대 보스몬스터로 향하는 화살표)
                    GameManager.Ui._directionArrowController.OnArrow("ToVolcanicField");
                }
                break;
            case 9:
                if( isConversationEnd)
                {
                    // ui 끄기
                    GameManager.Ui.UISetActiveFalse();
                    // 퀘스트 창 닫기
                    GameManager.Quest.QuestInfoActive(false);
                    // 엔딩
                    GameManager.Quest.Ending();
                }
                break;
        }

        GameManager.Quest.QuestConversationText(GameManager.Quest._conversationCount);
        GameManager.Quest.QuestConversationCountAdd();
    }
}
