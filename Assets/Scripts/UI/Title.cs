using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
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
        Time.timeScale = 0f;
    }

    public void OptionOff()
    {
        Option.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
