using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Define;
using static Util;
using DG.Tweening;

public class UiManager
{
    public Define.ItemType _itemType;

    // UI Root�� ����
    public GameObject go;

    // �÷��̾� Hp��
    public GameObject _playerHpBar;
    public PlayerHpBarEX _playerHpBarController;

    // ��Ʈ����Ʈ
    public GameObject _portrait;
    public Image _portraitImage;
    public PortraitController _portraitController;

    // ��� ǥ�� ��
    public GameObject _goldDisplay;
    // ��� ǥ�� �� ����
    public GameObject _goldDisplayShop;

    // ��� �˾�
    public GameObject _locationPopUp;
    public Text _locationPopUpText;

    // ���� �ȳ� ȭ��ǥ
    public GameObject _directionArrow;
    public DirectionArrowControlloer _directionArrowController;

    // ��ų����â
    public SkillViewController _skillViewController;
    
    //��ų ��ư
    public Ui_SceneAttackButton _uiSceneAttackButton;

    // �� ��ư
    public GameObject _sceneButton;

    // ���̽�ƽ
    public JoyStickController _joyStickController;
    public GameObject _joyStick;

    // ����â�� ���̴� �÷��̾�
    public GameObject _statePlayerObj;
    // ����â �÷��̾� �̸� Ȯ�ο�
    public Define.Job _job;
    public string _jobName;

    // �κ��丮
    public InventoryController _inventoryController;
    public GameObject _inven;
    // �κ��丮 ����â�� Text // GetComponent ����� ���̱� ���� �̸� �����ؼ� �����͸� ��� ���� ����
    Text atkText;
    Text defText;
    // �κ� ���� �̹��� // GetComponent ����� ���̱� ���� �̸� �����ؼ� �����͸� ��� ���� ����
    public List<Image> _slotImage;
    // �κ� ���� ���� �̹���
    public Image _weaponImage;
    public Image _armourImage;

    // �κ� ��ư (���������)
    public GameObject _invenButton;

    // �ɼ� ��ư
    public GameObject _OptionButton;

    // ����Ʈ ��ư
    public GameObject _questButton;

    // �ɼ� â(ȭ��)
    public GameObject _Option;

    // �̴ϸ�
    public GameObject _miniMap;

    // ������ ����â
    public GameObject _itemStatView;
    // ������ ����â ��ũ��Ʈ
    public ItemStatViewController _itemStatViewController;
    // ������ ����â �̹���
    public Image _findItemImage;
    // ������ ����â �ؽ�Ʈ >> ������â ������ �� ���� ������ ���� �뵵 // GetComponent ����� ���̱� ���� �̸� �����ؼ� �����͸� ��� ���� ����
    public Text _itemNameText;
    public Text _itemStatText;
    public Text _itemStat;
    public Text _itemIntroduce;
    public Text _equipText;
    public Text _dropText;

    // ������ ����â�� �Ѱ����� ���� ���� ����
    // ���� UI â ���������� �ؾ� �Ǹ� ����
    public bool _itemStatOpen;

    // ���� ������ ����â
    public GameObject _equipStatView;
    // ���� ������ ��ũ��Ʈ
    public EquipStatViewController _equipStatViewController;

    // ���� ������ ����â �̹���
    public Image _equipFindItemImage;
    // ���� ������ ����â �ؽ�Ʈ >> ������â ������ �� ���� ������ ���� �뵵 // GetComponent ����� ���̱� ���� �̸� �����ؼ� �����͸� ��� ���� ����
    public Text _equipItemNameText;
    public Text _equipItemStatText;
    public Text _equipItemStat;
    public Text _equipItemIntroduce;
    public Text _equipEquipText;
    public Text _equipDropText;
    // ���� ������ ����â ���� ������
    public bool _equipStatOpen;

    // ���� �Ұ� â
    public GameObject _cannotEquipView;

    // ���� �����ϱ� ����ϱ� ��ư
    public GameObject _buyCancel;
    public UI_BuyCancelButton _buyCancelScript;

    // ���� Ÿ�� ����
    public GameObject _targetMonster;
    public MonsterController _targetMonsterController; 
    public MonsterStat _targetMonsterStat;

    // ��ġ ��� ��ũ��
    public GameObject _lockScreen;
    // ��ŵ ��ư
    public Button _skipButton;
    public UI_SkipButton _skipButtonController;

    // ����Ʈ ����â
    public GameObject _questRewardUI;
    public Ui_QuestReward _uiQuestReward;

    // ��尡 �����մϴ�UI
    public GameObject _dontBuy;

    // ������ UI ��ũ��Ʈ
    public GameObject _uiLevelUpObj;
    public UI_LevelUp _uiLevelUp;

    // ���� HP��
    public GameObject _bossHpbar;

    // ����� ���� �� Ui
    public GameObject _uiOnDamaged;
    //Ui ������ ���⿡�� ó��
    //���� UI �̸� ���� Ui // UI ... ���� ����...
    public void Init()
    {
        go = new GameObject();
        go.name = "@UI_Root";
        ////////////////////////////////
        /// �ݺ��Ǵ� ���� 1�� ���� �Ϸ�
        /// ���� ĵ������ �����Ѵٸ� ���⼭ �� ���� �� ���� ����
        ////////////////////////////////

        // �����ϸ� ��Ʈ����Ʈ �ҷ���
        _portrait = GameManager.Create.CreateUi("UI_Portrait", go);
        _portraitImage = _portrait.GetComponentInChildren<Image>();
        _portraitController = _portrait.AddComponent<PortraitController>();
        // ������ ���� ��Ʈ����Ʈ üũ
        PortraitCheck();

        // ��� �˾� �ҷ���
        _locationPopUp = GameManager.Create.CreateUi("UI_LocationPopUp", go);
        _locationPopUpText = _locationPopUp.GetComponentInChildren<Text>();
        // �������ڸ��� �ִϸ��̼� ����Ǳ� ������ active false
        _locationPopUp.SetActive(false);

        // ���� �ȳ� ȭ��ǥ ����
        _directionArrow = GameManager.Create.CreateUi("UI_WayHelper", go);
        _directionArrowController = _directionArrow.GetComponent<DirectionArrowControlloer>();
        _directionArrowController.Init();

        // ��Ʈ����Ʈ�� ������ ��ųâ UI ����
        GameObject skillView = GameManager.Create.CreateUi("Ui_Skill", go);
        _skillViewController = skillView.GetComponent<SkillViewController>();
        // ��ųâ ���� �� ��Ȱ��ȭ
        skillView.gameObject.SetActive(false);

        // �����ϸ� Ui ��ư �ҷ���
        _sceneButton = GameManager.Create.CreateUi("Ui_Scene_Button", go);
        _uiSceneAttackButton = _sceneButton.GetComponent<Ui_SceneAttackButton>();
        // �����ϸ� ���̽�ƽ ���� �ҷ���
        _joyStick = GameManager.Create.CreateUi("Ui_JoystickController", go);
        _joyStickController = _joyStick.GetComponentInChildren<JoyStickController>();

        // ����â �÷��̾� ����
        GameObject statePlayerObj = GameManager.Resource.GetCharacter(GameManager.Select._jobName + "Temp");
        _statePlayerObj = GameObject.Instantiate<GameObject>(statePlayerObj, new Vector3(0,200,0), Quaternion.identity);
        // �����ϸ� �κ��丮��ư(���������) ���� �ҷ���
        _invenButton = GameManager.Create.CreateUi("Ui_SceneInventoryButton", go);
        // �����ϸ� �κ��丮�� �̸� �ҷ��ͼ� �켱 SetActive(false)�� ��
        _inven = GameManager.Create.CreateUi("Ui_Inventory", go);
        _inventoryController = _inven.GetComponentInChildren<InventoryController>();
        // �κ��丮 ����â ���� ���� ��ġ Ȯ�ο�
        Transform findAtkText = Util.FindChild("AtkText", _inventoryController.transform);
        Transform findDefText = Util.FindChild("DefText", _inventoryController.transform);
        atkText = findAtkText.GetComponent<Text>();
        defText = findDefText.GetComponent<Text>();
        // �κ��丮 ���������� �̹��� ���� ��ġ Ȯ�ο�
        _weaponImage = Util.FindChild("WeaponImage", _inventoryController.gameObject.transform).GetComponent<Image>();
        _armourImage = Util.FindChild("ArmourImage", _inventoryController.gameObject.transform).GetComponent<Image>();

        // ������ ��ü�� �κ��丮 �̹����� �̸� UI �Ŵ������� ������� // GetComponent ���̴� �뵵
        _slotImage = new List<Image>();
        for (int i = 0; i < 20; i++)
        {
            Transform invenImageTr = GameManager.Ui._inventoryController._invenSlotArray[i].transform.GetChild(0);
            // ó�� ���࿡�� GetComponent �� 20�� ����� // ���� ��Ĵ���ϸ� ������ ��ü���� GetComponent�� ����ؾߵ�
            Image slotImage = invenImageTr.gameObject.GetComponent<Image>();
            _slotImage.Add(slotImage);
        }
        InventoryClose();

        // �����ϸ� �̴ϸ� �ҷ���
        _miniMap = GameManager.Create.CreateUi("UI_MiniMap", go);
        // �����ϸ� �ɼǹ�ư �ҷ���
        _OptionButton = GameManager.Create.CreateUi("Ui_SceneOptionButton", go);
        // �����ϸ� �ɼ�â �ҷ��� // ���忬���� ���ؼ� �ɼ�â�� ���� �Ŵ������� ���� �����. ������ ���ӿ�����Ʈ��������
        // �ɼ�â �����̵尡 ���� �۵� �ǹǷ� ���� �Ŵ������� �ɼ�â�� ������ ��
        GameObject Option = GameManager.Sound._option;
        _Option = GameObject.Instantiate<GameObject>(Option);
        _Option.SetActive(false);

        // �����ϸ� ����Ʈ ��ư �ҷ���
        _questButton = GameManager.Create.CreateUi("Ui_SceneQuestButton", go);

        // ������ ����â ����� ������ �ʹ� �� �Լ� ó��
        InitItemStatView();
        // ���� ������ ����â ����� ������ �� �Լ� ó��
        EquipItemStatView();

        // ���� �����ϱ� ����ϱ� ��ư ����� ��Ȱ��ȭ
        _buyCancel = GameManager.Create.CreateUi("UI_BuyCancel", go);
        _buyCancelScript = _buyCancel.AddComponent<UI_BuyCancelButton>();
        _buyCancel.SetActive(false);

        // ��ġ ��� ��ũ�� ���� �� ��Ȱ��ȭ
        _lockScreen = GameManager.Create.CreateUi("UI_LockScreen", go);
        // ��ŵ ��ư ����
        _skipButton = _lockScreen.GetComponentInChildren<Button>();
        _skipButtonController = _skipButton.AddComponent<UI_SkipButton>();
        _skipButton.onClick.AddListener(_skipButtonController.SkipTutorialVideo);
        _lockScreen.SetActive(false);

        // ����Ʈ ������ UI
        _questRewardUI = GameManager.Create.CreateUi("Ui_QuestReward", GameManager.Ui.go);
        _uiQuestReward = _questRewardUI.GetComponent<Ui_QuestReward>();
        _questRewardUI.SetActive(false);

        // ��尡 �����մϴ� UI
        _dontBuy = GameManager.Create.CreateUi("Ui_DontBuy", GameManager.Ui.go);
        _dontBuy.SetActive(false);

        // ��� ǥ�� �� ����
        _goldDisplayShop = GameManager.Create.CreateUi("UI_GoldDisplayBarShop", GameManager.Ui.go);
        _goldDisplayShop.SetActive(false);

        // ������ UI ��ũ��Ʈ
        _uiLevelUpObj = GameManager.Create.CreateUi("Ui_LevelUp", GameManager.Ui.go);
        _uiLevelUp = _uiLevelUpObj.GetComponent<UI_LevelUp>();
        _uiLevelUpObj.gameObject.SetActive(false);
        // ������ UI �ؽ�Ʈ ����
        _uiLevelUp.Init();

        //����� ���� �� ����� Ui
        _uiOnDamaged = GameManager.Create.CreateUi("UI_OnDamaged", GameManager.Ui.go);
        // ������ ����
        OnDamagedUI(false);
    }
    
    // ������ ����â �Լ� Init
    private void InitItemStatView()
    {
        // �����ϸ� ������ ����â�� �Ѱ����� ���� ���� ������ �ʱ�ȭ // 1���� �� �� ����
        _itemStatOpen = false;
        // �����ϸ� ������ ����â �ҷ���
        _itemStatView = GameManager.Create.CreateUi("UI_ItemStatView", go);
        _itemStatViewController = _itemStatView.GetComponentInChildren<ItemStatViewController>();
        // UI_ItemStatView ���� �̹��� ���� ��ġ ã��
        Transform itemImage = Util.FindChild("ItemImage", _itemStatView.transform);
        // ������ �̹���
        _findItemImage = itemImage.GetComponent<Image>();

        // ����â ���뵵 �̸� ����־�ߵ� �ȱ׷��� GetComponent �ټ� ��� �ʿ�
        // �����۸�
        Transform itemNameText = Util.FindChild("ItemNameText", _itemStatView.transform);
        // ���ݷ� ����
        Transform itemStatText = Util.FindChild("ItemStatText", _itemStatView.transform);
        // ���ݷ� ���� ���� ������
        Transform itemStat = Util.FindChild("ItemStat", _itemStatView.transform);
        // ������ ����
        Transform itemIntroduce = Util.FindChild("ItemIntroduce", _itemStatView.transform);
        // �����ϱ� ����ϱ�
        Transform equipText = Util.FindChild("EquipText", _itemStatView.transform);
        // ������
        Transform dropText = Util.FindChild("DropText", _itemStatView.transform);
        // ���� �Ұ� â
        Transform cannotEquipView = Util.FindChild("CannotEquipPanel", _itemStatView.transform);

        _itemNameText = itemNameText.GetComponent<Text>();
        _itemStatText = itemStatText.GetComponent<Text>();
        _itemStat = itemStat.GetComponent<Text>();
        _itemIntroduce = itemIntroduce.GetComponent<Text>();
        _equipText = equipText.GetComponent<Text>();
        _dropText = dropText.GetComponent<Text>();
        _cannotEquipView = cannotEquipView.gameObject;

        //SetActive(false)�� ��
        _cannotEquipView.SetActive(false);
        _itemStatView.SetActive(false);
    }

    /// <summary>
    /// �κ��丮 ����
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
    /// �ɼ�â ����
    /// </summary>
    public void OptionOpen()
    {
        // �ɼ�â �ҷ����� ��ġ�� ũ�� �ʱ�ȭ
        // ĵ�������� �ʿ���� ���ӿ�����Ʈ ��Ȱ��ȭ
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

    // ����Ʈ ��ư ������ ����Ʈ â ����
    public void QuestOpen()
    {
        GameManager.Quest.QuestInfoActive(true);
    }

    /// <summary>
    /// ������ ����â ����
    /// </summary>

    // ������ ����â UI�� �Ѱ��� ���� �̹����� ������ �ҷ����� ������� ������
    // ���� ���� �� ����� ������ ����


    public GameObject ItemStatViewOpen(GameObject SlotItem)
    {
        // ���� Ȱ��ȭ ��Ȱ��ȭ ���ο� ���� ��ư �ؽ�Ʈ�� ������ / �Ǹ��ϱ�� ����
        // Shop�� �巡�׵������ �����ϰ� �װ� �ٽ� ������Ʈ�Ŵ����� �����߱� ������
        // ���Ͻ� �������� ���� �־�� ���װ� �Ȼ��� �׷��� ���Ͻ��� �������� �ʴ� ������ �־����
        if (GameManager.Obj._veniceController.Shop.activeSelf == true)
        {
            _dropText.text = "�Ǹ��ϱ�";
        }
        else
        {
            _dropText.text = "������";
        }
        // �� üũ
        if (SlotItem == null)
        {
            return null;
        }
            
        // ���� �̹����� ã��
        Sprite _sprite = GameManager.Resource.GetImage(SlotItem.name);

        if (_itemStatOpen == false)
        {
            // ������ ���� â ����
            _itemStatView.SetActive(true);

            _itemStatOpen = true;
        }
        // ��ġ��
        _itemStatView.transform.position = _inventoryController.transform.position;
        // �̹��� ����
        _findItemImage.sprite = _sprite;
        // �̿� ã�� �̹����� ���ݺ信 �־���� (������ ������)
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

        // ������ ����â�� �־���
        ItemStatViewStatAdd(SlotItem);

        return SlotItem;
    }
    
    // ������â �ݴ� �Լ�
    public void ItemStatViewClose()
    {
        if (_itemStatOpen == true)
        {
            _cannotEquipView.SetActive(false);
            _itemStatView.SetActive(false);
            _itemStatOpen = false;
        }
    }

    // ������â�� ���� �ִ� �Լ�
    public void ItemStatViewStatAdd(GameObject gameObject)
    {
        // �̸� ã�� Text ������Ʈ�� ������ ���� ����
        for(int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            if(GameManager.Ui._inventoryController._item[i].name == gameObject.name)
            {
                // �� ������Ʈ�� ������ �� ������ ���� ����
                // �켱�� ���� �������κ��丮 ��� ���ӿ�����Ʈ�ϰ� �Ű������� ������� ���ӿ������� �̸� �� �� ������ �־���
                ItemStatEX tmpStat = GameManager.Ui._inventoryController._item[i].GetComponent<ItemStatEX>();
                // ������ �̸�
                _itemNameText.text = tmpStat.Name;

                // ������ ������ ���� �ٸ� �ؽ�Ʈ ��� (���ݷ� ����)
                if(tmpStat.Type == "Weapon")
                {
                    // _itemStatText.text (���ݷ� ����)
                    // _equipText.text (�����ϱ� ����ϱ�)
                    _itemStatText.text = "���ݷ�";
                    _equipText.text = "�����ϱ�";
                }
                else if(tmpStat.Type == "Armour")
                {
                    _itemStatText.text = "����";
                    _equipText.text = "�����ϱ�";
                }
                else if(tmpStat.Type == "Consumables")
                {
                    _itemStatText.text = "ȸ����";
                    _equipText.text = "����ϱ�";
                }
                // ���ݷ� ���� ���� ��ġ
                _itemStat.text = tmpStat.Skill.ToString();
                // ������ ����
                _itemIntroduce.text = tmpStat.Info;
                //_dropText >> ���� ������ ���������� ����
            }
        }
    }

    /// <summary>
    /// ���� ������ �Լ�
    /// </summary>

    // ���� ������ ����â �Լ� Init
    private void EquipItemStatView()
    {

        // �����ϸ� ������ ����â�� �Ѱ����� ���� ���� ������ �ʱ�ȭ // 1���� �� �� ����
        _equipStatOpen = false;
        // �����ϸ� ������ ����â �ҷ���
        _equipStatView = GameManager.Create.CreateUi("UI_EquipStatView", go);
        _equipStatViewController = _equipStatView.GetComponentInChildren<EquipStatViewController>();
        // UI_ItemStatView ���� �̹��� ���� ��ġ ã��
        Transform itemImage = Util.FindChild("ItemImage", _equipStatView.transform);
        // ������ �̹���
        _equipFindItemImage = itemImage.GetComponent<Image>();

        // ����â ���뵵 �̸� ����־�ߵ� �ȱ׷��� GetComponent �ټ� ��� �ʿ�
        // �����۸�
        Transform itemNameText = Util.FindChild("ItemNameText", _equipStatView.transform);
        // ���ݷ� ����
        Transform itemStatText = Util.FindChild("ItemStatText", _equipStatView.transform);
        // ���ݷ� ���� ���� ������
        Transform itemStat = Util.FindChild("ItemStat", _equipStatView.transform);
        // ������ ����
        Transform itemIntroduce = Util.FindChild("ItemIntroduce", _equipStatView.transform);
        // �����ϱ� ����ϱ�
        Transform equipText = Util.FindChild("EquipText", _equipStatView.transform);
        // ������
        Transform dropText = Util.FindChild("DropText", _equipStatView.transform);

        _equipItemNameText = itemNameText.GetComponent<Text>();
        _equipItemStatText = itemStatText.GetComponent<Text>();
        _equipItemStat = itemStat.GetComponent<Text>();
        _equipItemIntroduce = itemIntroduce.GetComponent<Text>();
        _equipEquipText = equipText.GetComponent<Text>();
        _equipDropText = dropText.GetComponent<Text>();

        //SetActive(false)�� ��
        _equipStatView.SetActive(false);
    }

    // ���� ������ ����â ����
    public GameObject EquipStatViewOpen(GameObject SlotItem)
    {
        // �� üũ
        if (SlotItem == null)
        {
            return null;
        }

        // ���� Ȱ��ȭ ��Ȱ��ȭ ���ο� ���� ��ư �ؽ�Ʈ�� ������ / �Ǹ��ϱ�� ����
        // Shop�� �巡�׵������ �����ϰ� �װ� �ٽ� ������Ʈ�Ŵ����� �����߱� ������
        // ���Ͻ� �������� ���� �־�� ���װ� �Ȼ��� �׷��� ���Ͻ��� �������� �ʴ� ������ �־����
        if (GameManager.Obj._veniceController.Shop.activeSelf == true)
        {
            _equipDropText.text = "�Ǹ��ϱ�";
        }
        else
        {
            _equipDropText.text = "������";
        }

        // ���� �̹����� ã��
        Sprite _sprite = GameManager.Resource.GetImage(SlotItem.name);

        // â ���� Ȯ��
        if (_equipStatOpen == false)
        {
            // ������ ���� â ����
            _equipStatView.SetActive(true);

            _equipStatOpen = true;
        }
        // ��ġ��
        _equipStatView.transform.position = _inventoryController.transform.position;
        // �̹��� ����
        _equipFindItemImage.sprite = _sprite;

        // ������ ����â�� �־���
        EquipStatViewStatAdd(SlotItem);
        return SlotItem;
    }

    // ���� ������â�� ���� �ִ� �Լ�
    public void EquipStatViewStatAdd(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }

        // �� ������Ʈ�� ������ �� ������ ���� ����
        // �켱�� ���� �������κ��丮 ��� ���ӿ�����Ʈ�ϰ� �Ű������� ������� ���ӿ������� �̸� �� �� ������ �־���
        ItemStatEX tmpStat = null;
        if (GameManager.Ui._inventoryController._weapon == gameObject)
        {
            tmpStat = GameManager.Ui._inventoryController._weaponStat.GetComponent<ItemStatEX>();
        }
        else if (GameManager.Ui._inventoryController._armour == gameObject)
        {
            tmpStat = GameManager.Ui._inventoryController._armourStat.GetComponent<ItemStatEX>();
        }
        // ������ �̸�
        _equipItemNameText.text = tmpStat.Name;

        // ������ ������ ���� �ٸ� �ؽ�Ʈ ��� (���ݷ� ����)
        if(tmpStat.Type == "Weapon")
        {
            // _itemStatText.text (���ݷ� ����)
            // _equipText.text (�����ϱ� ����ϱ�)
            _equipItemStatText.text = "���ݷ�";
            _equipEquipText.text = "�����ϱ�";
        }
        else if(tmpStat.Type == "Armour")
        {
            _equipItemStatText.text = "����";
            _equipEquipText.text = "�����ϱ�";
        }
        else if(tmpStat.Type == "Consumables")
        {
            _equipItemStatText.text = "ȸ����";
            _equipEquipText.text = "����ϱ�";
        }
        // ���ݷ� ���� ���� ��ġ
        _equipItemStat.text = tmpStat.Skill.ToString();
        // ������ ����
        _equipItemIntroduce.text = tmpStat.Info;
        //_dropText >> ���� ������ ���������� ����
    }
    // ���������� ������ �Լ� + �Ǹ��ϴ� ��� �߰�
    public void EquipItemDropOrSell()
    {
        // �ǸŸ� ���� �ӽ� string
        string spriteName = null;
        // �κ��丮���� ���� ������ ����
        // ���� ������ Ÿ���� ����
        if (GameManager.Ui._equipStatViewController._itemType == ItemType.Weapon)
        {
            // �ǸŸ� ���� �ӽ� ����
            spriteName = _weaponImage.sprite.name;
            // ���� ���
            ItemStatEX tmpStatEx = _inventoryController._weapon.GetComponent<ItemStatEX>();
            GameManager.Obj._playerStat.Atk -= tmpStatEx.Skill;
            // ����â ���� ��������
            GameManager.Weapon.TempUnEquipWeapon(spriteName, GameManager.Obj._playerController.transform);
            // ���� ����
            GameManager.Ui._inventoryController._weapon = null;
            _weaponImage.sprite = null;
            _weaponImage.gameObject.SetActive(false);
        }
        else if (GameManager.Ui._equipStatViewController._itemType == ItemType.Armour)
        {
            // �ǸŸ� ���� �ӽ� ����
            spriteName = _armourImage.sprite.name;
            // ���� ���
            ItemStatEX tmpStatEx = _inventoryController._armour.GetComponent<ItemStatEX>();
            GameManager.Obj._playerStat.Def -= tmpStatEx.Skill;
            // �� ����
            GameManager.Ui._inventoryController._armour = null;
            _armourImage.sprite = null;
            _armourImage.gameObject.SetActive(false);
        }
        // ���� ������â �ݱ�
        EquipStatViewClose();
        // ���� ������ ���������� �������� ������ �ʰ� �Ǹ��Ѵ� (��� ���)
        if (GameManager.Obj._veniceController.Shop.activeSelf == true)
        {
            ItemSell(spriteName);
        }
        // �÷��̾� ��ũ��Ʈ�� �̿��ؼ� �κ��丮�� �ִ� ĳ����â�� ���ݷ� ������ ������
        InventoryStatUpdate();
    }
    // ���������� �����ϴ� �Լ�
    public void UnEquipItem()
    {
        // ���� ������ Ÿ���� ����
        if (GameManager.Ui._equipStatViewController._itemType == ItemType.Weapon)
        {
            // �κ��丮�� �ٽ� �־�ߵ�
            GameManager.Item.InventoryItemAdd(GameManager.Ui._inventoryController._weapon, false);
            // ���� ���
            ItemStatEX tmpStatEx = _inventoryController._weapon.GetComponent<ItemStatEX>();
            GameManager.Obj._playerStat.Atk -= tmpStatEx.Skill;
            // ����â ���� ��������
            GameManager.Weapon.TempUnEquipWeapon(_weaponImage.sprite.name, GameManager.Obj._playerController.transform);
            // ���� ����
            GameManager.Ui._inventoryController._weapon = null;
            _weaponImage.sprite = null;
            _weaponImage.gameObject.SetActive(false);
        }
        else if (GameManager.Ui._equipStatViewController._itemType == ItemType.Armour)
        {
            // �κ��丮�� �ٽ� �־�ߵ�
            GameManager.Item.InventoryItemAdd(GameManager.Ui._inventoryController._armour, false);
            // ���� ���
            ItemStatEX tmpStatEx = _inventoryController._armour.GetComponent<ItemStatEX>();
            GameManager.Obj._playerStat.Def -= tmpStatEx.Skill;
            // �� ����
            GameManager.Ui._inventoryController._armour = null;
            _armourImage.sprite = null;
            _armourImage.gameObject.SetActive(false);
        }
        EquipStatViewClose();

        // �÷��̾� ��ũ��Ʈ�� �̿��ؼ� �κ��丮�� �ִ� ĳ����â�� ���ݷ� ������ ������
        InventoryStatUpdate();
    }
    // ���� ������â �ݴ� �Լ�
    public void EquipStatViewClose()
    {
        if (_equipStatOpen == true)
        {
            // ������ ���� â ����
            _equipStatView.SetActive(false);
            _equipStatOpen = false;
        }
    }

    // ���� �Ұ� �޼��� â �ݴ� �Լ�
    public void CannotEquipViewClose()
    {
        if (_itemStatOpen == true)
        {
            _cannotEquipView.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>

    // ���� ���� �Լ� (���)
    // ���� ���� �����ϰ� ����
    public void ItemStatViewWeaponEquip(ItemType itemType)
    {
        // ���� ��� ����� �ӽ� ����
        Transform findTr = null;
        // ���� ����� ã�� / �������� Define enum ItemType���� �з�
        // json���� ���� Ÿ���� �����Ƿ� json���� ������� ���

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

        // ������ ���� ���� ���� Ȯ��
        if (!JobWeaponCheck())
        {
            // ���� ���⿡ �����Ҽ������ϴ� UI ������ ��
	        _cannotEquipView.SetActive(true);
            return;
        }
        Image findImage = findTr.GetComponent<Image>();

        // �̹��� ���� (_itemStatviewContlloer���� �̹����� �̹����� �������)
        findImage.sprite = _itemStatViewController._sprite;
        // �̹��� Ȱ��ȭ
        findImage.gameObject.SetActive(true);
        // �κ� ��Ʈ�ѷ����� ������ �̸��� �̹��� ������ �̸��� ���ؼ� ���� �̸��̸� �κ� ��Ʈ�ѷ� Weapon ���ӿ�����Ʈ�� ���� (�����ǹ�)
        // �κ��丮 ������ �� ã�� �̹����� �̸��� ������ (���� ������)

        if (itemType == ItemType.Weapon)
        {
            // �ӽ� ���� ����
            GameObject tmpWeapon = null;
            // �ӽ� ī��Ʈ ����
            // ������ �������� ���� ��� ������ ������ �ϸ� ������ ��� �������� ���� ������ �ϰ� ��ü�Ǵ� ���� �ذ��
            int count = 0;
            // �κ��丮���� ������ ���� �ƴ϶�� (���� ���� ������ �ߴٸ�)
            if (_inventoryController._weapon != null)
            {
                // ���� ���� ���⸦ �ӽ� ������ ����
                tmpWeapon = _inventoryController._weapon;
            }
            // �κ��丮 ������ ���ڸ�ŭ ����
            for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
            {
                // �κ��丮 �������ϰ� �̹����� �����ϰ� �ӽ� ī��Ʈ ������ 0 �ϰ��
                // �������� �����ϸ� �ӽ� ī��Ʈ ������ ���� ��Ŵ���� ���̻� ������ �������� ��� ��ü���� �ʴ´�.
                if (findImage.sprite.name == GameManager.Ui._inventoryController._item[i].name && count == 0)
                {
                    // ���� ���� (�κ��丮�� ����ִ� ���� / �κ��丮���� �������)
                    _inventoryController._weapon = GameManager.Ui._inventoryController._item[i];
                    _inventoryController._weaponStat = GameManager.Ui._inventoryController._item[i].GetComponent<ItemStatEX>();
                    // ���� ��ġ ã�� (�÷��̾ ������� ���� ��ġ)
                    //Transform findPos = GameManager.Weapon.FindWeaponPos(GameManager.Obj._playerController.transform);
                    // ã�� ��ġ�� ���� ���� (�÷��̾ ������� ����) ���� ���� ���װ� �־ �Ʒ� Temp�Լ��� ��ü
                    //GameManager.Weapon.EquipWeapon(_inventoryController._weapon.name, findPos);

                    // ���� ����
                    GameManager.Weapon.TempEquipWeapon(_inventoryController._weapon.name, GameManager.Obj._playerController.transform);
                    // ���� ���⿡�� ���� ��ũ��Ʈ ������ ��
                    ItemStatEX tmpStatEx = _inventoryController._weapon.GetComponent<ItemStatEX>();
                    // �÷��̾� ��ũ��Ʈ�� ���� ������
                    GameManager.Obj._playerStat.Atk += tmpStatEx.Skill;
                    // i��° ���ĺ��� �˻��ؼ� ���� ��ĭ ������ ����	
                    SetPotionPullForwardOneSpace(i);
                    // �κ����� ���� ���� >> ���ӿ�����Ʈ ����, �̹��� ���� 
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);

                    // �ӽ� ������ ���Ⱑ ���� �ƴ϶�� (������ ������ ���Ⱑ �ִٸ�)
                    if (tmpWeapon != null)
                    {
                        // �ӽ� ������ ���� ���� ������ �κ��丮�� ����
                        GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(tmpWeapon);
                        GameManager.Ui._inventoryController._item.Add(tmpWeapon);
                        ItemStatEX tmpStat = tmpWeapon.GetComponent<ItemStatEX>();
                        // �ӽ� ������ ���� ���� ������ ���� ���
                        GameManager.Obj._playerStat.Atk -= tmpStat.Skill;
                    }
                    // �÷��̾� ��ũ��Ʈ�� �̿��ؼ� �κ��丮�� �ִ� ĳ����â�� ���ݷ� ������ ������
                    InventoryStatUpdate();
                    // �ӽ� ī��Ʈ ���� ����
                    count++;
                }
            }
        }
        if (itemType == ItemType.Armour)
        {
            // �ӽ� �� ����
            GameObject tmpArmour = null;
            // ������ �������� ���� ��� ������ ������ �ϸ� ������ ��� �������� ���� ������ �ϰ� ��ü�Ǵ� ���� �ذ��
            int count = 0;
            // �κ��丮���� �ƸӰ� ���� �ƴ϶�� (���� ���� ������ �ߴٸ�)
            if (_inventoryController._armour != null)
            {
                // ���� ���� ���� �ӽ� ������ ����
                tmpArmour = _inventoryController._armour;
            }
            // �κ��丮 ������ ���ڸ�ŭ ����
            for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
            {
                // �κ��丮 �������ϰ� �̹����� �����ϰ� �ӽ� ī��Ʈ ������ 0 �ϰ��
                // �������� �����ϸ� �ӽ� ī��Ʈ ������ ���� ��Ŵ���� ���̻� ������ �������� ��� ��ü���� �ʴ´�.
                if (findImage.sprite.name == GameManager.Ui._inventoryController._item[i].name && count == 0)
                {
                    // �� ���� (�κ��丮�� ����ִ� �� / �κ��丮���� �������)
                    _inventoryController._armour = GameManager.Ui._inventoryController._item[i];
                    _inventoryController._armourStat = GameManager.Ui._inventoryController._item[i].GetComponent<ItemStatEX>();

                    // ���� ������ ���� ��ũ��Ʈ ������ ��
                    ItemStatEX tmpStatEx = _inventoryController._armour.GetComponent<ItemStatEX>();
                    // �÷��̾� ��ũ��Ʈ�� ���� ������
                    GameManager.Obj._playerStat.Def += tmpStatEx.Skill;

                    // i��° ���ĺ��� �˻��ؼ� ���� ��ĭ ������ ����
                    SetPotionPullForwardOneSpace(i);

                    // �κ����� �� ���� >> ���ӿ�����Ʈ ����, �̹��� ���� 
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);

                    // �ӽ� ������ ���Ⱑ ���� �ƴ϶�� (������ ������ ���Ⱑ �ִٸ�)
                    if (tmpArmour != null)
                    {
                        // �ӽ� ������ ���� ���� ������ �κ��丮�� ����
                        GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(tmpArmour);
                        GameManager.Ui._inventoryController._item.Add(tmpArmour);
                        ItemStatEX tmpStat = tmpArmour.GetComponent<ItemStatEX>();
                        // �ӽ� ������ ���� ���� ������ ���� ���
                        GameManager.Obj._playerStat.Atk -= tmpStat.Skill;
                    }
                    // �÷��̾� ��ũ��Ʈ�� �̿��ؼ� �κ��丮�� �ִ� ĳ����â�� ���ݷ� ������ ������
                    InventoryStatUpdate();
                    // �ӽ� ī��Ʈ ���� ����
                    count++;
                }
            }
        }

        // ������ ����â �ݱ�
        ItemStatViewClose();
        // �κ��丮�� ����ִ� ���ӿ�����Ʈ�� �̸��� �̹��� �̸��� ���ؼ� ������ �̹����� �ִ� �Լ�
        InventoryImageArray();
    }

    // ������ ������ �Լ� + �Ǹ��ϴ� ��� �߰�
    public void ItemStatViewWeaponDrop()
    {
        // �ǸŸ� ���� �ӽ� string
        string spriteName = null;
        // �κ��丮�� �������� ������ ���	
        // �κ��丮 ������ ���ڸ�ŭ ����	
        for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            if (_itemStatViewController._sprite.name == "potion1")
            {
                //int slotNum = 0;	
                if (GameManager.Ui._inventoryController._item[i].name == "potion1")
                {
                    // �ǸŸ� ���� �ӽ� string ����
                    spriteName = "potion1";

                    int slotNum = 0;
                    slotNum = i;
                    GameManager.Ui._inventoryController._invenSlotList[slotNum].SetOverlapItemCntSub();
                    GameManager.Ui._uiSceneAttackButton.PotionCountMinusOne();
                    // ����� ���� ����
                    int potionCnt = GameManager.Ui._inventoryController._invenSlotList[slotNum]._invenItemCount;
                    // ��ųʸ��� ���� ����
                    GameManager.Ui._inventoryController.SetItemCountDictionary(spriteName, potionCnt);
                    // ���� �پ�� �� ������ 0�����, �ش� �κ��丮ĭ �����
                    if (GameManager.Ui._inventoryController._invenSlotList[slotNum]._invenItemCount == 0)
                    {
                        GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                        GameManager.Ui._inventoryController._item.RemoveAt(i);
                        _slotImage[i].sprite = null;
                        _slotImage[i].gameObject.SetActive(false);
                        break;
                    }
                }

            }
            else
            {
                // �κ��丮 �������ϰ� �̹����� �����ϸ�	
                if (_itemStatViewController._sprite.name == GameManager.Ui._inventoryController._item[i].name)
                {
                    // �ǸŸ� ���� �ӽ� string ����
                    spriteName = _itemStatViewController._sprite.name;

                    // i��° ���ĺ��� �˻��ؼ� ���� ��ĭ ������ ����
                    SetPotionPullForwardOneSpace(i);

                    // �κ����� ���� ���� >> ���ӿ�����Ʈ ����, �̹��� ���� 	
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);

                    // ���� ������Ʈ
                    // �ӽ��ڵ�, �ϴ� ���� 0���� �������� ���Ŀ� ���� �̿� �������� ������ �������� ������ ����
                    GameManager.Ui._inventoryController.SetItemCountDictionary(spriteName, 0);
                    break;
                }
            }
        }
        // ���� ������ ���������� �������� ������ �ʰ� �Ǹ��Ѵ� (��� ���)
        if (GameManager.Obj._veniceController.Shop.activeSelf == true)
        {
            ItemSell(spriteName);
            // ���� ������Ʈ(save ��)
            // �ӽ��ڵ�, �ϴ� ���� 0���� �������� ���Ŀ� ���� �̿� �������� ������ �������� ������ ����
            GameManager.Ui._inventoryController.SetItemCountDictionary(spriteName, 0);
        }
        // ������ ����â �ݱ�	
        ItemStatViewClose();
        // �κ��丮�� ����ִ� ���ӿ�����Ʈ�� �̸��� �̹��� �̸��� ���ؼ� ������ �̹����� �ִ� �Լ�	
        InventoryImageArray();
    }

    // �ܺο��� ���� ���� ó���ϱ�(sprite���⶧���� ���� ��� �ȵ�)
    public void ItemStatViewUsePotion()
    {
        for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            if (GameManager.Ui._inventoryController._item[i].name == "potion1")
            {
                int slotNum = 0;
                slotNum = i;
                GameManager.Ui._inventoryController._invenSlotList[slotNum].SetOverlapItemCntSub(); // ������ ���������, ������ �ϳ� �پ��

                // ����� ���� ����
                int potionCnt = GameManager.Ui._inventoryController._invenSlotList[slotNum]._invenItemCount;
                // ��ųʸ��� ���� ����
                GameManager.Ui._inventoryController.SetItemCountDictionary("potion1", potionCnt);
                
                // ���� �پ�� �� ������ 0�����, �ش� �κ��丮ĭ �����
                if (GameManager.Ui._inventoryController._invenSlotList[slotNum]._invenItemCount == 0)
                {
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);
                    break;
                }
            }
        }

        InventoryImageArray();
    }

    // ������ �������� ��� ������ ��� �Ǹ��ϴ� �Լ� / �Ǹ��� �̹��� �̸����� ó��
    public void ItemSell(string spriteName)
    {
        if (spriteName == null)
        {
            return;
        }

        // ������Ʈ Ǯ������ �κ��丮 �������� ��� ����
        for (int i = 0; i < GameManager.Obj._objPool.Count; i++)
        {
            // ������� �̸� �� ������Ʈ Ǯ �̸��� ������
            // �� �������� ������ ���������Ƿ� �Ǹ��� ��������
            if (GameManager.Obj._objPool[i].name == spriteName)
            {
                // ������ ���ݿ��� ������ �����ؼ� �÷��̾�� �־���
                GameManager.Obj._playerController._goldController.GoldAmount +=
                    GameManager.Obj._objPool[i].GetComponent<ItemStatEX>().Sale_Price;
            }
        }
    }
    // �κ��丮 �̹��� ���� �Լ� (������)	
    // ������ ���� �� ���� �� ���	
    void InventoryImageArray()
    {
        for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            // �κ��丮 �����ϰ� �ִ� ������ �̸�	
            string tmpName = GameManager.Ui._inventoryController._item[i].name;
            // �̸��� �̿��� �̹��� ã��	
            Sprite tmpSprite = GameManager.Resource.GetImage(tmpName);
            // ã�� �̹����� �� ���� �̹����� ����	
            _slotImage[i].sprite = tmpSprite;
            // Ȱ��ȭ	
            _slotImage[i].gameObject.SetActive(true);
            // ������ �̸��� ���ӿ�����Ʈ�� ������ ���Կ� ����	
            GameObject tmpGameObject = GameManager.Resource.GetfieldItem(tmpName);  // �ʵ忡 �ִ� �������� ���� ���ؼ�, ���� �̸��� ������ ������Ʈ�� ��ȯ	
            GameObject go = Util.Instantiate(tmpGameObject);    // �ش� �������� ���� (����Ʈ�� �߰��ϱ� ���� �����Ѱ���)	
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(go);    // ���� ������ �������� ����Ʈ�� �߰���(�������� �����ϱ� ����)	
            //���Թ�ȣ ����	
            //GameManager.Ui._inventoryController._invenSlotList[i].SetSlotNumber();	
        }
        for (int i = 19; i > GameManager.Ui._inventoryController._item.Count - 1; i--)
        {
            // ������ ���� �̹��� ���� ���� ��Ȱ��ȭ	
            _slotImage[i].sprite = null;
            _slotImage[i].gameObject.SetActive(false);
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();

            GameManager.Ui._inventoryController._invenSlotList[i].itemCntText.text = " ";
        }
    }
    /// <summary>
    /// ���� �������� ��ĭ ������ ���� �Լ�
    /// </summary>
    /// <param name="_i">�κ��丮 �����ۿ��� �ش� ��ȣ ���ĺ��� �˻��ϴ� �뵵</param>
    public void SetPotionPullForwardOneSpace(int i)
    {
        // ���⿡�� ������� �������� ����index���� ����ĭ�� �ش� ��ĭ ������ �̵��ؾߵȴ�. (�������� remove�Ǳ����� text���� ������.
        for (int j = i; j < GameManager.Ui._inventoryController._item.Count; j++)
        {
            if (GameManager.Ui._inventoryController._item[j].name == "potion1")
            {
                Debug.Log("������� �������� �ε��� : " + j);
                // ������ ��ĭ �����̱�(������ ������)
                GameManager.Ui._inventoryController._invenSlotList[j - 1]._invenItemCount = GameManager.Ui._inventoryController._invenSlotList[j]._invenItemCount;
                GameManager.Ui._inventoryController._invenSlotList[j]._invenItemCount = 0;

                // �ش� ������ text�� ǥ���ϱ�
                GameManager.Ui._inventoryController._invenSlotList[j - 1].SetItemCntText();
                GameManager.Ui._inventoryController._invenSlotList[j].SetItemCntText();
            }
        }
    }

    // �κ��丮 ����â ���ݷ� / ���� ���� / �켱 �������
    public void InventoryStatUpdate()
    {
        atkText.text = GameManager.Obj._playerStat.Atk.ToString();
        defText.text = GameManager.Obj._playerStat.Def.ToString();
    }

    // ������ ���� ���������� ���� üũ �Լ�
    public bool JobWeaponCheck()
    {
        // ���ݺ信�� ����ִ� �̹����� ���� ������ �������� üũ

        // ������ ���� Ȯ��
        string jobName = GameManager.Select._jobName;
        Define.Job job = GameManager.Select.SelectJobCheck();
        // ����ִ� ������ ��������Ʈ Ȯ��
        string ImageName = _itemStatViewController._sprite.name;
        // �ӽ� ����Ʈ
        List<string> temp = new List<string>();

        // ���� �����ϴ°��� ���� �ڵ� �ƴ�
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
        // ���ؼ� ����ִ� �̹����� ������ �������� ������ true
        foreach (var one in temp)
        {
            if (one.Equals(ImageName))
            {
                return true;
            }
        }
        return false;
    }
    // ���� �� Hp ��
    public void HpBarCreate(GameObject gameObject)
    {
        GameObject hpBar = GameManager.Resource.GetUi("UI_HpBar");
        GameObject _hpBar = GameObject.Instantiate<GameObject>(hpBar);
        Canvas canvas = _hpBar.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main; // �̰͸� �ٸ�
        //��ġ ����ִ� �ڵ�
        _hpBar.transform.SetParent(gameObject.transform);
        _hpBar.transform.position = gameObject.transform.position + Vector3.up * (gameObject.GetComponent<NavMeshAgent>().height);
    }
    // �÷��̾�� HP�� �����ϴ� �Լ�
    public void PlayerHpBarCreate(GameObject player)
    {
        _playerHpBar = GameManager.Create.CreateUi("Ui_PlayerHpBar", player);
        _playerHpBarController = _playerHpBar.GetComponent<PlayerHpBarEX>();
        // ��ƼŬ ������ ���� ī�޶� ��� ���� �ڵ�
        // ĵ������ ������ �ͼ�
        // ĵ���� ��� ��ũ��������Ʈī�޶�� ������
        // ī�޶� ����

        Canvas canvas = _playerHpBar.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameManager.Cam._uiParticleCam;
        // ȭ�鿡 ���̴� ���� ��Ʈ����Ʈ���� �Ʒ��� ������ �ǰ�, ��ƼŬ���� �Ʒ��� ������ �Ǳ� ������ ���ÿ����� -1�� ������
        // ��Ʈ����Ʈ�� ��ƼŬ�� �⺻��
        canvas.sortingOrder = -1;
    }

    // �÷��̾�� ��� ǥ��â �����ϴ� �Լ�
    public void PlayerGoldDisplayCreate(GameObject player)
    {
        // ��� ǥ�� �� �ҷ���
        _goldDisplay = GameManager.Create.CreateUi("UI_GoldDisplayBar", player);
    }

    // �÷��̾� ��Ʈ����Ʈ ����� �Լ�
    public void PortraitCheck()
    {
        string temp = GameManager.Select._jobName;
        Sprite tempImage = GameManager.Resource.GetImage(temp);
        _portraitImage.sprite = tempImage;
    }

    // ��ȭâ ���� �� UI ���� SetActiveFalse
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
        _equipStatView.SetActive(false);
        _equipStatOpen = false;
        _questButton.SetActive(false);
        // ���� hp�ٰ� ������ ������
        if(_bossHpbar != null)
        {
            _bossHpbar.SetActive(false);
        }
        //_questRewardUI.SetActive(false); ����Ʈ ������� ������ �����ϱ� ����
        if (GameManager.Effect._levelUpPar.gameObject.activeSelf == true)
        {
            GameManager.Effect.LevelUpPortraitEffectOff();
        }
        _goldDisplay.SetActive(false);   
    }

    // ��ȭâ ���� �� UI ������ �ٽ� Ŵ
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
        // ��ų ����Ʈ�� 0���� Ŭ ��쿡�� ��Ʈ����Ʈ ����Ʈ ��
        // ���� hp�ٰ� ������ ������
        if (_bossHpbar != null)
        {
            _bossHpbar.SetActive(true);
        }
        _questButton.SetActive(true);
        if (_skillViewController._skillLevel.skillPoint > 0)
        {
            GameManager.Effect.LevelUpPortraitEffectOn();
        }
        _goldDisplay.SetActive(true);
    }
    // �����ϱ� ����ϱ� ��ư �Լ�(UI��ġ ������ Transform ������ ������ �;ߵ�)
    public void BuyCancelButtonOpen(Transform tr)
    {
        // x �� ������ ���� ���� / ���� �ڵ�� �ƴ� 
        int x = 110;
        int y = -70;
        // Ȱ��ȭ
        _buyCancel.SetActive(true);
        // ��ġ ��ư ��ġ ã��
        Transform buy = Util.FindChild("BuyButton", _buyCancel.transform);
        Transform cancel = Util.FindChild("CancelButton", _buyCancel.transform);
        Debug.Log(tr.position);
        // ��ư ������ġ�� Ŭ���� ��ü ��ǥ������ 
        //
        buy.position = tr.position + new Vector3(x, y, 0);
        cancel.position = tr.position + new Vector3(x * 3, y, 0);
    }

    // ���ӿ���
    public void GameOverUI()
    {
        GameManager.Create.CreateUi("UI_GameOver", go);
    }
    // ����Ʈ ����â OnOFF
    public void QuestRewardOnOff(bool value)
    {
        _questRewardUI.SetActive(value);
    }
    // ��尡 �����մϴ� UI
    public void DontBuyOnOff(bool value)
    {
        _dontBuy.SetActive(value);
    }
    // ���� ��� â
    public void goldDisplayShopOnOff(bool value)
    {
        _goldDisplayShop.SetActive(value);
    }
    // ����� ���� �� Ui
    public void OnDamagedUI(bool value)
    {
        _uiOnDamaged.SetActive(value);
    }

    /// <summary>
    /// ���� �� Attack��ư ����
    /// ���� �ڵ� ���� �ʿ� 
    /// UI �Ŵ����� �ʹ� ���������� �Ʒ� ���ݹ�ư �� ��ų ��ư�� ��ų�Ŵ����� �����ؼ� ���� �ؾ� �� �� ����
    /// </summary>
    public void AttackButton()
    {
        // ĳ���Ͱ� 3���� �̹Ƿ� �ڵ� ����
        // ���� �ڵ�� ������Ʈ �Ŵ������� ����
        // ���͸� ã�Ƽ� ������Ʈ �Ŵ����� �־��
        GameManager.Obj.FindMobListTarget();
        // ���� Ÿ�ٸ��Ͱ� ���� �ƴ϶��
        if(GameManager.Obj._targetMonster != null)
        {
            // �÷��̾� ��Ʈ�ѷ����� ó�� >> �켱 ���乫��� �ٰ������� ó��
            GameManager.Obj._playerController._creatureState = CreatureState.AutoMove;
            // ���ݹ�ư ������ �꿡�Ե� ���� Ÿ�� ���͸� �˷���
            GameManager.Obj._petController._target = GameManager.Obj._targetMonster.transform;
        }
    }

    // ��� �˾� �Լ�
    // �Ű����� Ÿ�� ���� ���氡��(Define) 
    public void PopUpLocation(string _locationName)
    {
        _locationPopUpText.text = _locationName;
        _locationPopUp.SetActive(true);
    }

    // ��� �˾� close �Լ�
    // ���� �ʾƵ� �������� ������ �ٽ� �Ӷ� ���
    public IEnumerator ClosePopUpLocation()
    {
        float playTime = 3.0f;
        // �ִϸ��̼� �ð����� ����ٰ�
        yield return new WaitForSeconds(playTime);
        // ������ active false
        _locationPopUp.SetActive(false);
    }

    public void Skill1Button(string imageName)
    {
        SkillCheck(imageName);
    }
    public void Skill2Button(string imageName)
    {
        SkillCheck(imageName);
    }
    public void Skill3Button(string imageName)
    {
        SkillCheck(imageName);
    }

    // ��ų �̹����� Ȯ���ؼ� �� ��ư�� ��ų�� ����
    public void SkillCheck(string imageName)
    {
        switch (imageName)
        {
            case "skill1":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill1;
                break;
            case "skill2":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill2;
                break;
            case "skill3":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill3;
                break;
            case "skill4":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill1;
                break;
            case "skill5":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill2;
                break;
            case "skill6":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill3;
                break;
            case "skill7":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill1;
                break;
            case "skill8":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill2;
                break;
            case "skill9":
                GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Skill3;
                break;
        }
    }

    public void RollingButton()
    {
        GameManager.Obj._playerController._sceneAttackButton = SceneAttackButton.Rolling;
    }

    // ��ġ ��� ��ũ�� On/Off �Լ�
    public void ScreenLock(bool state)
    {
        _lockScreen.SetActive(state);
    }
}
