using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킵 버튼 컨트롤러

public class UI_SkipButton : MonoBehaviour
{
    // 튜토리얼 비디오 가지고 있기
    private GameObject _ui_tutorialVideo;
    public GameObject UI_TutorialVideo
    {
        get { return _ui_tutorialVideo; }
        set { _ui_tutorialVideo = value; }
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    // 튜토리얼 회상 영상 스킵
    public void SkipTutorialVideo()
    {
        TutorialVideoController.TIME = 12.0f;
    }
}
