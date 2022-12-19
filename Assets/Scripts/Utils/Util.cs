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

    // �̴Ͻÿ��� �Ҷ����� �̸� �ٲٱ� �����Ƽ� ���� �Լ�
    public static GameObject Instantiate(GameObject go)
    {
        // ������Ʈ�Ŵ���
        // ���� ������ �������� ������ ������
        for (int i = 0; i < GameManager.Obj._objPool.Count; i++)
        {
            // ���̸� ��ŵ
            if (GameManager.Obj._objPool[i] == null)
                continue;
            if (go.name == GameManager.Obj._objPool[i].name)
            {
                //���� ������ �ʰ� ������ ������ �ִ� ��ü�� ��ü
                return GameManager.Obj._objPool[i];
            }
        }
        // ���� ��쿡�� ���� ����
        GameObject GameObj = GameObject.Instantiate<GameObject>(go);
        GameObj.name = go.name;
        //���� ��ü�� ������Ʈ �Ŵ������� ����
        GameObj.transform.SetParent(GameManager.Obj._go.transform);
        GameManager.Obj._objPool.Add(GameObj);
        GameObj.transform.position = Vector3.zero;
        return GameObj;
    }

    // Define.Job Ÿ������ ���� ������ string���� ��ȯ
    // �����ε�(1/2)
    public static string SortJob(Define.Job jobEnum)
    {
        // ������ �� �ʱ�ȭ
        string jobString = "None";
        switch(jobEnum)
        {
            case Define.Job.Superhuman:
                jobString = Define.Job.Superhuman.ToString();
                break;
            case Define.Job.Cyborg:
                jobString = Define.Job.Cyborg.ToString();
                break;
            case Define.Job.Scientist:
                jobString = Define.Job.Scientist.ToString();
                break;
            default:
                break;
        }
        return jobString;
    }

    // string Ÿ������ ���� ������ Define.Job���� ��ȯ
    // �����ε�(2/2)
    public static Define.Job SortJob(string jobString)
    {
        // ������ �� �ʱ�ȭ
        Define.Job jobEnum = Define.Job.None;
        switch(jobString)
        {
            case "Superhuman":
                jobEnum = Define.Job.Superhuman;
                break;
            case "Cyborg":
                jobEnum = Define.Job.Cyborg;
                break;
            case "Scientist":
                jobEnum = Define.Job.Scientist;
                break;
            default:
                break;
        }
        return jobEnum;
    }
}
