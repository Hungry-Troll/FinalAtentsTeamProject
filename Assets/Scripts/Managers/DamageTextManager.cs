using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager
{
    // ��Ʈ ���ӿ�����Ʈ
    public GameObject _damageTextPool;
    // ���� ī��Ʈ ����
    int _count;
    // ����� �ؽ�Ʈ ����Ʈ
    public List<GameObject> _damageTextList;
    public void Init()
    {
        _damageTextPool = new GameObject();
        _damageTextPool.name = "@DamageTextPool";
        _count = 15;
        _damageTextList = new List<GameObject>();
        //����� �ؽ�Ʈ ���� 15�� ���� <������Ʈ Ǯ�� ���>
        for (int i = 0; i < _count; i++)
        {
            GameObject tmp = GameManager.Create.CreateUi("UI_DamageText", _damageTextPool);
            tmp.name = tmp.name + "_" + i;
            _damageTextList.Add(tmp);
            tmp.SetActive(false);
        }
    }

    // ����� �ؽ�Ʈ�� �Ѱ��ִ� �Լ�
    public GameObject DamageTextStart(GameObject target, int damage)
    {
        for (int i = 0; i < _damageTextList.Count; i++)
        {
            if(_damageTextList[i].activeSelf == false)
            {
                // Ÿ���� �޾ƿͼ� ���� �θ�� ��������
                _damageTextList[i].transform.SetParent(target.transform);
                // ��ũ��Ʈ�� ����
                DamageTextEX dm = _damageTextList[i].GetComponent<DamageTextEX>();
                // ������� ����
                dm._damage = damage;
                _damageTextList[i].SetActive(true);
                GameObject tmp = _damageTextList[i];
                _damageTextList.RemoveAt(i);
                return tmp;
            }
        }
        return null;
    }
}
