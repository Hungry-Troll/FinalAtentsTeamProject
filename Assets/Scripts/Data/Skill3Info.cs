using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Info : MonoBehaviour
{
    //스킬 3번 효과
    [SerializeField]
    private int _Skill3Level;
    [SerializeField]
    private int _Skill3StatMaxHp;
    [SerializeField]
    private int _Skill3StatHp;
    [SerializeField]
    private int _Skill3StatAtk;
    [SerializeField]
    private int _Skill3StatDef;
    [SerializeField]
    private float _Duration;

    public int Skill3Level
    {
        get { return _Skill3Level; }
        set { _Skill3Level = value; }
    }
    public int Skill3StatMaxHp
    {
        get { return _Skill3StatMaxHp; }
        set { _Skill3StatMaxHp = value; }
    }
    public int Skill3StatHp
    {
        get { return _Skill3StatHp; }
        set { _Skill3StatHp = value; }
    }
    public int Skill3StatAtk
    {
        get { return _Skill3StatAtk; }
        set { _Skill3StatAtk = value; }
    }
    public int Skill3StatDef
    {
        get { return _Skill3StatDef; }
        set { _Skill3StatDef = value; }
    }
    public float Duration
    {
        get { return _Duration; }
        set { _Duration = value; }
    }
}
