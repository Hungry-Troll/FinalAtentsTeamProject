using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    // 슬롯 아래에 새로운 슬롯을 만들기 위한 위치 계산용 scrollRect
    public ScrollRect _scrollRect;
    // 슬롯 아이템 이미지
    public Image _itemImage;
    // 슬롯 아이템 컨트롤러
    public ShopSlotController _shopSlotController;
    // 슬롯 아이템 리스트
    public List<ShopSlotController> _shopSlotList;


    // 슬롯 아이템 게임오브젝트
    public List<GameObject> _shopItemList;

    // 아이템 매니저에게 넘겨줄 샵 컨트롤러 변수
    public ShopController _shopController;

    // 실제로 관리해야되는 슬롯
    public List<GameObject> _shopViewItemList;

    // 직업별로 나눠서 구매 할 수 있게 할 것
    // 방어구 1개 무기 2개
    // Start is called before the first frame update
    void Start()
    {
        ShopItemSell();
        // 아이템 매니저에서 샵 컨트롤러 관리
        GameManager.Item._shopController = GetComponent<ShopController>();
    }

    // 아이템 정렬 기능 
    private void ShopItemSell()
    {
        // 무기 3개씩
        // 추후 방어구 구현 되면 그때 구현

        string tmpName = GameManager.Select._jobName;
        int itemCount = 3;
        GameObject[] tmpGo = new GameObject[3];

        // 이름으로 게임오브젝트 찾음
        switch (tmpName)
        {
            case "Superhuman":
                for (int i = 0; i < itemCount; i++)
                {
                    tmpGo[i] = GameManager.Resource.GetfieldItem("sword" + (i + 1));
                }
                break;
            case "Cyborg":
                for (int i = 0; i < itemCount; i++)
                {
                    tmpGo[i] = GameManager.Resource.GetfieldItem("gun" + (i + 1));
                }
                break;
            case "Scientist":
                for (int i = 0; i < itemCount; i++)
                {
                    tmpGo[i] = GameManager.Resource.GetfieldItem("book" + (i + 1));
                }
                break;
        }

        // 찾은 오브젝트 리스트에 넣고 아래와 같이 계산
        for (int i = 0; i < tmpGo.Length; i++)
        {
            SlotArray(tmpGo[i]);
        }
    }

    public void SlotArray(GameObject go)
    {
        // 게임오브젝트 리스트를 들고있음 // 순수 계산용
        _shopItemList.Add(go);
        // 샵 아이템 슬롯 생성(원본을 public으로 만들어서 드래드 드롭으로 연결했음)
        // 여기가 실질적인 슬롯 역할을 함
        GameObject newItem;

        // 생성하는것은 갯수가 부족할때만 하는것으로

        newItem = Instantiate<GameObject>(_shopSlotController.gameObject);
        newItem.SetActive(true);
        newItem.transform.SetParent(_scrollRect.content);

        // 게임 아이템에 스텟 넣어줌
        ItemStatEX itemStat = go.AddComponent<ItemStatEX>();
        // 스텟 스크립트에 json 파일 스텟 적용
        GameManager.Stat.ItemStatLoadJson(go.name, itemStat);
        // 슬롯 안에도 게임오브젝트를 넣어줌 (지금까지 구조를 이런방식으로 했으므로 조금 비효율적이여도 그대로 함)
        ShopSlotController _shopSlotControllerEX = newItem.GetComponent<ShopSlotController>();
        _shopSlotControllerEX._slotItem.Add(go);
        // 마지막으로 실제로 관리해야되는 슬롯 대입
        _shopViewItemList.Add(newItem);

        // 변경할 지점 찾기
        // 아이템 이미지 위치
        Transform imageTr = Util.FindChild("ItemImage", newItem.transform);
        // 이름 찾기
        Transform nameTr = Util.FindChild("ItemName", newItem.transform);
        // 스텟 텍스트 찾기(공격력 방어력을 바꿔주기 위해서 무기는 공격력 방어구는 방어력)
        Transform itemStatTextTr = Util.FindChild("ItemStatText", newItem.transform);
        // 실제 스텟
        Transform itemStatTr = Util.FindChild("ItemStat", newItem.transform);
        // 가격 찾기
        Transform priceTr = Util.FindChild("ItemPrice", newItem.transform);
        // 겟 컴포넌트 사용을 추후 줄여야 됨
        Image itemImage = imageTr.GetComponent<Image>();
        Text itemName = nameTr.GetComponent<Text>();
        Text itemStatText = itemStatTextTr.GetComponent<Text>();
        Text abilityStat = itemStatTr.GetComponent<Text>();
        Text itemPrice = priceTr.GetComponent<Text>();

        // 이미지 넣음
        itemImage.sprite = GameManager.Resource.GetImage(go.name);
        // 이름 넣음
        itemName.text = itemStat.Name;
        // 추후 방어구 생길경우 수정
        itemStatText.text = "공격력";
        abilityStat.text = itemStat.Skill.ToString();
        // 가격 넣음
        itemPrice.text = itemStat.Get_Price.ToString() + " 골드";
    }
}
