using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarEX : MonoBehaviour
{
    // �̹��� ����� ���� ���� ����
    Stat _stat;
    // Hp �� �̹���
    Image _hpBarImage;
    // HP �� ���� ����
    float _maxHp;
    float _currentHp;

    private void Start()
    {
        // ���� ���� ������ ��
        _stat = gameObject.transform.parent.GetComponent<Stat>();
        // HpBar ��ġ ã��
        Transform tmpTr = Util.FindChild("HpBar", transform);
        // ã�� ���� �̹��� ������Ʈ�� ������ ����
        _hpBarImage = tmpTr.GetComponent<Image>();
        // ü�� Ǯ��
        _hpBarImage.fillAmount = 1;
    }
    //Update is called once per frame

    private void Update()
    {
        // �����Ѵ�� �׳� �θ� HP�� ������ ���ư��� ����
        transform.rotation = Camera.main.transform.rotation;
        // ���� ������Ʈ ���� �ѹ����� �����ǰ� ���� �ʿ�
        HpBarSystem();
    }

    // HP ��� �Լ�
    private void HpBarSystem()
    {
        _maxHp = _stat.Max_Hp;
        _currentHp = _stat.Hp;
        _hpBarImage.fillAmount = _currentHp / _maxHp;
    }


}