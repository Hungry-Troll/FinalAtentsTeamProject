using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScene : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePlayerMovement()
    {
        animator.enabled = true;

    }

    public void DisablePlayerMovement()
    {
        animator.enabled = false;
        
    }
}
