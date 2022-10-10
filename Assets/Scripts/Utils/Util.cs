using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // ���ӿ�����Ʈ �ڽ��� ã�� ��� �Լ�
    public static Transform FindChild(string name, Transform _tr)
    {
        if (_tr.name == name)
        {
            return _tr;
        }
        for (int i = 0; i < _tr.childCount; i++)
        {
            Transform findTr = FindChild(name, _tr.GetChild(i));
            if (findTr != null)
                return findTr;
        }
        return null;
    }
}
