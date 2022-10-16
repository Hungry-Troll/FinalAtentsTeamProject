using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SceneOptionButton : MonoBehaviour
{
    public void OpenOption()
    {
        // Ui는 Ui매니저에서 관리함
        GameManager.Ui.OptionOpen();
    }
}
