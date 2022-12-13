using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillStat : MonoBehaviour
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
