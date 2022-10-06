using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSceneStart : MonoBehaviour
{
    public void OnStartButton()
    {
        // ĳ���� �ƹ��͵� �������� ������ null
        if (GameManager.Data.playData.Job != null)
        {
            GameManager.Scene.LoadScene("SelectPet");
        }
        else
        {
            // �ӽ÷� Debug ���Ŀ� UI ������...
            Debug.LogWarning("ĳ���͸� �������ּ���");
        }
    }
}
