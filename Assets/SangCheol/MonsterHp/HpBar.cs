using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    private Camera _cam;
    private Canvas _canvas;
    private RectTransform _rectParent;
    private RectTransform _rectHp;

    //처음엔 (0,0,0)으로 생성
    [HideInInspector] public Vector3 offset = Vector3.zero;
    //몬스터 위치
    [HideInInspector] public Transform _mobPos;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _cam = _canvas.worldCamera;
        _rectParent = _canvas.GetComponent<RectTransform>();
        _rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    //Update is called once per frame
    void Update()
    {

    }

    //캐릭터가 움직인후에 HpBar가 그 좌표를 받아서 움직여야하기때문에 FixedUpdate사용
    private void FixedUpdate()
    {
        //월드좌표를 스크린좌표로 변경
        var _screenPos = Camera.main.WorldToScreenPoint(_mobPos.position + offset);

        //요건 플레이어가 몬스터 뒤로 갔을떄 HpBar가 없어지는거 방지
        //우리 프로젝트에서는 없어도 무방할듯함?
        if (_screenPos.z < 0.0f)
        {
            _screenPos *= -1.0f;
        }

        var _localPos = Vector2.zero;
        //스크린좌표를 캔버스에서 사용할수 있는 좌표로 변경
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectParent, _screenPos, _cam, out _localPos);
        _rectHp.localPosition = _localPos;
    }
}
