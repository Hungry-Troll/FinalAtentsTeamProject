using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDesign2 : MonoBehaviour
{
    public Light light;

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
            light.transform.position = new Vector3(-1.5f, 1, -5);
            bLeft = false;
        }

        if(bCenter)
        {
            Debug.Log("Archer");
            light.transform.position = new Vector3(0f, 1, -5);
            bCenter = false;
        }

        if(bRight)
        {
            Debug.Log("Scientist");
            light.transform.position = new Vector3(1.5f, 1, -5);
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
