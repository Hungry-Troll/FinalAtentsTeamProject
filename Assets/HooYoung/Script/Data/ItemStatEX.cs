using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ϰ� �����ۿ� ���̴� ������Ʈ �뵵

public class ItemStatEX : MonoBehaviour
{
    // �ڵ�
    [SerializeField]
    private string _Id;

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

    // ���� ����
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
