using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class Ui_SceneAttackButton : MonoBehaviour
{
    //버튼 확인용
    SceneAttackButton sceneAttackButton;
    public List<Ui_SceneSkillSlot> _skillSlots;
    
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
        GameManager.Ui.RollingButton();
    }
}
