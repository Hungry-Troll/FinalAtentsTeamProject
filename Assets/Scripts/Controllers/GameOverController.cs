using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public void RestartButton()
    {
        GameManager.Scene.LoadScene("CharacterSelectScene");
    }

    public void MainMenuMutton()
    {
        GameManager.Scene.LoadScene("Title");
    }
}
