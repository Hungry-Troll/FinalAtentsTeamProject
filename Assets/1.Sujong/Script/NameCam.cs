using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameCam : MonoBehaviour
{
    public GameObject Cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Cam.transform.rotation;
    }        
}
