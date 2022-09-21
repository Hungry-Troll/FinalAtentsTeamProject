using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlotController : MonoBehaviour
{
    // 슬롯에 들어가는 아이템
    public List<GameObject> _SlotItem = new List<GameObject>();

    void Start()
    {
        //_spriteImage = GetComponent<Sprite>();
        //_spriteImage = Resources.Load<Sprite>("Resource/Image/ItemImage" + gameObject.name);
        //_itmeImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
