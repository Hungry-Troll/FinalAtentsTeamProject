using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameObjLookCam : MonoBehaviour
{
    // 프리팹의 객체 중 알맞은 부모 객체를 찾아 빈 객체를 넣어주고
    // Text Mesh 컴포넌트를 붙여 이름을 넣어준다. 
    // Anchor를 Upper Center로, Font Size를 조정한 후 이 스크립트를 넣어준다.
    public GameObject Cam;

    void Update()
    {
        //transform.rotation = Cam.transform.rotation;
    }
}