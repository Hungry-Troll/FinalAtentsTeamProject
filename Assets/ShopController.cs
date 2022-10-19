using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    // ���� �Ʒ��� ���ο� ������ ����� ���� ��ġ ���� scrollRect
    public ScrollRect _scrollRect;
    // ���� ������ �̹���
    public Image _itemImage;
    // ���� ������ ��Ʈ�ѷ�
    public ShopSlotController _shopSlotController;
    // ���� ������ ����Ʈ
    public List<ShopSlotController> _shopSlotList;


    // ���� ������ ���ӿ�����Ʈ
    public List<GameObject> _shopItemList;

    // ������ �Ŵ������� �Ѱ��� �� ��Ʈ�ѷ� ����
    public ShopController _shopController;

    // ������ �����ؾߵǴ� ����
    public List<GameObject> _shopViewItemList;

    // �������� ������ ���� �� �� �ְ� �� ��
    // �� 1�� ���� 2��
    // Start is called before the first frame update
    void Start()
    {
        ShopItemSell();
        // ������ �Ŵ������� �� ��Ʈ�ѷ� ����
        GameManager.Item._shopController = GetComponent<ShopController>();
    }

    // ������ ���� ��� 
    private void ShopItemSell()
    {
        // ���� 3����
        // ���� �� ���� �Ǹ� �׶� ����

        string tmpName = GameManager.Select._jobName;
        int itemCount = 3;
        GameObject[] tmpGo = new GameObject[3];

        // �̸����� ���ӿ�����Ʈ ã��
        switch (tmpName)
        {
            case "Superhuman":
                for (int i = 0; i < itemCount; i++)
                {
                    tmpGo[i] = GameManager.Resource.GetfieldItem("sword" + (i + 1));
                }
                break;
            case "Cyborg":
                for (int i = 0; i < itemCount; i++)
                {
                    tmpGo[i] = GameManager.Resource.GetfieldItem("gun" + (i + 1));
                }
                break;
            case "Scientist":
                for (int i = 0; i < itemCount; i++)
                {
                    tmpGo[i] = GameManager.Resource.GetfieldItem("book" + (i + 1));
                }
                break;
        }

        // ã�� ������Ʈ ����Ʈ�� �ְ� �Ʒ��� ���� ���
        for (int i = 0; i < tmpGo.Length; i++)
        {
            SlotArray(tmpGo[i]);
        }
    }

    public void SlotArray(GameObject go)
    {
        // ���ӿ�����Ʈ ����Ʈ�� ������� // ���� ����
        _shopItemList.Add(go);
        // �� ������ ���� ����(������ public���� ���� �巡�� ������� ��������)
        // ���Ⱑ �������� ���� ������ ��
        GameObject newItem;

        // �����ϴ°��� ������ �����Ҷ��� �ϴ°�����

        newItem = Instantiate<GameObject>(_shopSlotController.gameObject);
        newItem.SetActive(true);
        newItem.transform.SetParent(_scrollRect.content);

        // ���� �����ۿ� ���� �־���
        ItemStatEX itemStat = go.AddComponent<ItemStatEX>();
        // ���� ��ũ��Ʈ�� json ���� ���� ����
        GameManager.Stat.ItemStatLoadJson(go.name, itemStat);
        // ���� �ȿ��� ���ӿ�����Ʈ�� �־��� (���ݱ��� ������ �̷�������� �����Ƿ� ���� ��ȿ�����̿��� �״�� ��)
        ShopSlotController _shopSlotControllerEX = newItem.GetComponent<ShopSlotController>();
        _shopSlotControllerEX._slotItem.Add(go);
        // ���������� ������ �����ؾߵǴ� ���� ����
        _shopViewItemList.Add(newItem);

        // ������ ���� ã��
        // ������ �̹��� ��ġ
        Transform imageTr = Util.FindChild("ItemImage", newItem.transform);
        // �̸� ã��
        Transform nameTr = Util.FindChild("ItemName", newItem.transform);
        // ���� �ؽ�Ʈ ã��(���ݷ� ������ �ٲ��ֱ� ���ؼ� ����� ���ݷ� ���� ����)
        Transform itemStatTextTr = Util.FindChild("ItemStatText", newItem.transform);
        // ���� ����
        Transform itemStatTr = Util.FindChild("ItemStat", newItem.transform);
        // ���� ã��
        Transform priceTr = Util.FindChild("ItemPrice", newItem.transform);
        // �� ������Ʈ ����� ���� �ٿ��� ��
        Image itemImage = imageTr.GetComponent<Image>();
        Text itemName = nameTr.GetComponent<Text>();
        Text itemStatText = itemStatTextTr.GetComponent<Text>();
        Text abilityStat = itemStatTr.GetComponent<Text>();
        Text itemPrice = priceTr.GetComponent<Text>();

        // �̹��� ����
        itemImage.sprite = GameManager.Resource.GetImage(go.name);
        // �̸� ����
        itemName.text = itemStat.Name;
        // ���� �� ������ ����
        itemStatText.text = "���ݷ�";
        abilityStat.text = itemStat.Skill.ToString();
        // ���� ����
        itemPrice.text = itemStat.Get_Price.ToString() + " ���";
    }
}
