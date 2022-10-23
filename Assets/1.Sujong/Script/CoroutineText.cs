using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineText : MonoBehaviour
{
    public Text TextEx;

    void Start()
    {
        string[] Str_Arr = new string[] { "������", "<color=red>���</color>��", "��ġ���ּ���" };
        TextEx.text = string.Empty;
        StartCoroutine(Displaystring(Str_Arr));
    }

    IEnumerator Displaystring(string[] _arrObj)
    {
        for (int i = 0; i < _arrObj.Length; i++)
        {
            TextEx.text += _arrObj[i];
            yield return new WaitForSeconds(0.5f);
        }
    }
}
