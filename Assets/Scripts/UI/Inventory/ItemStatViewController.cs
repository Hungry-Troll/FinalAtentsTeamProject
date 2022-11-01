using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Util;
using static Define;

public class ItemStatViewController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    // ������ �̹��� ���޿�
    public Sprite _sprite;
    // ������ Ÿ�� ���п� �̳�
    Define.ItemType _itemType;
    Define.ItemName _itemName;

    void Start()
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ����
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� ��
        transform.position = eventData.position;
    }

    public void CloseItem()
    {
        // �κ��丮 �ݱ� Ui�� Ui�Ŵ������� ������
        GameManager.Ui.ItemStatViewClose();
    }

    
    public void WeaponEquipItem()
    {
        
        switch (_itemType)
        {       //�۵����� �α׸� ��

            case Define.ItemType.Consumables:   
                //case Define.ItemName.potion1:
                Debug.Log("����Ŭ��");
                StartCoroutine("PotionEquipItem");
                break;

            case Define.ItemType.Weapon:
                GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
                Debug.Log("����Ŭ��");
                break;

            case Define.ItemType.Armour:
                GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
                Debug.Log("��Ŭ��");
                break;
        }
    }


    IEnumerator PotionEquipItem()
    {
        Debug.Log("�ڷ�ƾ �� ����");
        for (int i = 0; i < 50; i++)
        {
            Debug.Log("�ڷ�ƾ ���� ��");
            PlayerHpBarEX playerHP = new PlayerHpBarEX();
            float current = playerHP._currentHp;
            Debug.Log(current);
            yield return new WaitForSeconds(0.1f);
        }StopCoroutine("PotionEquipItem");
    }  
  

    



    //public void ArmourEquipItem()
    //{
    //    _itemType = Define.ItemType.Armour;
    //    // ���� ������ ������ ����
    //    GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
    //}

    public void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }

}
