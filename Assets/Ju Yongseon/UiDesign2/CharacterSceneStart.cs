using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSceneStart : MonoBehaviour
{
    public void OnStartButton()
    {
        // 캐릭터 아무것도 선택하지 않으면 null
        if (GameManager.Data.playData.Job != null)
        {
            GameManager.Scene.LoadScene("SelectPet");
        }
        else
        {
            // 임시로 Debug 추후에 UI 입혀야...
            Debug.LogWarning("캐릭터를 선택해주세요");
        }
    }
}
