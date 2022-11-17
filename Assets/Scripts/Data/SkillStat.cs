using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
