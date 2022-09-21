using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    // 인벤토리 아이템
    public List<GameObject> _item = new List<GameObject>();

    // 인벤토리 슬롯배열
    public InvenSlotController[] _invenSlotArray;

    // 인벤토리 슬롯 배열을 리스트로 변환
    public List<InvenSlotController> _invenSlotList = new List<InvenSlotController>();
    private void Start()
    {

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
}
