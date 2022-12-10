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
        // 안쓰는 코드 아닌가용?
        //GameObject potion = GameManager.Resource.GetfieldItem("potion1");

        // 선택한 아이템의 객체 정보를 오브젝트 풀링방식으로 가지고 옴
        GameObject tmpItem = Util.Instantiate(_shopSelectItem);
        // 가지고온 정보에서 가격정보만 빼옴
        int tmpGetPrice = tmpItem.GetComponent<ItemStatEX>().Get_Price;

        if (_shopSelectItem.name == "potion1")
        {
            // 아이템 구매 시 골드 소모
            bool buyBool = GameManager.Obj._goldController.SpendGold(tmpGetPrice);
            if (buyBool == false)
            {
                GameManager.Ui._dontBuy.SetActive(true);
                return;
            }
            GameManager.Item.InventoryItemAdd(_shopSelectItem, false);
            // 외부UI에도 포션 아이템이 증가해야 한다.
            Ui_SceneAttackButton tmp = GameManager.Ui._sceneButton.GetComponent<Ui_SceneAttackButton>();
            tmp.GetPotion();
            CancelButtonClick();
        }
        else
        {
            // 아이템 구매 시 골드 소모
            bool buyBool = GameManager.Obj._goldController.SpendGold(tmpGetPrice);
            if (buyBool == false)
            {
                GameManager.Ui._dontBuy.SetActive(true);
                return;
            }
            // 인벤토리에 아이템 넣음
            GameManager.Item.InventoryItemAdd(_shopSelectItem, false);
            // 상점에서 아이템도 제거 해야됨 어떻게??
            GameManager.Item.ShopSlotRemove(_shopSelectItem);
            // 구매 후 UI 꺼야 됨 안그러면 버그 있음
            CancelButtonClick();
        }
    }
    // 취소하기
    public void CancelButtonClick()
    {
        gameObject.SetActive(false);
    }
}
