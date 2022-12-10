using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���� Ŭ����
// ȹ�� �Լ�, �Һ� �Լ�

public class GoldController : MonoBehaviour
{
    // ���� ��� ������
    //[SerializeField]
    //private int _goldAmount;
    // ���� ��� �������� �÷��̾� ���ݿ� ���� ������

    public int GoldAmount
    {
        get
        {
            //return _goldAmount; 
            return GameManager.Obj._playerStat.Gold;
        }
        set
        {
            //_goldAmount = value;
            GameManager.Obj._playerStat.Gold = value;
        }
    }

    // �⺻ ������
    public GoldController() { }

    // ��差 �ʱⰪ �ִ� ������
    public GoldController(int goldAmount)
    {
        //_goldAmount = goldAmount;
        GoldAmount = goldAmount;
    }

    // ��� ȹ�� �Լ�(+)
    public void GetGold(int plusAmount)
    {
        // ȹ�淮 �����ֱ�
        //_goldAmount += plusAmount;
        GoldAmount += plusAmount;
    }

    // ��� �Һ� �Լ�(-)
    public bool SpendGold(int minusAmount)
    {
        // ���� �ݾ׺��� �Һ���� ũ�ٸ� ���������
        if (GoldAmount >= minusAmount)
        {
            // �Һ񷮸�ŭ ����
            GoldAmount -= minusAmount;
            // ����� ó�� ������ true return
            return true;
        }
        // �������� �� ���ٸ� false return
        return false;
    }
}
