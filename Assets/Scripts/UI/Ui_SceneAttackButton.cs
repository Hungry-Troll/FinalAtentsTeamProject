using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
public class Ui_SceneAttackButton : MonoBehaviour
{
    //��ư Ȯ�ο�
    SceneAttackButton sceneAttackButton;
    public List<Ui_SceneSkillSlot> _skillSlots;
    Transform _findRollCoolTime;

    public int potionCnt;   // ���ǰ��� ����	
    public Text potionCntTxt;

    public Image _rollCoolTimeImage;
    public Image _skill1CoolTimeImage;
    public Image _skill2CoolTimeImage;
    public Image _skill3CoolTimeImage;
    public Image _attackCoolTimeImage;
    bool _rollCoolTimeCheck;
    //bool _skill1CoolTimeCheck;
    //bool _skill2CoolTimeCheck;
    //bool _skill3CoolTimeCheck;
    //bool _attackCoolTimeCheck;
    Color originalColor;
    Color newColor;
    public void Start()
    {
        _skillSlots = new List<Ui_SceneSkillSlot>();
        // ����Լ��� ��ư�� Ʈ�������� ã�Ƽ� ��ũ��Ʈ�� ����Ʈ�� �־��
        Transform sk1 = Util.FindChild("Ui_Skill1_Button", transform);
        Transform sk2 = Util.FindChild("Ui_Skill2_Button", transform);
        Transform sk3 = Util.FindChild("Ui_Skill3_Button", transform);
        Ui_SceneSkillSlot skillSolt1 = sk1.GetComponent<Ui_SceneSkillSlot>();
        Ui_SceneSkillSlot skillSolt2 = sk2.GetComponent<Ui_SceneSkillSlot>();
        Ui_SceneSkillSlot skillSolt3 = sk3.GetComponent<Ui_SceneSkillSlot>();
        _skillSlots.Add(skillSolt1);
        _skillSlots.Add(skillSolt2);
        _skillSlots.Add(skillSolt3);
        _rollCoolTimeImage = Util.FindChild("RollCooltime", transform).GetComponent<Image>();
        _skill1CoolTimeImage = Util.FindChild("Skill1Cooltime", transform).GetComponent<Image>();
        _skill2CoolTimeImage = Util.FindChild("Skill2Cooltime", transform).GetComponent<Image>();
        _skill3CoolTimeImage = Util.FindChild("Skill3Cooltime", transform).GetComponent<Image>();
        _attackCoolTimeImage = Util.FindChild("AttackCooltime", transform).GetComponent<Image>();
        _rollCoolTimeCheck = false;
        //_skill1CoolTimeCheck = false;
        //_skill2CoolTimeCheck = false;
        //_skill3CoolTimeCheck = false;
        //_attackCoolTimeCheck = false;
        newColor = new Color(0, 1f, 0);
        newColor.a = 0.5f;
        originalColor = _skill3CoolTimeImage.color;

        // ���ǿ�	
        potionCntTxt = Util.FindChild("UI_PotionButton", transform).GetComponentInChildren<Text>();
        potionCnt = 0;
        potionCntTxt.text = "";

        // ���� ���� �ִٸ� ���� ���� �ؽ�Ʈ ������Ʈ
        if(GameManager.Ui._inventoryController._item != null)
        {
            // ������ ��� �� �ƴ϶�� �ҷ�����
            List<GameObject> itemList = GameManager.Ui._inventoryController._item;
            for (int i = 0; i < itemList.Count; i++)
            {
                // �� �߿� ���� ������
                if(itemList[i].name.Equals("potion1"))
                {
                    // �� ���� ��Ƽ�
                    int tempPotionCnt = GameManager.Ui._inventoryController._invenSlotList[i]._invenItemCount;
                    // �ؽ�Ʈ�� Set
                    potionCnt = tempPotionCnt;
                    potionCntTxt.text = tempPotionCnt.ToString();
                }
            }
        }


    }

    public void AttackButton()
    {
        GameManager.Ui.AttackButton();
    }

    public void Skill1Button(bool isSkill1)
    {
        //if (isSkill1 == true)
        //{
            //_skill1CoolTimeCheck = true;
            //StartCoroutine(Skill1CoolTime(10f));
        //}
        //if (_skill1CoolTimeCheck == false)
        //{
            GameManager.Ui.Skill1Button(_skill1CoolTimeImage.sprite.name);
        //}
    }
/*    IEnumerator Skill1CoolTime(float coolTime)
    {
        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            _skill1CoolTimeImage.fillAmount = (1f / coolTime);
            yield return new WaitForFixedUpdate();
        }
        _skill1CoolTimeCheck = false;
    }*/

    public void Skill2Button()
    {
        GameManager.Ui.Skill2Button(_skill2CoolTimeImage.sprite.name);
    }

    public void Skill3Button(int isSkill3)
    {
        //if (isSkill3 == 1)
        //{
        //    StartCoroutine(Skill3Duration(15f));
        //}
        //if (isSkill3 == 2)
        //{
        //    _skill3CoolTimeCheck = true;
        //    StartCoroutine(Skill3CoolTime(15f));
        //}
        //if (_skill3CoolTimeCheck == false)
        //{
            GameManager.Ui.Skill3Button(_skill3CoolTimeImage.sprite.name);
        //}
    }
/*    IEnumerator Skill3CoolTime(float coolTime)
    {
        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            _skill3CoolTimeImage.fillAmount = (1f / coolTime);
            yield return new WaitForFixedUpdate();
        }
        _skill3CoolTimeCheck = false;
    }*/
/*    IEnumerator Skill3Duration(float Duration)
    {
        while (Duration > 0)
        {
            Duration -= Time.deltaTime;
            _skill3CoolTimeImage.color = newColor;
            _skill3CoolTimeImage.fillAmount = (1f / Duration);
            yield return new WaitForFixedUpdate();
        }
        _skill1CoolTimeCheck = false;
        _skill3CoolTimeImage.color = originalColor;
    }*/
    public void RollingButton(bool isRoll)
    {
        //if (isRoll == true)
        //{
            //_rollCoolTimeCheck = true;
            //StartCoroutine(RollCoolTime(5f));
        //}
        //if (_rollCoolTimeCheck == false)
        //{
            GameManager.Ui.RollingButton();
        //}
    }
    IEnumerator RollCoolTime(float coolTime)
    {
        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            _rollCoolTimeImage.fillAmount = (1f / coolTime);
            yield return new WaitForFixedUpdate();
        }
        _rollCoolTimeCheck = false;
    }

    // ���� �Դ� ��ư	
    public void UsePotion()
    {
        // ������ 1���� �־���� ��� �� �� �ִ�.	
        if (potionCnt >= 1)
        {
            PlayerStat ob = GameManager.Obj._playerStat;
            int max = GameManager.Obj._playerStat.Max_Hp;
            GameManager.Ui._itemStatViewController.UsePotionExternal(ob, max);
            PotionCountMinusOne();
        }
    }
    public void PotionCountMinusOne()
    {
        potionCnt--;
        if (potionCnt == 0)
        {
            potionCntTxt.text = "";
        }
        else
        {
            potionCntTxt.text = potionCnt.ToString();
        }
    }

    public void GetPotion()
    {
        potionCnt++;
        potionCntTxt.text = potionCnt.ToString();
    }


    // UI ��ư �� ��ų ��ư�� �����ͼ� ����Ʈ ����� �����ϴ� �Լ�
    public List<Ui_SceneSkillSlot> LoadSceneSkillSlots()
    {
        List<Ui_SceneSkillSlot> _sceneSkillSlot = new List<Ui_SceneSkillSlot>();

        Transform sk1 = Util.FindChild("Ui_Skill1_Button", transform);
        Transform sk2 = Util.FindChild("Ui_Skill2_Button", transform);
        Transform sk3 = Util.FindChild("Ui_Skill3_Button", transform);
        Ui_SceneSkillSlot skillSolt1 = sk1.GetComponent<Ui_SceneSkillSlot>();
        Ui_SceneSkillSlot skillSolt2 = sk2.GetComponent<Ui_SceneSkillSlot>();
        Ui_SceneSkillSlot skillSolt3 = sk3.GetComponent<Ui_SceneSkillSlot>();
        _sceneSkillSlot.Add(skillSolt1);
        _sceneSkillSlot.Add(skillSolt2);
        _sceneSkillSlot.Add(skillSolt3);

        return _sceneSkillSlot;
    }

}


//https://codefinder.janndk.com/12
//https://solution94.tistory.com/16