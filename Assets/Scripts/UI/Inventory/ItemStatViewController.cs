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
    public Define.ItemType _itemType;
    public Define.ItemName _itemName;
    public Text _text;

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
        GameManager.Ui.ItemStatViewClose();
    }


    public void WeaponEquipItem()
    {
        //확인용 로그
        Debug.Log(_itemType);
        Debug.Log(_itemName);

        PlayerStat ob = GameManager.Obj._playerStat;
        int max = GameManager.Obj._playerStat.Max_Hp;

        switch (_itemType)
        {
            case Define.ItemType.Consumables:

                Debug.Log("포션클릭");

                if(ob.Hp <= max)
                {
                    ob.Hp += 50;
                    Debug.Log(ob.Hp);
                    if(ob.Hp >= max)
                    {
                        ob.Hp = max;
                    }
                    GameManager.Ui.ItemStatViewWeaponDrop();
                }
                break;

            case Define.ItemType.Weapon:
                GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
                Debug.Log("무기클릭");
                break;
            case Define.ItemType.Armour:
                GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
                Debug.Log("방어구클릭");
                break;
        }
    }
    public void UsePotionExternal(PlayerStat ob, int max)
    {
        if (ob.Hp <= max)
        {
            ob.Hp += 50;
            Debug.Log(ob.Hp);
            if (ob.Hp >= max)
            {
                ob.Hp = max;
            }
            //아이템 개수가 남아있으면 갯수만 줄이고
            //아이템 개수가 0개가 되면 사라지게 함
            GameManager.Ui.ItemStatViewUsePotion();
        }
    }


    void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }

    // 착용불가 창 닫기
    public void CloseCannotEquipView()
    {
        GameManager.Ui.CannotEquipViewClose();
    }
}
