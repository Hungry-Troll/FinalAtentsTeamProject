using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 스탯

// ===추가사항===
// 직업
// 레벨업 경험치
// 현재 경험치

[System.Serializable]
public class PlayerStat : Stat
{
    // 플레이어 스탯 싱글톤으로 관리
    // private static PlayerStat _instance;


    // null 대비 getter

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
        // 생성한 게임매니저 플레이어 스텟에 넣음
    }*/

    // 직업
    [SerializeField]
    private string _Job;

    // 현재 경험치
    [SerializeField]
    private int _Exp;

    // 레벨업 경험치
    [SerializeField]
    private int _Lv_Exp;

    // 보유 골드
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

// MonoBehaviour를 상속받지 않는 플레이어 스탯 클래스
// 이미 StatManager에 TempStatEX이 있지만 몬스터, 아이템 변수까지 포함되어있기 때문에 저장할 때 방해되므로 새로 생성
// 내용은 완전히 동일
[System.Serializable]
public class TempPlayerStat
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

    // 직업
    [SerializeField]
    private string _Job;

    // 현재 경험치
    [SerializeField]
    private int _Exp;

    // 레벨업 경험치
    [SerializeField]
    private int _Lv_Exp;

    // 보유 골드
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

