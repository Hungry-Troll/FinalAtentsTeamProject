using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 일시정지 메뉴 만드는 방법
// 버튼의 이미지 컴포넌트의 컬러를 검정으로,
// 버튼의 버튼 컴포넌트의 노말 컬러의 알파 값을 0으로,
// 하이라이트 컬러의 알파 값을 100 정도, 프레스드 컬러의 알파 값을 150 정도로,
// 버튼의 텍스트 UI 안에 Shadow 컴포넌트를 부착하고
// Effect Distance를 5, -5 정도로 설정.
// 모르겠다면 공유 문서의 출처 확인.

public class PauseGame : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // timeScale은 시간을 몇 배속하는지 정하는 멤버 함수.
    // 1이면 1 배속, 0.5면 2 배속, 0이면 정지
    void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("메뉴");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
