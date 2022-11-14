using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollanimatorTest : MonoBehaviour
{
    public Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetInteger("playerStat", 5);
    }
}
