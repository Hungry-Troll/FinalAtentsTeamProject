using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

// 텍스트 매쉬 프로는 안쓰기로 해서 쓰지 않는걸로,,,

public class DamageTextEX : MonoBehaviour
{
    Text _text;
    public int _damage;
    private Canvas _canvas;
    //private RectTransform _rectParent;
    private RectTransform _rectText;
    float _y;

    // Start is called before the first frame update
    void Start()
    {
        // 부모 오브젝트가 없으면 리턴처리 (버그 제거용)
        if(transform.parent.gameObject == null)
        {
            return;
        }
        _text = GetComponent<Text>();
        _canvas = GetComponentInParent<Canvas>();
        _rectText = GetComponent<RectTransform>();
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = Camera.main;
        
        // 데미지 숫자를 문자로 변환
        _text.text = _damage.ToString();

        // 데미지 뜰 위치 y 값
        Vector3 offset = gameObject.transform.position + Vector3.up * (gameObject.GetComponentInParent<NavMeshAgent>().height);
        _y = offset.y;
        // 데미지 뜰 위치 (로컬좌표)
        _rectText.localPosition = new Vector3(0, _y, 0);

        // 이하 DOTween 코드
        float power = 1.0f;
        _text.color = _damage > 30 ? Color.red : Color.white;
        _text.text = _damage.ToString();
        
        // null 체크
        if(_text != null)
        {
            // 점점 없어지는...
            _text.DOFade(0f, 1.0f);
        }
        
        // null 체크
        if(transform != null)
        {
            // 글씨 크기 증가
            transform.DOPunchScale(Vector3.one * 0.03f, 0.2f, 1, 0);
        }
        Vector3 endPos = transform.position + new Vector3(0, _y, 0);
        endPos.x += (Random.insideUnitCircle * power).x;
        
        // null 체크
        if(transform != null)
        {
            // 글씨 점프 효과
            transform.DOJump(endPos, power, 1, 1.0f).OnComplete(() => { gameObject.SetActive(false)/*Destroy(gameObject)*/; });
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
