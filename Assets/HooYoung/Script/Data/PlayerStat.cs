using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ����

// ===�߰�����===
// ����
// ������ ����ġ
// ���� ����ġ

[System.Serializable]
public class PlayerStat : Stat
{
    // �÷��̾� ���� �̱������� ����
    // private static PlayerStat _instance;


    // null ��� getter

/*    public static PlayerStat Instance()
    {
        if (_instance == null)
        {
            _instance = new PlayerStat();
        }
        return _instance;
    }*/

    // setter
/*    public static PlayerStat SetInstance
    {
        set { _instance = value; }
        // ������ ���ӸŴ��� �÷��̾� ���ݿ� ����
    }*/

    // ����
    [SerializeField]
    private string _Job;

    // ���� ����ġ
    [SerializeField]
    private int _Exp;

    // ������ ����ġ
    [SerializeField]
    private int _Lv_Exp;

    // ���� ���
    [SerializeField]
    private int _Gold;

    public string Job
    {
        get { return _Job; }
        set { _Job = value; }
    }

    public int Exp
    {
        get { return _Exp; }
        set { _Exp = value; }
    }

    public int Lv_Exp
    {
        get { return _Lv_Exp; }
        set { _Lv_Exp = value; }
    }

    public int Gold
    {
        get { return _Gold; }
        set { _Gold = value; }
    }
}

// MonoBehaviour�� ��ӹ��� �ʴ� �÷��̾� ���� Ŭ����
// �̹� StatManager�� TempStatEX�� ������ ����, ������ �������� ���ԵǾ��ֱ� ������ ������ �� ���صǹǷ� ���� ����
// ������ ������ ����
[System.Serializable]
public class TempPlayerStat
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

    // ����
    [SerializeField]
    private string _Job;

    // ���� ����ġ
    [SerializeField]
    private int _Exp;

    // ������ ����ġ
    [SerializeField]
    private int _Lv_Exp;

    // ���� ���
    [SerializeField]
    private int _Gold;

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

    public string Job
    {
        get { return _Job; }
        set { _Job = value; }
    }

    public int Exp
    {
        get { return _Exp; }
        set { _Exp = value; }
    }

    public int Lv_Exp
    {
        get { return _Lv_Exp; }
        set { _Lv_Exp = value; }
    }

    public int Gold
    {
        get { return _Gold; }
        set { _Gold = value; }
    }
}

