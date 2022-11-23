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
    bool _rollCoolTimeCheck;
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
        _findRollCoolTime = Util.FindChild("RollCooltime", gameObject.transform);
        _rollCoolTimeImage = _findRollCoolTime.GetComponent<Image>();
        _rollCoolTimeCheck = true;
    }

    public void AttackButton()
    {
        GameManager.Ui.AttackButton();
    }

    public void Skill1Button()
    {
        GameManager.Ui.Skill1Button();
    }

    public void Skill2Button()
    {
        GameManager.Ui.Skill2Button();
    }

    public void Skill3Button()
    {
        GameManager.Ui.Skill3Button();
    }
    public void RollingButton()
    {
        if (_rollCoolTimeCheck == true)
        {
            GameManager.Ui.RollingButton();
            _rollCoolTimeCheck = false;
            StartCoroutine(CoolTime(5f));
        }
    }
    IEnumerator CoolTime(float coolTime)
    {
        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            _rollCoolTimeImage.fillAmount = (1f / coolTime);
            yield return new WaitForFixedUpdate();
        }
        _rollCoolTimeCheck = true;
    }
}


//https://codefinder.janndk.com/12
//https://solution94.tistory.com/16