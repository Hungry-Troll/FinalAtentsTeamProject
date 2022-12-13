using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager
{
    // 루트 게임오브젝트
    public GameObject _damageTextPool;
    // 루프 카운트 변수
    int _count;
    // 대미지 텍스트 리스트
    public List<GameObject> _damageTextList;
    public void Init()
    {
        _damageTextPool = new GameObject();
        _damageTextPool.name = "@DamageTextPool";
        _count = 15;
        _damageTextList = new List<GameObject>();
        //대미지 텍스트 생성 15개 생성 <오브젝트 풀링 방식>
        for (int i = 0; i < _count; i++)
        {
            GameObject tmp = GameManager.Create.CreateUi("UI_DamageText", _damageTextPool);
            tmp.name = tmp.name + "_" + i;
            _damageTextList.Add(tmp);
            tmp.SetActive(false);
        }
    }

    // 대미지 텍스트를 넘겨주는 함수
    public GameObject DamageTextStart(GameObject target, int damage)
    {
        for (int i = 0; i < _damageTextList.Count; i++)
        {
            if(_damageTextList[i].activeSelf == false)
            {
                // 타겟을 받아와서 먼저 부모로 설정해줌
                _damageTextList[i].transform.SetParent(target.transform);
                // 스크립트로 접근
                DamageTextEX dm = _damageTextList[i].GetComponent<DamageTextEX>();
                // 대미지에 대입
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
