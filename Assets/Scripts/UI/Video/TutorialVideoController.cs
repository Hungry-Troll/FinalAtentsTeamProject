using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 비디오 실행 후 자동을 꺼지게 할 것
// 비디오 실행 후 배경음 일시 정지
// 비디오 종료 후 배경음 다시 실행
public class TutorialVideoController : MonoBehaviour
{
    // 비디오 플레이 시간을 계산하기 위한 변수
    private static float time;
    public static float TIME
    {
        get { return time; }
        set { time = value; }
    }
    void Start()
    {
        time = 0;
        // BGM 노래 제거
        GameManager.Sound._bgmAudioSource.clip = null;
        // 모든 UI 끔
        GameManager.Ui.UISetActiveFalse();
        // 터치 잠금 On
        GameManager.Ui.ScreenLock(true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // 동영상이 11.12초 짜리 이므로
        if(time > 12.0f)
        {
            ReturnToNormal();
        }
    }

    public void ReturnToNormal()
    {
        // BGM 재생
        GameManager.Sound.BGMPlay("-kpop_release-");
        // 모든 UI 킴
        GameManager.Ui.UISetActiveTrue();
        // 터치 잠금 Off
        GameManager.Ui.ScreenLock(false);
        // 비디오 플레이어 제거
        Destroy(gameObject);
    }
}
