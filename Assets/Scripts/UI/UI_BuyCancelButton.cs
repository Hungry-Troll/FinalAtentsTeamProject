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
        // 위치 버튼 위치 찾음
        Transform buy = Util.FindChild("BuyButton", transform);
        Transform cancel = Util.FindChild("CancelButton", transform);

        _buyButton = buy.GetComponent<Button>();
        _cancelButton = cancel.GetComponent<Button>();
        // 버튼을 코드로 연결함
        _buyButton.onClick.AddListener(BuyButtonClick);
        _cancelButton.onClick.AddListener(CancelButtonClick);
    }
    // 구매하기
    public void BuyButtonClick()
    {
        // 인벤토리에 아이템 넣음
        GameManager.Item.InventoryItemAdd(_shopSelectItem, false);
        // 상점에서 아이템도 제거 해야됨 어떻게??
        GameManager.Item.ShopSlotRemove(_shopSelectItem);
        // 구매 후 UI 꺼야 됨 안그러면 버그 있음
        CancelButtonClick();
    }
    // 취소하기
    public void CancelButtonClick()
    {
        gameObject.SetActive(false);
    }
}
