using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SceneQuestButton : MonoBehaviour
{
    public void OpenQuest()
    {
        // Ui�� Ui�Ŵ������� ������
        GameManager.Ui.QuestOpen();
    }
}
