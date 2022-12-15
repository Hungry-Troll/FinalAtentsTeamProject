using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �κ��丮�� �ִ� ��� / shop ���� /  ������ ���� �Ŵ���
public class ItemManager
{
    // ���� ������ Shop ��Ʈ�ѷ����� ó�� Start() �Լ� ����� ���� ��
    public ShopController _shopController;
    private int potionIndex;
    private bool IsOverlapItemContains(GameObject go)
    {
        for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            if (GameManager.Ui._inventoryController._item[i].name.Equals("potion1"))
            {
                potionIndex = i;
                return true;
            }
        }
        potionIndex = 0;
        return false;
    }
    /// <summary>	
    /// 1. �������� �����ϴ� ��� �߰� fieldItem => false�� ����ǰ� 	
    /// 2. �ʵ忡�� �������� �ݴ� ��쿡�� true	
    /// 2. 	
    /// </summary>	
    /// <param name="item"></param>	
    /// <param name="isfieldItem"></param>	
    public void InventoryItemAdd(GameObject item, bool isfieldItem)
    {
        Debug.Log("1������ : " + item.name.Equals("potion1"));
        Debug.Log("2������ : " + IsOverlapItemContains(item));
        if (item.name.Equals("potion1") &&
            IsOverlapItemContains(item))
        {
            GameManager.Ui._inventoryController._invenSlotList[potionIndex].SetOverlapItemCntAdd();
            // �̹� ���� �ִ� ������ ����
            int potionCnt = GameManager.Ui._inventoryController._invenSlotList[potionIndex]._invenItemCount;
            // potion1�� ���� update(��ųʸ�(InvenSlotController._itemCountDictionary))
            GameManager.Ui._inventoryController.SetItemCountDictionary("potion1", potionCnt);
        }
        else
        //ó�� ������ ��	
        {  // ������ �κ��丮�� �ֱ�	
            GameManager.Ui._inventoryController._item.Add(item);
            // ������ �κ��� ���� ���� Ȯ��	
            int count = GameManager.Ui._inventoryController._item.Count;
            // �κ��� ���ڿ� ���ؼ� �������� count��° ���Կ� �ֱ�	
            GameManager.Ui._inventoryController._invenSlotList[count - 1]._SlotItem.Add(item);
            //���� �߰��Ǵ� �������� �����̶��, ���������� ���� 1�� �־��ش�	
            if (item.name.Equals("potion1"))
            {
                //ó�� �Ծ��������� 1 ���� �ι����� ������ +2��	
                GameManager.Ui._inventoryController._invenSlotList[count - 1].SetOverlapItemCntAdd();
            }
            // ��ųʸ��� ������ ���� 1�� ����, ��ųʸ�<�κ��丮 ������, ����>
            GameManager.Ui._inventoryController.SetItemCountDictionary(item.name, 1);
            // ������ �̹��� �ε�	
            Sprite ItemSprite = GameManager.Resource.GetImage(item.name);
            // �����ϵ�� �̹��� ���� ���(�����̹���) ã��	
            Transform invenImageTr = GameManager.Ui._inventoryController._invenSlotArray[count - 1].transform.GetChild(0);
            // �����̹��� Ȱ��ȭ	
            invenImageTr.gameObject.SetActive(true);
            // �̹����� �ֱ����� GetComponent	
            //Image image = invenImageTr.gameObject.GetComponent<Image>();	
            // �̹��� ���� 	
            //image.sprite = _sprite;	
            // �� �ڵ带 UI �Ŵ��� Init() �Լ����� �̸� �ҷ��� �κ��丮 �����̹����� �����ϴ� ������ ���� �� : GameManager.Ui._slotImage[count - 1].sprite	
            // GetComponent ����� ����	
            // �̹����� ����	
            GameManager.Ui._slotImage[count - 1].sprite = ItemSprite;
            // ������ ȹ�� �� �κ����Կ��� ī��Ʈ�� ���� ���� ī��Ʈ�� �ٽ� ������Ʈ�ѷ��� �Ѱ���	
            // ������ ���� �� ���	
            GameManager.Ui._inventoryController._invenSlotCount++;
            //// ���� �����ۿ� ���� �ֱ�	
            //for (int i = 0; i < GameManager.Ui._inventoryController._invenSlotList.Count; i++)	
            //{	
            //    GameManager.Ui._inventoryController._invenSlotList[i].SetSlotNumber();	
            //}	
        }
        if (isfieldItem)
        {
            item.SetActive(false);
        }
        // �ʵ� ������ ���� // �������� �����Ұ�� �ʿ���� �ڵ�	
    }

    // ���� ������ ���� ��� ���
    public void ShopSlotRemove(GameObject gameObject)
    {
        // ���� �����۰� �Ű����� �������� �����ϸ� ���� ������ ���� ����
        for (int i = 0; i < _shopController._shopItemList.Count; i++)
        {
            if (_shopController._shopItemList[i].name == gameObject.name)
            {
                // ����Ʈ�ѷ����� ������ �ִ� ������ ����
                _shopController._shopItemList.RemoveAt(i);
                _shopController._shopViewItemList[i].SetActive(false);
                _shopController._shopViewItemList.RemoveAt(i);
            }
        }
    }
}
