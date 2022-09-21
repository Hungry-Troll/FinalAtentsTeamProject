using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSceneStart : MonoBehaviour
{
    public void OnStartButton()
    {
        GameManager.Scene.LoadScene("SelectPet");
    }
}
