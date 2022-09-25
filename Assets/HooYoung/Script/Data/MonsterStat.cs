using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    // 이름
    [SerializeField]
    private string _Name;
    // 골드
    [SerializeField]
    private int _Gold;

    // 현재 경험치
    [SerializeField]
    private int _Exp;

    // 이동속도
    [SerializeField]
    private int _Speed;

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
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
