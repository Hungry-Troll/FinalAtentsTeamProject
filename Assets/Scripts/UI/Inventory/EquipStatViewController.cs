using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class EquipStatViewController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    // 착용하고 있는 아이템
    public GameObject _equipItem;

    // 아이템 이미지 전달용
    public Sprite _sprite;
    // 아이템 타입 구분용 이넘
    public Define.ItemType _itemType;

    // Start is called before the first frame update
    void Start()
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

    public void CloseItem()
    {
        // 인벤토리 닫기 Ui는 Ui매니저에서 관리함
        GameManager.Ui.EquipStatViewClose();
    }
    // 장착아이템 해제 함수
    public void UnEquipWeaponItem()
    {
        // 아이템 타입에 따른 아이템 해제
        if (_itemType == ItemType.Weapon)
        {
            GameManager.Ui.UnEquipItem();
        }
        else if (_itemType == ItemType.Armour)
        {
            GameManager.Ui.UnEquipItem();
        }
    }
    // 아이템 버리기 함수 + 판매기능도 포함
    public void DropItem()
    {
        // 아이템 타입에 따른 아이템 해제
        if (_itemType == ItemType.Weapon)
        {
            GameManager.Ui.EquipItemDropOrSell();
        }
        else if (_itemType == ItemType.Armour)
        {
            GameManager.Ui.EquipItemDropOrSell();
        }
    }
}
