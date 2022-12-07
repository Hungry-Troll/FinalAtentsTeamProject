using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuyCancelButton : MonoBehaviour
{
    Button _buyButton;
    Button _cancelButton;
    public GameObject _shopSelectItem;

    public void Start()
    {
        // ��ġ ��ư ��ġ ã��
        Transform buy = Util.FindChild("BuyButton", transform);
        Transform cancel = Util.FindChild("CancelButton", transform);

        _buyButton = buy.GetComponent<Button>();
        _cancelButton = cancel.GetComponent<Button>();
        // ��ư�� �ڵ�� ������
        _buyButton.onClick.AddListener(BuyButtonClick);
        _cancelButton.onClick.AddListener(CancelButtonClick);
    }
    // �����ϱ�
    public void BuyButtonClick()
    {
        GameObject potion = GameManager.Resource.GetfieldItem("potion1");

        if (_shopSelectItem.name == "potion1")
        {
            GameManager.Item.InventoryItemAdd(_shopSelectItem, false);
            // �ܺ�UI���� ���� �������� �����ؾ� �Ѵ�.
            Ui_SceneAttackButton tmp = GameManager.Ui._sceneButton.GetComponent<Ui_SceneAttackButton>();
            tmp.GetPotion();
            CancelButtonClick();
        }
        else
        {
            // �κ��丮�� ������ ����
            GameManager.Item.InventoryItemAdd(_shopSelectItem, false);
            // �������� �����۵� ���� �ؾߵ� ���??
            GameManager.Item.ShopSlotRemove(_shopSelectItem);
            // ���� �� UI ���� �� �ȱ׷��� ���� ����
            CancelButtonClick();
        }
    }
    // ����ϱ�
    public void CancelButtonClick()
    {
        gameObject.SetActive(false);
    }
}
