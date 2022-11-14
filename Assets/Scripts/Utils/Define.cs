using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum SceneName
    {
        Title,
        CharacterSelectScene,
        SelectPet,
        Tutorial,
        Village02,
        DunGeon,
    }
    public enum ItemName
    {
        None,
        sword1,
        sword2,
        sword3,
        gun1,
        gun2,
        gun3,
        book1,
        book2,
        book3,
        armour1,
        armour2,
        armour3,
        potion1,
        potion2,
        potion3,
    }
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
        None,
        Weapon,
        Armour,
        Consumables,

    }
    public enum CreatureState
    {
        Idle,
        Move,
        KeyboardMove,
        AutoMove,
        Skill,
        Attack,
        Dead,
        Roll,
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

    public enum EffectType
    {
        Miss,
        Normal,
        Critical,
        Roar,
        Scratch,
        Bite
    }
}
