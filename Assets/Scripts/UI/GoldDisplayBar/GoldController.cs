using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 골드 관리 클래스
// 획득 함수, 소비 함수

public class GoldController : MonoBehaviour
{
    // 현재 골드 보유량
    //[SerializeField]
    //private int _goldAmount;
    // 현재 골드 보유량을 플레이어 스텟에 직접 연결함

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

    // 기본 생성자
    public GoldController() { }

    // 골드량 초기값 있는 생성자
    public GoldController(int goldAmount)
    {
        //_goldAmount = goldAmount;
        GoldAmount = goldAmount;
    }

    // 골드 획득 함수(+)
    public void GetGold(int plusAmount)
    {
        // 획득량 더해주기
        //_goldAmount += plusAmount;
        GoldAmount += plusAmount;
    }

    // 골드 소비 함수(-)
    public bool SpendGold(int minusAmount)
    {
        // 보유 금액보다 소비액이 크다면 막아줘야함
        if (GoldAmount >= minusAmount)
        {
            // 소비량만큼 차감
            GoldAmount -= minusAmount;
            // 제대로 처리 됐으면 true return
            return true;
        }
        // 보유량이 더 적다면 false return
        return false;
    }
}
