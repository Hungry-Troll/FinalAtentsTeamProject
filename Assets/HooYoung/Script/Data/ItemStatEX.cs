using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 순수하게 아이템에 붙이는 컴포넌트 용도

public class ItemStatEX : MonoBehaviour
{
    // 코드
    [SerializeField]
    private string _Id;

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

    // 보유 개수
    [SerializeField]
    private int _Count;

    public string Id
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

    public int Count
    {
        get { return _Count; }
        set { _Count = value; }
    }
}
