using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��� ǥ�� �� ��Ʈ�ѷ�

public class GoldDisplayController : MonoBehaviour
{
    // �÷��̾� ������Ʈ���� ������ ���� ����
    PlayerStat _stat;
    // ���ȿ��� ������ ��差
    int _goldAmount;
    // ��差 ǥ���� �ؽ�Ʈ UI
    Text _goldAmountText;

    void Start()
    {
        // �θ�(ĳ���� ������Ʈ)���� ���� ��������
        //_stat = transform.parent.GetComponent<PlayerStat>();
        
        // �÷��̾� ������Ʈ���� ���� ��������
        _stat = GameManager.Obj._playerStat;
        // ������ ��� ������ �ʱ�ȭ
        _goldAmount = _stat.Gold;
        // �ؽ�Ʈ ��������
        _goldAmountText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        GoldDisplaySystem();
    }

    private void GoldDisplaySystem()
    {
        _goldAmount = GameManager.Obj._goldController.GoldAmount;
        _goldAmountText.text = _goldAmount.ToString();
    }
}
