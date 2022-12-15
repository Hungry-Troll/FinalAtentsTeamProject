using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InvenSlotController : MonoBehaviour
{
    // ���Կ� ���� ������
    public List<GameObject> _SlotItem = new List<GameObject>();
    public int _invenSlotCount;//������ ���� ��ȣ  - �̰� ������ �ȵǾ�����	
    public int _invenItemCount;   //�ش� ���Կ� ����� ������ ������	
    public Text itemCntText;

    void Start()
    {
        _invenSlotCount = 0;
        //itemCntText = transform.GetChild(1).GetComponent<Text>();	
        //�ʱ�ȭ	
        //1. �ؽ�Ʈ�� �� �ִ� ������ ��ĭ���� ����	
        itemCntText.text = " ";
        //_spriteImage = GetComponent<Sprite>();	
        //_spriteImage = Resources.Load<Sprite>("Resource/Image/ItemImage" + gameObject.name);	
        //_itmeImage = GetComponentInChildren<Image>();	

    }

    void Update()
    {
        // �ش� ������ ������� �ʴٸ�
        if(_SlotItem.Count > 0)
        {
            // �ϴ��� ���Ǹ� üũ
            if (_SlotItem[0].name.Equals("potion1"))
            {
                // ���� ǥ��
                itemCntText.text = _invenItemCount.ToString();
            }
        }
    }

    public void OnButtonClick()
    {
        // ������ Ÿ�Կ� ���� ���� �����ߵ� �켱 ���⸸	
        if (_SlotItem.Count > 0)
        {
            GameManager.Ui.ItemStatViewOpen(_SlotItem[0]);
        }
    }
    /// <summary>	
    /// �������� �߰��� �� ��ø�Ǵ� ������ �����ϴ� ��	
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
            Debug.Log("������ ��ø������ 0���ε� �� ������ ��");
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
