using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ŵ ��ư ��Ʈ�ѷ�

public class UI_SkipButton : MonoBehaviour
{
    // Ʃ�丮�� ���� ������ �ֱ�
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

    // Ʃ�丮�� ȸ�� ���� ��ŵ
    public void SkipTutorialVideo()
    {
        TutorialVideoController.TIME = 12.0f;
    }
}
