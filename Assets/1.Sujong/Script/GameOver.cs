using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    float PlayerHP;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHP = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerHP = 0f;
        }

        if (PlayerHP == 0f)
        {

        }
    }
    
    public void RestartButton()
    {
        GameManager.Scene.LoadScene("Tutorial");
    }
    public void MainMenuButton()
    {
        GameManager.Scene.LoadScene("Main");
    }
}
