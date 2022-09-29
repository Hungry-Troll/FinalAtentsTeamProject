using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Util;

public class ItemStatView : MonoBehaviour , IBeginDragHandler, IDragHandler
{
    // 아이템 이미지 전달용
    Sprite _sprite;

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
        // 아이템 이미지 로드 
        // ItemStatView가 켜지면서 자동으로 인벤토리컨트롤러 웨폰에 게임오브젝트가 저장 됨

        // 아이템 이미지만 우선 교체
        // 추후 아이템 스텟을 적용
        _sprite = GameManager.Resource.GetImage(GameManager.Ui._inventoryController._weapon.name);
        GameManager.Ui.ItemStatViewWeaponEquip(_sprite, gameObject.transform);
    }

    public void DropItem()
    {
        // 아이템 버리기 코드
    }

}
