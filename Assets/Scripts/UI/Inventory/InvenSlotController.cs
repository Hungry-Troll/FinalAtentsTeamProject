using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InvenSlotController : MonoBehaviour
{
    // 슬롯에 들어가는 아이템
    public List<GameObject> _SlotItem = new List<GameObject>();
    public int _invenSlotCount;//각각의 슬롯 번호  - 이게 세팅이 안되어있음	
    public int _invenItemCount;   //해당 슬롯에 저장된 아이템 갯수들	
    public Text itemCntText;

    void Start()
    {
        _invenSlotCount = 0;
        //itemCntText = transform.GetChild(1).GetComponent<Text>();	
        //초기화	
        //1. 텍스트에 들어가 있는 내용을 빈칸으로 변경	
        itemCntText.text = " ";
        //_spriteImage = GetComponent<Sprite>();	
        //_spriteImage = Resources.Load<Sprite>("Resource/Image/ItemImage" + gameObject.name);	
        //_itmeImage = GetComponentInChildren<Image>();	

    }

    void Update()
    {
        // 해당 슬롯이 비어있지 않다면
        if(_SlotItem.Count > 0)
        {
            // 일단은 포션만 체크
            if (_SlotItem[0].name.Equals("potion1"))
            {
                // 수량 표시
                itemCntText.text = _invenItemCount.ToString();
            }
        }
    }

    public void OnButtonClick()
    {
        // 아이템 타입에 따라서 추후 나눠야됨 우선 무기만	
        if (_SlotItem.Count > 0)
        {
            GameManager.Ui.ItemStatViewOpen(_SlotItem[0]);
        }
    }
    /// <summary>	
    /// 아이템을 추가할 때 중첩되는 개수를 지정하는 것	
    /// </summary>	
    /// <param name="overlapCnt"></param>	
    public void SetOverlapItemCntAdd()
    {
        _invenItemCount++;
        itemCntText.text = _invenItemCount.ToString();
        Debug.Log(_invenItemCount);
    }
    public void SetOverlapItemCntSub()
    {
        if (_invenItemCount == 0)
        {
            Debug.Log("아이템 중첩갯수가 0개인데 더 빼려고 함");
        }
        else
        {
            _invenItemCount--;
            if (_invenItemCount == 0)
            {
                itemCntText.text = " ";
            }
            else
            {
                itemCntText.text = _invenItemCount.ToString();
            }
        }
    }

    public void SetItemCntText()
    {
        if (_invenItemCount != 0)
        {
            itemCntText.text = _invenItemCount.ToString();
        }
        else
        {
            itemCntText.text = " ";
        }
    }

}
