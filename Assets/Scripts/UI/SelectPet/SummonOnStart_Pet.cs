using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOnStart_Pet : MonoBehaviour
{
    private int _characters;
    public GameObject[] _sHero = new GameObject[3];
    private GameObject _respawn = null;

    void Start()
    {
        _characters = TurnOnTheStage_Pet._characterNum;
        _respawn = GameObject.FindGameObjectWithTag("Respawn");
        
        // 위치를 위한 오브젝트
        // 수정 4 -> 3
        for(int i = 0; i < 3; i++)
        {
            if(_characters == i)
            {
                Instantiate(_sHero[i], _respawn.transform.position, Quaternion.identity);
            }
        }
    }

    void Update()
    {

    }
}
