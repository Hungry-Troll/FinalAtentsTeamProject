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
        // ���ӿ�����Ʈ
        //Transform goTr = Util.FindChild("WesleyObjects", transform);
        //GameObject whesleyObject = goTr.gameObject;

        // ������ ��ȭâ ����
        Transform dialogTr = Util.FindChild("WesleyDialog0", transform);
        GameManager.Quest._conversationInfo = dialogTr.gameObject;

        // ������ ��ȭâ �̸� ����
        Transform NameTr = Util.FindChild("NameText", transform);
        GameManager.Quest._conversationCharacterNameText = NameTr.GetComponent<Text>();

        // ������ ��ȭâ ���� �ؽ�Ʈ ����
        Transform ContentTr = Util.FindChild("ContentText", transform);
        GameManager.Quest._conversationText = ContentTr.GetComponent<Text>();

        // ��ȭâ ��ư ����
        Transform buttonTr = Util.FindChild("WesleyButton", transform);
        _WesleyButton = buttonTr.GetComponent<Button>();
        _WesleyButton.onClick.AddListener(NextConversation);
    }


    // ��ȭ ��ư �������
    public void NextConversation()
    {
        // ����
        // ���� �ٸ� _conversationCount �� ���������� ��� ��ȭ�� �ؾߵǰ�
        // _conversationCount�� ������ ��ȭ�� �׸� �ϰ� UI�� �Ѿߵ�
        if (GameManager.Quest._questStroy.Count <= GameManager.Quest._conversationCount)
        {
            GameManager.Quest.QuestConversationEnd();
        }
        // ���� ������ ����� ���� ������? 2����
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
