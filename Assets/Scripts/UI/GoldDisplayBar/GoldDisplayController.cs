using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 골드 표시 바 컨트롤러

public class GoldDisplayController : MonoBehaviour
{
    // 플레이어 오브젝트에서 가져올 스탯 변수
    PlayerStat _stat;
    // 스탯에서 가져올 골드량
    int _goldAmount;
    // 골드량 표기할 텍스트 UI
    Text _goldAmountText;

    void Start()
    {
        // 부모(캐릭터 오브젝트)에서 스탯 가져오기
        //_stat = transform.parent.GetComponent<PlayerStat>();
        
        // 플레이어 오브젝트에서 스탯 가져오기
        _stat = GameManager.Obj._playerStat;
        // 스탯의 골드 값으로 초기화
        _goldAmount = _stat.Gold;
        // 텍스트 가져오기
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
