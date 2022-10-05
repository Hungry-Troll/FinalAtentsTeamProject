using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Pet
    {
        Triceratops,
        Pachycephalosaurus,
        Brachiosaurus,
        None,
    }
    public enum Monster
    {
        Parasaurolophus,
        Pteranodon,
        Velociraptor,
        None,
    }
    public enum Job
    {
        Superhuman,
        Cyborg,
        Scientist,
        None,
    }

    public enum ItemType
    {
        Weapon,
        Armour,
        Consumables,
        None,
    }
    public enum CreatureState
    {
        Idle,
        Move,
        AutoMove,
        Skill,
        Attack,
        Dead,
        None,
    }
    public enum JoystickState
    {
        InputFalse,
        InputTrue,
    }

    public enum SceneAttackButton
    {
        Attack,
        Skill1,
        Skill2,
        Skill3,
        Rolling,
    }

    public enum Info
    {
        Hp,
        Mp,
    }
}
