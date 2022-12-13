using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 스킬창 숫자 정리용


// 이 스크립트는 최대한 public 을 이용해서 연결 처리하는 것으로 처리해봄
// 개인적으로는 비추 방식이긴한대 아무튼...

// 버튼 연결만 드래그 드롭 말고 코드로 연결함
public class SkillViewController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEndDragHandler
{
    public struct SKILLNUM
    {
        // 스킬 레벨용 변수
        public int skill1;
        public int skill2;
        public int skill3;
        // 스킬 포인트 변수
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

    // 스킬 이미지
    public Image _skill1Image;
    public Image _skill2Image;
    public Image _skill3Image;
    // 스킬 배경 이미지
    public Image _baseImage1;
    public Image _baseImage2;
    public Image _baseImage3;
    
    // 스킬 Block 이미지 (배경)
    public Image _blockImage1;
    public Image _blockImage2;
    public Image _blockImage3;

    // 스킬 레벨 텍스트
    public Text _skill1LevelText;
    public Text _skill2LevelText;
    public Text _skill3LevelText;

    // 스킬 포인트 텍스트
    public Text _skillPointText;

    // 스킬 스텟 관련
    public SkillStat _skillStat;
    // 스킬 스텟 담은 리스트
    //public List<SkillStat> _playerSkillList;
    public List<TempSkillStat> _playerSkillList;


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

    /// <summary>
    /// 스킬 드래그 드롭 관련 변수
    /// </summary>
    // 스킬 슬롯 스크립트
    public SkillViewSlot _skillViewSlot1;
    public SkillViewSlot _skillViewSlot2;
    public SkillViewSlot _skillViewSlot3;
    // 스킬 슬롯 스크립트 리스트
    public List<SkillViewSlot> _slotList;
    // 스킬 슬롯 인덱스 확인용
    public int _selectedSlot;
    // 스킬 선택 시 움직일 이미지
    public Image _selectedIcon;
    // 씬 스킬 슬롯 리스트
    public List<Ui_SceneSkillSlot> _sceneSkillSlot;
    void Start()
    {
        // 스킬 레벨 숫자 0으로 초기화
        // 만약 기존 스킬레벨 있다면 초기화하지 않기
        if(_skillLevel.skill1 <= 0)
        {
            _skillLevel.skill1 = 0;
        }
        if (_skillLevel.skill2 <= 0)
        {
            _skillLevel.skill2 = 0;
        }
        if (_skillLevel.skill3 <= 0)
        {
            _skillLevel.skill3 = 0;
        }
        //_skillPoint = 0;

        // 스킬 레벨 숫자를 텍스트에 대입함
        _skill1LevelText.text = _skillLevel.skill1.ToString();
        _skill2LevelText.text = _skillLevel.skill2.ToString();
        _skill3LevelText.text = _skillLevel.skill3.ToString();

        // 스킬 포인트 숫자 텍스트에 대입
        _skillPointText.text = _skillLevel.skillPoint.ToString();

        // 각각 버튼 함수로 연결
        _closeButton.onClick.AddListener(CloseButton);
        _skillUpButton1.onClick.AddListener(SkillUpButton1);
        _skillUpButton2.onClick.AddListener(SkillUpButton2);
        _skillUpButton3.onClick.AddListener(SkillUpButton3);
        // 인자가 함수는 버튼 연결 시 람다식으로 연결 할 수 있음
        _skillSlotButton1.onClick.AddListener(() => SkillTextPanelOpen(0));
        _skillSlotButton2.onClick.AddListener(() => SkillTextPanelOpen(1));
        _skillSlotButton3.onClick.AddListener(() => SkillTextPanelOpen(2));

        /// <summary>
        /// 이하 스킬 드래그 드롭 관련 초기화
        /// </summary>
         
        // 스킬 슬롯 스크립트 리스트에 저장
        _slotList.Add(_skillViewSlot1);
        _slotList.Add(_skillViewSlot2);
        _slotList.Add(_skillViewSlot3);
        // 스킬 슬롯 인덱스 확인용 초기화 초기값은 -1;
        _selectedSlot = -1;
        // 선택한 이미지 움직이는 용도
        _selectedIcon.gameObject.SetActive(false);
        // 씬 스킬슬롯 찾아옴
        // 씬 스킬슬롯이 먼저 생성되기 때문에 문제 없이 찾아올 것으로 추정
        Ui_SceneAttackButton tmp = GameManager.Ui._sceneButton.GetComponent<Ui_SceneAttackButton>();
        _sceneSkillSlot = tmp._skillSlots;
        // 직업에 따른 스킬이미지 변경 함수
        SkillImageCheck();
        // 스킬 스텟 스크립트 대입(빈 껍대기)
        _skillStat = GetComponent<SkillStat>();
        
        // 리스트 초기화
        // SkillStat은 MonoBehaviour를 상속받기 때문에 new가 적용되지 않음
        // -> GetCompnent 같이 하나의 스크립트를 공유하게 되어서 Temp타입으로 생성
        // --> 이미 존재하는 인스턴스의 주소값만 전달해주는 방식인 것 같다.
        _playerSkillList = new List<TempSkillStat>();
    }

    // 레벨업시 사용
    public void LevelUp()
    {
        // 레벨업 파티클(이펙트) 생성
        GameManager.Effect.LevelUpPortraitEffectOn();
        // 스킬 업 버튼 활성화 + 스킬포인트 추가
        ButtonInteractableTrue();
        // 레벨이 1이 아닐경우 레벨업 시 스텟 가지고 와야 됨
    }

    private void CloseButton()
    {
        // 비활성화
        gameObject.SetActive(false);
        // 스킬 설명 창 UI
        _skillTextPanel.SetActive(false);
    }

    // 스킬 레벨업 버튼 활성화 함수 >> 레벨업시에만 활성화
    // 오버로딩(1 / 2)
    public void ButtonInteractableTrue()
    {
        _skillUpButton1.interactable = true;
        _skillUpButton2.interactable = true;

        // 스킬레벨 총합이 3이상일때만 3번 슬롯 스킬 오픈
        if(_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }
        // 스킬레벨이 부족할경우 스킬 오픈x
        _skillUpButton3.interactable = false;

        // 스킬포인트 증가
        _skillLevel.skillPoint++;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // 변경된 스킬포인트 저장
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
    }

    // 스킬 레벨업 버튼 활성화 함수(스킬 포인트 증가 없음) / 오버로딩을 위한 매개변수로 실질적 역할 없음
    // 오버로딩(2 / 2)
    public void ButtonInteractableTrue(bool isLevelUp)
    {
        // 스킬 포인트 없다면 아래 실행하지 않고 리턴
        if(_skillLevel.skillPoint <= 0)
        {
            return;
        }
        _skillUpButton1.interactable = true;
        _skillUpButton2.interactable = true;

        // 스킬레벨 총합이 3이상일때만 3번 슬롯 스킬 오픈
        if (_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }
        // 스킬레벨이 부족할경우 스킬 오픈x
        _skillUpButton3.interactable = false;

        // 스킬포인트 증가하지 않음
        //_skillLevel.skillPoint++;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // 변경된 스킬포인트 저장
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
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


        // 스킬레벨 총합이 3이상일때만 3번 슬롯 스킬 오픈
        if (_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }

        // 스킬레벨업 버튼 비활성화 함수 
        ButtonInteractableFalse();
        // 스킬레벨에 따라서 스킬 이미지 활성화
        SkillLevelCheck();
        // 스킬 업데이트
        SkillTextPanelOpen(0, false);
        // 스킬 레벨 업데이트
        _skillStat.SkillLevel = _skillLevel.skill1;
        // 스킬 업데이트해서 리스트에 담기
        GameManager.Skill.UpdatePlayerSkillList(_playerSkillList, _skillStat);
        // 변경된 스킬포인트 저장
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
    }

    private void SkillUpButton2()
    {
        _skillLevel.skill2++;
        _skill2LevelText.text = _skillLevel.skill2.ToString();
        // 스킬 포인트 감소
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();

        // 스킬레벨 총합이 3이상일때만 3번 슬롯 스킬 오픈
        if (_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }

        // 스킬레벨업 버튼 비활성화 함수
        ButtonInteractableFalse();
        // 스킬레벨에 따라서 스킬 이미지 활성화
        SkillLevelCheck();
        // 스킬 업데이트
        SkillTextPanelOpen(1, false);
        // 스킬 레벨 업데이트
        _skillStat.SkillLevel = _skillLevel.skill2;
        // 스킬 업데이트해서 리스트에 담기
        GameManager.Skill.UpdatePlayerSkillList(_playerSkillList, _skillStat);
        // 변경된 스킬포인트 저장
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
    }

    private void SkillUpButton3()
    {
        _skillLevel.skill3++;
        _skill3LevelText.text = _skillLevel.skill3.ToString();
        // 스킬 포인트 감소
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();

        // 스킬레벨 총합이 3이상일때만 3번 슬롯 스킬 오픈
        if (_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }

        // 스킬레벨업 버튼 비활성화 함수
        ButtonInteractableFalse();
        // 스킬레벨에 따라서 스킬 이미지 활성화
        SkillLevelCheck();
        // 스킬 업데이트
        SkillTextPanelOpen(2, false);
        // 스킬 레벨 업데이트
        _skillStat.SkillLevel = _skillLevel.skill3;
        // 스킬 업데이트해서 리스트에 담기
        GameManager.Skill.UpdatePlayerSkillList(_playerSkillList, _skillStat);
        // 변경된 스킬포인트 저장
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
    }

    // 스킬 텍스트 로드
    private void SkillTextPanelOpen(int skillNumber)
    {
        // 스킬 설명 창 UI
        _skillTextPanel.SetActive(true);

        // 직업별 스킬 텍스트 로드
        switch (GameManager.Select._job)
        {
            case Define.Job.Superhuman:
                {
                    // 이 함수가 실행되면 _skillStat에 스텟 저장 됨
                    GameManager.Skill.SkillStatLoadJson("Skill" + (skillNumber + 1).ToString(), _skillStat);
                }
                break;
            case Define.Job.Cyborg:
                {
                    GameManager.Skill.SkillStatLoadJson("Skill" + (skillNumber + 4).ToString(), _skillStat);
                }
                break;
            case Define.Job.Scientist:
                {
                    GameManager.Skill.SkillStatLoadJson("Skill" + (skillNumber + 7).ToString(), _skillStat);
                }
                break;
            case Define.Job.None:
                break;
        }

        // 스킬 이름
        _skillNameText.text = _skillStat.SkillName;
        // 스킬 설명
        _skillContentText.text = _skillStat.SkillContent;
        // 스킬 효과
        _skillEffectText.text = _skillStat.SkillEffect;
        // 스킬 레벨 업데이트 필요
        // 클릭할 때마다 _skillStat에 기본 데이터 로드되기 때문
        switch(skillNumber + 1)
        {
            // 스킬뷰 슬롯 1
            case 1:
                _skillStat.SkillLevel = _skillLevel.skill1;
                break;
            // 스킬뷰 슬롯 2
            case 2:
                _skillStat.SkillLevel = _skillLevel.skill2;
                break;
            // 스킬뷰 슬롯 3
            case 3:
                _skillStat.SkillLevel = _skillLevel.skill3;
                break;
        }
    }
    // 스킬 텍스트 로드 : 설명 창 UI 오픈 유무 결정하는 매개변수 추가
    // 오버로딩 2 / 2
    private void SkillTextPanelOpen(int skillNumber, bool openPanel)
    {
        // 스킬 설명 창 UI
        _skillTextPanel.SetActive(openPanel);

        // 직업별 스킬 텍스트 로드
        switch (GameManager.Select._job)
        {
            case Define.Job.Superhuman:
                {
                    // 이 함수가 실행되면 _skillStat에 스텟 저장 됨
                    GameManager.Skill.SkillStatLoadJson("Skill" + (skillNumber + 1).ToString(), _skillStat);
                }
                break;
            case Define.Job.Cyborg:
                {
                    GameManager.Skill.SkillStatLoadJson("Skill" + (skillNumber + 4).ToString(), _skillStat);
                }
                break;
            case Define.Job.Scientist:
                {
                    GameManager.Skill.SkillStatLoadJson("Skill" + (skillNumber + 7).ToString(), _skillStat);
                }
                break;
            case Define.Job.None:
                break;
        }

        // 스킬 이름
        _skillNameText.text = _skillStat.SkillName;
        // 스킬 설명
        _skillContentText.text = _skillStat.SkillContent;
        // 스킬 효과
        _skillEffectText.text = _skillStat.SkillEffect;
        // 스킬 레벨 업데이트 필요
        // 클릭할 때마다 _skillStat에 기본 데이터 로드되기 때문
        switch (skillNumber + 1)
        {
            // 스킬뷰 슬롯 1
            case 1:
                _skillStat.SkillLevel = _skillLevel.skill1;
                break;
            // 스킬뷰 슬롯 2
            case 2:
                _skillStat.SkillLevel = _skillLevel.skill2;
                break;
            // 스킬뷰 슬롯 3
            case 3:
                _skillStat.SkillLevel = _skillLevel.skill3;
                break;
        }
    }
    // 직업별 스킬 이미지 아이콘 변경 함수
    private void SkillImageCheck()
    {
        // 임시 변수 선언 및 초기화 / 임시 이미지 저장용
        Sprite tmpImage1 = null;
        Sprite tmpImage2 = null;
        Sprite tmpImage3 = null;

        // 직업별 이미지 로드
        switch (GameManager.Select._job)
        {
            case Define.Job.Superhuman:
                {
                    tmpImage1 = GameManager.Resource.GetImage("skill1");
                    tmpImage2 = GameManager.Resource.GetImage("skill2");
                    tmpImage3 = GameManager.Resource.GetImage("skill3");
                }
                break;

            case Define.Job.Cyborg:
                {
                    tmpImage1 = GameManager.Resource.GetImage("skill4");
                    tmpImage2 = GameManager.Resource.GetImage("skill5");
                    tmpImage3 = GameManager.Resource.GetImage("skill6");
                }
                break;
            case Define.Job.Scientist:
                {
                    tmpImage1 = GameManager.Resource.GetImage("skill7");
                    tmpImage2 = GameManager.Resource.GetImage("skill8");
                    tmpImage3 = GameManager.Resource.GetImage("skill9");
                }
                break;
            case Define.Job.None:
                break;
        }
        // 임시 저장한 이미지 대입(스킬이미지)
        _skill1Image.sprite = tmpImage1;
        _skill2Image.sprite = tmpImage2;
        _skill3Image.sprite = tmpImage3;
        // 스킬 배경 이미지
        _baseImage1.sprite = tmpImage1;
        _baseImage2.sprite = tmpImage2;
        _baseImage3.sprite = tmpImage3;
    }

    // 스킬레벨이 1 이상이면 스킬 이미지 활성화 (진하게)
    // raycastTarget 을 이용해서 드래그 드롭 안되게
    public void SkillLevelCheck()
    {
        // todo
        if (_skillLevel.skill1 > 0)
        {
            _skill1Image.gameObject.SetActive(true);
            _skill1Image.raycastTarget = false;
            _blockImage1.raycastTarget = false;
        }
        if (_skillLevel.skill2 > 0)
        {
            _skill2Image.gameObject.SetActive(true);
            _skill2Image.raycastTarget = false;
            _blockImage2.raycastTarget = false;
        }
        if (_skillLevel.skill3 > 0)
        {
            _skill3Image.gameObject.SetActive(true);
            _skill3Image.raycastTarget = false;
            _blockImage3.raycastTarget = false;
        }
    }
            

    /// <summary>
    /// 이하 스킬 드래그 드롭 관련 함수
    /// </summary>

    //  누를 때
    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            if(_slotList[i].IsInRect(eventData.position))
            {
                // 선택한 슬롯 확인용
                _selectedSlot = i;
                // 이미지 찾아서 교체 (새로 만든 이미지로 변경해야됨/ 씬 이미지로 할 경우 참조가 되기 때문)
                _selectedIcon.sprite = GameManager.Resource.GetImage(_slotList[i]._uiImage.sprite.name);
                // 위치 옮김
                _selectedIcon.rectTransform.position = eventData.position;
                _selectedIcon.gameObject.SetActive(true);
                // 선택한 슬롯 찾기
                Debug.Log("선택한 슬롯 = " + _slotList[i].gameObject.name);
                // 스킬 설명창 오픈
                SkillTextPanelOpen(i);
            }
        }
    }
    // 땔 때 (드래그 상황이 아니고 제자리에서 눌렀다가 땔 때)
    public void OnPointerUp(PointerEventData eventData)
    {
        // 비어있는 슬롯일 경우에는 그냥 리턴
        if (_selectedSlot == -1)
            return;

        int tmpSelectedIcon = -1;
        // 몇번째 슬롯을 눌렀는지 확인하는 코드
        for (int i = 0; i < _slotList.Count; i++)
        {
            if (_slotList[i].IsInRect(eventData.position))
            {
                tmpSelectedIcon = i;
                break;
            }
        }
        // 같은 슬롯에서 땔 때
        if (tmpSelectedIcon != -1 && tmpSelectedIcon == _selectedSlot)
        {
            // 드래그용 임시 이미지 초기화
            _selectedIcon.sprite = null;
            _selectedIcon.gameObject.SetActive(false);
            _selectedSlot = -1;
        }
    }
    // 드래그
    public void OnDrag(PointerEventData eventData)
    {
        // 선택한 슬롯이 있을경우 -1 이 아님
        if (_selectedSlot != -1)
        {
            _selectedIcon.rectTransform.position = eventData.position;
        }
    }
    // 드래그 종료
    public void OnEndDrag(PointerEventData eventData)
    {
        // 헷갈려서 색상을 다르게 하기위한 변수선언 ... 이 리스트가 씬 버튼에 있는 스킬 슬롯
        List<Ui_SceneSkillSlot> sceneSkillSlot = _sceneSkillSlot;

        // 비어있는 슬롯일 경우에는 그냥 리턴
        if (_selectedSlot == -1)
            return;

        int tmpSelectedIcon = -1;
        // 몇번째 씬 스킬버튼 슬롯에서 땠는지
        for (int i = 0; i < sceneSkillSlot.Count; i++)
        {
            if (sceneSkillSlot[i].IsInRect(eventData.position))
            {
                tmpSelectedIcon = i;
                break;
            }
        }

        // 이상한 위치에서 땔 때
        if (tmpSelectedIcon == -1 && _selectedSlot != -1)
        {
            // 드래그용 임시 이미지 초기화
            _selectedIcon.sprite = null;
            _selectedIcon.gameObject.SetActive(false);
            _selectedSlot = -1;
            return;
        }

        // 빈 씬 스킬 버튼 슬롯에서 땔 대 
        if (sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite == null)
        {
            // 내려놓는곳에 로드 후 추가
            sceneSkillSlot[tmpSelectedIcon]._uiImage.gameObject.SetActive(true);
            sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite = GameManager.Resource.GetImage(_selectedIcon.sprite.name);
            // 스킬을 넣으면 스킬 이미지가 잘보이게 색상을 조금 수정함
            Color tmpColor;
            tmpColor.a = 0.7f;
            tmpColor.r = 1.0f;
            tmpColor.g = 1.0f;
            tmpColor.b = 1.0f;
            sceneSkillSlot[tmpSelectedIcon]._uiImage.color = tmpColor;
            Debug.Log(sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite.name);
            // 드래그용 임시 이미지 해제
            _selectedIcon.sprite = null;
            _selectedIcon.gameObject.SetActive(false);

            // 처음 선택한 슬롯 초기화
            _selectedSlot = -1;
            // 씬 버튼 스킬 텍스트 비활성화
            sceneSkillSlot[tmpSelectedIcon]._skillText.gameObject.SetActive(false);
            return;
        }

        // 이미지가 있는 씬 스킬 버튼 슬롯에서 땔 때
        if (sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite != null)
        {
            // 내려놓는곳에 로드 후 추가
            sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite = null;
            sceneSkillSlot[tmpSelectedIcon]._uiImage.gameObject.SetActive(true);
            sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite = GameManager.Resource.GetImage(_selectedIcon.sprite.name);
            // 스킬을 넣으면 스킬 이미지가 잘보이게 색상을 조금 수정함
            Color tmpColor;
            tmpColor.a = 0.7f;
            tmpColor.r = 1.0f;
            tmpColor.g = 1.0f;
            tmpColor.b = 1.0f;
            sceneSkillSlot[tmpSelectedIcon]._uiImage.color = tmpColor;
            Debug.Log(_slotList[_selectedSlot]._uiImage.sprite.name);
            
            // 플레이어 스킬 목록 업데이트
            // 드래그한 스킬 아이디
            //string skillId = _slotList[_selectedSlot]._uiImage.sprite.name.Replace("s", "S").Trim();
            // 업데이트
            // _skillStat 은 view목록의 활성화된 스킬을 클릭할 때마다 해당 스탯으로 변경되므로 따로 넣어줄 필요 없음
            _skillStat.SkillSlotNumber = tmpSelectedIcon;
            GameManager.Skill.UpdatePlayerSkillList(_playerSkillList, _skillStat);
            
            // 드래그용 임시 이미지 해제
            _selectedIcon.sprite = null;
            _selectedIcon.gameObject.SetActive(false);

            // 처음 선택한 슬롯 초기화
            _selectedSlot = -1;
            // 씬 버튼 스킬 텍스트 비활성화
            sceneSkillSlot[tmpSelectedIcon]._skillText.gameObject.SetActive(false);
            return;
        }
    }
}
