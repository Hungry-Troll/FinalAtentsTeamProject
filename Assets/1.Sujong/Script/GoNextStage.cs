using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextStage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ãæµ¹");
        SceneManager.LoadScene("EndingScene");
    }
}
