using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SceneOptionButton : MonoBehaviour
{
    public void OpenOption()
    {
        Debug.Log("ButtonOn");
        // Ui�� Ui�Ŵ������� ������
        GameManager.Ui.OptionOpen();
    }
}
