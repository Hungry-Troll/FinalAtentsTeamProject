using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class QuestDataManager
{
    // 저장 데이터를 딕셔너리로 정리 (멀티맵)
    [HideInInspector] 
    public Dictionary<int, QuestDataEX> _questDict;

    // 퀘스트 레벨을 매니저에서 가지고있음
    public int _questLevel;

    // Init 함수에서는 csv 파일을 단 한번만 읽어서 데이터를 딕셔너리에 저장함 >> 데이터 Init함수는 GameManager Awake 함수에서 불러올 예정
    // 사용시에는 QuestManagerEX에 _questDict에서 저장된 데이터를 가지고 올 수 있음 >> QuestController 에서 사용
    // 다른 스크립트 Update 함수에서 ReadQuest를 매프레임마다 불러오는 방식은 시간복잡도와 공간복잡도가 비정상적으로 올라 갈 수 있음
    // 특히 Update문에 new 키워드를 사용하는것은 최대한 자제해야됨, 왜냐하면 가비지가 매 프레임마다 쌓여서 게임 시작 얼마 후 비정상적인 렉이 생길 수 있음.
    public void Init()
    {
        // 딕셔너리 생성
        _questDict = new Dictionary<int, QuestDataEX>();
        // CSV 데이터 읽어오기
        ReadQuestCSV();
        _questLevel = 1;
    }

    public void ReadQuestCSV()
    {
        //문서 위치
        //csv로 만드려고 하였으나 인코딩이 안되어 텍스트로 만들었음 >> 인코딩해서 csv로 만듬 utf-8
        string path = Application.dataPath + "/Resources/Data/csv/QuestTest.csv";
        using (StreamReader sr = new StreamReader(path))
        {
            //첫번째줄은 데이터의 종류를 가르쳐 주는 줄이라 스킵
            sr.ReadLine();
            string line = string.Empty;
            //문서의 한줄한줄이 퀘스트의 정보가 들어있음!

            while ((line = sr.ReadLine()) != null)
            {
                //데이터를 넣어놓을 빈 껍대기 클래스 생성
                QuestDataEX data = new QuestDataEX();

                //csv는 각 줄의 셀을 나눌때 ,로 나누어서 text파일에서 ,로 퀘스트의 정보들을 나눴음!
                List<string> readQuestInfo = line.Split(',').ToList();
                //매개변수로 받아온 퀘스트의 레벨(번호)을 통하여 문서에서 첫번째가
                //퀘스트의 레벨이라 비교하여 같으면 위의 변수에 데이터를 집어 넣는다.
                
                //첫번째 퀘스트 레벨
                data.property_QuestLevel = int.Parse(readQuestInfo[0]);
                //두번째 타이틀
                data.property_QuestTitle = readQuestInfo[1];
                //세번째 퀘스트 목표의 이름
                data.property_QuestObjectiveName = readQuestInfo[2];
                //네번째 퀘스트 목표치
                data.property_QuestObjectiveScore = int.Parse(readQuestInfo[3]);
                //5번째 퀘스트 보상
                data.property_QuestReward = int.Parse(readQuestInfo[4]);
                //6번째는 퀘스트의 스토리 내용이라 for문의 통하여 나머지는 List에 저장하였음

                for (int i = 5; i < readQuestInfo.Count; i++)
                {
                    data.property_Stroy.Add(readQuestInfo[i]);
                }

                // 다하고 마지막으로 딕셔너리에 저장
                _questDict.Add(data.property_QuestLevel, data);
                // data.property_Stroy 리스트 초기화
                // data.property_Stroy.Clear();
            }
            sr.Close();
        }
    }
}
//PlayerConversation = line.Split(',').ToList(); 리스트 나누는 부분!
//http://daplus.net/c-%EA%B5%AC%EB%B6%84-%EB%90%9C-%EB%AC%B8%EC%9E%90%EC%97%B4%EC%9D%84-list-string%EC%9C%BC%EB%A1%9C-split-%ED%95%98%EB%8A%94-%EB%B0%A9%EB%B2%95/

public class QuestDataEX
{
    //문서에 저장되어있는 데이터
    [SerializeField]
    private int _level;
    [SerializeField]
    private string _title;
    [SerializeField]
    private string _objectiveName;
    [SerializeField]
    private int _objectiveScore;
    [SerializeField]
    private int _reward;
    [SerializeField]
    private List<string> _stroy = new List<string>();

    //각 변수들을 프로퍼티로 선언
    public int property_QuestLevel 
    { 
        get { return _level; } 
        set { _level = value; }
    }

    public string property_QuestTitle 
    { 
        get { return _title; } 
        set { _title = value; }
    }

    public string property_QuestObjectiveName 
    { 
        get { return _objectiveName; } 
        set { _objectiveName = value; }
    }
    public int property_QuestObjectiveScore 
    { 
        get { return _objectiveScore; } 
        set { _objectiveScore = value; }
    }

    // 보상 추가
    public int property_QuestReward 
    { 
        get { return _reward; }
        set { _reward = value; }
    }

    //스토리는 리스트 자료구조로 만들어 이름,대화내용으로 추가하여 짝수는 대화자명 홀수는 대화내용
    public List<string> property_Stroy 
    { 
        get { return _stroy; } 
        set { _stroy = value; }
    }
}
