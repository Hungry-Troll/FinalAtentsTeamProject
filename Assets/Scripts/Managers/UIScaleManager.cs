using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIScaleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform UI_Portrait = Util.FindChild("Image", gameObject.transform);
        Button UI_PortraitToClick = UI_Portrait.AddComponent<Button>();
        Debug.Log(UI_PortraitToClick);
    }
}
