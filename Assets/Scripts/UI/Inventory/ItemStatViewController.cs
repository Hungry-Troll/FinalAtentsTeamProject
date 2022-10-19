using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Util;
using static Define;

public class ItemStatViewController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    // 아이템 이미지 전달용
    public Sprite _sprite;
    // 아이템 타입 구분용 이넘
    Define.ItemType _itemType;
    Define.StatView _statView;

    // Start is called before the first frame update
    void Start()
    {
        _statView = Define.StatView.ItemStatView;
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
        GameManager.Ui.StatViewClose(_statView);
    }

    public void WeaponEquipItem()
    {
        _itemType = Define.ItemType.Weapon;
        // 추후 아이템 스텟을 적용
        GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
    }

    public void ArmourEquipItem()
    {
        _itemType = Define.ItemType.Armour;
        // 추후 아이템 스텟을 적용
        GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
    }

    public void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }

}
