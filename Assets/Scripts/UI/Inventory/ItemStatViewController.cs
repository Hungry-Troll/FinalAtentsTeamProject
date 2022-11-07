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
        GameManager.Ui.ItemStatViewClose();
    }


    public void WeaponEquipItem()
    {

        Debug.Log(_itemType);
        Debug.Log(_itemName);

        PlayerHpBarEX PhpEX = gameObject.GetComponent<PlayerHpBarEX>(); //
        HpBarEX hpbarEX = gameObject.GetComponent<HpBarEX>();   //
        Stat stat = gameObject.GetComponent<Stat>();   //
        PlayerStat playerStat = gameObject.GetComponent<PlayerStat>();
        PlayerStat ob = GameManager.Obj._playerStat;
        int max = GameManager.Obj._playerStat.Max_Hp;


        switch (_itemType)
        {
            case Define.ItemType.Consumables:

                Debug.Log("포션클릭");

                //PhpEX._currentHp += 50;
                //Debug.Log(PhpEX._currentHp);

                //TempStatEX tmp = new TempStatEX();
                //tmp.Hp = +50;
                //Debug.Log(tmp.Hp);

                //playerStat.Hp += 50;
                //Debug.Log(playerStat.Hp);

                if(ob.Hp <= max)
                {
                    ob.Hp += 50;
                    Debug.Log(ob.Hp);
                    if(ob.Hp >= max)
                    {
                        ob.Hp = max;
                    }
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
    void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }
}
