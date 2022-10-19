using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotController : MonoBehaviour
{
    public Button _button;
    // ���� ���� ������
    public List<GameObject> _slotItem;

    public void Start()
    {
        _button = GetComponent<Button>();
        // ��ư�� �ڵ�� ������
        _button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // �����ϱ� ����ϱ� ��ư ����
        GameManager.Ui.BuyCancelButtonOpen(transform);
        // ���� ���Կ� �ִ� �������� �����ϱ� ��ư�� ���� �Ѱ���
        GameManager.Ui._buyCancelScript._shopSelectItem = _slotItem[0]; ;
    }
}
