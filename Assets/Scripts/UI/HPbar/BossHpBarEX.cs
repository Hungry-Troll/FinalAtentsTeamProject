using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBarEX : MonoBehaviour
{
    // 이미지 계산을 위한 스텟 변수
    Stat _stat;
    // Hp 바 이미지
    Image _hpBarImage;
    // HP 바 계산용 변수
    float _maxHp;
    float _currentHp;

    private void Start()
    {
        // 스텟 정보 가지고 옴
        _stat = GameManager.Obj._bossStat;
        // HpBar 위치 찾음
        Transform tmpTr = Util.FindChild("HpBar", transform);
        // 찾은 곳의 이미지 컴포넌트를 가지고 있음
        _hpBarImage = tmpTr.GetComponent<Image>();
        // 체력 풀피
        _hpBarImage.fillAmount = 1;
    }
    //Update is called once per frame

    private void Update()
    {
        // 추후 업데이트 말고 한번씩만 구현되게 수정 필요
        HpBarSystem();
    }

    // HP 계산 함수
    private void HpBarSystem()
    {
        _maxHp = _stat.Max_Hp;
        _currentHp = _stat.Hp;
        _hpBarImage.fillAmount = _currentHp / _maxHp;
        // HP 텍스트
    }


}
