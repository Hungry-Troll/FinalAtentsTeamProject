using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어에게 방향 안내해주는 UI 관리 클래스
// DirectionArrow UI들이 담겨있는 Root 격 게임오브젝트에 이 스크립트를 추가하여 사용하는 형식으로 제작
// 예) Tutorial 씬의 TutorialWayHelper 오브젝트에 이 스크립트 추가
//      -> OnArrow("ToWesley"), OffArrow("ToVenice") 등 UI들이 담긴 오브젝트 이름을 전달하여 on/off 제어

public class DirectionArrowControlloer : MonoBehaviour
{
    // Arrow UI들이 들어있는 자식 오브젝트 개수
    private int _childCount;
    // 자식 오브젝트 배열
    private Transform[] _arrowArr;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        //
    }

    public void Init()
    {
        // 자식 오브젝트 몇개인지 담기
        _childCount = transform.childCount;
        // 알아낸 수량으로 배열 초기화
        _arrowArr = new Transform[_childCount];

        // 배열에 자식 담기
        for (int i = 0; i < _childCount; i++)
        {
            _arrowArr[i] = transform.GetChild(i);
        }
    }

    // 이름으로 검색해서 자식 중에 찾는 게임오브젝트 반환하는 함수
    private GameObject FindObject(string name)
    {
        // null 체크
        if(_arrowArr != null)
        {
            for (int i = 0; i < _arrowArr.Length; i++)
            {
                if (name.Equals(_arrowArr[i].gameObject.name))
                {
                    return _arrowArr[i].gameObject;
                }
            }
        }
        return null;
    }

    // 이름으로 검색해서 원하는 자식 오브젝트 ON
    public void OnArrow(string arrowObjName)
    {
        GameObject obj = FindObject(arrowObjName);
        obj.SetActive(true);
    }

    // 이름으로 검색해서 원하는 자식 오브젝트 OFF
    public void OffArrow(string arrowObjName)
    {
        GameObject obj = FindObject(arrowObjName);
        obj.SetActive(false);
    }

    // 모든 화살표 끄기
    public void OffAllArrows()
    {
        // null 체크
        if(_arrowArr != null)
        {
            for (int i = 0; i < _arrowArr.Length; i++)
            {
                _arrowArr[i].gameObject.SetActive(false);
            }
        }
    }
}
