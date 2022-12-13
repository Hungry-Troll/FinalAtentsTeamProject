using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ��ųâ ���� ������


// �� ��ũ��Ʈ�� �ִ��� public �� �̿��ؼ� ���� ó���ϴ� ������ ó���غ�
// ���������δ� ���� ����̱��Ѵ� �ƹ�ư...

// ��ư ���Ḹ �巡�� ��� ���� �ڵ�� ������
public class SkillViewController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEndDragHandler
{
    public struct SKILLNUM
    {
        // ��ų ������ ����
        public int skill1;
        public int skill2;
        public int skill3;
        // ��ų ����Ʈ ����
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

    // ��ų �̹���
    public Image _skill1Image;
    public Image _skill2Image;
    public Image _skill3Image;
    // ��ų ��� �̹���
    public Image _baseImage1;
    public Image _baseImage2;
    public Image _baseImage3;
    
    // ��ų Block �̹��� (���)
    public Image _blockImage1;
    public Image _blockImage2;
    public Image _blockImage3;

    // ��ų ���� �ؽ�Ʈ
    public Text _skill1LevelText;
    public Text _skill2LevelText;
    public Text _skill3LevelText;

    // ��ų ����Ʈ �ؽ�Ʈ
    public Text _skillPointText;

    // ��ų ���� ����
    public SkillStat _skillStat;
    // ��ų ���� ���� ����Ʈ
    //public List<SkillStat> _playerSkillList;
    public List<TempSkillStat> _playerSkillList;


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

    /// <summary>
    /// ��ų �巡�� ��� ���� ����
    /// </summary>
    // ��ų ���� ��ũ��Ʈ
    public SkillViewSlot _skillViewSlot1;
    public SkillViewSlot _skillViewSlot2;
    public SkillViewSlot _skillViewSlot3;
    // ��ų ���� ��ũ��Ʈ ����Ʈ
    public List<SkillViewSlot> _slotList;
    // ��ų ���� �ε��� Ȯ�ο�
    public int _selectedSlot;
    // ��ų ���� �� ������ �̹���
    public Image _selectedIcon;
    // �� ��ų ���� ����Ʈ
    public List<Ui_SceneSkillSlot> _sceneSkillSlot;
    void Start()
    {
        // ��ų ���� ���� 0���� �ʱ�ȭ
        // ���� ���� ��ų���� �ִٸ� �ʱ�ȭ���� �ʱ�
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

        // ��ų ���� ���ڸ� �ؽ�Ʈ�� ������
        _skill1LevelText.text = _skillLevel.skill1.ToString();
        _skill2LevelText.text = _skillLevel.skill2.ToString();
        _skill3LevelText.text = _skillLevel.skill3.ToString();

        // ��ų ����Ʈ ���� �ؽ�Ʈ�� ����
        _skillPointText.text = _skillLevel.skillPoint.ToString();

        // ���� ��ư �Լ��� ����
        _closeButton.onClick.AddListener(CloseButton);
        _skillUpButton1.onClick.AddListener(SkillUpButton1);
        _skillUpButton2.onClick.AddListener(SkillUpButton2);
        _skillUpButton3.onClick.AddListener(SkillUpButton3);
        // ���ڰ� �Լ��� ��ư ���� �� ���ٽ����� ���� �� �� ����
        _skillSlotButton1.onClick.AddListener(() => SkillTextPanelOpen(0));
        _skillSlotButton2.onClick.AddListener(() => SkillTextPanelOpen(1));
        _skillSlotButton3.onClick.AddListener(() => SkillTextPanelOpen(2));

        /// <summary>
        /// ���� ��ų �巡�� ��� ���� �ʱ�ȭ
        /// </summary>
         
        // ��ų ���� ��ũ��Ʈ ����Ʈ�� ����
        _slotList.Add(_skillViewSlot1);
        _slotList.Add(_skillViewSlot2);
        _slotList.Add(_skillViewSlot3);
        // ��ų ���� �ε��� Ȯ�ο� �ʱ�ȭ �ʱⰪ�� -1;
        _selectedSlot = -1;
        // ������ �̹��� �����̴� �뵵
        _selectedIcon.gameObject.SetActive(false);
        // �� ��ų���� ã�ƿ�
        // �� ��ų������ ���� �����Ǳ� ������ ���� ���� ã�ƿ� ������ ����
        Ui_SceneAttackButton tmp = GameManager.Ui._sceneButton.GetComponent<Ui_SceneAttackButton>();
        _sceneSkillSlot = tmp._skillSlots;
        // ������ ���� ��ų�̹��� ���� �Լ�
        SkillImageCheck();
        // ��ų ���� ��ũ��Ʈ ����(�� �����)
        _skillStat = GetComponent<SkillStat>();
        
        // ����Ʈ �ʱ�ȭ
        // SkillStat�� MonoBehaviour�� ��ӹޱ� ������ new�� ������� ����
        // -> GetCompnent ���� �ϳ��� ��ũ��Ʈ�� �����ϰ� �Ǿ TempŸ������ ����
        // --> �̹� �����ϴ� �ν��Ͻ��� �ּҰ��� �������ִ� ����� �� ����.
        _playerSkillList = new List<TempSkillStat>();
    }

    // �������� ���
    public void LevelUp()
    {
        // ������ ��ƼŬ(����Ʈ) ����
        GameManager.Effect.LevelUpPortraitEffectOn();
        // ��ų �� ��ư Ȱ��ȭ + ��ų����Ʈ �߰�
        ButtonInteractableTrue();
        // ������ 1�� �ƴҰ�� ������ �� ���� ������ �;� ��
    }

    private void CloseButton()
    {
        // ��Ȱ��ȭ
        gameObject.SetActive(false);
        // ��ų ���� â UI
        _skillTextPanel.SetActive(false);
    }

    // ��ų ������ ��ư Ȱ��ȭ �Լ� >> �������ÿ��� Ȱ��ȭ
    // �����ε�(1 / 2)
    public void ButtonInteractableTrue()
    {
        _skillUpButton1.interactable = true;
        _skillUpButton2.interactable = true;

        // ��ų���� ������ 3�̻��϶��� 3�� ���� ��ų ����
        if(_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }
        // ��ų������ �����Ұ�� ��ų ����x
        _skillUpButton3.interactable = false;

        // ��ų����Ʈ ����
        _skillLevel.skillPoint++;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // ����� ��ų����Ʈ ����
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
    }

    // ��ų ������ ��ư Ȱ��ȭ �Լ�(��ų ����Ʈ ���� ����) / �����ε��� ���� �Ű������� ������ ���� ����
    // �����ε�(2 / 2)
    public void ButtonInteractableTrue(bool isLevelUp)
    {
        // ��ų ����Ʈ ���ٸ� �Ʒ� �������� �ʰ� ����
        if(_skillLevel.skillPoint <= 0)
        {
            return;
        }
        _skillUpButton1.interactable = true;
        _skillUpButton2.interactable = true;

        // ��ų���� ������ 3�̻��϶��� 3�� ���� ��ų ����
        if (_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }
        // ��ų������ �����Ұ�� ��ų ����x
        _skillUpButton3.interactable = false;

        // ��ų����Ʈ �������� ����
        //_skillLevel.skillPoint++;
        _skillPointText.text = _skillLevel.skillPoint.ToString();
        // ����� ��ų����Ʈ ����
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
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


        // ��ų���� ������ 3�̻��϶��� 3�� ���� ��ų ����
        if (_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }

        // ��ų������ ��ư ��Ȱ��ȭ �Լ� 
        ButtonInteractableFalse();
        // ��ų������ ���� ��ų �̹��� Ȱ��ȭ
        SkillLevelCheck();
        // ��ų ������Ʈ
        SkillTextPanelOpen(0, false);
        // ��ų ���� ������Ʈ
        _skillStat.SkillLevel = _skillLevel.skill1;
        // ��ų ������Ʈ�ؼ� ����Ʈ�� ���
        GameManager.Skill.UpdatePlayerSkillList(_playerSkillList, _skillStat);
        // ����� ��ų����Ʈ ����
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
    }

    private void SkillUpButton2()
    {
        _skillLevel.skill2++;
        _skill2LevelText.text = _skillLevel.skill2.ToString();
        // ��ų ����Ʈ ����
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();

        // ��ų���� ������ 3�̻��϶��� 3�� ���� ��ų ����
        if (_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }

        // ��ų������ ��ư ��Ȱ��ȭ �Լ�
        ButtonInteractableFalse();
        // ��ų������ ���� ��ų �̹��� Ȱ��ȭ
        SkillLevelCheck();
        // ��ų ������Ʈ
        SkillTextPanelOpen(1, false);
        // ��ų ���� ������Ʈ
        _skillStat.SkillLevel = _skillLevel.skill2;
        // ��ų ������Ʈ�ؼ� ����Ʈ�� ���
        GameManager.Skill.UpdatePlayerSkillList(_playerSkillList, _skillStat);
        // ����� ��ų����Ʈ ����
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
    }

    private void SkillUpButton3()
    {
        _skillLevel.skill3++;
        _skill3LevelText.text = _skillLevel.skill3.ToString();
        // ��ų ����Ʈ ����
        _skillLevel.skillPoint--;
        _skillPointText.text = _skillLevel.skillPoint.ToString();

        // ��ų���� ������ 3�̻��϶��� 3�� ���� ��ų ����
        if (_skillLevel.skill1 + _skillLevel.skill2 + _skillLevel.skill3 >= 2)
        {
            _skillUpButton3.interactable = true;
        }

        // ��ų������ ��ư ��Ȱ��ȭ �Լ�
        ButtonInteractableFalse();
        // ��ų������ ���� ��ų �̹��� Ȱ��ȭ
        SkillLevelCheck();
        // ��ų ������Ʈ
        SkillTextPanelOpen(2, false);
        // ��ų ���� ������Ʈ
        _skillStat.SkillLevel = _skillLevel.skill3;
        // ��ų ������Ʈ�ؼ� ����Ʈ�� ���
        GameManager.Skill.UpdatePlayerSkillList(_playerSkillList, _skillStat);
        // ����� ��ų����Ʈ ����
        GameManager.Select._skillPoint = _skillLevel.skillPoint;
    }

    // ��ų �ؽ�Ʈ �ε�
    private void SkillTextPanelOpen(int skillNumber)
    {
        // ��ų ���� â UI
        _skillTextPanel.SetActive(true);

        // ������ ��ų �ؽ�Ʈ �ε�
        switch (GameManager.Select._job)
        {
            case Define.Job.Superhuman:
                {
                    // �� �Լ��� ����Ǹ� _skillStat�� ���� ���� ��
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

        // ��ų �̸�
        _skillNameText.text = _skillStat.SkillName;
        // ��ų ����
        _skillContentText.text = _skillStat.SkillContent;
        // ��ų ȿ��
        _skillEffectText.text = _skillStat.SkillEffect;
        // ��ų ���� ������Ʈ �ʿ�
        // Ŭ���� ������ _skillStat�� �⺻ ������ �ε�Ǳ� ����
        switch(skillNumber + 1)
        {
            // ��ų�� ���� 1
            case 1:
                _skillStat.SkillLevel = _skillLevel.skill1;
                break;
            // ��ų�� ���� 2
            case 2:
                _skillStat.SkillLevel = _skillLevel.skill2;
                break;
            // ��ų�� ���� 3
            case 3:
                _skillStat.SkillLevel = _skillLevel.skill3;
                break;
        }
    }
    // ��ų �ؽ�Ʈ �ε� : ���� â UI ���� ���� �����ϴ� �Ű����� �߰�
    // �����ε� 2 / 2
    private void SkillTextPanelOpen(int skillNumber, bool openPanel)
    {
        // ��ų ���� â UI
        _skillTextPanel.SetActive(openPanel);

        // ������ ��ų �ؽ�Ʈ �ε�
        switch (GameManager.Select._job)
        {
            case Define.Job.Superhuman:
                {
                    // �� �Լ��� ����Ǹ� _skillStat�� ���� ���� ��
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

        // ��ų �̸�
        _skillNameText.text = _skillStat.SkillName;
        // ��ų ����
        _skillContentText.text = _skillStat.SkillContent;
        // ��ų ȿ��
        _skillEffectText.text = _skillStat.SkillEffect;
        // ��ų ���� ������Ʈ �ʿ�
        // Ŭ���� ������ _skillStat�� �⺻ ������ �ε�Ǳ� ����
        switch (skillNumber + 1)
        {
            // ��ų�� ���� 1
            case 1:
                _skillStat.SkillLevel = _skillLevel.skill1;
                break;
            // ��ų�� ���� 2
            case 2:
                _skillStat.SkillLevel = _skillLevel.skill2;
                break;
            // ��ų�� ���� 3
            case 3:
                _skillStat.SkillLevel = _skillLevel.skill3;
                break;
        }
    }
    // ������ ��ų �̹��� ������ ���� �Լ�
    private void SkillImageCheck()
    {
        // �ӽ� ���� ���� �� �ʱ�ȭ / �ӽ� �̹��� �����
        Sprite tmpImage1 = null;
        Sprite tmpImage2 = null;
        Sprite tmpImage3 = null;

        // ������ �̹��� �ε�
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
        // �ӽ� ������ �̹��� ����(��ų�̹���)
        _skill1Image.sprite = tmpImage1;
        _skill2Image.sprite = tmpImage2;
        _skill3Image.sprite = tmpImage3;
        // ��ų ��� �̹���
        _baseImage1.sprite = tmpImage1;
        _baseImage2.sprite = tmpImage2;
        _baseImage3.sprite = tmpImage3;
    }

    // ��ų������ 1 �̻��̸� ��ų �̹��� Ȱ��ȭ (���ϰ�)
    // raycastTarget �� �̿��ؼ� �巡�� ��� �ȵǰ�
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
    /// ���� ��ų �巡�� ��� ���� �Լ�
    /// </summary>

    //  ���� ��
    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            if(_slotList[i].IsInRect(eventData.position))
            {
                // ������ ���� Ȯ�ο�
                _selectedSlot = i;
                // �̹��� ã�Ƽ� ��ü (���� ���� �̹����� �����ؾߵ�/ �� �̹����� �� ��� ������ �Ǳ� ����)
                _selectedIcon.sprite = GameManager.Resource.GetImage(_slotList[i]._uiImage.sprite.name);
                // ��ġ �ű�
                _selectedIcon.rectTransform.position = eventData.position;
                _selectedIcon.gameObject.SetActive(true);
                // ������ ���� ã��
                Debug.Log("������ ���� = " + _slotList[i].gameObject.name);
                // ��ų ����â ����
                SkillTextPanelOpen(i);
            }
        }
    }
    // �� �� (�巡�� ��Ȳ�� �ƴϰ� ���ڸ����� �����ٰ� �� ��)
    public void OnPointerUp(PointerEventData eventData)
    {
        // ����ִ� ������ ��쿡�� �׳� ����
        if (_selectedSlot == -1)
            return;

        int tmpSelectedIcon = -1;
        // ���° ������ �������� Ȯ���ϴ� �ڵ�
        for (int i = 0; i < _slotList.Count; i++)
        {
            if (_slotList[i].IsInRect(eventData.position))
            {
                tmpSelectedIcon = i;
                break;
            }
        }
        // ���� ���Կ��� �� ��
        if (tmpSelectedIcon != -1 && tmpSelectedIcon == _selectedSlot)
        {
            // �巡�׿� �ӽ� �̹��� �ʱ�ȭ
            _selectedIcon.sprite = null;
            _selectedIcon.gameObject.SetActive(false);
            _selectedSlot = -1;
        }
    }
    // �巡��
    public void OnDrag(PointerEventData eventData)
    {
        // ������ ������ ������� -1 �� �ƴ�
        if (_selectedSlot != -1)
        {
            _selectedIcon.rectTransform.position = eventData.position;
        }
    }
    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        // �򰥷��� ������ �ٸ��� �ϱ����� �������� ... �� ����Ʈ�� �� ��ư�� �ִ� ��ų ����
        List<Ui_SceneSkillSlot> sceneSkillSlot = _sceneSkillSlot;

        // ����ִ� ������ ��쿡�� �׳� ����
        if (_selectedSlot == -1)
            return;

        int tmpSelectedIcon = -1;
        // ���° �� ��ų��ư ���Կ��� ������
        for (int i = 0; i < sceneSkillSlot.Count; i++)
        {
            if (sceneSkillSlot[i].IsInRect(eventData.position))
            {
                tmpSelectedIcon = i;
                break;
            }
        }

        // �̻��� ��ġ���� �� ��
        if (tmpSelectedIcon == -1 && _selectedSlot != -1)
        {
            // �巡�׿� �ӽ� �̹��� �ʱ�ȭ
            _selectedIcon.sprite = null;
            _selectedIcon.gameObject.SetActive(false);
            _selectedSlot = -1;
            return;
        }

        // �� �� ��ų ��ư ���Կ��� �� �� 
        if (sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite == null)
        {
            // �������°��� �ε� �� �߰�
            sceneSkillSlot[tmpSelectedIcon]._uiImage.gameObject.SetActive(true);
            sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite = GameManager.Resource.GetImage(_selectedIcon.sprite.name);
            // ��ų�� ������ ��ų �̹����� �ߺ��̰� ������ ���� ������
            Color tmpColor;
            tmpColor.a = 0.7f;
            tmpColor.r = 1.0f;
            tmpColor.g = 1.0f;
            tmpColor.b = 1.0f;
            sceneSkillSlot[tmpSelectedIcon]._uiImage.color = tmpColor;
            Debug.Log(sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite.name);
            // �巡�׿� �ӽ� �̹��� ����
            _selectedIcon.sprite = null;
            _selectedIcon.gameObject.SetActive(false);

            // ó�� ������ ���� �ʱ�ȭ
            _selectedSlot = -1;
            // �� ��ư ��ų �ؽ�Ʈ ��Ȱ��ȭ
            sceneSkillSlot[tmpSelectedIcon]._skillText.gameObject.SetActive(false);
            return;
        }

        // �̹����� �ִ� �� ��ų ��ư ���Կ��� �� ��
        if (sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite != null)
        {
            // �������°��� �ε� �� �߰�
            sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite = null;
            sceneSkillSlot[tmpSelectedIcon]._uiImage.gameObject.SetActive(true);
            sceneSkillSlot[tmpSelectedIcon]._uiImage.sprite = GameManager.Resource.GetImage(_selectedIcon.sprite.name);
            // ��ų�� ������ ��ų �̹����� �ߺ��̰� ������ ���� ������
            Color tmpColor;
            tmpColor.a = 0.7f;
            tmpColor.r = 1.0f;
            tmpColor.g = 1.0f;
            tmpColor.b = 1.0f;
            sceneSkillSlot[tmpSelectedIcon]._uiImage.color = tmpColor;
            Debug.Log(_slotList[_selectedSlot]._uiImage.sprite.name);
            
            // �÷��̾� ��ų ��� ������Ʈ
            // �巡���� ��ų ���̵�
            //string skillId = _slotList[_selectedSlot]._uiImage.sprite.name.Replace("s", "S").Trim();
            // ������Ʈ
            // _skillStat �� view����� Ȱ��ȭ�� ��ų�� Ŭ���� ������ �ش� �������� ����ǹǷ� ���� �־��� �ʿ� ����
            _skillStat.SkillSlotNumber = tmpSelectedIcon;
            GameManager.Skill.UpdatePlayerSkillList(_playerSkillList, _skillStat);
            
            // �巡�׿� �ӽ� �̹��� ����
            _selectedIcon.sprite = null;
            _selectedIcon.gameObject.SetActive(false);

            // ó�� ������ ���� �ʱ�ȭ
            _selectedSlot = -1;
            // �� ��ư ��ų �ؽ�Ʈ ��Ȱ��ȭ
            sceneSkillSlot[tmpSelectedIcon]._skillText.gameObject.SetActive(false);
            return;
        }
    }
}
