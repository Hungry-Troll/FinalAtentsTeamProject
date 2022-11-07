using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� �� �ڵ��� ������ �� ��
// ���� ���� �� ����� �Ͻ� ����
// ���� ���� �� ����� �ٽ� ����
public class TutorialVideoController : MonoBehaviour
{
    // ���� �÷��� �ð��� ����ϱ� ���� ����
    float time;
    void Start()
    {
        time = 0;
        // BGM �뷡 ����
        GameManager.Sound._bgmAudioSource.clip = null;
        // ��� UI ��
        GameManager.Ui.UISetActiveFalse();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // �������� 11.12�� ¥�� �̹Ƿ�
        if(time > 12.0f)
        {
            // BGM ���
            GameManager.Sound.BGMPlay("-kpop_release-");
            // ��� UI Ŵ
            GameManager.Ui.UISetActiveTrue();
            // ���� �÷��̾� ����
            Destroy(gameObject);
        }
    }
}
