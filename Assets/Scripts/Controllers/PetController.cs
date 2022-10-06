using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PetController : MonoBehaviour
{
    public PetStat _petStat;

    void Start()
    {
        _petStat = GetComponent<PetStat>();
    }

    void Update()
    {

    }
}