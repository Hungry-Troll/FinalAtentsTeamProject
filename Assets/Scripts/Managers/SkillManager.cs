using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// ��ų json ���� ������ ����
// ���� ��ų ������ ���⼭ �� ����?
public class SkillManager
{
    // json ���� ������ ������
    string[] _skillJsonArr;
    private List<TempSkillStat> _skillList;

    public void Init()
    {
        _skillList = new List<TempSkillStat>();
        LoadSkillList();
    }

    // ��ų�ε�
    public void LoadSkillList()
    {
        string fileName = "Skills";
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/Skill/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // {}{}{} -> {} {} {} ���� �迭�� ����
        SplitJson(json);

        for (int i = 0; i < _skillJsonArr.Length; i++)
        {
            // {name, type, skill...} �� ��Ʈ�� ItemStat Ÿ������ parsing
            TempSkillStat skill = JsonUtility.FromJson<TempSkillStat>(_skillJsonArr[i]);
            // ����Ʈ�� �߰�
            _skillList.Add(skill);
        }
    }

    void SplitJson(string jsonSentence)
    {
        _skillJsonArr = jsonSentence.Trim().Split("}");

        for (int i = 0; i < _skillJsonArr.Length - 1; i++)
        {
            _skillJsonArr[i] += "}";

            // ������
            //Debug.Log(_itemJsonArr += "}");
        }
    }

    // ��ų ����(�̸�/����/ȿ��) ������ ���� �Լ�
    public void SkillStatLoadJson(string skillName, SkillStat skillStat)
    {
        // �̸����� ��ġ
        TempSkillStat tempStat = SearchItem(skillName);
        // ������ ������ ����
        skillStat.Id = tempStat.Id;
        skillStat.SkillName = tempStat.SkillName;
        skillStat.SkillContent = tempStat.SkillContent;
        skillStat.SkillEffect = tempStat.SkillEffect;
        skillStat.SkillAtk = tempStat.SkillAtk;
    }

    // ����Ʈ���� ã�� �Լ�
    public TempSkillStat SearchItem(string findId)
    {
        foreach (TempSkillStat one in _skillList)
        {
            if (one.Id.Equals(findId))
            {
                return one;
            }
        }
        return null;
    }
}


// ������ �����ϱ� ���� �ӽ� Ŭ���� (������� ��� ������ json ���� �����Ͱ� ������ �ȵ�)
[System.Serializable]
public class TempSkillStat
{
    // �ڵ�
    [SerializeField]
    private string _Id;

    // �̸�
    [SerializeField]
    private string _SkillName;

    // ����
    [SerializeField]
    private string _SkillContent;

    // ȿ��
    [SerializeField]
    private string _SkillEffect;

    // ���ݷ�
    [SerializeField]
    private int _SkillAtk;

    public string Id
    {
        get { return _Id; }
        set { _Id = value; }
    }

    public string SkillName
    {
        get { return _SkillName; }
        set { _SkillName = value; }
    }

    public string SkillContent
    {
        get { return _SkillContent; }
        set { _SkillContent = value; }
    }

    public string SkillEffect
    {
        get { return _SkillEffect; }
        set { _SkillEffect = value; }
    }

    public int SkillAtk
    {
        get { return _SkillAtk; }
        set { _SkillAtk = value; }
    }
}