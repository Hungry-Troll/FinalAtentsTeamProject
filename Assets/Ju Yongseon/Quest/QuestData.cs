using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class QuestData 
{
    //������ ����Ǿ��ִ� ������
    private int questLevel;
    private string questTitle;
    private string questObjectiveName;
    private int questObjectiveScore;
    private List<string> questStroy;

    //�� �������� ������Ƽ�� ����
    public int QuestLevel { get { return questLevel; } }
    public string QuestTitle { get { return questTitle; } }
    public string QuestObjectiveName { get { return questObjectiveName; } }
    public int QuestObjectiveScore { get { return questObjectiveScore; } }
    //���丮�� ����Ʈ �ڷᱸ���� ����� �̸�,��ȭ�������� �߰��Ͽ� ¦���� ��ȭ�ڸ� Ȧ���� ��ȭ����
    public List<string> QuestStroy { get { return questStroy; } }

    public void ReadQuest(int _questLevel)
    {
        //���� ��ġ
        //csv�� ������� �Ͽ����� ���ڵ��� �ȵǾ� �ؽ�Ʈ�� �������
        string path = Application.dataPath + "/Ju Yongseon/Quest/Quest.txt";
        using (StreamReader sr = new StreamReader(path))
        {
            //ù��°���� �������� ������ ������ �ִ� ���̶� ��ŵ
            sr.ReadLine();
            string line = string.Empty;
            //������ ���������� ����Ʈ�� ������ �������!
            while ((line = sr.ReadLine()) != null)
            {
                //csv�� �� ���� ���� ������ ,�� ����� text���Ͽ��� ,�� ����Ʈ�� �������� ������!
                List<string> readQuestInfo = line.Split(',').ToList();
                //�Ű������� �޾ƿ� ����Ʈ�� ����(��ȣ)�� ���Ͽ� �������� ù��°��
                //����Ʈ�� �����̶� ���Ͽ� ������ ���� ������ �����͸� ���� �ִ´�.
                if (int.Parse(readQuestInfo[0]) == _questLevel)
                {
                    //ù��° ����Ʈ ����
                    questLevel = int.Parse(readQuestInfo[0]);
                    //�ι�° Ÿ��Ʋ
                    questTitle = readQuestInfo[1];
                    //����° ����Ʈ ��ǥ�� �̸�
                    questObjectiveName = readQuestInfo[2];
                    //�׹�° ����ũ ��ǥġ
                    questObjectiveScore = int.Parse(readQuestInfo[3]);
                    //5��°�� ����Ʈ�� ���丮 �����̶� for���� ���Ͽ� �������� List�� �����Ͽ���
                    questStroy = new List<string>();
                    for (int i = 4; i < readQuestInfo.Count; i++)
                    {
                        questStroy.Add(readQuestInfo[i]);
                    }
                    sr.Close();
                    return;
                }
            }
            //������ ����Ʈ�� �Ϸ�ǥ��
            if (line == null)
            {
                Debug.Log("����Ʈ�� �����ϴ�.");
            }
            sr.Close();
        }
    }
}
//PlayerConversation = line.Split(',').ToList(); ����Ʈ ������ �κ�!
//http://daplus.net/c-%EA%B5%AC%EB%B6%84-%EB%90%9C-%EB%AC%B8%EC%9E%90%EC%97%B4%EC%9D%84-list-string%EC%9C%BC%EB%A1%9C-split-%ED%95%98%EB%8A%94-%EB%B0%A9%EB%B2%95/
