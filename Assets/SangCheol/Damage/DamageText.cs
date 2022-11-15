using System.Collections;
using System.Collections.Generic;
//TextMeshPro
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    //위로 움직이는 속도
    private float _moveSpeed;
    //흐려지는 속도
    private float _alphaSpeed;
    //점점 흐려지다 없어지는 시간
    private float _destroyTime;
    //ui에서 textMeshPro 사용하려면 TextMeshProUGUI를 사용해야됨
    TextMeshProUGUI text;
    Color alpha;
    public int damage;

    //아래부터는 HpBar스크립트와 거의 동일
    private Camera _cam;
    private Canvas _canvas;
    private RectTransform _rectParent;
    private RectTransform _rectText;

    //처음엔 (0,0,0)으로 생성
    [HideInInspector] public Vector3 offset = Vector3.zero;
    //몬스터 위치
    [HideInInspector] public Transform _mobPos;

    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = 2.0f;
        _alphaSpeed = 2.0f;
        _destroyTime = 3.0f;

        text = GetComponent<TextMeshProUGUI>();
        _canvas = GetComponentInParent<Canvas>();
        _cam = _canvas.worldCamera;
        _rectParent = _canvas.GetComponent<RectTransform>();
        _rectText = this.gameObject.GetComponent<RectTransform>();
        alpha = text.color;
        //데미지 숫자를 문자로 변환
        text.text = damage.ToString();
        Invoke("DestroyObject", _destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, _moveSpeed * Time.deltaTime, 0));
        transform.rotation = Camera.main.transform.rotation;
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * _alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        //월드좌표를 스크린좌표로 변경
        var _screenPos = Camera.main.WorldToScreenPoint(_mobPos.position + offset);

        var _localPos = Vector2.zero;
        //스크린좌표를 캔버스에서 사용할수 있는 좌표로 변경
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectParent, _screenPos, _cam, out _localPos);
        _rectText.localPosition = _localPos;
    }
}
