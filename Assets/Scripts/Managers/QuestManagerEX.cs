using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class QuestManagerEX
{
    // ����Ʈ �Ŵ������� ������ �ִ� csv ���� ������ ������� �ӽ� ����
    QuestDataEX _questData;
    //����Ʈ�� �Ϸᰡ �Ǹ� ����Ʈ�� ��Ƽ� ����
    List<QuestDataEX> _questCompletionList = new List<QuestDataEX>();

    // ���� ��ҷ� �Ѿ�� �ϱ����� ���ӿ�����Ʈ
    public BoxCollider[] _boxCollider;
    public NavMeshObstacle[] _navMeshObstacle;


    // ��ųʸ����� ������� �����͸� ���� �� ����
    //����Ʈ ����
    int _questLevel;
    //����Ʈ Ÿ��Ʋ
    string _questTile;
    //����Ʈ ��ǥ�̸�
    string _questObjectiveName;
    //����Ʈ ��ǥġ
    public int _questObjectiveScore;
    //����Ʈ ����
    int _reward;
    [HideInInspector]
    //����Ʈ ���丮 ����Ʈ
    public List<string> _questStroy;

    // ���� �Ŵ������� ����� ����

    //��ǥġ
    public int _questProgressValue;

    //��ǥġ�� �ٲ𶧸��� �Լ��� �����ؼ� ���� ��ǥġ ����
    public int Property_QuestProgressValue
    {
        get
        {
            return _questProgressValue;
        }
        set
        {
            _questProgressValue = value;
            QuestInfoText();
        }
    }

    //���� ����Ʈ ��ȭ ī��Ʈ
    public int _conversationCount;

    public int Property_ConversationCount
    {
        get
        {
            return _conversationCount;
        }
        set
        {
            _conversationCount = value;
            //QuestConversationCountAdd();
        }
    }
    /// <summary>
    /// ������ ��Ʈ�ѷ� ���� / ��ȭâ / NPC �̸�
    /// </summary>
    /// 
    // ����Ʈ ���� NPC�� ��������Ʈ�ѷ��� ����Ʈ �Ŵ������� ������ ����
    [HideInInspector]
    public WesleyController _wesleyController;
    // ����Ʈ ���� NPC�� ������ ����
    [HideInInspector]
    public Ui_NpcController _ui_NpcController;

    // ����Ʈ ��ȭâ
    public GameObject _conversationInfo;
    // ����Ʈ ĳ���� �̸� �ؽ�Ʈ (NPC �̸� / ���ΰ� �̸�)
    public Text _conversationCharacterNameText;
    // ��ȭ �ؽ�Ʈ
    public Text _conversationText;
    // ��ȭ ��ư
    public Button _conversationButton;


    /// <summary>
    /// ����Ʈ ����â 
    /// </summary>
    /// 
    // ����Ʈ Ÿ��Ʋâ
    public GameObject _questInfo;
    // ����Ʈ Ÿ��Ʋ �ؽ�Ʈ
    public Text _questTitleText;
    // ����Ʈ ��ǥ�� �ؽ�Ʈ
    public Text _questObjectivesText;

    /// <summary>
    /// ����Ʈ ���â
    /// </summary>
    // 
    public GameObject _questWarningText;


    public void Init()
    {
        // ����Ʈ�� ��ֹ� ����
        _boxCollider = new BoxCollider[2];
        _navMeshObstacle = new NavMeshObstacle[2];
    }

    // ����Ʈ ���ۿ� �ʿ��� ù ������ �Լ�
    // ���� �ʿ� �Ѿ� ���� ��� questLevel ��ġ�� �־��ָ� �ǹǷ� save�� QuestManager._questController._questLevel �� �����ϸ� ����Ʈ������ �̾ �� �� ����.
    public void QuestReset(int questLevel)
    {
        //���丮���� ������ �� ���ӿ� ������ ����1�� ����Ʈ�� ������ �ȴ�.
        _questLevel = questLevel;
        _questProgressValue = 0;
        _conversationCount = 0;
    }

    public void QuestStart(int questLevel)
    {
        _questData = new QuestDataEX();
        QuestReset(GameManager.QuestData._questLevel);
        // ����Ʈ ��ȭâ ����
        ConversationInfoActive(true);
        // ��ȭâ ���� UI ��
        GameManager.Ui.UISetActiveFalse();
        // �� ������ ��
        QuestDataGetValue(GameManager.QuestData._questLevel);
        // ���� // ����Ʈ ��ǥ��
        QuestInfoText();
        // ����Ʈ ��ȭ
        QuestConversationText(Property_ConversationCount);
        // ����Ʈ ���� ��ȭ �غ�
        QuestConversationCountAdd();
    }


    // QuestManagerEX�� ������ ��ųʸ����� �����͸� ������ ���� �Լ�
    public void QuestDataGetValue(int questLevel)
    {
        if(GameManager.QuestData._questDict.TryGetValue(questLevel, out _questData))
        {
            // ����Ʈ ����
            _questLevel = _questData.property_QuestLevel;
            // ����Ʈ Ÿ��Ʋ
            _questTile = _questData.property_QuestTitle;
            // ����Ʈ ��ǥ�̸�
            _questObjectiveName = _questData.property_QuestObjectiveName;
            // ����Ʈ ��ǥġ
            _questObjectiveScore = _questData.property_QuestObjectiveScore;
            // ����Ʈ ����
            _reward = _questData.property_QuestReward;
            // ����Ʈ ���丮
            _questStroy = _questData.property_Stroy;
        }
    }

    // ����Ʈ ��ȭ ��ŵ �Լ�
    public void QuestConvesationSkip()
    {
        Property_ConversationCount += 2;
    }

    // ����Ʈ ��ȭ üũ �Լ�
    public void QuestConversationCountAdd()
    {
        Property_ConversationCount += 2;
    }

    // ����Ʈ �����Ȳ ���� �Լ�
    public void QuestProgressValueAdd()
    {
        Property_QuestProgressValue++;
    }

    // ����Ʈ ��ǥ�� �Լ� >> ��ǥġ �޼��ϸ� ����
    public void QuestInfoText()
    {
        // ����Ʈ ��ǥ �̸�
        _questObjectivesText.text = _questObjectiveName + " " + Property_QuestProgressValue + " / <color=red>" + _questObjectiveScore + "</color>";
        // ����Ʈ Ÿ��Ʋ
        _questTitleText.text = _questTile;

        //  ����Ʈ ���ھ ��ǥġ�� �����Ȳ���� �۰ų� ������
        switch (GameManager.Scene._sceneNameEnum)
        {
            // ���丮���
            case Define.SceneName.Tutorial:
                // // ����Ʈ ������ 1�̸�
                if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 1)
                {
                    // ��Ʈ ����Ʈ��
                    GameManager.Quest._wesleyController.QuestHeartOn();
                }
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 2)
                {

                }
                break;
            case Define.SceneName.Village02:
                if (_questProgressValue >= _questObjectiveScore)
                {

                }
                break;
            case Define.SceneName.DunGeon:
                // ����Ʈ ������ 5�̸�
                if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 5)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                    DungeonDoor1Open();
                }
                // ����Ʈ ������ 6�̸�
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 6)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                    DungeonDoor2Open();
                }
                // ����Ʈ ������ 7�̸�
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 7)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                }
                // ����Ʈ ������ 8�̸�
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 8)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                }
                // ����Ʈ ������ 9�̸� ����
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 9)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                }
                break;
        }
    }

    // ����Ʈ ��ȭ �Լ�
    public void QuestConversationText(int conversationCount)
    {   
        // NPC �̸��� �־���
        _conversationCharacterNameText.text = _questStroy[conversationCount];
        // �÷��̾��� �̸�(Ȧ��)�� ���� >> ���� �߰��� ���ؼ� 
        // _questStroy[_conversationCount] �� "Player"�̸� ĳ���� �̸��� �־���
        if (_questStroy[conversationCount] == "Player")
        {
            _conversationCharacterNameText.text = GameManager.Select._playerName;
        }

        // ����Ʈ ����(¦��)�� �ڷ�ƾ�� �̿��ؾߵǱ� ������ ������� ��ӹ޴� ������ ��Ʈ�ѷ����� ���� ����
        _wesleyController.WeleyDialogStart(_questStroy[conversationCount + 1]);

        //����Ʈ�� ����(¦��)
        //_conversationText.text = _questStroy[Property_ConversationCount + 1];
    }

    // ����Ʈ ��ȭ ����
    public void QuestConversationEnd()
    {
        // ����Ʈ ��ȭ ��� �ʱ�ȭ // ���� ��ȭ�� �̿�
        Property_ConversationCount = 0;
        //���丮�� ������ ��ȭâ�� ��Ȱ��ȭ
        ConversationInfoActive(false);
        //����Ʈâ�� Ȱ��ȭ >> ��ư�� ����Ʈ�� �ִ� ������� �ؼ� ���� ������ ����
        // _questInfo.SetActive(true);
        //����Ʈ Ÿ��Ʋ �ؽ�Ʈ�� Ÿ��Ʋ�� �־��ش�
        //_questTitleText.text = _questTile;

        _wesleyController.EndToTalkWithWesley();
    }

    // ����Ʈ �Ϸ� �Լ�
    public void QuestCompletion()
    {
        // ����Ʈ ����
        QuestReward();
        //����Ʈ �Ϸ� ����Ʈ�� ���� ����Ʈ�� ��ü�� �߰�
        _questCompletionList.Add(_questData);
        //����Ʈ ������ �´� ���� ��Ȱ��ȭ �Ǿ� ���� �ܰ�� �Ѿ�� ����
        //_door[_questLevel - 1].SetActive(false);
        //���� ����Ʈ�� ������ �ǰ� ������
        GameManager.QuestData._questLevel++;
        //����Ʈ�� ��ǥ���� �ʱ�ȭ
        Property_QuestProgressValue = 0;
        //����Ʈ Ÿ��Ʋ �ؽ�Ʈ null
        _questTitleText.text = null;
    }

    //����Ʈ ��ȭâ ��Ȱ��ȭ �Լ�
    public void ConversationInfoActive(bool value)
    {
        _conversationInfo.SetActive(value);
    }

    //����Ʈ ����â ��Ȱ��ȭ �Լ�
    public void QuestInfoActive(bool value)
    {
        _questInfo.SetActive(value);
    }

    //���丮�󿡼� ���� ���������� �Ѿ�� �Լ�
    //�������ִ°�
    public void TutorialDoorOpen()
    {
        for (int i = 0; i < _boxCollider.Length; i++)
        {
            _boxCollider[i].gameObject.SetActive(false);
            _navMeshObstacle[i].gameObject.SetActive(false);
        }
    }
    //���� �� �����ִ� �Լ�
    public void DungeonDoor1Open()
    {
        GameObject.Destroy(_boxCollider[0].gameObject);
        //_boxCollider[0].gameObject.
        //_navMeshObstacle[0].gameObject.SetActive(false);
    }
    //���� �� �����ִ� �Լ�
    public void DungeonDoor2Open()
    {
        _boxCollider[1].gameObject.SetActive(false);
        _navMeshObstacle[1].gameObject.SetActive(false);
    }

    //����Ʈ �����Լ�
    public void QuestReward()
    {
        // ������Ʈ�Ŵ��� �����Ʈ�ѷ��� �����ؼ� �����带 ó��
        GameManager.Obj._goldController.GetGold(_reward);
        // ������� UI ����
        GameManager.Ui.QuestRewardOnOff(true);
        // ������� ��� �ؽ�Ʈ ����
        GameManager.Ui._uiQuestReward._rewardText.text = _reward.ToString() + " G";
    }
    // ����
    public void Ending()
    {
        // ����ui
        GameManager.Create.CreateUi("UI_Ending", GameManager.Ui.go);
    }

    public void QuestJoystickStop()
    {
        // ���̽�ƽ �̵� �޽�
        GameManager.Ui._joyStickController._joystickState = Define.JoystickState.InputFalse;
        // �÷��̾� ���¸� ����
        GameManager.Obj._playerController._creatureState = Define.CreatureState.Idle;
    }

    //�� ���� �� ���� �� ����
    /*
        //�ڷ�ƾ�� ���Ͽ� ����Ʈ �̿Ϸ� �汳ǥ�� �ð� ����
        IEnumerator QuestWarning()
        {
            //2�ʵڿ� ����Ʈ ���â�� ��Ȱ��ȭ
            yield return new WaitForSeconds(2f);
            _questWarningText.SetActive(false);
        }
        private void OnCollisionEnter(Collision col)
        {
            //����Ʈ �̿Ϸ�� �������� ���� ������ �ϰ� �Ǹ� ���ǥ��
            if (col.gameObject == _door[_questLevel - 1])
            {
                //�÷��̾ �̿ϷṮ�� �����ϰ� �ȴٸ� ���� x�������� ƨ������ ��
                transform.position = _door[_questLevel - 1].transform.position + new Vector3(5f, 0, 0);
                //���UI Ȱ��ȭ  
                _questWarningText.SetActive(true);
                //�ڷ�ƾ�� ���Ͽ� ���â�� 2�ʵ��� Ȱ��ȭ �ǰ� ����
                StartCoroutine(QuestWarning());
            }
            //�÷��̾ ���Ϳ��� ��ȣ�ۿ��� ���� ���ǽ� �ִ� ��
            if (col.collider.CompareTag("Monster"))
            {
                //����� ������ ������Ʈ�� ��Ȱ��ȭ �ϰ� �Ǹ� ��ǥ���� ī���� ��
                col.gameObject.SetActive(false);
                if (_conversationInfo.activeSelf == false)
                {
                    _questProgressValue += 1;
                }
            }
        }*/
}
