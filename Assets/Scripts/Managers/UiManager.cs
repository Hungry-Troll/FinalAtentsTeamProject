using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Util;


public class UiManager
{

    // UI Root용 변수
    GameObject go;

    // HpMp바
    public GameObject _hpMpBar;

    // 씬 버튼
    public GameObject _sceneButton;

    // 조이스틱
    public JoyStickController _joyStickController;
    public GameObject _joyStick;

    // 상태창에 보이는 플레이어
    public GameObject _statePlayerObj;

    // 인벤토리
    public InventoryController _inventoryController;
    public GameObject _inven;
    // 인벤 슬롯 이미지 // GetComponent 사용을 줄이기 위해 미리 선언해서 데이터를 들고 있을 예정
    public List<Image> _slotImage;

    // 인벤 버튼
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
    public ItemStatViewController _itemStatViewContoller;

    // 아이템 스텟창을 한개씩만 띄우기 위한 변수
    // 추후 UI 창 갯수관리를 해야 되면 변경
    public bool _itemStatOpen;


    // 공격 타겟 몬스터
    public GameObject targetMonster;

    

    //Ui 관리는 여기에서 처리
    public void Init()
    {
        go = new GameObject();
        go.name = "@UI_Root";

        ////////////////////////////////
        /// 반복되는 내용 나중에 함수로 정리
        ////////////////////////////////
        ///
        // 시작하면 HpMp바 씬에 불러옴
        GameObject hpMpBar = GameManager.Resource.GetUi("Ui_HpMpBar");
        _hpMpBar = GameObject.Instantiate<GameObject>(hpMpBar);
        _hpMpBar.transform.SetParent(go.transform);

        // 시작하면 씬 버튼(공격 스킬 버튼) 씬에 불러옴
        GameObject sceneButton = GameManager.Resource.GetUi("Ui_Scene_Button");
        _sceneButton = GameObject.Instantiate<GameObject>(sceneButton);
        _sceneButton.transform.SetParent(go.transform);

        // 시작하면 조이스틱 씬에 불러옴
        GameObject joystick = GameManager.Resource.GetUi("Ui_JoystickController");
        _joyStick = GameObject.Instantiate<GameObject>(joystick);
        _joyStickController = _joyStick.GetComponentInChildren<JoyStickController>();
        _joyStick.transform.SetParent(go.transform);

        // 시작하면 상태창에 보이는 플레이어 불러옴, 상태창은 인벤토리와 세트
        GameObject statePlayerObj = GameManager.Resource.GetCharacter("tempPlayer");
        _statePlayerObj = GameObject.Instantiate<GameObject>(statePlayerObj, new Vector3(0,200,0), Quaternion.identity);

        // 시작하면 인벤토리버튼(가방아이콘) 씬에 불러옴
        GameObject invenButton = GameManager.Resource.GetUi("Ui_SceneInventoryButton");
        _invenButton = GameObject.Instantiate<GameObject>(invenButton);
        _invenButton.transform.SetParent(go.transform);

        // 시작하면 인벤토리를 미리 불러와서 우선 SetActive(false)로 함
        GameObject inven = GameManager.Resource.GetUi("Ui_Inventory");
        _inven = GameObject.Instantiate<GameObject>(inven);
        _inventoryController = _inven.GetComponentInChildren<InventoryController>();
        _inven.transform.SetParent(go.transform);

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
        GameObject miniMap = GameManager.Resource.GetUi("UI_MiniMap");
        _miniMap = GameObject.Instantiate<GameObject>(miniMap);
        _miniMap.transform.SetParent(go.transform);

        // 시작하면 옵션버튼 불러옴
        GameObject optionButton = GameManager.Resource.GetUi("Ui_SceneOptionButton");
        _OptionButton = GameObject.Instantiate<GameObject>(optionButton);
        _OptionButton.transform.SetParent(go.transform);

        // 시작하면 옵션창 불러옴 // 사운드연결을 위해서 옵션창을 사운드 매니저에서 먼저 사용함. 동일한 게임오브젝트여야지만
        // 옵션창 슬라이드가 정상 작동 되므로 사운드 매니저에서 옵션창을 가지고 옴
        GameObject Option = GameManager.Sound._option;
        _Option = GameObject.Instantiate<GameObject>(Option);
        _Option.SetActive(false);

        // 시작하면 아이템 스텟창을 한개씩만 띄우기 위한 변수를 초기화 // 1개만 열 수 있음
        _itemStatOpen = false;
        // 시작하면 아이템 스텟창 불러 우선 SetActive(false)로 함
        GameObject itemStatView = GameManager.Resource.GetUi("UI_ItemStatView");
        _itemStatView = GameObject.Instantiate<GameObject>(itemStatView);
        _itemStatView.SetActive(false);
        // 아이템 스텟창 스크립트도 미리 들고있음
        _itemStatViewContoller = _itemStatView.GetComponentInChildren<ItemStatViewController>();
    }
    /// <summary>
    /// 인벤토리 관련
    /// </summary>
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
    public GameObject ItemStatViewOpen(GameObject invenSlotItem)
    {
        //_itemStatOpen로 창 열려있는지 체크
        if(_itemStatOpen == false)
        {
            // 아이템 정보 창 열고
            _itemStatView.SetActive(true);
        }

        _itemStatView.transform.position = _inventoryController.transform.position;
        // UI_ItemStatView 에서 넣을 위치 찾음
        Transform itemImage = Util.FindChild("ItemImage", _itemStatView.transform);
        Image FindItemImage = itemImage.GetComponent<Image>();

        // 넣을 이미지를 찾음
        Sprite _sprite = GameManager.Resource.GetImage(invenSlotItem.name);
        FindItemImage.sprite = _sprite;
        // 이왕 찾은 이미지를 스텟뷰에 넣어놓음 (아이템 장착용)
        _itemStatViewContoller._sprite = _sprite;
        _itemStatOpen = true;

        return invenSlotItem;
    }
    
    public void ItemStatViewClose(string name)
    {
        //_itemStatOpen로 창 열려있는지 체크
        if(_itemStatOpen == true)
        {
            _itemStatView.SetActive(false);
            _itemStatOpen = false;
        }
    }

    // 무기 장착 함수 (계산)
    // 추후 방어구도 동일하게 적용
    public void ItemStatViewWeaponEquip()
    {
        // 장착 대상을 찾음
        Transform weaponImage = Util.FindChild("WeaponImage", _inventoryController.transform);
        Image findImage = weaponImage.GetComponent<Image>();

        // 이미지 넣음 (_itemStatviewContlloer에서 이미지를 이미지를 들고있음)
        findImage.sprite = _itemStatViewContoller._sprite;
        // 이미지 활성화
        findImage.gameObject.SetActive(true);

        // 인벤 컨트롤러에서 아이템 이름과 이미지 아이템 이름을 비교해서 동일 이름이면 인벤 컨트롤러 Weapon 게임오브젝트에 넣음 (장착의미)
        // 인벤토리 아이템 중 찾음 이미지와 이름이 같으면 (동일 아이템)

        // 인벤토리에서 웨폰이 널이라면 (기존 무기 착용 x)
        if(_inventoryController._weapon == null)
        {
            // 인벤토리 아이템 숫자만큼 루프
            for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
            {
                // 인벤토리 아이템하고 이미지가 동일하면
                if (findImage.sprite.name == GameManager.Ui._inventoryController._item[i].name)
                {
                    // 무기 장착
                    _inventoryController._weapon = GameManager.Ui._inventoryController._item[i];
                    // 인벤에서 무기 제거 >> 게임오브젝트 제거, 이미지 제거 
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);
                    break;
                }
            }
        }
        // 인벤토리에서 웨폰이 널이 아니라면 (기존 무기 착용을 했다면)
        else if (_inventoryController._weapon != null)
        {
            // 임시 무기 변수
            GameObject tmpWeapon = null;
            // 기존 착용 무기를 임시 변수에 대입
            tmpWeapon = _inventoryController._weapon;
            // 인벤토리 아이템 숫자만큼 루프
            for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
            {
                // 인벤토리 아이템하고 이미지가 동일하면
                if (findImage.sprite.name == GameManager.Ui._inventoryController._item[i].name)
                {
                    // 무기 장착
                    _inventoryController._weapon = GameManager.Ui._inventoryController._item[i];
                    // 인벤에서 무기 제거 >> 게임오브젝트 제거, 이미지 제거 
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);

                    // 임시 저장한 기존 장착 아이템 인벤토리로 넣음
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(tmpWeapon);
                    GameManager.Ui._inventoryController._item.Add(tmpWeapon);
                    break;
                }
            }
        }
        // 인벤토리에 들어있는 게임오브젝트의 이름을 이미지 이름과 비교해서 동일한 이미지를 넣는 함수
        InventoryImageArray();
    }

    // 아이템 버리는 함수
    public void ItemStatViewWeaponDrop()
    {
        // 인벤토리의 아이템을 버리는 경우
        // 인벤토리 아이템 숫자만큼 루프
        for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            // 인벤토리 아이템하고 이미지가 동일하면
            if (_itemStatViewContoller._sprite.name == GameManager.Ui._inventoryController._item[i].name)
            {
                // 인벤에서 무기 제거 >> 게임오브젝트 제거, 이미지 제거 
                GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                GameManager.Ui._inventoryController._item.RemoveAt(i);
                _slotImage[i].sprite = null;
                _slotImage[i].gameObject.SetActive(false);
                break;
            }
        }
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
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(tmpGameObject);
        }
        for(int i = 19; i > GameManager.Ui._inventoryController._item.Count -1; i--)
        {
            // 나머지 슬롯 이미지 전부 삭제 비활성화
            _slotImage[i].sprite = null;
            _slotImage[i].gameObject.SetActive(false);
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
        }
    }




    /// <summary>
    /// 이하 씬 Attack버튼 관련
    /// 추후 코드 수정 필요 
    /// UI 매니저가 너무 비대해질경우 아래 공격버튼 및 스킬 버튼은 스킬매니저를 생성해서 관리 해야 될 수 있음
    /// </summary>
    public void AttackButton()
    {
        List<float> targetDistance = new List<float>();
        float distance = 0;
        targetMonster = null;

        // 몬스터들을 찾는다 >> 추후 몬스터 리스폰 시 오브젝트매니저에서 몬스터를 들고있게 할 예정 그러면 파인드 사용 안해도 됨.
        // 각각의 몬스터들의 거리 비교
        GameObject[] monster = GameObject.FindGameObjectsWithTag("Monster");
        for(int i = 0; i < monster.Length; i++)
        {
            targetDistance.Add(Vector3.Distance(monster[i].transform.position, GameManager.Obj._playerController.transform.position));
            
            if(distance < targetDistance[i])
            {
                distance = targetDistance[i];
                targetMonster = monster[i];
            }
        }

        // 가까운 몬스터를 찾았으면 가까이 이동하거나 공격한다.
        if(targetMonster != null)
        {
            // 가까이 있으면 공격한다.
            if(distance < 2.0f)
            {
                // 플레이어 컨트롤러에서 처리
                GameManager.Obj._playerController._creatureState = CreatureState.Attack;
            }
            // 멀리 있으면 이동한다.
            if(distance >= 2.0f)
            {
                // 플레이어 컨트롤러에서 처리
                GameManager.Obj._playerController._creatureState = CreatureState.AutoMove;
            }
        }


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

    }
}
