using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    // �ε�â ��Ʈ�ѷ�
    [SerializeField]
    // �ε���
    Image _progressBar;
    // �ε�����
    AsyncOperation _op;
    // �ε�â ��Ʈ�ѷ�
    [SerializeField]
    // �ε� ��� ��ȯ�� �̹���
    Image _backImage;

    void Start()
    {
        // �ε� ��� ��ȯ�� �ڵ�
        int tmpRandom = Random.Range(0, 3);
        Sprite tmpSprite;
        switch (tmpRandom)
        {
            case 0:
                // Ʈ���ɶ��齺 �̹���
                tmpSprite = GameManager.Resource.GetImage("BackImage1");
                _backImage.sprite = tmpSprite;
                break;
            case 1:
                // ��Ű���ȷ� �̹���
                tmpSprite = GameManager.Resource.GetImage("BackImage2");
                _backImage.sprite = tmpSprite;
                break;
            case 2:
                // ���Ű�� �̹���
                tmpSprite = GameManager.Resource.GetImage("BackImage3");
                _backImage.sprite = tmpSprite;
                break;
        }
        // �ε�â ����
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
