using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SceneInventoryButton : MonoBehaviour
{
    public void OpenInventory()
    {
        // Ui는 Ui매니저에서 관리함
        GameManager.Ui.InventoryOpen();
    }
}
