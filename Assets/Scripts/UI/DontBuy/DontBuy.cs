using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontBuy : MonoBehaviour
{
    // 드래그 드롭으로 연결
    float _time;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > 3.0f)
        {
            // 5초가 지나면 스스로 꺼짐
            GameManager.Ui.DontBuyOnOff(false);
            _time -= _time;
        }
    }
}
