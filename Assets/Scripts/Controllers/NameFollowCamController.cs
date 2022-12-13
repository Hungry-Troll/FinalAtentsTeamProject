using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameFollowCamController : MonoBehaviour
{
    GameObject Cam;

    // Start is called before the first frame update
    public void Awake()
    {
        
    }
    private void Start()
    {
        Debug.Log(Cam.name);
    }

    // Update is called once per frame
    void Update()
    {
      
    }        
}
