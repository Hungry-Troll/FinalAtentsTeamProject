using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class QuestDataManager
{
    // ���� �����͸� ��ųʸ��� ���� (��Ƽ��)
    [HideInInspector] 
    public Dictionary<int, QuestDataEX> _questDict;

    // ����Ʈ ������ �Ŵ������� ����������
    public int _questLevel;

    // Init �Լ������� csv ������ �� �ѹ��� �о �����͸� ��ųʸ��� ������ >> ������ Init�Լ��� GameManager Awake �Լ����� �ҷ��� ����
    // ���ÿ��� QuestManagerEX�� _questDict���� ����� �����͸� ������ �� �� ���� >> QuestController ���� ���
    // �ٸ� ��ũ��Ʈ Update �Լ����� ReadQuest�� �������Ӹ��� �ҷ����� ����� �ð����⵵�� �������⵵�� ������������ �ö� �� �� ����
    // Ư�� Update���� new Ű���带 ����ϴ°��� �ִ��� �����ؾߵ�, �ֳ��ϸ� �������� �� �����Ӹ��� �׿��� ���� ���� �� �� ���������� ���� ���� �� ����.
    public void Init()
    {
        // ��ųʸ� ����
        _questDict = new Dictionary<int, QuestDataEX>();
        // CSV ������ �о����
        ReadQuestCSV();
        _questLevel = 1;
    }

    public void ReadQuestCSV()
    {
        //���� ��ġ
        //csv�� ������� �Ͽ����� ���ڵ��� �ȵǾ� �ؽ�Ʈ�� ������� >> ���ڵ��ؼ� csv�� ���� utf-8
        string path = Application.dataPath + "/Resources/Data/csv/QuestTest.csv";
        using (StreamReader sr = new StreamReader(path))
        {
            //ù��°���� �������� ������ ������ �ִ� ���̶� ��ŵ
            sr.ReadLine();
            string line = string.Empty;
            //������ ���������� ����Ʈ�� ������ �������!

            while ((line = sr.ReadLine()) != null)
            {
                //�����͸� �־���� �� ����� Ŭ���� ����
                QuestDataEX data = new QuestDataEX();

                //csv�� �� ���� ���� ������ ,�� ����� text���Ͽ��� ,�� ����Ʈ�� �������� ������!
                List<string> readQuestInfo = line.Split(',').ToList();
                //�Ű������� �޾ƿ� ����Ʈ�� ����(��ȣ)�� ���Ͽ� �������� ù��°��
                //����Ʈ�� �����̶� ���Ͽ� ������ ���� ������ �����͸� ���� �ִ´�.
                
                //ù��° ����Ʈ ����
                data.property_QuestLevel = int.Parse(readQuestInfo[0]);
                //�ι�° Ÿ��Ʋ
                data.property_QuestTitle = readQuestInfo[1];
                //����° ����Ʈ ��ǥ�� �̸�
                data.property_QuestObjectiveName = readQuestInfo[2];
                //�׹�° ����Ʈ ��ǥġ
                data.property_QuestObjectiveScore = int.Parse(readQuestInfo[3]);
                //5��° ����Ʈ ����
                data.property_QuestReward = int.Parse(readQuestInfo[4]);
                //6��°�� ����Ʈ�� ���丮 �����̶� for���� ���Ͽ� �������� List�� �����Ͽ���

                for (int i = 5; i < readQuestInfo.Count; i++)
                {
                    data.property_Stroy.Add(readQuestInfo[i]);
                }

                // ���ϰ� ���������� ��ųʸ��� ����
                _questDict.Add(data.property_QuestLevel, data);
                // data.property_Stroy ����Ʈ �ʱ�ȭ
                // data.property_Stroy.Clear();
            }
            sr.Close();
        }
    }
}
//PlayerConversation = line.Split(',').ToList(); ����Ʈ ������ �κ�!
//http://daplus.net/c-%EA%B5%AC%EB%B6%84-%EB%90%9C-%EB%AC%B8%EC%9E%90%EC%97%B4%EC%9D%84-list-string%EC%9C%BC%EB%A1%9C-split-%ED%95%98%EB%8A%94-%EB%B0%A9%EB%B2%95/

public class QuestDataEX
{
    //������ ����Ǿ��ִ� ������
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

    //�� �������� ������Ƽ�� ����
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

    // ���� �߰�
    public int property_QuestReward 
    { 
        get { return _reward; }
        set { _reward = value; }
    }

    //���丮�� ����Ʈ �ڷᱸ���� ����� �̸�,��ȭ�������� �߰��Ͽ� ¦���� ��ȭ�ڸ� Ȧ���� ��ȭ����
    public List<string> property_Stroy 
    { 
        get { return _stroy; } 
        set { _stroy = value; }
    }
}
