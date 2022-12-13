using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���� �߰����� ��
// �̵� �ӵ�, ��Ȱ ��Ÿ��

[System.Serializable]
public class PetStat : Stat
{
    // �̵� �ӵ�
    [SerializeField]
    private int _Speed;

    // ��Ȱ ��Ÿ��
    [SerializeField]
    private int _Revive_Time;

    public int Speed
    {
        get { return _Speed; }
        set { _Speed = value; }
    }

    public int Revive_Time
    {
        get { return _Revive_Time; }
        set { _Revive_Time = value; }
    }
}


// MonoBehaviour�� ��ӹ��� �ʴ� �꽺�� Ŭ����
// ������ ������ ����
[System.Serializable]
public class TempPetStat
{
    // �̸�
    [SerializeField]
    private string _Name;

    // ���� ü��
    [SerializeField]
    private int _Hp;

    // ���ݷ�
    [SerializeField]
    private int _Atk;

    // ����
    [SerializeField]
    private int _Def;

    // ���� ����
    [SerializeField]
    private int _Lv;

    // �ִ� ü��
    [SerializeField]
    private int _Max_Hp;
   
    // �̵� �ӵ�
    [SerializeField]
    private int _Speed;

    // ��Ȱ ��Ÿ��
    [SerializeField]
    private int _Revive_Time;

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public int Hp
    {
        get { return _Hp; }
        set { _Hp = value; }
    }

    public int Atk
    {
        get { return _Atk; }
        set { _Atk = value; }
    }

    public int Def
    {
        get { return _Def; }
        set { _Def = value; }
    }

    public int Lv
    {
        get { return _Lv; }
        set { _Lv = value; }
    }

    public int Max_Hp
    {
        get { return _Max_Hp; }
        set { _Max_Hp = value; }
    }

    public int Speed
    {
        get { return _Speed; }
        set { _Speed = value; }
    }

    public int Revive_Time
    {
        get { return _Revive_Time; }
        set { _Revive_Time = value; }
    }
}
