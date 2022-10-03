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
            // 아이템 인벤토리에 넣기
            GameManager.Ui._inventoryController._item.Add(gameObject);
            // 아이템 인벤에 넣은 숫자 확인
            int count = GameManager.Ui._inventoryController._item.Count;
            // 인벤에 숫자와 비교해서 아이템을 count번째 슬롯에 넣기
            GameManager.Ui._inventoryController._invenSlotList[count - 1]._SlotItem.Add(gameObject);
            // 아이템 이미지 로드
            _sprite = GameManager.Resource.GetImage(gameObject.name);
            // 겟차일드로 이미지 넣을 대상(슬롯이미지) 찾음
            Transform invenImageTr = GameManager.Ui._inventoryController._invenSlotArray[count - 1].transform.GetChild(0);
            // 슬롯이미지 활성화
            invenImageTr.gameObject.SetActive(true);

            // 이미지를 넣기위한 GetComponent
            //Image image = invenImageTr.gameObject.GetComponent<Image>();
            // 이미지 대입 
            //image.sprite = _sprite;

            // 위 코드를 UI 매니저 Init() 함수에서 미리 불러온 인벤토리 슬롯이미지에 저장하는 것으로 변경 함 : GameManager.Ui._slotImage[count - 1].sprite
            // GetComponent 사용을 줄임
            // 이미지를 대입
            GameManager.Ui._slotImage[count - 1].sprite = _sprite;

            // 아이템 획득 시 인벤슬롯에서 카운트를 따로 세고 카운트를 다시 슬롯컨트롤러에 넘겨줌
            // 아이템 장착 시 사용
            GameManager.Ui._inventoryController._invenSlotCount++;
            // 필드 아이템 제거
            gameObject.SetActive(false);
        }
    }

    private void UsingItem()
    {
        Vector3 mousePos = Input.mousePosition;
    }
}
