using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Effect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < GameManager.Obj._monsterContList.Count; i++)
        {
            if (other.name == GameManager.Obj._monsterContList[i].name)
            {
                GameManager.Obj._monsterContList[i].OnDamaged(GameManager.Obj._playerStat.Atk, 1);
                GameManager.Obj._monsterContList[i].OnDamaged(GameManager.Obj._playerStat.Atk, 1);
                GameManager.Obj._monsterContList[i].OnDamaged(GameManager.Obj._playerStat.Atk, 1);
            }
        }
    }
}