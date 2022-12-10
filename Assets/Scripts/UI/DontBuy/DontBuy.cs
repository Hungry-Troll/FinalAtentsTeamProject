using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontBuy : MonoBehaviour
{
    // �巡�� ������� ����
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
            // 5�ʰ� ������ ������ ����
            GameManager.Ui.DontBuyOnOff(false);
            _time -= _time;
        }
    }
}
