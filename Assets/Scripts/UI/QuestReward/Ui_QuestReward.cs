using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_QuestReward : MonoBehaviour
{
    // �巡�� ������� ����
    // ���� ���� �ؽ�Ʈ
    public Text _rewardText;
    float _time;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if(_time > 3.0f)
        {
            // 5�ʰ� ������ ������ ����
            GameManager.Ui.QuestRewardOnOff(false);
            _time -= _time;
        }
    }
}
