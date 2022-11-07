using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스킬창 숫자 정리용


// 이 스크립트는 최대한 public 을 이용해서 연결 처리하는 것으로 처리해봄
// 개인적으로는 비추 방식이긴한대 아무튼...

// 버튼 연결만 드래그 드롭 말고 코드로 연결함
public class SkillViewController : MonoBehaviour
{
    public struct SKILLNUM
    {
        public int skill1;
        public int skill2;
        public int skill3;
        public int skillPoint;
    }

    // 스킬레벨 관리용 
    public SKILLNUM _skillLevel;
    // 창 닫기 버튼
    public Button _closeButton;
    // 스킬 레벨업 버튼
    public Button _skillUpButton1;
    public Button _skillUpButton2;
    public Button _skillUpButton3;
    // 스킬 슬롯 버튼
    public Button _skillSlotButton1;
    public Button _skillSlotButton2;
    public Button _skillSlotButton3;
    // 스킬 누를경우 나오는 슬롯 이미지
    public Image _skill1Image;
    public Image _skill2Image;
    public Image _skill3Image;
    // 스킬 레벨 텍스트
    public Text _skill1LevelText;
    public Text _skill2LevelText;
    public Text _skill3LevelText;

    // 스킬 포인트 텍스트
    public Text _skillPointText;

    /// <summary>
    /// 스킬 누를 경우 UI 게임오브젝트
    /// </summary>
    public GameObject _skillTextPanel;
    // 스킬 이름
    public Text _skillNameText;
    // 스킬 설명
    public Text _skillContentText;
    // 스킬 효과
    public Text _skillEffectText;

    void Start()
    {
        // 스킬 레벨 숫자 0으로 초기화
        _skillLevel.skill1 = 0;
        _skillLevel.skill2 = 0;
        _skillLevel.skill3 = 0;
        _skillLevel.skillPoint = 0;

        // 스킬 레벨 숫자를 텍스트에 대입함
        _skill1LevelText.text = _skillLevel.skill1.ToString();
        _skill2LevelText.text = _skillLevel.skill2.ToString();
        _skill3LevelText.text = _skillLevel.skill3.ToString();

        // 각각 버튼 함수로 연결
        _closeButton.onClick.AddListener(CloseButton);
        _skillUpButton1.onClick.AddListener(SkillUpButton1);
        _skillUpButton2.onClick.AddListener(SkillUpButton2);
        _skillUpButton3.onClick.AddListener(SkillUpButton3);
        _skillSlotButton1.onClick.AddListener(SkillSlotButton1);
        _skillSlotButton2.onClick.AddListener(SkillSlotButton2);
        _skillSlotButton3.onClick.AddListener(SkillSlotButton3);
    }

    public void LevelUp()
    {
        // 시작 시 첫 스킬 찍을 수 있게 스킬포인트 1 줌
        _skillLevel.skillPoint++;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // 레벨업 파티클(이펙트) 생성
        GameManager.Effect.LevelUpPortraitEffectOn();
    }

    private void CloseButton()
    {
        // 비활성화
        gameObject.SetActive(false);
        // 스킬 설명 창 UI
        _skillTextPanel.SetActive(false);
    }

    // 스킬 레벨업 버튼 활성화 함수 >> 레벨업시에만 활성화
    public void ButtonInteractableTrue()
    {
        _skillUpButton1.interactable = true;
        _skillUpButton2.interactable = true;
        _skillUpButton3.interactable = true;
        // 스킬포인트 증가
        _skillLevel.skillPoint++;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
    }

    // 스킬 레벨업 버튼 비활성화 함수
    public void ButtonInteractableFalse()
    {
        // 스킬 포인트가 있으면 스킬 레벨업 버튼은 계속 활성화
        if(_skillLevel.skillPoint > 0)
        {
            return;
        }    
        _skillUpButton1.interactable = false;
        _skillUpButton2.interactable = false;
        _skillUpButton3.interactable = false;
        // 레벨업 파티클 끄기
        GameManager.Effect.LevelUpPortraitEffectOff();
    }

    // 스킬 레벨 올리는 함수
    private void SkillUpButton1()
    {
        _skillLevel.skill1++;
        _skill1LevelText.text = _skillLevel.skill1.ToString();
        // 스킬 포인트 감소
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // 스킬레벨업 버튼 비활성화 함수 
        ButtonInteractableFalse();
    }

    private void SkillUpButton2()
    {
        _skillLevel.skill2++;
        _skill2LevelText.text = _skillLevel.skill2.ToString();
        // 스킬 포인트 감소
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // 스킬레벨업 버튼 비활성화 함수
        ButtonInteractableFalse();
    }

    private void SkillUpButton3()
    {
        _skillLevel.skill3++;
        _skill3LevelText.text = _skillLevel.skill3.ToString();
        // 스킬 포인트 감소
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // 스킬레벨업 버튼 비활성화 함수
        ButtonInteractableFalse();
    }

    // 스킬 버튼 누를경우 스킬 이미지 활성화 (드래그 드롭용) 이미지는 컨트롤러를 따로 만들어서 그쪽에서 관리
    private void SkillSlotButton1()
    {
        _skill1Image.gameObject.SetActive(true);
        // 스킬 설명 창 UI
        _skillTextPanel.SetActive(true);

        // 스킬 이름
        //_skillNameText.text = 
        // 스킬 설명
        //_skillContentText.text = 
        // 스킬 효과
        //_skillEffectText.text = 
    }

    private void SkillSlotButton2()
    {
        _skill2Image.gameObject.SetActive(true);
        // 스킬 설명 창 UI
        _skillTextPanel.SetActive(true);

        // 스킬 이름
        //_skillNameText.text = 
        // 스킬 설명
        //_skillContentText.text = 
        // 스킬 효과
        //_skillEffectText.text = 
    }

    private void SkillSlotButton3()
    {
        _skill3Image.gameObject.SetActive(true);
        // 스킬 설명 창 UI
        _skillTextPanel.SetActive(true);

        // 스킬 이름
        //_skillNameText.text = 
        // 스킬 설명
        //_skillContentText.text = 
        // 스킬 효과
        //_skillEffectText.text = 
    }
}
