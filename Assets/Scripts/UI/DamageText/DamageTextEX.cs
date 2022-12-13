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

    void OnEnable()
    {
        DamageLog();
    }

    public void DamageLog()
    {
        _text = GetComponent<Text>();
        _canvas = GetComponentInParent<Canvas>();
        _rectText = GetComponent<RectTransform>();
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = Camera.main;

        // 재사용하기위해 초기화
        // 페이트 초기화
        _text.DOFade(1f, 0.0f);
        // 위치 초기화
        Vector3 offset = new Vector3(0, 0, 0);

        // 데미지 숫자를 문자로 변환
        _text.text = _damage.ToString();

        // 데미지 뜰 위치 y 값
        offset = Vector3.up * (gameObject.GetComponentInParent<NavMeshAgent>().height);
        _y = offset.y;
        // 데미지 뜰 위치 (로컬좌표)
        _rectText.localPosition = new Vector3(0, _y, 0);

        // 이하 DOTween 코드
        float power = 1.0f;
        _text.color = _damage > 30 ? Color.red : Color.white;
        _text.text = _damage.ToString();

        // 점점 없어지는...
        _text.DOFade(0f, 1.0f);

        // 글씨 크기 증가
        transform.DOPunchScale(Vector3.one * 0.03f, 0.2f, 1, 0);

        Vector3 endPos = transform.position + new Vector3(0, _y, 0);
        endPos.x += (Random.insideUnitCircle * power).x;


        // 글씨 점프 효과
        transform.DOJump(endPos, power, 1, 1.5f).OnComplete(() =>
        // 글씨 점프가 끝나면 비활성화하고 대미지텍스트풀에 다시 넣음
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(GameManager.DamText._damageTextPool.transform);
            GameManager.DamText._damageTextList.Add(this.gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라 방향으로 회전
        transform.rotation = Camera.main.transform.rotation;
    }
}
