using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Util;


public class UiManager
{

    // UI Root�� ����
    GameObject go;

    // HpMp��
    public GameObject _hpMpBar;

    // �� ��ư
    public GameObject _sceneButton;

    // ���̽�ƽ
    public JoyStickController _joyStickController;
    public GameObject _joyStick;

    // ����â�� ���̴� �÷��̾�
    public GameObject _statePlayerObj;

    // �κ��丮
    public InventoryController _inventoryController;
    public GameObject _inven;
    // �κ� ���� �̹��� // GetComponent ����� ���̱� ���� �̸� �����ؼ� �����͸� ��� ���� ����
    public List<Image> _slotImage;

    // �κ� ��ư
    public GameObject _invenButton;

    // �ɼ� ��ư
    public GameObject _OptionButton;

    // �ɼ� â(ȭ��)
    public GameObject _Option;

    // �̴ϸ�
    public GameObject _miniMap;

    // ������ ����â
    public GameObject _itemStatView;
    // ������ ����â ��ũ��Ʈ
    public ItemStatViewController _itemStatViewContoller;

    // ������ ����â�� �Ѱ����� ���� ���� ����
    // ���� UI â ���������� �ؾ� �Ǹ� ����
    public bool _itemStatOpen;


    // ���� Ÿ�� ����
    public GameObject targetMonster;

    

    //Ui ������ ���⿡�� ó��
    public void Init()
    {
        go = new GameObject();
        go.name = "@UI_Root";

        ////////////////////////////////
        /// �ݺ��Ǵ� ���� ���߿� �Լ��� ����
        ////////////////////////////////
        ///
        // �����ϸ� HpMp�� ���� �ҷ���
        GameObject hpMpBar = GameManager.Resource.GetUi("Ui_HpMpBar");
        _hpMpBar = GameObject.Instantiate<GameObject>(hpMpBar);
        _hpMpBar.transform.SetParent(go.transform);

        // �����ϸ� �� ��ư(���� ��ų ��ư) ���� �ҷ���
        GameObject sceneButton = GameManager.Resource.GetUi("Ui_Scene_Button");
        _sceneButton = GameObject.Instantiate<GameObject>(sceneButton);
        _sceneButton.transform.SetParent(go.transform);

        // �����ϸ� ���̽�ƽ ���� �ҷ���
        GameObject joystick = GameManager.Resource.GetUi("Ui_JoystickController");
        _joyStick = GameObject.Instantiate<GameObject>(joystick);
        _joyStickController = _joyStick.GetComponentInChildren<JoyStickController>();
        _joyStick.transform.SetParent(go.transform);

        // �����ϸ� ����â�� ���̴� �÷��̾� �ҷ���, ����â�� �κ��丮�� ��Ʈ
        GameObject statePlayerObj = GameManager.Resource.GetCharacter("tempPlayer");
        _statePlayerObj = GameObject.Instantiate<GameObject>(statePlayerObj, new Vector3(0,200,0), Quaternion.identity);

        // �����ϸ� �κ��丮��ư(���������) ���� �ҷ���
        GameObject invenButton = GameManager.Resource.GetUi("Ui_SceneInventoryButton");
        _invenButton = GameObject.Instantiate<GameObject>(invenButton);
        _invenButton.transform.SetParent(go.transform);

        // �����ϸ� �κ��丮�� �̸� �ҷ��ͼ� �켱 SetActive(false)�� ��
        GameObject inven = GameManager.Resource.GetUi("Ui_Inventory");
        _inven = GameObject.Instantiate<GameObject>(inven);
        _inventoryController = _inven.GetComponentInChildren<InventoryController>();
        _inven.transform.SetParent(go.transform);

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
        GameObject miniMap = GameManager.Resource.GetUi("UI_MiniMap");
        _miniMap = GameObject.Instantiate<GameObject>(miniMap);
        _miniMap.transform.SetParent(go.transform);

        // �����ϸ� �ɼǹ�ư �ҷ���
        GameObject optionButton = GameManager.Resource.GetUi("Ui_SceneOptionButton");
        _OptionButton = GameObject.Instantiate<GameObject>(optionButton);
        _OptionButton.transform.SetParent(go.transform);

        // �����ϸ� �ɼ�â �ҷ��� // ���忬���� ���ؼ� �ɼ�â�� ���� �Ŵ������� ���� �����. ������ ���ӿ�����Ʈ��������
        // �ɼ�â �����̵尡 ���� �۵� �ǹǷ� ���� �Ŵ������� �ɼ�â�� ������ ��
        GameObject Option = GameManager.Sound._option;
        _Option = GameObject.Instantiate<GameObject>(Option);
        _Option.SetActive(false);

        // �����ϸ� ������ ����â�� �Ѱ����� ���� ���� ������ �ʱ�ȭ // 1���� �� �� ����
        _itemStatOpen = false;
        // �����ϸ� ������ ����â �ҷ� �켱 SetActive(false)�� ��
        GameObject itemStatView = GameManager.Resource.GetUi("UI_ItemStatView");
        _itemStatView = GameObject.Instantiate<GameObject>(itemStatView);
        _itemStatView.SetActive(false);
        // ������ ����â ��ũ��Ʈ�� �̸� �������
        _itemStatViewContoller = _itemStatView.GetComponentInChildren<ItemStatViewController>();
    }
    /// <summary>
    /// �κ��丮 ����
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

    /// <summary>
    /// ������ ����â ����
    /// </summary>

    // ������ ����â UI�� �Ѱ��� ���� �̹����� ������ �ҷ����� ������� ������
    // ���� ���� �� ����� ������ ����
    public GameObject ItemStatViewOpen(GameObject invenSlotItem)
    {
        //_itemStatOpen�� â �����ִ��� üũ
        if(_itemStatOpen == false)
        {
            // ������ ���� â ����
            _itemStatView.SetActive(true);
        }

        _itemStatView.transform.position = _inventoryController.transform.position;
        // UI_ItemStatView ���� ���� ��ġ ã��
        Transform itemImage = Util.FindChild("ItemImage", _itemStatView.transform);
        Image FindItemImage = itemImage.GetComponent<Image>();

        // ���� �̹����� ã��
        Sprite _sprite = GameManager.Resource.GetImage(invenSlotItem.name);
        FindItemImage.sprite = _sprite;
        // �̿� ã�� �̹����� ���ݺ信 �־���� (������ ������)
        _itemStatViewContoller._sprite = _sprite;
        _itemStatOpen = true;

        return invenSlotItem;
    }
    
    public void ItemStatViewClose(string name)
    {
        //_itemStatOpen�� â �����ִ��� üũ
        if(_itemStatOpen == true)
        {
            _itemStatView.SetActive(false);
            _itemStatOpen = false;
        }
    }

    // ���� ���� �Լ� (���)
    // ���� ���� �����ϰ� ����
    public void ItemStatViewWeaponEquip()
    {
        // ���� ����� ã��
        Transform weaponImage = Util.FindChild("WeaponImage", _inventoryController.transform);
        Image findImage = weaponImage.GetComponent<Image>();

        // �̹��� ���� (_itemStatviewContlloer���� �̹����� �̹����� �������)
        findImage.sprite = _itemStatViewContoller._sprite;
        // �̹��� Ȱ��ȭ
        findImage.gameObject.SetActive(true);

        // �κ� ��Ʈ�ѷ����� ������ �̸��� �̹��� ������ �̸��� ���ؼ� ���� �̸��̸� �κ� ��Ʈ�ѷ� Weapon ���ӿ�����Ʈ�� ���� (�����ǹ�)
        // �κ��丮 ������ �� ã�� �̹����� �̸��� ������ (���� ������)

        // �κ��丮���� ������ ���̶�� (���� ���� ���� x)
        if(_inventoryController._weapon == null)
        {
            // �κ��丮 ������ ���ڸ�ŭ ����
            for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
            {
                // �κ��丮 �������ϰ� �̹����� �����ϸ�
                if (findImage.sprite.name == GameManager.Ui._inventoryController._item[i].name)
                {
                    // ���� ����
                    _inventoryController._weapon = GameManager.Ui._inventoryController._item[i];
                    // �κ����� ���� ���� >> ���ӿ�����Ʈ ����, �̹��� ���� 
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);
                    break;
                }
            }
        }
        // �κ��丮���� ������ ���� �ƴ϶�� (���� ���� ������ �ߴٸ�)
        else if (_inventoryController._weapon != null)
        {
            // �ӽ� ���� ����
            GameObject tmpWeapon = null;
            // ���� ���� ���⸦ �ӽ� ������ ����
            tmpWeapon = _inventoryController._weapon;
            // �κ��丮 ������ ���ڸ�ŭ ����
            for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
            {
                // �κ��丮 �������ϰ� �̹����� �����ϸ�
                if (findImage.sprite.name == GameManager.Ui._inventoryController._item[i].name)
                {
                    // ���� ����
                    _inventoryController._weapon = GameManager.Ui._inventoryController._item[i];
                    // �κ����� ���� ���� >> ���ӿ�����Ʈ ����, �̹��� ���� 
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                    GameManager.Ui._inventoryController._item.RemoveAt(i);
                    _slotImage[i].sprite = null;
                    _slotImage[i].gameObject.SetActive(false);

                    // �ӽ� ������ ���� ���� ������ �κ��丮�� ����
                    GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(tmpWeapon);
                    GameManager.Ui._inventoryController._item.Add(tmpWeapon);
                    break;
                }
            }
        }
        // �κ��丮�� ����ִ� ���ӿ�����Ʈ�� �̸��� �̹��� �̸��� ���ؼ� ������ �̹����� �ִ� �Լ�
        InventoryImageArray();
    }

    // ������ ������ �Լ�
    public void ItemStatViewWeaponDrop()
    {
        // �κ��丮�� �������� ������ ���
        // �κ��丮 ������ ���ڸ�ŭ ����
        for (int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            // �κ��丮 �������ϰ� �̹����� �����ϸ�
            if (_itemStatViewContoller._sprite.name == GameManager.Ui._inventoryController._item[i].name)
            {
                // �κ����� ���� ���� >> ���ӿ�����Ʈ ����, �̹��� ���� 
                GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
                GameManager.Ui._inventoryController._item.RemoveAt(i);
                _slotImage[i].sprite = null;
                _slotImage[i].gameObject.SetActive(false);
                break;
            }
        }
        // �κ��丮�� ����ִ� ���ӿ�����Ʈ�� �̸��� �̹��� �̸��� ���ؼ� ������ �̹����� �ִ� �Լ�
        InventoryImageArray();
    }

    // �κ��丮 �̹��� ���� �Լ� (������)
    // ������ ���� �� ���� �� ���
    void InventoryImageArray()
    {
        for(int i = 0; i < GameManager.Ui._inventoryController._item.Count; i++)
        {
            // �κ��丮 ������ �̸�
            string tmpName = GameManager.Ui._inventoryController._item[i].name;
            // �̸��� �̿��� �̹��� ã��
            Sprite tmpSprite = GameManager.Resource.GetImage(tmpName);
            // ã�� �̹����� �� ���� �̹����� ����
            _slotImage[i].sprite = tmpSprite;
            // Ȱ��ȭ
            _slotImage[i].gameObject.SetActive(true);
            // ������ �̸��� ���ӿ�����Ʈ�� ������ ���Կ� ����
            GameObject tmpGameObject = GameManager.Resource.GetfieldItem(tmpName);
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Add(tmpGameObject);
        }
        for(int i = 19; i > GameManager.Ui._inventoryController._item.Count -1; i--)
        {
            // ������ ���� �̹��� ���� ���� ��Ȱ��ȭ
            _slotImage[i].sprite = null;
            _slotImage[i].gameObject.SetActive(false);
            GameManager.Ui._inventoryController._invenSlotList[i]._SlotItem.Clear();
        }
    }




    /// <summary>
    /// ���� �� Attack��ư ����
    /// ���� �ڵ� ���� �ʿ� 
    /// UI �Ŵ����� �ʹ� ���������� �Ʒ� ���ݹ�ư �� ��ų ��ư�� ��ų�Ŵ����� �����ؼ� ���� �ؾ� �� �� ����
    /// </summary>
    public void AttackButton()
    {
        List<float> targetDistance = new List<float>();
        float distance = 0;
        targetMonster = null;

        // ���͵��� ã�´� >> ���� ���� ������ �� ������Ʈ�Ŵ������� ���͸� ����ְ� �� ���� �׷��� ���ε� ��� ���ص� ��.
        // ������ ���͵��� �Ÿ� ��
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

        // ����� ���͸� ã������ ������ �̵��ϰų� �����Ѵ�.
        if(targetMonster != null)
        {
            // ������ ������ �����Ѵ�.
            if(distance < 2.0f)
            {
                // �÷��̾� ��Ʈ�ѷ����� ó��
                GameManager.Obj._playerController._creatureState = CreatureState.Attack;
            }
            // �ָ� ������ �̵��Ѵ�.
            if(distance >= 2.0f)
            {
                // �÷��̾� ��Ʈ�ѷ����� ó��
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
