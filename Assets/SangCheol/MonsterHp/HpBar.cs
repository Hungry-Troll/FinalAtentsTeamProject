using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    private Camera _cam;
    private Canvas _canvas;
    private RectTransform _rectParent;
    private RectTransform _rectHp;

    //ó���� (0,0,0)���� ����
    [HideInInspector] public Vector3 offset = Vector3.zero;
    //���� ��ġ
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

    //ĳ���Ͱ� �������Ŀ� HpBar�� �� ��ǥ�� �޾Ƽ� ���������ϱ⶧���� FixedUpdate���
    private void FixedUpdate()
    {
        //������ǥ�� ��ũ����ǥ�� ����
        var _screenPos = Camera.main.WorldToScreenPoint(_mobPos.position + offset);

        //��� �÷��̾ ���� �ڷ� ������ HpBar�� �������°� ����
        //�츮 ������Ʈ������ ��� �����ҵ���?
        if (_screenPos.z < 0.0f)
        {
            _screenPos *= -1.0f;
        }

        var _localPos = Vector2.zero;
        //��ũ����ǥ�� ĵ�������� ����Ҽ� �ִ� ��ǥ�� ����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectParent, _screenPos, _cam, out _localPos);
        _rectHp.localPosition = _localPos;
    }
}
