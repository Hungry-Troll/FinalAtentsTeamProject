using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerEX
{
    //씬이름 확인용
    public string LoadSceneName;
    public void LoadScene(string SceneName)
    {
        LoadSceneName = SceneName;
        SceneManager.LoadSceneAsync("LoadingScene");
    }
}
