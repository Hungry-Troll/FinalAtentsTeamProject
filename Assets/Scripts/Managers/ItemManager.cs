using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 인벤토리에 넣는 기능 / shop 관리 /  아이템 관리 매니저
public class ItemManager
{
    // 여기 연결은 Shop 컨트롤러에서 처음 Start() 함수 실행시 대입 됨
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
    /// 1. 상점에서 구매하는 경우 추가 fieldItem => false로 실행되고 	
    /// 2. 필드에서 아이템을 줍는 경우에는 true	
    /// 2. 	
    /// </summary>	
    /// <param name="item"></param>	
    /// <param name="isfieldItem"></param>	
    public void InventoryItemAdd(GameObject item, bool isfieldItem)
    {
        Debug.Log("1번조건 : " + item.name.Equals("potion1"));
        Debug.Log("2번조건 : " + IsOverlapItemContains(item));
        if (item.name.Equals("potion1") &&
            IsOverlapItemContains(item))
        {
            GameManager.Ui._inventoryController._invenSlotList[potionIndex].SetOverlapItemCntAdd();
            // 이미 갖고 있는 포션의 개수
            int potionCnt = GameManager.Ui._inventoryController._invenSlotList[potionIndex]._invenItemCount;
            // potion1의 개수 update(딕셔너리(InvenSlotController._itemCountDictionary))
            GameManager.Ui._inventoryController.SetItemCountDictionary("potion1", potionCnt);
        }
        else
        //처음 생성될 떄	
        {  // 아이템 인벤토리에 넣기	
            GameManager.Ui._inventoryController._item.Add(item);
            // 아이템 인벤에 넣은 숫자 확인	
            int count = GameManager.Ui._inventoryController._item.Count;
            // 인벤에 숫자와 비교해서 아이템을 count번째 슬롯에 넣기	
            GameManager.Ui._inventoryController._invenSlotList[count - 1]._SlotItem.Add(item);
            //만약 추가되는 아이템이 포션이라면, 내부적으로 숫자 1을 넣어준다	
            if (item.name.Equals("potion1"))
            {
                //처음 먹었을떄부터 1 시작 두번ㅉㅐ 먹으면 +2됨	
                GameManager.Ui._inventoryController._invenSlotList[count - 1].SetOverlapItemCntAdd();
            }
            // 딕셔너리에 아이템 수량 1로 저장, 딕셔너리<인벤토리 아이템, 수량>
            GameManager.Ui._inventoryController.SetItemCountDictionary(item.name, 1);
            // 아이템 이미지 로드	
            Sprite ItemSprite = GameManager.Resource.GetImage(item.name);
            // 겟차일드로 이미지 넣을 대상(슬롯이미지) 찾음	
            Transform invenImageTr = GameManager.Ui._inventoryController._invenSlotArray[count - 1].transform.GetChild(0);
            // 슬롯이미지 활성화	
            invenImageTr.gameObject.SetActive(true);
            // 이미지를 넣기위한 GetComponent	
            //Image image = invenImageTr.gameObject.GetComponent<Image>();	
            // 이미지 대입 	
            //image.sprite = _sprite;	
            // 위 코드를 UI 매니저 Init() 함수에서 미리 불러온 인벤토리 슬롯이미지에 저장하는 것으로 변경 함 : GameManager.Ui._slotImage[count - 1].sprite	
            // GetComponent 사용을 줄임	
            // 이미지를 대입	
            GameManager.Ui._slotImage[count - 1].sprite = ItemSprite;
            // 아이템 획득 시 인벤슬롯에서 카운트를 따로 세고 카운트를 다시 슬롯컨트롤러에 넘겨줌	
            // 아이템 장착 시 사용	
            GameManager.Ui._inventoryController._invenSlotCount++;
            //// 슬롯 아이템에 숫자 넣기	
            //for (int i = 0; i < GameManager.Ui._inventoryController._invenSlotList.Count; i++)	
            //{	
            //    GameManager.Ui._inventoryController._invenSlotList[i].SetSlotNumber();	
            //}	
        }
        if (isfieldItem)
        {
            item.SetActive(false);
        }
        // 필드 아이템 제거 // 상점에서 구매할경우 필요없는 코드	
    }

    // 상점 아이템 제거 기능 담당
    public void ShopSlotRemove(GameObject gameObject)
    {
        // 상점 아이템과 매개변수 아이템이 동일하면 상점 아이템 슬롯 제거
        for (int i = 0; i < _shopController._shopItemList.Count; i++)
        {
            if (_shopController._shopItemList[i].name == gameObject.name)
            {
                // 샵컨트롤러에서 가지고 있는 아이템 제거
                _shopController._shopItemList.RemoveAt(i);
                _shopController._shopViewItemList[i].SetActive(false);
                _shopController._shopViewItemList.RemoveAt(i);
            }
        }
    }
}
