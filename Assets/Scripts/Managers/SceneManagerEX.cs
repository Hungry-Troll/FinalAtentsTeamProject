using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;


public class SceneManagerEX
{
    // ���̸� Ȯ�ο�
    public string _LoadSceneName;
    // �ٸ��Ŵ������� ����� Ȯ���ϱ����� Enum _sceneName�� ������ üũ
    public Define.SceneName _sceneNameEnum;
    // �ε� �� �ҷ����� �Լ� >> �ε� �� ���� ������ �ҷ���
    public void LoadScene(string SceneName)
    {
        _LoadSceneName = SceneName;
        SceneCheck();
        SceneManager.LoadSceneAsync("LoadingScene");
    }

    // ���� ��������� Ȯ���ϴ� �Լ� >> ���� �� ������ �ٸ��� �����ؾߵǴ� 
    // ���� �Ŵ����� ����ϱ� ���ؼ� ����
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
