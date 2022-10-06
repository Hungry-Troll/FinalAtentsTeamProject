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
