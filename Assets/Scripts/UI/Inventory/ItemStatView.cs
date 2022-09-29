using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Util;

public class ItemStatView : MonoBehaviour , IBeginDragHandler, IDragHandler
{
    // ������ �̹��� ���޿�
    Sprite _sprite;

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
        // ������ �̹��� �ε� 
        // ItemStatView�� �����鼭 �ڵ����� �κ��丮��Ʈ�ѷ� ������ ���ӿ�����Ʈ�� ���� ��

        // ������ �̹����� �켱 ��ü
        // ���� ������ ������ ����
        _sprite = GameManager.Resource.GetImage(GameManager.Ui._inventoryController._weapon.name);
        GameManager.Ui.ItemStatViewWeaponEquip(_sprite, gameObject.transform);
    }

    public void DropItem()
    {
        // ������ ������ �ڵ�
    }

}
