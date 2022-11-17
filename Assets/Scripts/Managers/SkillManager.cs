using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// 스킬 json 파일 가지고 오기
// 추후 스킬 관리를 여기서 할 수도?
public class SkillManager
{
    // json 파일 저장할 변수들
    string[] _skillJsonArr;
    private List<TempSkillStat> _skillList;

    public void Init()
    {
        _skillList = new List<TempSkillStat>();
        LoadSkillList();
    }

    // 스킬로드
    public void LoadSkillList()
    {
        string fileName = "Skills";
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/Skill/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // {}{}{} -> {} {} {} 각각 배열에 저장
        SplitJson(json);

        for (int i = 0; i < _skillJsonArr.Length; i++)
        {
            // {name, type, skill...} 한 세트씩 ItemStat 타입으로 parsing
            TempSkillStat skill = JsonUtility.FromJson<TempSkillStat>(_skillJsonArr[i]);
            // 리스트에 추가
            _skillList.Add(skill);
        }
    }

    void SplitJson(string jsonSentence)
    {
        _skillJsonArr = jsonSentence.Trim().Split("}");

        for (int i = 0; i < _skillJsonArr.Length - 1; i++)
        {
            _skillJsonArr[i] += "}";

            // 디버깅용
            //Debug.Log(_itemJsonArr += "}");
        }
    }

    // 스킬 스텟(이름/설명/효과) 가지고 오는 함수
    public void SkillStatLoadJson(string skillName, SkillStat skillStat)
    {
        // 이름으로 서치
        TempSkillStat tempStat = SearchItem(skillName);
        // 가져온 데이터 대입
        skillStat.Id = tempStat.Id;
        skillStat.SkillName = tempStat.SkillName;
        skillStat.SkillContent = tempStat.SkillContent;
        skillStat.SkillEffect = tempStat.SkillEffect;
        skillStat.SkillAtk = tempStat.SkillAtk;
    }

    // 리스트에서 찾는 함수
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


// 스텟을 저장하기 위한 임시 클래스 (모노비헤비어를 상속 받으면 json 파일 데이터가 저장이 안됨)
[System.Serializable]
public class TempSkillStat
{
    // 코드
    [SerializeField]
    private string _Id;

    // 이름
    [SerializeField]
    private string _SkillName;

    // 설명
    [SerializeField]
    private string _SkillContent;

    // 효과
    [SerializeField]
    private string _SkillEffect;

    // 공격력
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