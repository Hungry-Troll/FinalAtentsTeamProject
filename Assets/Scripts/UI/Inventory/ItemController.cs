using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemController : MonoBehaviour
{
    // �÷��̾� �ݶ��̴� Ȯ�ο�
    Collider _playerCollider;
    // ������ �̹��� ���� ��
    Sprite _sprite;
    // ������ Ÿ�� (�������� ���� �� �� ������� �� / � ������� �������� ���)
    public ItemType _itemType;



    // Start is called before the first frame update
    private void Start()
    {
        // OnTriggerEnter ����� ���� �÷��̾� �ݶ��̴��� ������ ��
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �ݶ��̴��� �浹�ϸ�
        if(other == _playerCollider)
        {
            // ������ �κ��丮�� �ֱ�
            GameManager.Ui._inventoryController._item.Add(gameObject);
            // ������ �κ��� ���� ���� Ȯ��
            int count = GameManager.Ui._inventoryController._item.Count;
            // �κ��� ���ڿ� ���ؼ� �������� count��° ���Կ� �ֱ�
            GameManager.Ui._inventoryController._invenSlotList[count - 1]._SlotItem.Add(gameObject);
            // ������ �̹��� �ε�
            _sprite = GameManager.Resource.GetImage(gameObject.name);
            // �����ϵ�� �̹��� ���� ���(�����̹���) ã��
            Transform invenImageTr = GameManager.Ui._inventoryController._invenSlotArray[count - 1].transform.GetChild(0);
            // �����̹��� Ȱ��ȭ
            invenImageTr.gameObject.SetActive(true);

            // �̹����� �ֱ����� GetComponent
            //Image image = invenImageTr.gameObject.GetComponent<Image>();
            // �̹��� ���� 
            //image.sprite = _sprite;

            // �� �ڵ带 UI �Ŵ��� Init() �Լ����� �̸� �ҷ��� �κ��丮 �����̹����� �����ϴ� ������ ���� �� : GameManager.Ui._slotImage[count - 1].sprite
            // GetComponent ����� ����
            // �̹����� ����
            GameManager.Ui._slotImage[count - 1].sprite = _sprite;

            // ������ ȹ�� �� �κ����Կ��� ī��Ʈ�� ���� ���� ī��Ʈ�� �ٽ� ������Ʈ�ѷ��� �Ѱ���
            // ������ ���� �� ���
            GameManager.Ui._inventoryController._invenSlotCount++;
            // �ʵ� ������ ����
            gameObject.SetActive(false);
        }
    }

    private void UsingItem()
    {
        Vector3 mousePos = Input.mousePosition;
    }
}
