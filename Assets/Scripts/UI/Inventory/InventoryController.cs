using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
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
    // �÷��̾� ���� ���� ����
    public ItemStatEX _weaponStat;

    // �÷��̾� ���� ��
    public GameObject _armour = null;
    // �÷��̾� ���� �� ����
    public ItemStatEX _armourStat;

    // ������ �̸�, �� ���� ������ ��ųʸ�
    private Dictionary<string, int> _itemCountDictionary = new Dictionary<string, int>(); 
    public Dictionary<string, int> ItemCountDictionary
    {
        get { return _itemCountDictionary; }
    }

    Text Text_Count;
    
    private void Start()
    {
        // �ʿ�������� ����
        _invenSlotCount = 0;
        // �κ��丮 ������ ���� Text UI ���� ǥ��
        Transform Text_CountTr = Util.FindChild("Text_Count", gameObject.transform);
        Text_Count = Text_CountTr.GetComponent<Text>();
    }
    void Update()
    {
        Text_Count.text = _item.Count.ToString() + "/20";
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

    /// <summary>
    /// ������ ������ �����ϴ� �Լ�(string ������ �̸�, int ������ ����)
    /// Set : InventoryController._itemCountDictionary
    /// </summary>
    public void SetItemCountDictionary(string itemName, int amount)
    {
        // ��ųʸ��� �ش� �������� �̹� �ִٸ�
        if(_itemCountDictionary.ContainsKey(itemName))
        {
            // �Ѿ�� ���� ������ Set
            _itemCountDictionary[itemName] = amount;
        }
        else
        {
            // ��ųʸ��� �������� ���ٸ� ���� ����
            _itemCountDictionary.Add(itemName, amount);
        }
    }

    /// <summary>
    /// �κ��丮 �����۵� ���� �ʱ�ȭ�ϴ� �Լ�<br/>
    /// ���ڷθ� ������ ����Ʈ�� �Ѿ���� ������ �ݵ�� ���� ������ ���� ���� ��ġ�ϴ� ��쿡�� ����� ��
    /// </summary>
    /// <param name="itemCntList"></param>
    public void SetInvenSlotItemsCount(List<int> itemCntList)
    {
        // null üũ
        if(itemCntList != null)
        {
            // _invenSlotList�� �������� ��� ũ��� �׻� 23�̱⶧���� ���� ����Ʈ��ŭ ����
            for (int i = 0; i < itemCntList.Count; i++)
            {
                // ����� ������ ���� ������ �״�� ����
                _invenSlotList[i]._invenItemCount = itemCntList[i];
            }
        }
    }
}
