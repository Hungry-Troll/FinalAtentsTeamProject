using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class EquipSlotController : MonoBehaviour
{

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

    public void OnButtonClick()
    {
        // ������ Ÿ�Կ� ���� ���� �����ߵ� �켱 ���⸸
        if (GameManager.Ui._inventoryController._weapon != null)
        {
            GameManager.Ui.EquipStatViewOpen(GameManager.Ui._inventoryController._weapon);
        } 
    }
}
