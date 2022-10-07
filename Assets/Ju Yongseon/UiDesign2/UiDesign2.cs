using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDesign2 : MonoBehaviour
{
    public Animator aniViking;
    public Animator aniSoldier;
    public Animator aniDoctor;
    public Light light1;

    bool bLeft = false;
    bool bCenter = false;
    bool bRight = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(bLeft)
        {
            Debug.Log("Warrior");
            light1.transform.position = new Vector3(-1.5f, 1, -5);
            aniViking.SetInteger("Index", 1);
            aniSoldier.SetInteger("Index", 0);
            aniDoctor.SetInteger("Index", 0);
            bLeft = false;
        }

        if(bCenter)
        {
            Debug.Log("Archer");
            light1.transform.position = new Vector3(0f, 1, -5);
            aniViking.SetInteger("Index", 0);
            aniSoldier.SetInteger("Index", 1);
            aniDoctor.SetInteger("Index", 0);
            bCenter = false;
        }

        if(bRight)
        {
            Debug.Log("Scientist");
            light1.transform.position = new Vector3(1.5f, 1, -5);
            aniViking.SetInteger("Index", 0);
            aniSoldier.SetInteger("Index", 0);
            aniDoctor.SetInteger("Index", 1);
            bRight = false;
        }

        
    }

    public void LeftButtom()
    {
        bLeft = true;
        bRight = false;
        bCenter = false;
    }

    public void CenterButtom()
    {
        bLeft = false;
        bRight = false;
        bCenter = true;
    }

    public void RightButtom()
    {
        bLeft = false;
        bRight = true;
        bCenter = false;
    }

    
}
