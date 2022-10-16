using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    // ���� �Ʒ��� ���ο� ������ ����� ���� ��ġ ���� scrollRect
    public ScrollRect scrollRect;
    // ���� ������ �̹���
    public Image itemImage;
    // ���� ������
    public ShopSlotController shopSlotController;
    // ���� ������ ����Ʈ
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
