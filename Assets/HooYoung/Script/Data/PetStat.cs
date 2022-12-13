using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상속 제외 추가받을 것
// 이동 속도, 부활 쿨타임

[System.Serializable]
public class PetStat : Stat
{
    // 이동 속도
    [SerializeField]
    private int _Speed;

    // 부활 쿨타임
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


// MonoBehaviour를 상속받지 않는 펫스탯 클래스
// 내용은 완전히 동일
[System.Serializable]
public class TempPetStat
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
   
    // 이동 속도
    [SerializeField]
    private int _Speed;

    // 부활 쿨타임
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
