using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 스탯 클래스
// Stat 상속받지 않음

// 코드 : 세 자리 숫자 (타입코드, 직업코드, 레벨)
//          무기          : 1, 방어         : 2,  소모품 : 3
//          강화인간    : 1, 사이보그    : 2, 사이언티스트 : 3
//          레벨코드는 무기에만 해당, 그 외 타입은 모두 1
//          직업코드에서 소모품같이 모두 사용하는 경우는 0
//          ex) 122 : 강철검
//                231 : 흰색 가운
//                301 : 체력 물약
// 이름
// 타입 : Weapon(무기), Armour(방어), Potion(포션/소모품)
// 스킬 : 효과
// 상세정보
// 구매비용
// 판매비용

[System.Serializable]
public class ItemStat
{
    // 생성자 오버로드시 기본 생성자 사라지므로 만들어 놓음
    public ItemStat() { }
    // 아이템 생성할 때 대비 생성자
    public ItemStat(int id, string name, string type, int skill, string info, int get_price, int sale_price)
    {
        _Id = id;
        _Name = name;
        _Type = type;
        _Skill = skill;
        _Info = info;
        _Get_Price = get_price;
        _Sale_Price = sale_price;
    }

    // 코드
    [SerializeField]
    private int _Id;

    // 이름
    [SerializeField]
    private string _Name;
    
    // 타입
    [SerializeField]
    private string _Type;
    
    // 효과
    [SerializeField]
    private int _Skill;
    
    // 상세 설명
    [SerializeField]
    private string _Info;
    
    // 구매비용
    [SerializeField]
    private int _Get_Price;
    
    // 판매비용
    [SerializeField]
    private int _Sale_Price;

    public int Id
    {
        get { return _Id; }
        set { _Id = value; }
    }

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public string Type
    {
        get { return _Type; }
        set { _Type = value; }
    }

    public int Skill
    {
        get { return _Skill; }
        set { _Skill = value; }
    }

    public string Info
    {
        get { return _Info; }
        set { _Info = value; }
    }

    public int Get_Price
    {
        get { return _Get_Price; }
        set { _Get_Price = value; }
    }

    public int Sale_Price
    {
        get { return _Sale_Price; }
        set { _Sale_Price = value; }
    }
}
