using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
public class Ui_SceneAttackButton : MonoBehaviour
{
    //버튼 확인용
    SceneAttackButton sceneAttackButton;
    public List<Ui_SceneSkillSlot> _skillSlots;
    Transform _findRollCoolTime;
    Image _rollCoolTimeImage;
    Image _skill1CoolTimeImage;
    Image _skill2CoolTimeImage;
    Image _skill3CoolTimeImage;
    Image _attackCoolTimeImage;
    bool _rollCoolTimeCheck;
    bool _skill1CoolTimeCheck;
    bool _skill2CoolTimeCheck;
    bool _skill3CoolTimeCheck;
    bool _attackCoolTimeCheck;
    int i = 0;
    public void Start()
    {
        _skillSlots = new List<Ui_SceneSkillSlot>();
        // 재귀함수로 버튼의 트랜스폼을 찾아서 스크립트를 리스트에 넣어둠
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
        _skill1CoolTimeCheck = false;
        _skill2CoolTimeCheck = false;
        _skill3CoolTimeCheck = false;
        _attackCoolTimeCheck = false;
    }

    public void AttackButton()
    {
        GameManager.Ui.AttackButton();
    }

    public void Skill1Button(bool isSkill1)
    {
        if (_skill1CoolTimeCheck == false)
        {
            GameManager.Ui.Skill1Button();
        }
        if (isSkill1 == true)
        {
            _skill1CoolTimeCheck = true;
            StartCoroutine(Skill1CoolTime(10f));
        }
    }
    IEnumerator Skill1CoolTime(float coolTime)
    {
        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            _skill1CoolTimeImage.fillAmount = (1f / coolTime);
            yield return new WaitForFixedUpdate();
        }
        _skill1CoolTimeCheck = false;
    }

    public void Skill2Button()
    {
        GameManager.Ui.Skill2Button();
    }

    public void Skill3Button(bool isSkill3)
    {
        if (_skill3CoolTimeCheck == false)
        {
            GameManager.Ui.Skill3Button();
        }
        if (isSkill3 == true)
        {
            _skill3CoolTimeCheck = true;
            StartCoroutine(Skill1CoolTime(20f));
        }
    }
    public void RollingButton(bool isRoll)
    {
        if (_rollCoolTimeCheck == false)
        {
            GameManager.Ui.RollingButton();
            return;
        }
        if (isRoll == true)
        {
            _rollCoolTimeCheck = true;
            StartCoroutine(RollCoolTime(5f));
        }
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
}


//https://codefinder.janndk.com/12
//https://solution94.tistory.com/16