using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UiManager
{
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

    // �κ���ư
    public GameObject _invenButton;

    // ���� Ÿ�� ����
    public GameObject targetMonster;

    //Ui ������ ���⿡�� ó��
    public void Init()
    {
        GameObject go = new GameObject();
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
        InventoryClose();

        // �κ��丮 ������ �迭�� ������� �κ��丮 ��Ʈ�ѷ����� �Ϸ��� ������ SetActive(false)��  Start() �Լ� ����� �ȵ�
        _inventoryController._invenSlotArray = _inventoryController.GetComponentsInChildren<InvenSlotController>();
        // ������ �� �迭�� ����Ʈ�� ��ȯ
        foreach (InvenSlotController one in _inventoryController._invenSlotArray)
        {
            _inventoryController._invenSlotList.Add(one);
        }

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
    /// ���� �� Attack��ư ����
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
