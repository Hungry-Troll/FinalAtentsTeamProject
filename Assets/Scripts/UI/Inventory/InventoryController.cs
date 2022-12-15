using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    // 인벤토리 아이템
    public List<GameObject> _item = new List<GameObject>();

    // 인벤토리 슬롯배열
    public InvenSlotController[] _invenSlotArray;

    // 인벤토리 슬롯 배열을 리스트로 변환
    public List<InvenSlotController> _invenSlotList = new List<InvenSlotController>();

    // 인벤토리 슬롯 번호 확인용 변수
    public int _invenSlotCount;

    // 플레이어 장착 무기
    public GameObject _weapon = null;
    // 플레이어 장착 무기 스텟
    public ItemStatEX _weaponStat;

    // 플레이어 장착 방어구
    public GameObject _armour = null;
    // 플레이어 장착 방어구 스텟
    public ItemStatEX _armourStat;

    // 아이템 이름, 총 개수 보관할 딕셔너리
    private Dictionary<string, int> _itemCountDictionary = new Dictionary<string, int>(); 
    public Dictionary<string, int> ItemCountDictionary
    {
        get { return _itemCountDictionary; }
    }

    Text Text_Count;
    
    private void Start()
    {
        // 필요없을수도 있음
        _invenSlotCount = 0;
        // 인벤토리 아이템 개수 Text UI 개수 표시
        Transform Text_CountTr = Util.FindChild("Text_Count", gameObject.transform);
        Text_Count = Text_CountTr.GetComponent<Text>();
    }
    void Update()
    {
        Text_Count.text = _item.Count.ToString() + "/20";
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중
        transform.position = eventData.position;
    }

    public void CloseInventory()
    {
        // 인벤토리 닫기 Ui는 Ui매니저에서 관리함
        GameManager.Ui.InventoryClose();
    }

    /// <summary>
    /// 아이템 수량을 설정하는 함수(string 아이템 이름, int 설정할 수량)
    /// Set : InventoryController._itemCountDictionary
    /// </summary>
    public void SetItemCountDictionary(string itemName, int amount)
    {
        // 딕셔너리에 해당 아이템이 이미 있다면
        if(_itemCountDictionary.ContainsKey(itemName))
        {
            // 넘어온 수량 값으로 Set
            _itemCountDictionary[itemName] = amount;
        }
        else
        {
            // 딕셔너리에 아이템이 없다면 새로 생성
            _itemCountDictionary.Add(itemName, amount);
        }
    }

    /// <summary>
    /// 인벤토리 아이템들 수량 초기화하는 함수<br/>
    /// 숫자로만 구성된 리스트가 넘어오기 때문에 반드시 슬롯 순서와 수량 값이 일치하는 경우에만 사용할 것
    /// </summary>
    /// <param name="itemCntList"></param>
    public void SetInvenSlotItemsCount(List<int> itemCntList)
    {
        // null 체크
        if(itemCntList != null)
        {
            // _invenSlotList에 아이템이 없어도 크기는 항상 23이기때문에 수량 리스트만큼 실행
            for (int i = 0; i < itemCntList.Count; i++)
            {
                // 저장된 순서가 같기 때문에 그대로 저장
                _invenSlotList[i]._invenItemCount = itemCntList[i];
            }
        }
    }
}
