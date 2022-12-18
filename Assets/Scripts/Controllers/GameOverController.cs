using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public void RestartButton()
    {
        // 씬 이동시 모든 몬스터 오브젝트 초기화
        // 씬 매니저에서 하는 것은 어떨지...
        GameManager.Obj.RemoveAllMobList();
        GameManager.Scene.LoadScene("CharacterSelectScene");
        
    }

    // 메인 메뉴로 되돌아갈 때 기존 게임매니저와 오디오 소스 파괴
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
