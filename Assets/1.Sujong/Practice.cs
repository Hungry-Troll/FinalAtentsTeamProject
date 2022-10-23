using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Practice : MonoBehaviour
{
    public Text Text;
    string Str;
    // Start is called before the first frame update
    void Start()
    {
        Str = "丑丑丑丑丑中中中中";
        transform.DOMove(new Vector3(3, 3, 3), 3f).SetEase(Ease.Linear);
        Text.DOText(Str, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
