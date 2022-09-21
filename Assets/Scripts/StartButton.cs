using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject Option;
    
    public void StartBtn()
    {
        //SceneManager.LoadScene("LoadingScene");
        GameManager.Scene.LoadScene("CharacterSelectScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 옵션 함수들
    public void OptionOn()
    {
        Option.SetActive(true);
    }

    public void OptionOff()
    {
        Option.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
