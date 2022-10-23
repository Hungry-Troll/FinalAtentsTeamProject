using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �Ͻ����� �޴� ����� ���
// ��ư�� �̹��� ������Ʈ�� �÷��� ��������,
// ��ư�� ��ư ������Ʈ�� �븻 �÷��� ���� ���� 0����,
// ���̶���Ʈ �÷��� ���� ���� 100 ����, �������� �÷��� ���� ���� 150 ������,
// ��ư�� �ؽ�Ʈ UI �ȿ� Shadow ������Ʈ�� �����ϰ�
// Effect Distance�� 5, -5 ������ ����.
// �𸣰ڴٸ� ���� ������ ��ó Ȯ��.

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

    // timeScale�� �ð��� �� ����ϴ��� ���ϴ� ��� �Լ�.
    // 1�̸� 1 ���, 0.5�� 2 ���, 0�̸� ����
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
        SceneManager.LoadScene("�޴�");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
