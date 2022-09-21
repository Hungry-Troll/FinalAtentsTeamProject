using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    // �κ��丮 ������
    public List<GameObject> _item = new List<GameObject>();

    // �κ��丮 ���Թ迭
    public InvenSlotController[] _invenSlotArray;

    // �κ��丮 ���� �迭�� ����Ʈ�� ��ȯ
    public List<InvenSlotController> _invenSlotList = new List<InvenSlotController>();
    private void Start()
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

    public void CloseInventory()
    {
        // �κ��丮 �ݱ� Ui�� Ui�Ŵ������� ������
        GameManager.Ui.InventoryClose();
    }
}
