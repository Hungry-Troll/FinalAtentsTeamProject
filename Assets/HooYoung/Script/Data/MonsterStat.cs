using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    
    // ���
    [SerializeField]
    private int _Gold;

    // ���� ����ġ
    [SerializeField]
    private int _Exp;

    // �̵��ӵ�
    [SerializeField]
    private int _Speed;


    public int Gold
    {
        get { return _Gold; }
        set { _Gold = value; }
    }

    public int Exp
    {
        get { return _Exp; }
        set { _Exp = value; }
    }

    public int Speed
    {
        get { return _Speed; }
        set { _Speed = value; }
    }

}


// MonoBehaviour�� ��ӹ��� �ʴ� ���ͽ��� Ŭ����
// ������ ������ ����
[System.Serializable]
public class TempMonsterStat
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

    // ���
    [SerializeField]
    private int _Gold;

    // ���� ����ġ
    [SerializeField]
    private int _Exp;

    // �̵��ӵ�
    [SerializeField]
    private int _Speed;

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

    public int Gold
    {
        get { return _Gold; }
        set { _Gold = value; }
    }

    public int Exp
    {
        get { return _Exp; }
        set { _Exp = value; }
    }

    public int Speed
    {
        get { return _Speed; }
        set { _Speed = value; }
    }

}
