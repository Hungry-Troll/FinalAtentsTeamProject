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

    // �κ��丮 ���� ��ȣ Ȯ�ο� ����
    public int _invenSlotCount;

    // �÷��̾� ���� ����
    public GameObject _weapon = null;
    // �÷��̾� ���� ����
    public ItemStatEX _weaponStat;

    // �÷��̾� ���� ��
    public GameObject _armour = null;

    private void Start()
    {
        // �ʿ�������� ����
        _invenSlotCount = 0;
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
