using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlotController : MonoBehaviour
{
    // 슬롯에 들어가는 아이템
    public List<GameObject> _SlotItem = new List<GameObject>();
    public int _invenSlotCount;

    void Start()
    {
        _invenSlotCount = 0;
        //_spriteImage = GetComponent<Sprite>();
        //_spriteImage = Resources.Load<Sprite>("Resource/Image/ItemImage" + gameObject.name);
        //_itmeImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        // 아이템 타입에 따라서 추후 나눠야됨 우선 무기만
        if (_SlotItem.Count > 0 )
        {
            GameManager.Ui._inventoryController._weapon = GameManager.Ui.ItemStatViewOpen(_SlotItem[0]);
        } 
    }
}
