using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Util;

public class ItemStatViewController : MonoBehaviour , IBeginDragHandler, IDragHandler
{
    // 아이템 이미지 전달용
    public Sprite _sprite;

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
        GameManager.Ui.ItemStatViewClose(gameObject.name);
    }

    public void WeaponEquipItem()
    {
        // 추후 아이템 스텟을 적용
        GameManager.Ui.ItemStatViewWeaponEquip();
    }

    public void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }

}
