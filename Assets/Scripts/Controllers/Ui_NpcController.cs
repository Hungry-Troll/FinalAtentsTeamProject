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
        // ��ȭ ���� �Ǿ����� Ȯ���� ����
        bool isConversationEnd = false;
        if (GameManager.Quest._questStroy.Count <= GameManager.Quest._conversationCount)
        {
            isConversationEnd = true;
            GameManager.Quest.QuestConversationEnd();
        }
        // ���� ������ ����� ���� ������? 2����
        switch(GameManager.QuestData._questLevel)
        {
            case 1 :
                // ȭ��ǥ ����
                GameManager.Ui._directionArrowController.OffAllArrows();
                // ��ȭ ���� �� 
                if(isConversationEnd)
                {
                    // ���� ȭ��ǥ ����(�������� ���ϴ� ȭ��ǥ)
                    GameManager.Ui._directionArrowController.OnArrow("ToWesley");
                }
                break;
            case 2 :
                // ��Ʈ ����Ʈ ����
                GameManager.Quest._wesleyController.QuestHeartOff();
                // ȭ��ǥ ����... ��� WesleyController ���� �̹� ������ ��. �����ص� �Ǵ� �ڵ�.
                GameManager.Ui._directionArrowController.OffAllArrows();
                // ���� ���
                GameManager.Quest._wesleyController.OpeningVideoPlay();
                if (isConversationEnd)
                {
                    // ��Ʈ ����Ʈ��
                    GameManager.Quest._wesleyController.QuestHeartOn();
                }
                break;
            case 3 :
                // ȭ��ǥ ����
                GameManager.Ui._directionArrowController.OffAllArrows();
                // ��ȭ ���� �� 
                if(isConversationEnd)
                {
                    // ��Ʈ ����Ʈ ����
                    GameManager.Quest._wesleyController.QuestHeartOff();
                    // ���� ȭ��ǥ ����(���� �� ��Ż�� ���ϴ� ȭ��ǥ)
                    GameManager.Ui._directionArrowController.OnArrow("ToVenice");
                    GameManager.Ui._directionArrowController.OnArrow("TutorialToVillage");
                    // ���Ͻ� ��Ʈ ����Ʈ �ѱ�
                    GameManager.Quest._veniceController.QuestHeartOn();
                }
                break;
            case 4 :
                // ȭ��ǥ ����
                GameManager.Ui._directionArrowController.OffAllArrows();
                // ��ȭ ���� ��
                if(isConversationEnd)
                {
                    // ���� ȭ��ǥ ����(���� �� ��Ż�� ���ϴ� ȭ��ǥ)
                    GameManager.Ui._directionArrowController.OnArrow("VillageToDungeon");
                    // ��� �̸� �˾�(����)
                    GameManager.Ui.PopUpLocation("�����ڵ��� ����");
                    StartCoroutine(GameManager.Ui.ClosePopUpLocation());
                }
                break;
            case 5 :
                // ȭ��ǥ ����
                GameManager.Ui._directionArrowController.OffAllArrows();
                // ��ȭ ���� ��
                if(isConversationEnd)
                {
                    // ���� ȭ��ǥ ����(���� �� ���͵�� ���ϴ� ȭ��ǥ)
                    GameManager.Ui._directionArrowController.OnArrow("ToIceField");
                    // ��� �̸� �˾�(����)
                    GameManager.Ui.PopUpLocation("Ʈ���� ���");
                    StartCoroutine(GameManager.Ui.ClosePopUpLocation());
                }
                break;
            case 6:
                // ȭ��ǥ ����
                GameManager.Ui._directionArrowController.OffAllArrows();
                // ��ȭ ���� ��
                if (isConversationEnd)
                {
                    // ���� ȭ��ǥ ����(�ʿ� �� ���͵�� ���ϴ� ȭ��ǥ)
                    GameManager.Ui._directionArrowController.OnArrow("ToGreenField");
                }
                break;
            case 7:
                // ȭ��ǥ ����
                GameManager.Ui._directionArrowController.OffAllArrows();
                // ��ȭ ���� ��
                if (isConversationEnd)
                {
                    // ���� ȭ��ǥ ����(ȭ������ �������ͷ� ���ϴ� ȭ��ǥ)
                    GameManager.Ui._directionArrowController.OnArrow("ToVolcanicField");
                }
                break;
            case 9:
                if( isConversationEnd)
                {
                    // ui ����
                    GameManager.Ui.UISetActiveFalse();
                    // ����Ʈ â �ݱ�
                    GameManager.Quest.QuestInfoActive(false);
                    // ����
                    GameManager.Quest.Ending();
                }
                break;
        }

        GameManager.Quest.QuestConversationText(GameManager.Quest._conversationCount);
        GameManager.Quest.QuestConversationCountAdd();
    }
}
