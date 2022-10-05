using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Util;
using static Define;

public class ItemStatViewController : MonoBehaviour , IBeginDragHandler, IDragHandler
{
    // ������ �̹��� ���޿�
    public Sprite _sprite;
    // ������ Ÿ�� ���п� �̳�
    Define.ItemType _itemType;

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
        _itemType = Define.ItemType.Weapon;
        // ���� ������ ������ ����
        GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
    }

    public void ArmourEquipItem()
    {
        _itemType = Define.ItemType.Armour;
        // ���� ������ ������ ����
        GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
    }

    public void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }

}