using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class QuestData 
{
    //문서에 저장되어있는 데이터
    private int questLevel;
    private string questTitle;
    private string questObjectiveName;
    private int questObjectiveScore;
    private List<string> questStroy;

    //각 변수들을 프로퍼티로 선언
    public int QuestLevel { get { return questLevel; } }
    public string QuestTitle { get { return questTitle; } }
    public string QuestObjectiveName { get { return questObjectiveName; } }
    public int QuestObjectiveScore { get { return questObjectiveScore; } }
    //스토리는 리스트 자료구조로 만들어 이름,대화내용으로 추가하여 짝수는 대화자명 홀수는 대화내용
    public List<string> QuestStroy { get { return questStroy; } }

    public void ReadQuest(int _questLevel)
    {
        //문서 위치
        //csv로 만드려고 하였으나 인코딩이 안되어 텍스트로 만들었음
        string path = Application.dataPath + "/Ju Yongseon/Quest/Quest.txt";
        using (StreamReader sr = new StreamReader(path))
        {
            //첫번째줄은 데이터의 종류를 가르쳐 주는 줄이라 스킵
            sr.ReadLine();
            string line = string.Empty;
            //문서의 한줄한줄이 퀘스트의 정보가 들어있음!
            while ((line = sr.ReadLine()) != null)
            {
                //csv는 각 줄의 셀을 나눌때 ,로 나누어서 text파일에서 ,로 퀘스트의 정보들을 나눴음!
                List<string> readQuestInfo = line.Split(',').ToList();
                //매개변수로 받아온 퀘스트의 레벨(번호)을 통하여 문서에서 첫번째가
                //퀘스트의 레벨이라 비교하여 같으면 위의 변수에 데이터를 집어 넣는다.
                if (int.Parse(readQuestInfo[0]) == _questLevel)
                {
                    //첫번째 퀘스트 레벨
                    questLevel = int.Parse(readQuestInfo[0]);
                    //두번째 타이틀
                    questTitle = readQuestInfo[1];
                    //세번째 퀘스트 목표의 이름
                    questObjectiveName = readQuestInfo[2];
                    //네번째 퀘스크 목표치
                    questObjectiveScore = int.Parse(readQuestInfo[3]);
                    //5번째는 퀘스트의 스토리 내용이라 for문의 통하여 나머지는 List에 저장하였음
                    questStroy = new List<string>();
                    for (int i = 4; i < readQuestInfo.Count; i++)
                    {
                        questStroy.Add(readQuestInfo[i]);
                    }
                    sr.Close();
                    return;
                }
            }
            //없으면 퀘스트가 완료표시
            if (line == null)
            {
                Debug.Log("퀘스트가 없습니다.");
            }
            sr.Close();
        }
    }
}
//PlayerConversation = line.Split(',').ToList(); 리스트 나누는 부분!
//http://daplus.net/c-%EA%B5%AC%EB%B6%84-%EB%90%9C-%EB%AC%B8%EC%9E%90%EC%97%B4%EC%9D%84-list-string%EC%9C%BC%EB%A1%9C-split-%ED%95%98%EB%8A%94-%EB%B0%A9%EB%B2%95/
