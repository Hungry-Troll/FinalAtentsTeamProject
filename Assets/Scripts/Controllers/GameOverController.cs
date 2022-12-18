using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public void RestartButton()
    {
        // �� �̵��� ��� ���� ������Ʈ �ʱ�ȭ
        // �� �Ŵ������� �ϴ� ���� ���...
        GameManager.Obj.RemoveAllMobList();
        GameManager.Scene.LoadScene("CharacterSelectScene");
        
    }

    // ���� �޴��� �ǵ��ư� �� ���� ���ӸŴ����� ����� �ҽ� �ı�
    public void MainMenuMutton()
    {
        GameObject[] fpx = new GameObject[11];
        GameObject gm = GameObject.Find("GameManager");
        GameObject bgm = GameObject.Find("BGMAudioSource");
/*        for (int i = 0; i < fpx.Length; i++)
        {
            fpx[i] = GameObject.Find("FPXAudioSource");
            Destroy(fpx[i]);
        }*/
        Destroy(gm);
        Destroy(bgm);
        

        GameManager.Scene.LoadScene("Title");
    }
}
