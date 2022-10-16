using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    // 슬롯 아래에 새로운 슬롯을 만들기 위한 위치 계산용 scrollRect
    public ScrollRect scrollRect;
    // 슬롯 아이템 이미지
    public Image itemImage;
    // 슬롯 아이템
    public ShopSlotController shopSlotController;
    // 슬롯 아이템 리스트
    public List<ShopSlotController> shopSlotList;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newItem = Instantiate<GameObject>(shopSlotController.gameObject);
        newItem.SetActive(true);
        newItem.transform.SetParent(scrollRect.content);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
