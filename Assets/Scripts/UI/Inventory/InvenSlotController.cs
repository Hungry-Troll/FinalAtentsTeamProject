using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlotController : MonoBehaviour
{
    // ���Կ� ���� ������
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
        // ������ Ÿ�Կ� ���� ���� �����ߵ� �켱 ���⸸
        if (_SlotItem.Count > 0 )
        {
            GameManager.Ui._inventoryController._weapon = GameManager.Ui.ItemStatViewOpen(_SlotItem[0]);
        } 
    }
}
