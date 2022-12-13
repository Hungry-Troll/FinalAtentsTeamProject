using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    
    // 골드
    [SerializeField]
    private int _Gold;

    // 현재 경험치
    [SerializeField]
    private int _Exp;

    // 이동속도
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


// MonoBehaviour를 상속받지 않는 몬스터스탯 클래스
// 내용은 완전히 동일
[System.Serializable]
public class TempMonsterStat
{
    // 이름
    [SerializeField]
    private string _Name;

    // 현재 체력
    [SerializeField]
    private int _Hp;

    // 공격력
    [SerializeField]
    private int _Atk;

    // 방어력
    [SerializeField]
    private int _Def;

    // 현재 레벨
    [SerializeField]
    private int _Lv;

    // 최대 체력
    [SerializeField]
    private int _Max_Hp;

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
