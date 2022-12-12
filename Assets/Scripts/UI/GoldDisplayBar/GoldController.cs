using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���� Ŭ����
// ȹ�� �Լ�, �Һ� �Լ�

public class GoldController : MonoBehaviour
{
    // ���� ��� ������
    //[SerializeField]
    private int _goldAmount;
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

    // �����ڴ� MonoBehaviour ������ �̿�, ����Ѵٸ� ���Ŀ� ����
    // �⺻ ������
    public GoldController() { }

    // ��差 �ʱⰪ �ִ� ������
    public GoldController(int goldAmount)
    {
        //_goldAmount = goldAmount;
        GoldAmount = goldAmount;
    }

    private void Start()
    {
        //
    }

    private void Update() 
    {
        // ��差 ��� ������Ʈ ���ֱ�
        if(GameManager.Obj != null || GameManager.Obj._playerStat != null)
        {
            GoldAmount = GameManager.Obj._playerStat.Gold;
            // �÷��̾� ���� �������� ���� ����ؼ� �����صα�
            _goldAmount = GoldAmount;
            //_goldAmount = GameManager.Obj._playerStat.Gold;
        }
        else
        {
            // ���� ���� ������ ���� 0
            GoldAmount = _goldAmount;
        }

        // �÷��̾� ������Ʈ ������ ���⼭ ������Ʈ
        if(GameManager.Obj._playerStat != null)
        {
            GameManager.Obj._playerStat.Gold = GoldAmount;
            //GameManager.Obj._playerStat.Gold = _goldAmount;
        }
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
