using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SceneInventoryButton : MonoBehaviour
{
    public void OpenInventory()
    {
        // Ui�� Ui�Ŵ������� ������
        GameManager.Ui.InventoryOpen();
    }
}
