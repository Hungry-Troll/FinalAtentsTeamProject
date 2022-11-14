using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Define;
using static Util;

public class UiManager
{
    public Define.ItemType _itemType;

    // UI Root용 변수
    public GameObject go;

    // 플레이어 Hp바
    public GameObject _playerHpBar;

    // 포트레이트
    public GameObject _portrait;
    public Image _portraitImage;
    public PortraitController _portraitController;

    // 장소 팝업
    public GameObject _locationPopUp;
    public Text _locationPopUpText;

    // 스킬상태창
    public SkillViewController _skillViewController;

    // 씬 버튼
    public GameObject _sceneButton;

    // 조이스틱
    public JoyStickController _joyStickController;
    public GameObject _joyStick;

    // 상태창에 보이는 플레이어
    public GameObject _statePlayerObj;
    // 상태창 플레이어 이름 확인용
    public Define.Job _job;
    public string _jobName;

    // 인벤토리
    public InventoryController _inventoryController;
    public GameObject _inven;
    // 인벤토리 스텟창용 Text // GetComponent 사용을 줄이기 위해 미리 선언해서 데이터를 들고 있을 예정
    Text atkText;
    Text defText;
    // 인벤 슬롯 이미지 // GetComponent 사용을 줄이기 위해 미리 선언해서 데이터를 들고 있을 예정
    public List<Image> _slotImage;

    // 인벤 버튼 (가방아이콘)
    public GameObject _invenButton;

    // 옵션 버튼
    public GameObject _OptionButton;

    // 옵션 창(화면)
    public GameObject _Option;

    // 미니맵
    public GameObject _miniMap;

    // 아이템 스텟창
    public GameObject _itemStatView;
    // 아이템 스텟창 스크립트
    public ItemStatViewController _itemStatViewController;
    // 아이템 스텟창 이미지
    public Image _findItemImage;
    // 아이템 스텟창 텍스트 >> 아이템창 열었을 때 각각 데이터 적용 용도 // GetComponent 사용을 줄이기 위해 미리 선언해서 데이터를 들고 있을 예정
    public Text _itemNameText;
    public Text _itemStatText;
    public Text _itemStat;
    public Text _itemIntroduce;
    public Text _equipText;
    public Text _dropText;

    // 아이템 스텟창을 한개씩만 띄우기 위한 변수
    // 추후 UI 창 갯수관리를 해야 되면 변경
    public bool _itemStatOpen;

    // 장착 아이템 스텟창
    public GameObject _equipStatView;
    // 장착 아이템 스크립트
    public EquipStatViewController _equipStatViewController;

    // 장착 아이템 스텟창 이미지
    public Image _equipFindItemImage;
    // 장착 아이템 스텟창 텍스트 >> 아이템창 열었을 때 각각 데이터 적용 용도 // GetComponent 사용을 줄이기 위해 미리 선언해서 데이터를 들고 있을 예정
    public Text _equipItemNameText;
    public Text _equipItemStatText;
    public Text _equipItemStat;
    public Text _equipItemIntroduce;
    public Text _equipEquipText;
    public Text _equipDropText;
    // 장착 아이템 스텟창 갯수 관리용
    public bool _equipStatOpen;

    // 장착 불가 창
    public GameObject _cannotEquipView;

    // 상점 구매하기 취소하기 버튼
    public GameObject _buyCancel;
    public UI_BuyCancelButton _buyCancelScript;

    // 공격 타겟 몬스터
    public GameObject _targetMonster;
    public MonsterController _targetMonsterController; 
    public MonsterStat _targetMonsterStat;

    


    //Ui 관리는 여기에서 처리
    //현재 UI 이름 개판 Ui // UI ... 추후 정리...
    public void Init()
    {
        go = new GameObject();
        go.name = "@UI_Root";
        ////////////////////////////////
        /// 반복되는 내용 1차 정리 완료
        /// 추후 캔버스를 정리한다면 여기서 더 정리 할 수도 있음
        ////////////////////////////////

        // 시작하면 포트레이트 불러옴
        _portrait = GameManager.Create.CreateUi("UI_Portrait", go);
        _portraitImage = _portrait.GetComponentInChildren<Image>();
        _portraitController = _portrait.AddComponent<PortraitController>();
        // 직업에 따른 포트레이트 체크
        PortraitCheck();

        // 장소 팝업 불러옴
        _locationPopUp = GameManager.Create.CreateUi("UI_LocationPopUp", go);
        _locationPopUpText = _locationPopUp.GetComponentInChildren<Text>();
        // 생성하자마자 애니메이션 실행되기 때문에 active false
        _locationPopUp.SetActive(false);

        // 포트레이트와 연결할 스킬창 UI 생성
        GameObject skillView = GameManager.Create.CreateUi("Ui_Skill", go);
        _skillViewController = skillView.GetComponent<SkillViewController>();
        // 스킬창 생성 후 비활성화
        skillView.gameObject.SetActive(false);

        // 시작하면 Ui 버튼 불러옴
        _sceneButton = GameManager.Create.CreateUi("Ui_Scene_Button", go);
        // 시작하면 조이스틱 씬에 불러옴
        _joyStick = GameManager.Create.CreateUi("Ui_JoystickController", go);
        _joyStickController = _joyStick.GetComponentInChildren<JoyStickController>();

        // 상태창 플레이어 생성
        GameObject statePlayerObj = GameManager.Resource.GetCharacter(GameManager.Select._jobName + "Temp");
        _statePlayerObj = GameObject.Instantiate<GameObject>(statePlayerObj, new Vector3(0,200,0), Quaternion.identity);
        // 시작하면 인벤토리버튼(가방아이콘) 씬에 불러옴
        _invenButton = GameManager.Create.CreateUi("Ui_SceneInventoryButton", go);
        // 시작하면 인벤토리를 미리 불러와서 우선 SetActive(false)로 함
        _inven = GameManager.Create.CreateUi("Ui_Inventory", go);
        _inventoryController = _inven.GetComponentInChildren<InventoryController>();
        // 인벤토리 상태창 스텟 넣을 위치 확인용
        Transform findAtkText = Util.FindChild("AtkText", _inventoryController.transform);
        Transform findDefText = Util.FindChild("DefText", _inventoryController.transform);
        atkText = findAtkText.GetComponent<Text>();
        defText = findDefText.GetComponent<Text>();

        // 아이템 교체용 인벤토리 이미지를 미리 UI 매니지에서 들고있음 // GetComponent 줄이는 용도
        _slotImage = new List<Image>();
        for (int i = 0; i < 20; i++)
        {
            Transform invenImageTr = GameManager.Ui._inventoryController._invenSlotArray[i].transform.GetChild(0);
            // 처음 실행에만 GetComponent 를 20번 사용함 // 이전 방식대로하면 아이템 교체마다 GetComponent를 사용해야됨
            Image slotImage = invenImageTr.gameObject.GetComponent<Image>();
            _slotImage.Add(slotImage);
        }
        InventoryClose();

        // 시작하면 미니맵 불러옴
        _miniMap = GameManager.Create.CreateUi("UI_MiniMap", go);
        // 시작하면 옵션버튼 불러옴
        _OptionButton = GameManager.Create.CreateUi("Ui_SceneOptionButton", go);
        // 시작하면 옵션창 불러옴 // 사운드연결을 위해서 옵션창을 사운드 매니저에서 먼저 사용함. 동일한 게임오브젝트여야지만
        // 옵션창 슬라이드가 정상 작동 되므로 사운드 매니저에서 옵션창을 가지고 옴
        GameObject Option = GameManager.Sound._option;
        _Option = GameObject.Instantiate<GameObject>(Option);
        _Option.SetActive(false);

        // 아이템 스텟창 만드는 내용이 너무 길어서 함수 처리
        InitItemStatView();
        // 장착 아이템 스텟창 만드는 내용이 길어서 함수 처리
        EquipItemStatView();

        // 상점 구매하기 취소하기 버튼 만들고 비활성화
        _buyCancel = GameManager.Create.CreateUi("UI_BuyCancel", go);
        _buyCancelScript = _buyCancel.AddComponent<UI_BuyCancelButton>();
        _buyCancel.SetActive(false);
    }
    
    // 아이템 스텟창 함수 Init
    private void InitItemStatView()
    {
        // 시작하면 아이템 스텟창을 한개씩만 띄우기 위한 변수를 초기화 // 1개만 열 수 있음
        _itemStatOpen = false;
        // 시작하면 아이템 스텟창 불러옴
        _itemStatView = GameManager.Create.CreateUi("UI_ItemStatView", go);
        _itemStatViewController = _itemStatView.GetComponentInChildren<ItemStatViewController>();
        // UI_ItemStatView 에서 이미지 넣을 위치 찾음
        Transform itemImage = Util.FindChild("ItemImage", _itemStatView.transform);
        // 아이템 이미지
        _findItemImage = itemImage.GetComponent<Image>();

        // 스텟창 내용도 미리 들고있어야됨 안그러면 GetComponent 다수 사용 필요
        // 아이템명
        Transform itemNameText = Util.FindChild("ItemNameText", _itemStatView.transform);
        // 공격력 방어력
        Transform itemStatText = Util.FindChild("ItemStatText", _itemStatView.transform);
        // 공격력 방어력 실제 데이터
        Transform itemStat = Util.FindChild("ItemStat", _itemStatView.transform);
        // 아이템 설명
        Transform itemIntroduce = Util.FindChild("ItemIntroduce", _itemStatView.transform);
        // 장착하기 사용하기
        Transform equipText = Util.FindChild("EquipText", _itemStatView.transform);
        // 버리기
        Transform dropText = Util.FindChild("DropText", _itemStatView.transform);
        // 장착 불가 창
        Transform cannotEquipView = Util.FindChild("CannotEquipPanel", _itemStatView.transform);

        _itemNameText = itemNameText.GetComponent<Text>();
        _itemStatText = itemStatText.GetComponent<Text>();
        _itemStat = itemStat.GetComponent<Text>();
        _itemIntroduce = itemIntroduce.GetComponent<Text>();
        _equipText = equipText.GetComponent<Text>();
        _dropText = dropText.GetComponent<Text>();
        _cannotEquipView = cannotEquipView.gameObject;

        //SetActive(false)로 함
        _cannotEquipView.SetActive(false);
        _itemStatView.SetActive(false);
    }

    /// <summary>
    /// 인벤토리 관련
    /// </summary>
    /// 

    public void InventoryOpen()
    {
        _inventoryController.gameObject.SetActive(true);
    }

    public void InventoryClose()
    {
        _inventoryController.gameObject.SetActive(false);
    }

    /// <summary>
    /// 옵션창 관련
    /// </summary>
    public void OptionOpen()
    {
        // 옵션창 불러오고 위치와 크기 초기화
        // 캔버스에서 필요없는 게임오브젝트 비활성화
        _Option.SetActive(true);
        _Option.transform.localScale = new Vector3(1, 1, 1);
        _Option.transform.localPosition = new Vector3(0, 0, 0);
        Transform videoPlayer = Util.FindChild("VideoPlay", _Option.transform);
        Transform tiltle = Util.FindChild("Title", _Option.transform);
        Transform buttons = Util.FindChild("Buttons", _Option.transform);
        Transform option = Util.FindChild("Option", _Option.transform);
        videoPlayer.gameObject.SetActive(false);
        tiltle.gameObject.SetActive(false);
        buttons.gameObject.SetActive(false);
        option.gameObject.SetActive(true);
    }

    /// <summary>
    /// 아이템 상태창 관련
    /// </summary>

    // 아이템 상태창 UI를 한개만 쓰고 이미지와 스텟은 불러오는 방식으로 구현함
    // 추후 스텟 비교 기능을 넣으면 좋음

    
    public GameObject ItemStatViewOpen(GameObject SlotItem)
    {
        // 널 체크
        if (SlotItem == null)
        {
            return null;
        }
            
        // 넣을 이미지를 찾음
        Sprite _sprite = GameManager.Resource.GetImage(SlotItem.name);

        if (_itemStatOpen == false)
        {
            // 아이템 정보 창 열고
            _itemStatView.SetActive(true);

            _itemStatOpen = true;
        }
        // 위치용
        _itemStatView.transform.position = _inventoryController.transform.position;
        // 이미지 대입
        _findItemImage.sprite = _sprite;
        // 이왕 찾은 이미지를 스텟뷰에 넣어놓음 (아이템 장착용)
        _itemStatViewController._sprite = _sprite;

        foreach (Define.ItemType itemType in Enum.GetValues(typeof(Define.ItemType)))
        {
            if (GameManager.Stat.SearchItem(SlotItem.name).Type == itemType.ToString())
            {
                _itemStatViewController._itemType = itemType;
                break;
            }
            else
            {
                _itemStatViewController._itemType = Define.ItemType.None;
            }
        }
        foreach (Define.ItemName itemName in Enum.GetValues(typeof(Define.ItemName)))
        {
            if (GameManager.Stat.SearchItem(SlotItem.name).Id == itemName.ToString())
            {
                _itemStatViewController._itemName = itemName;
                break;
            }
            else
            {
                _itemStatViewController._itemName = Define.ItemName.None;
            }
        }

        // 스텟을 상태창에 넣어줌
        ItemStatViewStatAdd(SlotItem);

        return SlotItem;
    }
    
    // 아이템창 닫는 함수
    public void ItemStatViewClose()
    {
        if (_itemStatOpen == true)
        {
            _cannotEquipView.SetActive(false);
            _itemStatView.SetActive(false);
            _itemStatOpen = false;
        }
    }

    // 아이템창에 스텟 넣는 함수
    public void ItemStatViewStatAdd(GameObject gameObject)
    {
        // 미리 찾은 Text 컴포넌트에 아이템 스텟 적용
        for(int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            if(GameManager.Ui._inventoryController._item[i].name == gameObject.name)
            {
                // 겟 컴포넌트가 많아질 것 같으면 추후 수정
                // 우선은 쉽게 아이템인벤토리 목록 게임오브젝트하고 매개변수로 가지고온 게임오브젝르 이름 비교 후 스텟을 넣어줌
                ItemStatEX tmpStat = GameManager.Ui._inventoryController._item[i].GetComponent<ItemStatEX>();
                // 아이템 이름
                _itemNameText.text = tmpStat.Name;

                // 아이템 종류에 따라 다른 텍스트 출력 (공격력 방어력)
                if(tmpStat.Type == "Weapon")
                {
                    // _itemStatText.text (공격력 방어력)
                    // _equipText.text (착용하기 사용하기)
                    _itemStatText.text = "공격력";
                    _equipText.text = "착용하기";
                }
                else if(tmpStat.Type == "Armour")
                {
                    _itemStatText.text = "방어력";
                    _equipText.text = "착용하기";
                }
                else if(tmpStat.Type == "Consumables")
                {
                    _itemStatText.text = "회복력";
                    _equipText.text = "사용하기";
                }
                // 공격력 방어력 실제 수치
                _itemStat.text = tmpStat.Skill.ToString();
                // 아이템 설명
                _itemIntroduce.text = tmpStat.Info;
                //_dropText >> 거의 쓸일이 없을것으로 추정
            }
        }
    }

    /// <summary>
    /// 장착 아이템 함수
    /// </summary>

    // 장착 아이템 스텟창 함수 Init
    private void EquipItemStatView()
    {

        // 시작하면 아이템 스텟창을 한개씩만 띄우기 위한 변수를 초기화 // 1개만 열 수 있음
        _equipStatOpen = false;
        // 시작하면 아이템 스텟창 불러옴
        _equipStatView = GameManager.Create.CreateUi("UI_EquipStatView", go);
        _equipStatViewController = _equipStatView.GetComponentInChildren<EquipStatViewController>();
        // UI_ItemStatView 에서 이미지 넣을 위치 찾음
        Transform itemImage = Util.FindChild("ItemImage", _equipStatView.transform);
        // 아이템 이미지
        _equipFindItemImage = itemImage.GetComponent<Image>();

        // 스텟창 내용도 미리 들고있어야됨 안그러면 GetComponent 다수 사용 필요
        // 아이템명
        Transform itemNameText = Util.FindChild("ItemNameText", _equipStatView.transform);
        // 공격력 방어력
        Transform itemStatText = Util.FindChild("ItemStatText", _equipStatView.transform);
        // 공격력 방어력 실제 데이터
        Transform itemStat = Util.FindChild("ItemStat", _equipStatView.transform);
        // 아이템 설명
        Transform itemIntroduce = Util.FindChild("ItemIntroduce", _equipStatView.transform);
        // 장착하기 사용하기
        Transform equipText = Util.FindChild("EquipText", _equipStatView.transform);
        // 버리기
        Transform dropText = Util.FindChild("DropText", _equipStatView.transform);

        _equipItemNameText = itemNameText.GetComponent<Text>();
        _equipItemStatText = itemStatText.GetComponent<Text>();
        _equipItemStat = itemStat.GetComponent<Text>();
        _equipItemIntroduce = itemIntroduce.GetComponent<Text>();
        _equipEquipText = equipText.GetComponent<Text>();
        _equipDropText = dropText.GetComponent<Text>();

        //SetActive(false)로 함
        _equipStatView.SetActive(false);
    }

    public GameObject EquipStatViewOpen(GameObject SlotItem)
    {
        // 널 체크
        if (SlotItem == null)
        {
            return null;
        }
        // 넣을 이미지를 찾음
        Sprite _sprite = GameManager.Resource.GetImage(SlotItem.name);

        // 창 오픈 확인
        if (_equipStatOpen == false)
        {
            // 아이템 정보 창 열고
            _equipStatView.SetActive(true);

            _equipStatOpen = true;
        }
        // 위치용
        _equipStatView.transform.position = _inventoryController.transform.position;
        // 이미지 대입
        _equipFindItemImage.sprite = _sprite;

        // 스텟을 상태창에 넣어줌
        EquipStatViewStatAdd(SlotItem);
        return SlotItem;
    }

    // 장착 아이템창에 스텟 넣는 함수
    public void EquipStatViewStatAdd(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }

        // 겟 컴포넌트가 많아질 것 같으면 추후 수정
        // 우선은 쉽게 아이템인벤토리 목록 게임오브젝트하고 매개변수로 가지고온 게임오브젝르 이름 비교 후 스텟을 넣어줌
        ItemStatEX tmpStat = null;
        if (GameManager.Ui._inventoryController._weapon == gameObject)
        {
            tmpStat = GameManager.Ui._inventoryController._weaponStat.GetComponent<ItemStatEX>();
        }
        else if (GameManager.Ui._inventoryController._armour == gameObject)
        {
            tmpStat = GameManager.Ui._inventoryController._armourStat.GetComponent<ItemStatEX>();
        }
        // 아이템 이름
        _equipItemNameText.text = tmpStat.Name;

        // 아이템 종류에 따라 다른 텍스트 출력 (공격력 방어력)
        if(tmpStat.Type == "Weapon")
        {
            // _itemStatText.text (공격력 방어력)
            // _equipText.text (착용하기 사용하기)
            _equipItemStatText.text = "공격력";
            _equipEquipText.text = "해제하기";
        }
        else if(tmpStat.Type == "Armour")
        {
            _equipItemStatText.text = "방어력";
            _equipEquipText.text = "해제하기";
        }
        else if(tmpStat.Type == "Consumables")
        {
            _equipItemStatText.text = "회복력";
            _equipEquipText.text = "사용하기";
        }
        // 공격력 방어력 실제 수치
        _equipItemStat.text = tmpStat.Skill.ToString();
        // 아이템 설명
        _equipItemIntroduce.text = tmpStat.Info;
        //_dropText >> 거의 쓸일이 없을것으로 추정
    }
    // 장착 아이템창 닫는 함수
    public void EquipStatViewClose()
    {
        if (_equipStatOpen == true)
        {
            // 아이템 정보 창 열고
            _equipStatView.SetActive(false);
            _equipStatOpen = false;
        }
    }

    // 장착 불가 메세지 창 닫는 함수
    public void CannotEquipViewClose()
    {
        if (_itemStatOpen == true)
        {
            _cannotEquipView.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 무기 장착 관련
    /// </summary>

    // 무기 장착 함수 (계산)
    // 추후 방어구도 동일하게 적용
    public void ItemStatViewWeaponEquip(ItemType itemType)
    {
        // 장착 대상 저장용 임시 변수
        Transform findTr = null;
        // 장착 대상을 찾음 / 아이템은 Define enum ItemType으로 분류
        // json에도 장착 타입이 있으므로 json으로 사용할지 고민

        switch (itemType)
        {
            case ItemType.Weapon:
                findTr = Util.FindChild("WeaponImage", _inventoryController.transform);
                break;
            case ItemType.Armour:
                findTr = Util.FindChild("ArmourImage", _inventoryController.transform);
                break;
            case ItemType.Consumables:
                break;
        }

        // 직업에 따른 무기 장착 확인
        if (!JobWeaponCheck())
        {
            // 추후 여기에 착용할수없습니다 UI 넣으면 됨
            return;
        }
        Image findImage = findTr.GetComponent<Image>();

        // 이미지 넣음 (_itemStatviewContlloer에서 이미지를 이미지를 들고있음)
        findImage.sprite = _itemStatViewController._sprite;
        // 이미지 활성화
        findImage.gameObject.SetActive(true);
        // 인벤 컨트롤러에서 아이템 이름과 이미지 아이템 이름을 비교해서 동일 이름이면 인벤 컨트롤러 Weapon 게임오브젝트에 넣음 (장착의미)
        // 인벤토리 아이템 중 찾음 이미지와 이름이 같으면 (동일 아이템)

        if (itemType == ItemType.Weapon)
        {
            // 임시 무기 변수
            GameObject tmpWeapon = null;
            // 인벤토리에서 웨폰이 널이 아니라면 (기존 무기 착용을 했다면)
            if (_inventoryController._weapon != null)
            {
                // 기존 착용 무기를 임시 변수에 대입
                tmpWeapon = _inventoryController._weapon;
            }
            // 인벤토리 아이템 숫자만큼 루프
            for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
            {
                // 인벤토리 아이템하고 이미지가 동일하면
                if (findImage.sprite.name == GameManager.Ui._inventoryController._item[i].name)
                {
                    // 무기 장착 (인벤토리상 들고있는 무기 / 인벤토리에서 들고있음)
                    _inventoryController._weapon = GameManager.Ui._inventoryController._item[i];
                    _inventoryController._weaponStat = GameManager.Ui._inventoryController._item[i].GetComponent<ItemStatEX>();
                    // 무기 위치 찾음 (플레이어가 들고있을 무기 위치)
                    //Transform findPos = GameManager.Weapon.FindWeaponPos(GameManager.Obj._playerController.transform);
                    // 찾은 위치에 무기 착용 (플레이어가 들고있을 무기) 무기 방향 버그가 있어서 아래 Temp함수로 대체
                    //GameManager.Weapon.EquipWeapon(_inventoryController._weapon.name, findPos);

                    // 무기 착용
                    GameManager.Weapon.TempEquipWeapon(_inventoryController._weapon.name, GameManager.Obj._playerController.transform);
                    // 넣을 무기에서 스텟 스크립트 가지고 옴
                    ItemStatEX tmpStatEx = _inventoryController._weapon.GetComponent<ItemStatEX>();
                    // 플레이어 스크립트에 스텟 더해줌
                    GameManager.Obj._playerStat.Atk += tmpStatEx.Skill;

                    // 인벤에서 무기 제거 >> 게임오브젝트 제거, 이미지 제거 
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);

                    // 임시 저장한 무기가 널이 아니라면 (기존에 장착한 무기가 있다면)
                    if (tmpWeapon != null)
                    {
                        // 임시 저장한 기존 장착 아이템 인벤토리로 넣음
                        GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(tmpWeapon);
                        GameManager.Ui._inventoryController._item.Add(tmpWeapon);
                        ItemStatEX tmpStat = tmpWeapon.GetComponent<ItemStatEX>();
                        // 임시 저장한 기존 장착 아이템 스텟 계산
                        GameManager.Obj._playerStat.Atk -= tmpStat.Skill;
                    }
                    // 플레이어 스크립트를 이용해서 인벤토리에 있는 캐릭터창에 공격력 방어력을 보여줌
                    InventoryStatUpdate();
                }
            }
        }
        if (itemType == ItemType.Armour)
        {
            // 임시 방어구 변수
            GameObject tmpArmour = null;
            // 인벤토리에서 아머가 널이 아니라면 (기존 무기 착용을 했다면)
            if (_inventoryController._armour != null)
            {
                // 기존 착용 방어구를 임시 변수에 대입
                tmpArmour = _inventoryController._armour;
            }
            // 인벤토리 아이템 숫자만큼 루프
            for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
            {
                // 인벤토리 아이템하고 이미지가 동일하면
                if (findImage.sprite.name == GameManager.Ui._inventoryController._item[i].name)
                {
                    // 방어구 장착 (인벤토리상 들고있는 방어구 / 인벤토리에서 들고있음)
                    _inventoryController._armour = GameManager.Ui._inventoryController._item[i];
                    _inventoryController._armourStat = GameManager.Ui._inventoryController._item[i].GetComponent<ItemStatEX>();

                    // 넣을 방어구에서 스텟 스크립트 가지고 옴
                    ItemStatEX tmpStatEx = _inventoryController._armour.GetComponent<ItemStatEX>();
                    // 플레이어 스크립트에 스텟 더해줌
                    GameManager.Obj._playerStat.Def += tmpStatEx.Skill;

                    // 인벤에서 방어구 제거 >> 게임오브젝트 제거, 이미지 제거 
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);

                    // 임시 저장한 무기가 널이 아니라면 (기존에 장착한 무기가 있다면)
                    if (tmpArmour != null)
                    {
                        // 임시 저장한 기존 장착 아이템 인벤토리로 넣음
                        GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(tmpArmour);
                        GameManager.Ui._inventoryController._item.Add(tmpArmour);
                        ItemStatEX tmpStat = tmpArmour.GetComponent<ItemStatEX>();
                        // 임시 저장한 기존 장착 아이템 스텟 계산
                        GameManager.Obj._playerStat.Atk -= tmpStat.Skill;
                    }
                    // 플레이어 스크립트를 이용해서 인벤토리에 있는 캐릭터창에 공격력 방어력을 보여줌
                    InventoryStatUpdate();
                }
            }
        }

        // 아이템 상태창 닫기
        ItemStatViewClose();
        // 인벤토리에 들어있는 게임오브젝트의 이름을 이미지 이름과 비교해서 동일한 이미지를 넣는 함수
        InventoryImageArray();
    }

    // 아이템 버리는 함수
    public void ItemStatViewWeaponDrop()
    {
        // 인벤토리의 아이템을 버리는 경우
        // 인벤토리 아이템 숫자만큼 루프
        for(int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            // 인벤토리 아이템하고 이미지가 동일하면
            if(_itemStatViewController._sprite.name == GameManager.Ui._inventoryController._item[i].name)
            {
                // 인벤에서 무기 제거 >> 게임오브젝트 제거, 이미지 제거 
                GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                GameManager.Ui._inventoryController._item.RemoveAt(i);
                _slotImage[i].sprite = null;
                _slotImage[i].gameObject.SetActive(false);
                break;
            }
        }
        // 아이템 상태창 닫기
        ItemStatViewClose();

        // 인벤토리에 들어있는 게임오브젝트의 이름을 이미지 이름과 비교해서 동일한 이미지를 넣는 함수
        InventoryImageArray();
    }

    // 인벤토리 이미지 정렬 함수 (랜더링)
    // 아이템 장착 시 해제 시 사용
    void InventoryImageArray()
    {
        for(int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            // 인벤토리 아이템 이름
            string tmpName = GameManager.Ui._inventoryController._item[i].name;
            // 이름을 이용한 이미지 찾기
            Sprite tmpSprite = GameManager.Resource.GetImage(tmpName);
            // 찾은 이미지를 각 슬롯 이미지에 넣음
            _slotImage[i].sprite = tmpSprite;
            // 활성화
            _slotImage[i].gameObject.SetActive(true);
            // 동일한 이름의 게임오브젝트를 동일한 슬롯에 넣음
            GameObject tmpGameObject = GameManager.Resource.GetfieldItem(tmpName);
            GameObject go = Util.Instantiate(tmpGameObject);
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(go);
        }
        for(int i = 19; i > GameManager.Ui._inventoryController._item.Count -1; i--)
        {
            // 나머지 슬롯 이미지 전부 삭제 비활성화
            _slotImage[i].sprite = null;
            _slotImage[i].gameObject.SetActive(false);
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
        }
    }

    // 인벤토리 상태창 공격력 / 방어력 구현 / 우선 무기부터
    public void InventoryStatUpdate()
    {
        atkText.text = GameManager.Obj._playerStat.Atk.ToString();
        defText.text = GameManager.Obj._playerStat.Def.ToString();
    }

    // 직업에 따라서 장착가능한 무기 체크 함수
    public bool JobWeaponCheck()
    {
        // 스텟뷰에서 들고있는 이미지가 착용 가능한 무기인지 체크

        // 선택한 직업 확인
        string jobName = GameManager.Select._jobName;
        Define.Job job = GameManager.Select.SelectJobCheck();
        // 들고있는 아이템 스프라이트 확인
        string ImageName = _itemStatViewController._sprite.name;
        // 임시 리스트
        List<string> temp = new List<string>();

        // 직접 대입하는것은 좋은 코드 아님
        switch (job)
        {
            case Define.Job.Superhuman:
                temp.Add("sword1");
                temp.Add("sword2");
                temp.Add("sword3");
                temp.Add("armour1");
                break;
            case Define.Job.Cyborg:
                temp.Add("gun1");
                temp.Add("gun2");
                temp.Add("gun3");
                temp.Add("armour2");
                break;
            case Define.Job.Scientist:
                temp.Add("book1");
                temp.Add("book2");
                temp.Add("book3");
                temp.Add("armour3");
                break;
        }
        // 비교해서 들고있는 이미지와 직업별 아이템이 같으면 true
        foreach (var one in temp)
        {
            if (one.Equals(ImageName))
            {
                return true;
            }
        }
        return false;
    }
    // 몬스터 펫 Hp 바
    public void HpBarCreate(GameObject gameObject)
    {
        GameObject hpBar = GameManager.Resource.GetUi("UI_HpBar");
        GameObject _hpBar = GameObject.Instantiate<GameObject>(hpBar);
        Canvas canvas = _hpBar.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main; // 이것만 다름
        //위치 잡아주는 코드
        _hpBar.transform.SetParent(gameObject.transform);
        _hpBar.transform.position = gameObject.transform.position + Vector3.up * (gameObject.GetComponent<NavMeshAgent>().height);
    }
    // 플레이어에게 HP바 연결하는 함수
    public void PlayerHpBarCreate(GameObject player)
    {
        _playerHpBar = GameManager.Create.CreateUi("Ui_PlayerHpBar", player);
        // 파티클 설정을 위한 카메라 모드 변경 코드
        // 캔버스를 가지고 와서
        // 캔버스 모드 스크린스페이트카메라로 변경후
        // 카메라 연결

        Canvas canvas = _playerHpBar.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameManager.Cam._uiParticleCam;
        // 화면에 보이는 것이 포트레이트보다 아래에 보여야 되고, 파티클보다 아래에 보여야 되기 때문에 소팅오더를 -1로 수정함
        // 포트레이트와 파티클은 기본값
        canvas.sortingOrder = -1;
    }

    // 플레이어 포트레이트 만드는 함수
    public void PortraitCheck()
    {
        string temp = GameManager.Select._jobName;
        Sprite tempImage = GameManager.Resource.GetImage(temp);
        _portraitImage.sprite = tempImage;
    }

    // 대화창 열릴 때 UI 전부 SetActiveFalse
    public void UISetActiveFalse()
    {
        _playerHpBar.SetActive(false);
        _portrait.SetActive(false);
        _sceneButton.SetActive(false);
        _joyStick.SetActive(false);
        InventoryClose();
        _OptionButton.SetActive(false);
        _invenButton.SetActive(false);
        _Option.SetActive(false);
        _miniMap.SetActive(false);
        _itemStatView.SetActive(false);
        if(GameManager.Effect._levelUpPar.gameObject.activeSelf == true)
        {
            GameManager.Effect.LevelUpPortraitEffectOff();
        }   
    }

    // 대화창 열릴 때 UI 껏던것 다시 킴
    public void UISetActiveTrue()
    {
        _playerHpBar.SetActive(true);
        _portrait.SetActive(true);
        _sceneButton.SetActive(true);
        _joyStick.SetActive(true);
        _OptionButton.SetActive(true);
        _invenButton.SetActive(true);
        //_Option.SetActive(false);
        _miniMap.SetActive(true);
        //_itemStatView.SetActive(false);
        // 스킬 포인트가 0보다 클 경우에만 포트레이트 이펙트 온
        if(_skillViewController._skillLevel.skillPoint > 0)
        {
            GameManager.Effect.LevelUpPortraitEffectOn();
        }
    }
    // 구매하기 취소하기 버튼 함수(UI위치 때문에 Transform 정보를 가지고 와야됨)
    public void BuyCancelButtonOpen(Transform tr)
    {
        // x 축 보정을 위한 변수 / 좋은 코드는 아님 
        int x = 110;
        int y = -70;
        // 활성화
        _buyCancel.SetActive(true);
        // 위치 버튼 위치 찾음
        Transform buy = Util.FindChild("BuyButton", _buyCancel.transform);
        Transform cancel = Util.FindChild("CancelButton", _buyCancel.transform);
        Debug.Log(tr.position);
        // 버튼 생성위치를 클릭한 객체 좌표쪽으로 
        //
        buy.position = tr.position + new Vector3(x, y, 0);
        cancel.position = tr.position + new Vector3(x * 3, y, 0);
    }

    // 게임오버
    public void GameOverUI()
    {
        GameManager.Create.CreateUi("UI_GameOver", go);
    }

    /// <summary>
    /// 이하 씬 Attack버튼 관련
    /// 추후 코드 수정 필요 
    /// UI 매니저가 너무 비대해질경우 아래 공격버튼 및 스킬 버튼은 스킬매니저를 생성해서 관리 해야 될 수 있음
    /// </summary>
    public void AttackButton()
    {
        // 캐릭터가 3마리 이므로 코드 수정
        // 기존 코드는 오브젝트 매니저에서 관리
        // 몬스터만 찾아서 오브젝트 매니저에 넣어둠
        GameManager.Obj.FindMobListTarget();
        // 만약 타겟몬스터가 널이 아니라면
        if(GameManager.Obj._targetMonster != null)
        {
            // 플레이어 컨트롤러에서 처리 >> 우선 오토무브로 다가가도록 처리
            GameManager.Obj._playerController._creatureState = CreatureState.AutoMove;
            // 공격버튼 누르면 펫에게도 몬스터 타겟 몬스터를 알려줌
            GameManager.Obj._petController._target = GameManager.Obj._targetMonster.transform;
        }
    }

    // 장소 팝업 함수
    // 매개변수 타입 추후 변경가능(Define) 
    public void PopUpLocation(string _locationName)
    {
        _locationPopUpText.text = _locationName;
        _locationPopUp.SetActive(true);
    }

    // 장소 팝업 close 함수
    // 끄지 않아도 보이지는 않지만 다시 켤때 대비
    public IEnumerator ClosePopUpLocation()
    {
        float playTime = 3.0f;
        // 애니메이션 시간동안 멈췄다가
        yield return new WaitForSeconds(playTime);
        // 끝나면 active false
        _locationPopUp.SetActive(false);
    }

    public void Skill1Button()
    {

    }
    public void Skill2Button()
    {

    }

    public void Skill3Button()
    {

    }

    public void RollingButton()
    {
        GameManager.Obj._playerController.KeyboardMove(true);
    }
}
