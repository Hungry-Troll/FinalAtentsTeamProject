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

    public void MainMenuMutton()
    {
        GameManager.Scene.LoadScene("Title");
    }
}
