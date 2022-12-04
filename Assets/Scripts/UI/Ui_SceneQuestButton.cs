using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SceneQuestButton : MonoBehaviour
{
    public void OpenQuest()
    {
        // Ui는 Ui매니저에서 관리함
        GameManager.Ui.QuestOpen();
    }
}
