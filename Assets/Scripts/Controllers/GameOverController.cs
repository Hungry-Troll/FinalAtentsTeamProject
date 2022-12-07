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

    public void MainMenuMutton()
    {
        GameManager.Scene.LoadScene("Title");
    }
}
