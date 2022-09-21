using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    // 로딩창 컨트롤러
    [SerializeField]
    Image _progressBar;
    AsyncOperation _op; 
    
    void Start()
    {
        StartCoroutine("LoadSceneProcess");
    }

    IEnumerator LoadSceneProcess()
    {
        float timer = 0f;
        _op = SceneManager.LoadSceneAsync(GameManager.Scene.LoadSceneName);
        _op.allowSceneActivation = false;
        
        while(!_op.isDone)
        {
            yield return null;
            if(_op.progress < 0.9f)
            {
                _progressBar.fillAmount = _op.progress;
            } 
            else
            {
                timer += Time.unscaledDeltaTime;
                _progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(_progressBar.fillAmount >= 1f)
                {
                    _op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
