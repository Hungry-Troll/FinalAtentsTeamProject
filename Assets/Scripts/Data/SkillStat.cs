using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillStat : MonoBehaviour
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
