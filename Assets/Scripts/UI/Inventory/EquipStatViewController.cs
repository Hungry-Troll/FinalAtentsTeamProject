using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class EquipStatViewController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    // �����ϰ� �ִ� ������
    public GameObject _equipItem;

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
        GameManager.Ui.EquipStatViewClose();
    }

    public void UnEquipWeaponItem()
    {

    }

    public void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }
}
