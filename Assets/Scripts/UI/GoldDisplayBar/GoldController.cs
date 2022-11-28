using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���� Ŭ����
// ȹ�� �Լ�, �Һ� �Լ�

public class GoldController : MonoBehaviour
{
    // ���� ��� ������
    private int _goldAmount;

    public int GoldAmount
    {
        get { return _goldAmount; }
        set { _goldAmount = value; }
    }

    // �⺻ ������
    public GoldController() {}

    // ��差 �ʱⰪ �ִ� ������
    public GoldController(int goldAmount)
    {
        _goldAmount = goldAmount;
    }

    // ��� ȹ�� �Լ�(+)
    public void GetGold(int plusAmount)
    {
        // ȹ�淮 �����ֱ�
        _goldAmount += plusAmount;
    }

    // ��� �Һ� �Լ�(-)
    public bool SpendGold(int minusAmount)
    {
        // ���� �ݾ׺��� �Һ���� ũ�ٸ� ���������
        if(_goldAmount >= minusAmount)
        {
            // �Һ񷮸�ŭ ����
            _goldAmount -= minusAmount;
            // ����� ó�� ������ true return
            return true;
        }
        // �������� �� ���ٸ� false return
        return false;
    }
}
