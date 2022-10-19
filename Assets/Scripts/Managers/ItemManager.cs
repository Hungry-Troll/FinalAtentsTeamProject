using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �κ��丮�� �ִ� ��� / shop ���� /  ������ ���� �Ŵ���
public class ItemManager
{
    // ���� ������ Shop ��Ʈ�ѷ����� ó�� Start() �Լ� ����� ���� ��
    public ShopController _shopController;

    public void InventoryItemAdd(GameObject gameObject, bool fieldItem)
    {
        // ������ �κ��丮�� �ֱ�
        GameManager.Ui._inventoryController._item.Add(gameObject);
        // ������ �κ��� ���� ���� Ȯ��
        int count = GameManager.Ui._inventoryController._item.Count;
        // �κ��� ���ڿ� ���ؼ� �������� count��° ���Կ� �ֱ�
        GameManager.Ui._inventoryController._invenSlotList[count - 1]._SlotItem.Add(gameObject);
        // ������ �̹��� �ε�
        Sprite ItemSprite = GameManager.Resource.GetImage(gameObject.name);
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

        if(fieldItem)
        {
            gameObject.SetActive(false);
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
