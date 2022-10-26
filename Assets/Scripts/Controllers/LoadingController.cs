using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    // 로딩창 컨트롤러
    [SerializeField]
    // 로딩바
    Image _progressBar;
    // 로딩변수
    AsyncOperation _op;
    // 로딩창 컨트롤러
    [SerializeField]
    // 로딩 배경 변환용 이미지
    Image _backImage;

    void Start()
    {
        // 로딩 배경 변환용 코드
        int tmpRandom = Random.Range(0, 3);
        Sprite tmpSprite;
        switch (tmpRandom)
        {
            case 0:
                // 트리케라톱스 이미지
                tmpSprite = GameManager.Resource.GetImage("BackImage1");
                _backImage.sprite = tmpSprite;
                break;
            case 1:
                // 파키케팔로 이미지
                tmpSprite = GameManager.Resource.GetImage("BackImage2");
                _backImage.sprite = tmpSprite;
                break;
            case 2:
                // 브라키오 이미지
                tmpSprite = GameManager.Resource.GetImage("BackImage3");
                _backImage.sprite = tmpSprite;
                break;
        }
        // 로딩창 열림
        StartCoroutine("LoadSceneProcess");
    }

    IEnumerator LoadSceneProcess()
    {

        float timer = 0f;
        _op = SceneManager.LoadSceneAsync(GameManager.Scene._LoadSceneName);
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
