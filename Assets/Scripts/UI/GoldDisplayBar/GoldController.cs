using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 골드 관리 클래스
// 획득 함수, 소비 함수

public class GoldController : MonoBehaviour
{
    // 현재 골드 보유량
    //[SerializeField]
    private int _goldAmount;
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

    // 생성자는 MonoBehaviour 없으면 이용, 사용한다면 추후에 삭제
    // 기본 생성자
    public GoldController() { }

    // 골드량 초기값 있는 생성자
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
        // 골드량 계속 업데이트 해주기
        if(GameManager.Obj != null || GameManager.Obj._playerStat != null)
        {
            GoldAmount = GameManager.Obj._playerStat.Gold;
            // 플레이어 스탯 없어졌을 때를 대비해서 저장해두기
            _goldAmount = GoldAmount;
            //_goldAmount = GameManager.Obj._playerStat.Gold;
        }
        else
        {
            // 기존 스탯 없으면 골드는 0
            GoldAmount = _goldAmount;
        }

        // 플레이어 오브젝트 있으면 여기서 업데이트
        if(GameManager.Obj._playerStat != null)
        {
            GameManager.Obj._playerStat.Gold = GoldAmount;
            //GameManager.Obj._playerStat.Gold = _goldAmount;
        }
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
