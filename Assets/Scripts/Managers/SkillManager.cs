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
        skillStat.SkillSlotNumber = tempStat.SkillSlotNumber;
        skillStat.SkillLevel = tempStat.SkillLevel;
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


    // 플레이어별 스킬 리스트에 스킬 추가하는 함수
    // AddSkillToPlayerSkillList(저장할 스킬 리스트, 저장할 스킬)
    // -> 스킬 리스트에 추가하려는 스킬이 존재하는지 검사하고 없으면 추가, 있으면 업데이트
    public void UpdatePlayerSkillList(List<TempSkillStat> playerSkillList, SkillStat playerSkill)
    {
        // 널이면 초기화
        if(playerSkillList == null)
            playerSkillList = new List<TempSkillStat>();

        // 반복문 실행중에 직접 추가하면 반복 횟수 증가하므로 임시로 추가된 값 가지고 있을 변수
        TempSkillStat tmpBox = null;
        // 스킬 목록 수정 했는지 확인용 변수
        bool hadUpdate = false;

        // 목록에 스킬이 하나라도 있는 경우
        // 이름 겹치는 스킬 있는지 목록에서 검사
        for(int i = 0; i < playerSkillList.Count; i++)
        {
            // 일단 스킬 레벨 업데이트
            if(playerSkillList[i].SkillName.Equals(playerSkill.SkillName))
            {
                // 기존 리스트에 있던 레벨이 더 높거나 같으면 true, 넘어온 스탯 레벨이 더 높으면 false
                bool isHigherBeforeOne = playerSkillList[i].SkillLevel >= playerSkill.SkillLevel ? true : false;
                if(isHigherBeforeOne)
                {
                    // 기존에 있던 스킬레벨로 업데이트
                    playerSkill.SkillLevel = playerSkillList[i].SkillLevel;
                }
                else
                {
                    // 새로 들어온 스킬레벨로 업데이트
                    playerSkillList[i].SkillLevel = playerSkill.SkillLevel;
                }
            }

            // 겹치는 스킬 이름 있다면 이미 그 스킬이 목록에 있다는 것, 슬롯 중복 체크
            if(playerSkillList[i].SkillName.Equals(playerSkill.SkillName) &&
                    playerSkillList[i].SkillSlotNumber == playerSkill.SkillSlotNumber)
            {
                // 새롭게 추가하는 스킬로 변경
                // Skill -> Temp로 타입 변경, MonoBehaviour 간섭 없애기 위함
                playerSkillList[i] = MigrationSkillToTempStat(playerSkill, playerSkillList[i]);
                hadUpdate = true;
                break;
            }
            else
            {
                // 이름, 슬롯 전부 일치하지 않는 경우
                // 이름만 일치하지 않는 경우
                // 슬롯만 일치하지 않는 경우
                // 겹치지 않으니 추가 / 여러개의 슬롯에 등록 가능하기 때문
                // TempSkillStat 타입으로 변환해서 리턴된 값을 리스트에 추가
                TempSkillStat tempSkill = MigrationSkillToTempStat(playerSkill, new TempSkillStat());
                //playerSkillList.Add(tempSkill);
                tmpBox = tempSkill;
            }
        }

        // 리스트에 하나라도 추가되어있고 리스트에 추가된 흔적 없으면 추가
        if(tmpBox != null && !hadUpdate)
        {
            playerSkillList.Add(tmpBox);
        }

        // 목록에 하나도 없거나 중복 없다면
        if(playerSkillList.Count == 0)
        {
            // TempSkillStat 타입으로 변환해서 리턴된 값을 리스트에 추가
            TempSkillStat tempSkill = MigrationSkillToTempStat(playerSkill, new TempSkillStat());
            playerSkillList.Add(tempSkill);
        }
    }

    // 일일이 타입 옮기는거 귀찮아서 만든 함수(원본 데이터, 새로 담을 변수)
    // SkillStat 타입 -> TempSkillStat 타입
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

    // 일일이 타입 옮기는거 귀찮아서 만든 함수2
    // TempSkillStat 타입 -> SkillStat 타입
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

    // 두 TempSkillStat 객체의 각 멤버의 값이 일치하는지 검사하는 함수
    // 필요하다면 오버로딩 할 예정
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
        // 모두 통과했으면 true 리턴
        return true;
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

    // 슬롯 번호
    [SerializeField]
    private int _SkillSlotNumber = -1;

    // 레벨
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