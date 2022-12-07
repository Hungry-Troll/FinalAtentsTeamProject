using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // 게임오브젝트 자식을 찾는 재귀 함수
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

    // 이니시에잇 할때마다 이름 바꾸기 귀찮아서 만든 함수
    public static GameObject Instantiate(GameObject go)
    {
        GameObject GameObj = GameObject.Instantiate<GameObject>(go);
        GameObj.name = go.name;
        return GameObj;
    }

    // Define.Job 타입으로 직업 받으면 string으로 반환
    // 오버로딩(1/2)
    public static string SortJob(Define.Job jobEnum)
    {
        // 리턴할 값 초기화
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

    // string 타입으로 직업 받으면 Define.Job으로 반환
    // 오버로딩(2/2)
    public static Define.Job SortJob(string jobString)
    {
        // 리턴할 값 초기화
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
