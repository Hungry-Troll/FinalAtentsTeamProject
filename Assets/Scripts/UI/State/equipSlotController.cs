using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class equipSlotController : MonoBehaviour
{
    // ���Կ� ���� ������ 
    public List<GameObject> _equipSlotItem = new List<GameObject>();


    public void Start()
    {

    }
    // ���� ���Կ��� ������ ���� �����ִ� �Լ�
    public void WeaponButtonClick()
    {
        GameManager.Ui.ItemStatViewOpen(_equipSlotItem[0]);
    }
 
}
