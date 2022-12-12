using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBarEX : MonoBehaviour
{
    // �̹��� ����� ���� ���� ����
    Stat _stat;
    // Hp �� �̹���
    Image _hpBarImage;
    // HP �� ���� ����
    public float _maxHp;
    public float _currentHp;
    // �̸� ����
    Text _name;
    // HP ���ڿ�
    Text _hpText;
    // HP ������
    public Text _Lv;
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

        // ĳ���� HP�� ���� ��
        Transform HpTextTr = Util.FindChild("HpText", transform);
        _hpText = HpTextTr.GetComponent<Text>();
        _hpText.text = _stat.Hp.ToString() + "/" + _stat.Max_Hp.ToString();
        // ĳ���� �̸� ������ ���� HP�ٿ�����Ʈ�� ������
        Transform NameTr = Util.FindChild("NameText", transform);
        _name = NameTr.GetComponent<Text>();
        _name.text = GameManager.Select._playerName;
        // ���� ����
        Transform LevelTr = Util.FindChild("LvText", transform);
        _Lv = LevelTr.GetComponent<Text>();
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
    public void HpBarSystem()
    {
        _maxHp = _stat.Max_Hp;
        _currentHp = _stat.Hp;
        _hpBarImage.fillAmount = _currentHp / _maxHp;
        // HP �� ����
        _hpText.text = _stat.Hp.ToString() + "/" + _stat.Max_Hp.ToString();
    }
}
