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
