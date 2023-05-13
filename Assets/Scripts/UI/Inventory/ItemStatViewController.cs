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
    public Define.ItemType _itemType;
    public Define.ItemName _itemName;
    public Text _text;

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
        //Ȯ�ο� �α�
        Debug.Log(_itemType);
        Debug.Log(_itemName);

        PlayerStat ob = GameManager.Obj._playerStat;
        int max = GameManager.Obj._playerStat.Max_Hp;

        switch (_itemType)
        {
            case Define.ItemType.Consumables:

                Debug.Log("����Ŭ��");

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
                Debug.Log("����Ŭ��");
                break;
            case Define.ItemType.Armour:
                GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
                Debug.Log("��Ŭ��");
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
            //������ ������ ���������� ������ ���̰�
            //������ ������ 0���� �Ǹ� ������� ��
            GameManager.Ui.ItemStatViewUsePotion();
        }
    }


    void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }

    // ����Ұ� â �ݱ�
    public void CloseCannotEquipView()
    {
        GameManager.Ui.CannotEquipViewClose();
    }
}
