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

    // 인벤토리 슬롯 번호 확인용 변수
    public int _invenSlotCount;

    // 플레이어 장착 무기
    public GameObject _weapon;
    // 플레이어 장착 방어구
    public GameObject _armour;

    private void Start()
    {
        // 필요없을수도 있음
        _invenSlotCount = 0;
        // 무기 방어구 장착 게임 시작 시 null 추후 세이브 기능 추가시 체크
        _weapon = null;
        _armour = null;
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
