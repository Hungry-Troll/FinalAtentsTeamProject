using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Util;

public class ItemStatViewController : MonoBehaviour , IBeginDragHandler, IDragHandler
{
    // ������ �̹��� ���޿�
    public Sprite _sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ����
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� ��
        transform.position = eventData.position;
    }

    public void CloseItem()
    {
        // �κ��丮 �ݱ� Ui�� Ui�Ŵ������� ������
        GameManager.Ui.ItemStatViewClose(gameObject.name);
    }

    public void WeaponEquipItem()
    {
        // ���� ������ ������ ����
        GameManager.Ui.ItemStatViewWeaponEquip();
    }

    public void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }

}
