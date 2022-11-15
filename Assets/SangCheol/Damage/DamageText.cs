using System.Collections;
using System.Collections.Generic;
//TextMeshPro
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    //���� �����̴� �ӵ�
    private float _moveSpeed;
    //������� �ӵ�
    private float _alphaSpeed;
    //���� ������� �������� �ð�
    private float _destroyTime;
    //ui���� textMeshPro ����Ϸ��� TextMeshProUGUI�� ����ؾߵ�
    TextMeshProUGUI text;
    Color alpha;
    public int damage;

    //�Ʒ����ʹ� HpBar��ũ��Ʈ�� ���� ����
    private Camera _cam;
    private Canvas _canvas;
    private RectTransform _rectParent;
    private RectTransform _rectText;

    //ó���� (0,0,0)���� ����
    [HideInInspector] public Vector3 offset = Vector3.zero;
    //���� ��ġ
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
        //������ ���ڸ� ���ڷ� ��ȯ
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
        //������ǥ�� ��ũ����ǥ�� ����
        var _screenPos = Camera.main.WorldToScreenPoint(_mobPos.position + offset);

        var _localPos = Vector2.zero;
        //��ũ����ǥ�� ĵ�������� ����Ҽ� �ִ� ��ǥ�� ����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectParent, _screenPos, _cam, out _localPos);
        _rectText.localPosition = _localPos;
    }
}
