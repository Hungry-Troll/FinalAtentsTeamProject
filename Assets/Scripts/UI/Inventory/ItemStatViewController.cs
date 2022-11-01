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
    Define.ItemName _itemName;

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
        
        switch (_itemType)
        {       //작동안함 로그만 찍어봄

            case Define.ItemType.Consumables:   
                //case Define.ItemName.potion1:
                Debug.Log("포션클릭");
                StartCoroutine("PotionEquipItem");
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


    IEnumerator PotionEquipItem()
    {
        Debug.Log("코루틴 전 포문");
        for (int i = 0; i < 50; i++)
        {
            Debug.Log("코루틴 포문 안");
            PlayerHpBarEX playerHP = new PlayerHpBarEX();
            float current = playerHP._currentHp;
            Debug.Log(current);
            yield return new WaitForSeconds(0.1f);
        }StopCoroutine("PotionEquipItem");
    }  
  

    



    //public void ArmourEquipItem()
    //{
    //    _itemType = Define.ItemType.Armour;
    //    // 추후 아이템 스텟을 적용
    //    GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
    //}

    public void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }

}
