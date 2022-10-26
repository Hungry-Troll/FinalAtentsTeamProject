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
            // �Ű������� �ڱ��ڽŰ�, �ʵ������ ����(���� ���� �������� �ʵ� �������� �ƴ�)
            GameManager.Item.InventoryItemAdd(gameObject, true);
        }
    }

    private void UsingItem()
    {
        Vector3 mousePos = Input.mousePosition;
    }
}
