using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public interface ICreature
{
    public void UpdateController();
    public void UpdateMove();
    public void UpdateAttack();
    public void UpdateSkill();
    public void UpdateIdle();
    public void UpdateDead();
}
