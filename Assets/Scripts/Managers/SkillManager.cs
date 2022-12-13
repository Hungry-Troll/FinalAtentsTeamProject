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
        skillStat.SkillSlotNumber = tempStat.SkillSlotNumber;
        skillStat.SkillLevel = tempStat.SkillLevel;
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


    // �÷��̾ ��ų ����Ʈ�� ��ų �߰��ϴ� �Լ�
    // AddSkillToPlayerSkillList(������ ��ų ����Ʈ, ������ ��ų)
    // -> ��ų ����Ʈ�� �߰��Ϸ��� ��ų�� �����ϴ��� �˻��ϰ� ������ �߰�, ������ ������Ʈ
    public void UpdatePlayerSkillList(List<TempSkillStat> playerSkillList, SkillStat playerSkill)
    {
        // ���̸� �ʱ�ȭ
        if(playerSkillList == null)
            playerSkillList = new List<TempSkillStat>();

        // �ݺ��� �����߿� ���� �߰��ϸ� �ݺ� Ƚ�� �����ϹǷ� �ӽ÷� �߰��� �� ������ ���� ����
        TempSkillStat tmpBox = null;
        // ��ų ��� ���� �ߴ��� Ȯ�ο� ����
        bool hadUpdate = false;

        // ��Ͽ� ��ų�� �ϳ��� �ִ� ���
        // �̸� ��ġ�� ��ų �ִ��� ��Ͽ��� �˻�
        for(int i = 0; i < playerSkillList.Count; i++)
        {
            // �ϴ� ��ų ���� ������Ʈ
            if(playerSkillList[i].SkillName.Equals(playerSkill.SkillName))
            {
                // ���� ����Ʈ�� �ִ� ������ �� ���ų� ������ true, �Ѿ�� ���� ������ �� ������ false
                bool isHigherBeforeOne = playerSkillList[i].SkillLevel >= playerSkill.SkillLevel ? true : false;
                if(isHigherBeforeOne)
                {
                    // ������ �ִ� ��ų������ ������Ʈ
                    playerSkill.SkillLevel = playerSkillList[i].SkillLevel;
                }
                else
                {
                    // ���� ���� ��ų������ ������Ʈ
                    playerSkillList[i].SkillLevel = playerSkill.SkillLevel;
                }
            }

            // ��ġ�� ��ų �̸� �ִٸ� �̹� �� ��ų�� ��Ͽ� �ִٴ� ��, ���� �ߺ� üũ
            if(playerSkillList[i].SkillName.Equals(playerSkill.SkillName) &&
                    playerSkillList[i].SkillSlotNumber == playerSkill.SkillSlotNumber)
            {
                // ���Ӱ� �߰��ϴ� ��ų�� ����
                // Skill -> Temp�� Ÿ�� ����, MonoBehaviour ���� ���ֱ� ����
                playerSkillList[i] = MigrationSkillToTempStat(playerSkill, playerSkillList[i]);
                hadUpdate = true;
                break;
            }
            else
            {
                // �̸�, ���� ���� ��ġ���� �ʴ� ���
                // �̸��� ��ġ���� �ʴ� ���
                // ���Ը� ��ġ���� �ʴ� ���
                // ��ġ�� ������ �߰� / �������� ���Կ� ��� �����ϱ� ����
                // TempSkillStat Ÿ������ ��ȯ�ؼ� ���ϵ� ���� ����Ʈ�� �߰�
                TempSkillStat tempSkill = MigrationSkillToTempStat(playerSkill, new TempSkillStat());
                //playerSkillList.Add(tempSkill);
                tmpBox = tempSkill;
            }
        }

        // ����Ʈ�� �ϳ��� �߰��Ǿ��ְ� ����Ʈ�� �߰��� ���� ������ �߰�
        if(tmpBox != null && !hadUpdate)
        {
            playerSkillList.Add(tmpBox);
        }

        // ��Ͽ� �ϳ��� ���ų� �ߺ� ���ٸ�
        if(playerSkillList.Count == 0)
        {
            // TempSkillStat Ÿ������ ��ȯ�ؼ� ���ϵ� ���� ����Ʈ�� �߰�
            TempSkillStat tempSkill = MigrationSkillToTempStat(playerSkill, new TempSkillStat());
            playerSkillList.Add(tempSkill);
        }
    }

    // ������ Ÿ�� �ű�°� �����Ƽ� ���� �Լ�(���� ������, ���� ���� ����)
    // SkillStat Ÿ�� -> TempSkillStat Ÿ��
    public TempSkillStat MigrationSkillToTempStat(SkillStat originSkillData, TempSkillStat tempData)
    {
        tempData.Id = originSkillData.Id;
        tempData.SkillName = originSkillData.SkillName;
        tempData.SkillContent = originSkillData.SkillContent;
        tempData.SkillEffect = originSkillData.SkillEffect;
        tempData.SkillAtk = originSkillData.SkillAtk;
        tempData.SkillSlotNumber = originSkillData.SkillSlotNumber;
        tempData.SkillLevel = originSkillData.SkillLevel;

        return tempData;
    }

    // ������ Ÿ�� �ű�°� �����Ƽ� ���� �Լ�2
    // TempSkillStat Ÿ�� -> SkillStat Ÿ��
    public SkillStat MigrationTempToSkillStat(TempSkillStat originTempData, SkillStat skillData)
    {
        skillData.Id = originTempData.Id;
        skillData.SkillName = originTempData.SkillName;
        skillData.SkillContent = originTempData.SkillContent;
        skillData.SkillEffect = originTempData.SkillEffect;
        skillData.SkillAtk = originTempData.SkillAtk;
        skillData.SkillSlotNumber = originTempData.SkillSlotNumber;
        skillData.SkillLevel = originTempData.SkillLevel;

        return skillData;
    }

    // �� TempSkillStat ��ü�� �� ����� ���� ��ġ�ϴ��� �˻��ϴ� �Լ�
    // �ʿ��ϴٸ� �����ε� �� ����
    public bool CompareSkillStat(TempSkillStat tempSkill, TempSkillStat originSkill)
    {
        if(tempSkill.Id != originSkill.Id)
            return false;
        if(!tempSkill.SkillName.Equals(originSkill.SkillName))
            return false;
        if(!tempSkill.SkillContent.Equals(originSkill.SkillContent))
            return false;
        if(!tempSkill.SkillEffect.Equals(originSkill.SkillEffect))
            return false;
        if(tempSkill.SkillAtk != originSkill.SkillAtk)
            return false;
        if(tempSkill.SkillSlotNumber != originSkill.SkillSlotNumber)
            return false;
        if(tempSkill.SkillLevel != originSkill.SkillLevel)
            return false;
        // ��� ��������� true ����
        return true;
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

    // ���� ��ȣ
    [SerializeField]
    private int _SkillSlotNumber = -1;

    // ����
    [SerializeField]
    private int _SkillLevel;

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

    public int SkillSlotNumber
    {
        get { return _SkillSlotNumber; }
        set { _SkillSlotNumber = value; }
    }

    public int SkillLevel
    {
        get { return _SkillLevel; }
        set { _SkillLevel = value; }
    }
}