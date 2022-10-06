using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���� �߰����� ��
// �̵� �ӵ�, ��Ȱ ��Ÿ��

[System.Serializable]
public class PetStat : Stat
{
    // �̵� �ӵ�
    [SerializeField]
    private int _Speed;

    // ��Ȱ ��Ÿ��
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
