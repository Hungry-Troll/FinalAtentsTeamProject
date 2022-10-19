using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotController : MonoBehaviour
{
    public Button _button;
    // 상점 슬롯 아이템
    public List<GameObject> _slotItem;

    public void Start()
    {
        _button = GetComponent<Button>();
        // 버튼을 코드로 연결함
        _button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // 구매하기 취소하기 버튼 열기
        GameManager.Ui.BuyCancelButtonOpen(transform);
        // 상점 슬롯에 있는 아이템을 구매하기 버튼에 정보 넘겨줌
        GameManager.Ui._buyCancelScript._shopSelectItem = _slotItem[0]; ;
    }
}
