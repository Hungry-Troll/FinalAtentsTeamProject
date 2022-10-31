using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;


public class SceneManagerEX
{
    // 씬이름 확인용
    public string _LoadSceneName;
    // 다른매니저에서 현재씬 확인하기위한 Enum _sceneName의 값으로 체크
    public Define.SceneName _sceneNameEnum;
    // 로딩 씬 불러오는 함수 >> 로딩 씬 이후 다음씬 불러옴
    public void LoadScene(string SceneName)
    {
        _LoadSceneName = SceneName;
        SceneCheck();
        SceneManager.LoadSceneAsync("LoadingScene");
    }

    // 현재 어느씬인지 확인하는 함수 >> 추후 각 씬마다 다르게 설정해야되는 
    // 각각 매니저에 사용하기 위해서 만듬
    public void SceneCheck()
    {
        switch (_LoadSceneName)
        {
            case "Title":
                _sceneNameEnum = Define.SceneName.Title;
                break;
            case "CharacterSelectScene":
                _sceneNameEnum = Define.SceneName.CharacterSelectScene;
                break;
            case "SelectPet":
                _sceneNameEnum = Define.SceneName.SelectPet;
                break;
            case "Tutorial":
                _sceneNameEnum = Define.SceneName.Tutorial;
                break;
            case "Village02":
                _sceneNameEnum = Define.SceneName.Village02;
                break;
            case "DunGeon":
                _sceneNameEnum = Define.SceneName.DunGeon;
                break;
        }
    }

}
