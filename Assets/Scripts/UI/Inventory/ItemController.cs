using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemController : MonoBehaviour
{
    // 플레이어 콜라이더 확인용
    Collider _playerCollider;
    // 아이템 이미지 전달 용
    Sprite _sprite;
    // 아이템 타입 (아이템이 생성 될 때 정해줘야 됨 / 어떤 방식으로 구현할지 고민)
    public ItemType _itemType;

    // Start is called before the first frame update
    private void Start()
    {
        // OnTriggerEnter 사용을 위한 플레이어 콜라이더를 가지고 옴
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 콜라이더와 충돌하면
        if(other == _playerCollider)
        {
            // 매개변수는 자기자신과, 필드아이템 여부(상점 구매 아이템은 필드 아이템이 아님)
            GameManager.Item.InventoryItemAdd(gameObject, true);
        }
    }

    private void UsingItem()
    {
        Vector3 mousePos = Input.mousePosition;
    }
}
