using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��� ǥ�� �� ��Ʈ�ѷ�

public class GoldDisplayShopController : MonoBehaviour
{
    // ���ȿ��� ������ ��差
    int _goldAmount;
    // ��差 ǥ���� �ؽ�Ʈ UI
    Text _goldAmountText;

    void Start()
    {
        // �ؽ�Ʈ ��������
        _goldAmountText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        GoldDisplaySystem();
    }

    private void GoldDisplaySystem()
    {        
        // �����Ʈ�ѷ��� ����
        _goldAmount = GameManager.Obj._playerController._goldController.GoldAmount;
        _goldAmountText.text = _goldAmount.ToString();
    }
}
