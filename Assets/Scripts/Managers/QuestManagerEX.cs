using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class QuestManagerEX
{
    // 퀘스트 매니저에서 가지고 있는 csv 파일 정보를 담기위한 임시 변수
    QuestDataEX _questData;
    //퀘스트가 완료가 되면 리스트에 담아서 저장
    List<QuestDataEX> _questCompletionList = new List<QuestDataEX>();

    // 다음 장소로 넘어가게 하기위한 게임오브젝트
    public BoxCollider[] _boxCollider;
    public NavMeshObstacle[] _navMeshObstacle;


    // 딕셔너리에서 가지고온 데이터를 저장 할 변수
    //퀘스트 레벨
    int _questLevel;
    //퀘스트 타이틀
    string _questTile;
    //퀘스트 목표이름
    string _questObjectiveName;
    //퀘스트 목표치
    public int _questObjectiveScore;
    //퀘스트 보상
    int _reward;
    [HideInInspector]
    //퀘스트 스토리 리스트
    public List<string> _questStroy;

    // 현재 매니저에서 사용할 변수

    //목표치
    public int _questProgressValue;

    //목표치가 바뀔때마다 함수를 실행해서 값을 목표치 변경
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

    //현재 퀘스트 대화 카운트
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
    /// 웨슬리 컨트롤러 정보 / 대화창 / NPC 이름
    /// </summary>
    /// 
    // 퀘스트 진행 NPC인 웨슬리컨트롤러도 퀘스트 매니저에서 가지고 있음
    [HideInInspector]
    public WesleyController _wesleyController;
    // 퀘스트 진행 NPC도 가지고 있음
    [HideInInspector]
    public Ui_NpcController _ui_NpcController;

    // 퀘스트 대화창
    public GameObject _conversationInfo;
    // 퀘스트 캐릭터 이름 텍스트 (NPC 이름 / 주인공 이름)
    public Text _conversationCharacterNameText;
    // 대화 텍스트
    public Text _conversationText;
    // 대화 버튼
    public Button _conversationButton;


    /// <summary>
    /// 퀘스트 인포창 
    /// </summary>
    /// 
    // 퀘스트 타이틀창
    public GameObject _questInfo;
    // 퀘스트 타이틀 텍스트
    public Text _questTitleText;
    // 퀘스트 목표량 텍스트
    public Text _questObjectivesText;

    /// <summary>
    /// 퀘스트 경고창
    /// </summary>
    // 
    public GameObject _questWarningText;


    public void Init()
    {
        // 퀘스트용 장애물 제거
        _boxCollider = new BoxCollider[2];
        _navMeshObstacle = new NavMeshObstacle[2];
    }

    // 퀘스트 시작에 필요한 첫 데이터 함수
    // 다음 맵에 넘어 갔을 경우 questLevel 수치만 넣어주면 되므로 save시 QuestManager._questController._questLevel 만 저장하면 퀘스트진행을 이어서 할 수 있음.
    public void QuestReset(int questLevel)
    {
        //듀토리얼이 끝나고 본 게임에 들어오면 레벨1의 퀘스트가 실행이 된다.
        _questLevel = questLevel;
        _questProgressValue = 0;
        _conversationCount = 0;
    }

    public void QuestStart(int questLevel)
    {
        _questData = new QuestDataEX();
        QuestReset(GameManager.QuestData._questLevel);
        // 퀘스트 대화창 오픈
        ConversationInfoActive(true);
        // 대화창 제외 UI 끔
        GameManager.Ui.UISetActiveFalse();
        // 값 가지고 옴
        QuestDataGetValue(GameManager.QuestData._questLevel);
        // 대입 // 퀘스트 목표량
        QuestInfoText();
        // 퀘스트 대화
        QuestConversationText(Property_ConversationCount);
        // 퀘스트 다음 대화 준비
        QuestConversationCountAdd();
    }


    // QuestManagerEX에 저장한 딕셔너리에서 데이터를 가지고 오는 함수
    public void QuestDataGetValue(int questLevel)
    {
        if(GameManager.QuestData._questDict.TryGetValue(questLevel, out _questData))
        {
            // 퀘스트 레벨
            _questLevel = _questData.property_QuestLevel;
            // 퀘스트 타이틀
            _questTile = _questData.property_QuestTitle;
            // 퀘스트 목표이름
            _questObjectiveName = _questData.property_QuestObjectiveName;
            // 퀘스트 목표치
            _questObjectiveScore = _questData.property_QuestObjectiveScore;
            // 퀘스트 보상
            _reward = _questData.property_QuestReward;
            // 퀘스트 스토리
            _questStroy = _questData.property_Stroy;
        }
    }

    // 퀘스트 대화 스킵 함수
    public void QuestConvesationSkip()
    {
        Property_ConversationCount += 2;
    }

    // 퀘스트 대화 체크 함수
    public void QuestConversationCountAdd()
    {
        Property_ConversationCount += 2;
    }

    // 퀘스트 진행상황 증가 함수
    public void QuestProgressValueAdd()
    {
        Property_QuestProgressValue++;
    }

    // 퀘스트 목표량 함수 >> 목표치 달성하면 실행
    public void QuestInfoText()
    {
        // 퀘스트 목표 이름
        _questObjectivesText.text = _questObjectiveName + " " + Property_QuestProgressValue + " / <color=red>" + _questObjectiveScore + "</color>";
        // 퀘스트 타이틀
        _questTitleText.text = _questTile;

        //  퀘스트 스코어가 목표치가 진행상황보다 작거나 같으면
        switch (GameManager.Scene._sceneNameEnum)
        {
            // 듀토리얼씬
            case Define.SceneName.Tutorial:
                // // 퀘스트 레벨이 1이면
                if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 1)
                {
                    // 하트 이펙트온
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
                // 퀘스트 레벨이 5이면
                if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 5)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                    DungeonDoor1Open();
                }
                // 퀘스트 레벨이 6이면
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 6)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                    DungeonDoor2Open();
                }
                // 퀘스트 레벨이 7이면
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 7)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                }
                // 퀘스트 레벨이 8이면
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 8)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                }
                // 퀘스트 레벨이 9이면 엔딩
                else if (_questProgressValue >= _questObjectiveScore && GameManager.QuestData._questLevel == 9)
                {
                    QuestJoystickStop();
                    _wesleyController.RadioQuest();
                }
                break;
        }
    }

    // 퀘스트 대화 함수
    public void QuestConversationText(int conversationCount)
    {   
        // NPC 이름을 넣어줌
        _conversationCharacterNameText.text = _questStroy[conversationCount];
        // 플레이어의 이름(홀수)로 변경 >> 보상열 추가로 인해서 
        // _questStroy[_conversationCount] 이 "Player"이면 캐릭터 이름을 넣어줌
        if (_questStroy[conversationCount] == "Player")
        {
            _conversationCharacterNameText.text = GameManager.Select._playerName;
        }

        // 퀘스트 내용(짝수)은 코루틴을 이용해야되기 때문에 모노비헤비어를 상속받는 웨슬리 컨트롤러에서 직접 실행
        _wesleyController.WeleyDialogStart(_questStroy[conversationCount + 1]);

        //퀘스트의 내용(짝수)
        //_conversationText.text = _questStroy[Property_ConversationCount + 1];
    }

    // 퀘스트 대화 종료
    public void QuestConversationEnd()
    {
        // 퀘스트 대화 목록 초기화 // 다음 대화에 이용
        Property_ConversationCount = 0;
        //스토리가 끝나면 대화창은 비활성화
        ConversationInfoActive(false);
        //퀘스트창은 활성화 >> 버튼에 이펙트를 넣는 방식으로 해서 직접 누르게 유도
        // _questInfo.SetActive(true);
        //퀘스트 타이틀 텍스트에 타이틀의 넣어준다
        //_questTitleText.text = _questTile;

        _wesleyController.EndToTalkWithWesley();
    }

    // 퀘스트 완료 함수
    public void QuestCompletion()
    {
        // 퀘스트 보상
        QuestReward();
        //퀘스트 완료 리스트에 현재 퀘스트의 객체를 추가
        _questCompletionList.Add(_questData);
        //퀘스트 레벨에 맞는 문이 비활성화 되어 다음 단계로 넘어갈수 있음
        //_door[_questLevel - 1].SetActive(false);
        //다음 퀘스트가 실행이 되게 레벨업
        GameManager.QuestData._questLevel++;
        //퀘스트의 목표량을 초기화
        Property_QuestProgressValue = 0;
        //퀘스트 타이틀 텍스트 null
        _questTitleText.text = null;
    }

    //퀘스트 대화창 비활성화 함수
    public void ConversationInfoActive(bool value)
    {
        _conversationInfo.SetActive(value);
    }

    //퀘스트 정보창 비활성화 함수
    public void QuestInfoActive(bool value)
    {
        _questInfo.SetActive(value);
    }

    //듀토리얼에서 다음 스테이지로 넘어가는 함수
    //문열어주는것
    public void TutorialDoorOpen()
    {
        for (int i = 0; i < _boxCollider.Length; i++)
        {
            _boxCollider[i].gameObject.SetActive(false);
            _navMeshObstacle[i].gameObject.SetActive(false);
        }
    }
    //던전 문 열어주는 함수
    public void DungeonDoor1Open()
    {
        GameObject.Destroy(_boxCollider[0].gameObject);
        //_boxCollider[0].gameObject.
        //_navMeshObstacle[0].gameObject.SetActive(false);
    }
    //던전 문 열어주는 함수
    public void DungeonDoor2Open()
    {
        _boxCollider[1].gameObject.SetActive(false);
        _navMeshObstacle[1].gameObject.SetActive(false);
    }

    //퀘스트 보상함수
    public void QuestReward()
    {
        // 오브젝트매니저 골드컨트롤러에 접근해서 리워드를 처리
        GameManager.Obj._goldController.GetGold(_reward);
        // 리워드용 UI 생성
        GameManager.Ui.QuestRewardOnOff(true);
        // 리워드용 골드 텍스트 수정
        GameManager.Ui._uiQuestReward._rewardText.text = _reward.ToString() + " G";
    }
    // 엔딩
    public void Ending()
    {
        // 엔딩ui
        GameManager.Create.CreateUi("UI_Ending", GameManager.Ui.go);
    }

    public void QuestJoystickStop()
    {
        // 조이스틱 이동 펄스
        GameManager.Ui._joyStickController._joystickState = Define.JoystickState.InputFalse;
        // 플레이어 상태를 대기로
        GameManager.Obj._playerController._creatureState = Define.CreatureState.Idle;
    }

    //위 내용 다 구현 후 정리
    /*
        //코루틴을 통하여 퀘스트 미완료 경교표시 시간 설정
        IEnumerator QuestWarning()
        {
            //2초뒤에 퀘스트 경고창이 비활성화
            yield return new WaitForSeconds(2f);
            _questWarningText.SetActive(false);
        }
        private void OnCollisionEnter(Collision col)
        {
            //퀘스트 미완료시 각레벨의 문에 접촉을 하게 되면 경고표시
            if (col.gameObject == _door[_questLevel - 1])
            {
                //플레이어가 미완료문에 접촉하게 된다면 문의 x방향으로 튕겨지게 됨
                transform.position = _door[_questLevel - 1].transform.position + new Vector3(5f, 0, 0);
                //경고UI 활성화  
                _questWarningText.SetActive(true);
                //코루틴을 통하여 경고창을 2초동안 활성화 되게 만듬
                StartCoroutine(QuestWarning());
            }
            //플레이어가 몬스터와의 상호작용을 통한 조건식 넣는 곳
            if (col.collider.CompareTag("Monster"))
            {
                //현재는 접촉한 오브젝트가 비활성화 하게 되면 목표량이 카운터 됨
                col.gameObject.SetActive(false);
                if (_conversationInfo.activeSelf == false)
                {
                    _questProgressValue += 1;
                }
            }
        }*/
}
