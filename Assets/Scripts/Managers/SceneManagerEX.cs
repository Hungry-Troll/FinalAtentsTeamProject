using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerEX
{
    //���̸� Ȯ�ο�
    public string LoadSceneName;
    public void LoadScene(string SceneName)
    {
        LoadSceneName = SceneName;
        SceneManager.LoadSceneAsync("LoadingScene");
    }
}
