using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� �� �ڵ��� ������ �� ��
// ���� ���� �� ����� �Ͻ� ����
// ���� ���� �� ����� �ٽ� ����
public class TutorialVideoController : MonoBehaviour
{
    // ���� �÷��� �ð��� ����ϱ� ���� ����
    private static float time;
    public static float TIME
    {
        get { return time; }
        set { time = value; }
    }
    void Start()
    {
        time = 0;
        // BGM �뷡 ����
        GameManager.Sound._bgmAudioSource.clip = null;
        // ��� UI ��
        GameManager.Ui.UISetActiveFalse();
        // ��ġ ��� On
        GameManager.Ui.ScreenLock(true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // �������� 11.12�� ¥�� �̹Ƿ�
        if(time > 12.0f)
        {
            ReturnToNormal();
        }
    }

    public void ReturnToNormal()
    {
        // BGM ���
        GameManager.Sound.BGMPlay("-kpop_release-");
        // ��� UI Ŵ
        GameManager.Ui.UISetActiveTrue();
        // ��ġ ��� Off
        GameManager.Ui.ScreenLock(false);
        // ���� �÷��̾� ����
        Destroy(gameObject);
    }
}
