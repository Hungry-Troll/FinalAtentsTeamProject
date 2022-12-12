using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBarEX : MonoBehaviour
{
    // 이미지 계산을 위한 스텟 변수
    Stat _stat;
    // Hp 바 이미지
    Image _hpBarImage;
    // HP 바 계산용 변수
    public float _maxHp;
    public float _currentHp;
    // 이름 계산용
    Text _name;
    // HP 숫자용
    Text _hpText;
    // HP 레벨용
    public Text _Lv;
    private void Start()
    {
        // 스텟 정보 가지고 옴
        _stat = gameObject.transform.parent.GetComponent<Stat>();
        // HpBar 위치 찾음
        Transform tmpTr = Util.FindChild("HpBar", transform);
        // 찾은 곳의 이미지 컴포넌트를 가지고 있음
        _hpBarImage = tmpTr.GetComponent<Image>();
        // 체력 풀피
        _hpBarImage.fillAmount = 1;

        // 캐릭터 HP바 숫자 용
        Transform HpTextTr = Util.FindChild("HpText", transform);
        _hpText = HpTextTr.GetComponent<Text>();
        _hpText.text = _stat.Hp.ToString() + "/" + _stat.Max_Hp.ToString();
        // 캐릭터 이름 가지고 오기 HP바오브젝트가 관리함
        Transform NameTr = Util.FindChild("NameText", transform);
        _name = NameTr.GetComponent<Text>();
        _name.text = GameManager.Select._playerName;
        // 레벨 연결
        Transform LevelTr = Util.FindChild("LvText", transform);
        _Lv = LevelTr.GetComponent<Text>();
    }
    //Update is called once per frame

    private void Update()
    {
        // 생성한대로 그냥 두면 HP바 방향이 돌아가서 고정
        transform.rotation = Camera.main.transform.rotation;
        // 추후 업데이트 말고 한번씩만 구현되게 수정 필요
        HpBarSystem();
    }

    // HP 계산 함수
    public void HpBarSystem()
    {
        _maxHp = _stat.Max_Hp;
        _currentHp = _stat.Hp;
        _hpBarImage.fillAmount = _currentHp / _maxHp;
        // HP 바 숫자
        _hpText.text = _stat.Hp.ToString() + "/" + _stat.Max_Hp.ToString();
    }
}
