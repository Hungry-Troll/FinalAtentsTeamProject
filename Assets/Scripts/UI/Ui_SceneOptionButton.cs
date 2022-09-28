using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SceneOptionButton : MonoBehaviour
{
    public void OpenOption()
    {
        Debug.Log("ButtonOn");
        // Ui는 Ui매니저에서 관리함
        GameManager.Ui.OptionOpen();
    }
}
