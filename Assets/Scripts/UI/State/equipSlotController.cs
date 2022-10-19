using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class equipSlotController : MonoBehaviour
{
    // 슬롯에 들어가는 아이템 
    public List<GameObject> _equipSlotItem = new List<GameObject>();


    public void Start()
    {

    }
    // 무기 슬롯에서 착용한 무기 보여주는 함수
    public void WeaponButtonClick()
    {
        GameManager.Ui.ItemStatViewOpen(_equipSlotItem[0]);
    }
 
}
