using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager 
{

    public void PlayerDamaged(int atk) 
    {
        GameManager.Obj._playerController.OnDamaged(atk);
    }
}
