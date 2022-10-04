using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    Transform mainCam;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
        offset = mainCam.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mainCam.position = player.position + offset;
    }
}