using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� Ŭ����
// Stat ��ӹ��� ����

// �ڵ� : �� �ڸ� ���� (Ÿ���ڵ�, �����ڵ�, ����)
//          ����          : 1, ���         : 2,  �Ҹ�ǰ : 3
//          ��ȭ�ΰ�    : 1, ���̺���    : 2, ���̾�Ƽ��Ʈ : 3
//          �����ڵ�� ���⿡�� �ش�, �� �� Ÿ���� ��� 1
//          �����ڵ忡�� �Ҹ�ǰ���� ��� ����ϴ� ���� 0
//          ex) 122 : ��ö��
//                231 : ��� ����
//                301 : ü�� ����
// �̸�
// Ÿ�� : Weapon(����), Armour(���), Potion(����/�Ҹ�ǰ)
// ��ų : ȿ��
// ������
// ���ź��
// �Ǹź��

[System.Serializable]
public class ItemStat
{
    // ������ �����ε�� �⺻ ������ ������Ƿ� ����� ����
    public ItemStat() { }
    // ������ ������ �� ��� ������
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

    // �ڵ�
    [SerializeField]
    private int _Id;

    // �̸�
    [SerializeField]
    private string _Name;
    
    // Ÿ��
    [SerializeField]
    private string _Type;
    
    // ȿ��
    [SerializeField]
    private int _Skill;
    
    // �� ����
    [SerializeField]
    private string _Info;
    
    // ���ź��
    [SerializeField]
    private int _Get_Price;
    
    // �Ǹź��
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
