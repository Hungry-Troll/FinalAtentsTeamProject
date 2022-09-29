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

    // 인벤 버튼
    public GameObject _invenButton;

    // 옵션 버튼
    public GameObject _OptionButton;

    // 옵션 창(화면)
    public GameObject _Option;

    // 미니맵
    public GameObject _miniMap;

    // 아이템스텟창
    public GameObject _ItemStatView;

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
        InventoryClose();

        // 인벤토리 슬롯을 배열로 가지고옴 인벤토리 컨트롤러에서 하려고 했지만 SetActive(false)라  Start() 함수 사용이 안됨
        _inventoryController._invenSlotArray = _inventoryController.GetComponentsInChildren<InvenSlotController>();
        // 가지고 온 배열을 리스트로 변환
        foreach (InvenSlotController one in _inventoryController._invenSlotArray)
        {
            _inventoryController._invenSlotList.Add(one);
        }

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
        // 테스트용 임시 코드
        GameObject itemStat = GameManager.Resource.GetUi("UI_ItemStatView");
        _ItemStatView = GameObject.Instantiate<GameObject>(itemStat);
        _ItemStatView.SetActive(false);
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

    // 이름으로 각각 아이템 ui를 가지고 올 예정
    // 아니면 ui에 직접 접근해서 재귀함수를 이용 각각 스텟이나 이미지 등등을 변경하는 방법도 존재함

    public GameObject ItemStatViewOpen(GameObject invenSlotItem)
    {
        //_itemStatOpen로 창 열려있는지 체크
        if (_itemStatOpen == false)
        {
            // 아이템 정보 창 열고
            _ItemStatView.SetActive(true);
            _ItemStatView.transform.position = _inventoryController.transform.position;
            // 넣을 대상 찾음
            Transform itemImage = Util.FindChild("ItemImage", _ItemStatView.transform);
            Image FindItemImage = itemImage.GetComponent<Image>();

            // 넣을 이미지를 찾음
            Sprite _sprite = GameManager.Resource.GetImage(invenSlotItem.name);
            FindItemImage.sprite = _sprite;
            _itemStatOpen = true;
            return invenSlotItem;
        }
        return null;
    }

    public void ItemStatViewClose(string name)
    {
        //_itemStatOpen로 창 열려있는지 체크
        if (_itemStatOpen == true)
        {
            _ItemStatView.SetActive(false);
            _itemStatOpen = false;
        }
    }

    public void ItemStatViewWeaponEquip(Sprite sprite, Transform itemStatViewTr)
    {
/*        // 넣을 대상을 찾음
        Transform itemImage = Util.FindChild("ItemImage", itemStatViewTr);

        // 이미지 컴포넌트 찾고
        Image findImage = itemImage.GetComponent<Image>();

        // 이미지 넣음
        findImage.sprite = sprite;

        // 장착한 이미지 인벤에서 제거
        GameManager.Ui._inventoryController._invenSlotList[GameManager.Ui._inventoryController._invenSlotCount - 1]._SlotItem.Clear();
        GameManager.Ui._inventoryController._invenSlotCount--;*/
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
