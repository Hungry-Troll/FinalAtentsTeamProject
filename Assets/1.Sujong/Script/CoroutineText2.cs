using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineText2 : MonoBehaviour
{
    public Text Dialog;
    string Str_Of_Dialog;

    // Start is called before the first frame update
    void Start()
    {
        Str_Of_Dialog = "왼쪽으로 가면 위험합니다.";
        Dialog.text = string.Empty;
        char[] Str_Of_Dialog_Arr = Str_Of_Dialog.ToCharArray();
        StartCoroutine(Display_Str(Str_Of_Dialog_Arr));
    }

    IEnumerator Display_Str(char[] _Arr)
    {
        for (int i = 0; i < _Arr.Length; i++)
        {
            Dialog.text += _Arr[i];
            yield return new WaitForSeconds(0.2f);
        }
    }
}
