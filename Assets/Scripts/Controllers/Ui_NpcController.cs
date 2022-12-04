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
        if (GameManager.Quest._questStroy.Count <= GameManager.Quest._conversationCount)
        {
            GameManager.Quest.QuestConversationEnd();
        }
        // 조건 동영상 재생을 위한 레벨은? 2레벨
        switch(GameManager.QuestData._questLevel)
        {
            case 1 :
                break;
            case 2 :
                GameManager.Quest._wesleyController.OpeningVideoPlay();
                break;
            case 3 :
                break;
            case 4 :
                break;
            case 5 :
                break;
            case 6:
                break;
            case 7:
                break;
        }

        GameManager.Quest.QuestConversationText(GameManager.Quest._conversationCount);
        GameManager.Quest.QuestConversationCountAdd();
    }
}
