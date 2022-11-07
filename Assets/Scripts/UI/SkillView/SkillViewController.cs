using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ųâ ���� ������


// �� ��ũ��Ʈ�� �ִ��� public �� �̿��ؼ� ���� ó���ϴ� ������ ó���غ�
// ���������δ� ���� ����̱��Ѵ� �ƹ�ư...

// ��ư ���Ḹ �巡�� ��� ���� �ڵ�� ������
public class SkillViewController : MonoBehaviour
{
    public struct SKILLNUM
    {
        public int skill1;
        public int skill2;
        public int skill3;
        public int skillPoint;
    }

    // ��ų���� ������ 
    public SKILLNUM _skillLevel;
    // â �ݱ� ��ư
    public Button _closeButton;
    // ��ų ������ ��ư
    public Button _skillUpButton1;
    public Button _skillUpButton2;
    public Button _skillUpButton3;
    // ��ų ���� ��ư
    public Button _skillSlotButton1;
    public Button _skillSlotButton2;
    public Button _skillSlotButton3;
    // ��ų ������� ������ ���� �̹���
    public Image _skill1Image;
    public Image _skill2Image;
    public Image _skill3Image;
    // ��ų ���� �ؽ�Ʈ
    public Text _skill1LevelText;
    public Text _skill2LevelText;
    public Text _skill3LevelText;

    // ��ų ����Ʈ �ؽ�Ʈ
    public Text _skillPointText;

    /// <summary>
    /// ��ų ���� ��� UI ���ӿ�����Ʈ
    /// </summary>
    public GameObject _skillTextPanel;
    // ��ų �̸�
    public Text _skillNameText;
    // ��ų ����
    public Text _skillContentText;
    // ��ų ȿ��
    public Text _skillEffectText;

    void Start()
    {
        // ��ų ���� ���� 0���� �ʱ�ȭ
        _skillLevel.skill1 = 0;
        _skillLevel.skill2 = 0;
        _skillLevel.skill3 = 0;
        _skillLevel.skillPoint = 0;

        // ��ų ���� ���ڸ� �ؽ�Ʈ�� ������
        _skill1LevelText.text = _skillLevel.skill1.ToString();
        _skill2LevelText.text = _skillLevel.skill2.ToString();
        _skill3LevelText.text = _skillLevel.skill3.ToString();

        // ���� ��ư �Լ��� ����
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
        // ���� �� ù ��ų ���� �� �ְ� ��ų����Ʈ 1 ��
        _skillLevel.skillPoint++;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // ������ ��ƼŬ(����Ʈ) ����
        GameManager.Effect.LevelUpPortraitEffectOn();
    }

    private void CloseButton()
    {
        // ��Ȱ��ȭ
        gameObject.SetActive(false);
        // ��ų ���� â UI
        _skillTextPanel.SetActive(false);
    }

    // ��ų ������ ��ư Ȱ��ȭ �Լ� >> �������ÿ��� Ȱ��ȭ
    public void ButtonInteractableTrue()
    {
        _skillUpButton1.interactable = true;
        _skillUpButton2.interactable = true;
        _skillUpButton3.interactable = true;
        // ��ų����Ʈ ����
        _skillLevel.skillPoint++;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
    }

    // ��ų ������ ��ư ��Ȱ��ȭ �Լ�
    public void ButtonInteractableFalse()
    {
        // ��ų ����Ʈ�� ������ ��ų ������ ��ư�� ��� Ȱ��ȭ
        if(_skillLevel.skillPoint > 0)
        {
            return;
        }    
        _skillUpButton1.interactable = false;
        _skillUpButton2.interactable = false;
        _skillUpButton3.interactable = false;
        // ������ ��ƼŬ ����
        GameManager.Effect.LevelUpPortraitEffectOff();
    }

    // ��ų ���� �ø��� �Լ�
    private void SkillUpButton1()
    {
        _skillLevel.skill1++;
        _skill1LevelText.text = _skillLevel.skill1.ToString();
        // ��ų ����Ʈ ����
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // ��ų������ ��ư ��Ȱ��ȭ �Լ� 
        ButtonInteractableFalse();
    }

    private void SkillUpButton2()
    {
        _skillLevel.skill2++;
        _skill2LevelText.text = _skillLevel.skill2.ToString();
        // ��ų ����Ʈ ����
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // ��ų������ ��ư ��Ȱ��ȭ �Լ�
        ButtonInteractableFalse();
    }

    private void SkillUpButton3()
    {
        _skillLevel.skill3++;
        _skill3LevelText.text = _skillLevel.skill3.ToString();
        // ��ų ����Ʈ ����
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // ��ų������ ��ư ��Ȱ��ȭ �Լ�
        ButtonInteractableFalse();
    }

    // ��ų ��ư ������� ��ų �̹��� Ȱ��ȭ (�巡�� ��ӿ�) �̹����� ��Ʈ�ѷ��� ���� ���� ���ʿ��� ����
    private void SkillSlotButton1()
    {
        _skill1Image.gameObject.SetActive(true);
        // ��ų ���� â UI
        _skillTextPanel.SetActive(true);

        // ��ų �̸�
        //_skillNameText.text = 
        // ��ų ����
        //_skillContentText.text = 
        // ��ų ȿ��
        //_skillEffectText.text = 
    }

    private void SkillSlotButton2()
    {
        _skill2Image.gameObject.SetActive(true);
        // ��ų ���� â UI
        _skillTextPanel.SetActive(true);

        // ��ų �̸�
        //_skillNameText.text = 
        // ��ų ����
        //_skillContentText.text = 
        // ��ų ȿ��
        //_skillEffectText.text = 
    }

    private void SkillSlotButton3()
    {
        _skill3Image.gameObject.SetActive(true);
        // ��ų ���� â UI
        _skillTextPanel.SetActive(true);

        // ��ų �̸�
        //_skillNameText.text = 
        // ��ų ����
        //_skillContentText.text = 
        // ��ų ȿ��
        //_skillEffectText.text = 
    }
}
