using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class Ui_SceneAttackButton : MonoBehaviour
{
    //버튼 확인용
    SceneAttackButton sceneAttackButton;

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
