using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 골드 표시 바 컨트롤러

public class GoldDisplayShopController : MonoBehaviour
{
    // 스탯에서 가져올 골드량
    int _goldAmount;
    // 골드량 표기할 텍스트 UI
    Text _goldAmountText;

    void Start()
    {
        // 텍스트 가져오기
        _goldAmountText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        GoldDisplaySystem();
    }

    private void GoldDisplaySystem()
    {        
        // 골드컨트롤러에 연결
        _goldAmount = GameManager.Obj._playerController._goldController.GoldAmount;
        _goldAmountText.text = _goldAmount.ToString();
    }
}
