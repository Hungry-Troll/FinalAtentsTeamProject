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

                Debug.Log("����Ŭ��");

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
                Debug.Log("����Ŭ��");
                break;

            case Define.ItemType.Armour:
                GameManager.Ui.ItemStatViewWeaponEquip(_itemType);
                Debug.Log("��Ŭ��");
                break;
        }


    } 
    void DropItem()
    {
        GameManager.Ui.ItemStatViewWeaponDrop();
    }
}
