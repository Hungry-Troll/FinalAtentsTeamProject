using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

// �ؽ�Ʈ �Ž� ���δ� �Ⱦ���� �ؼ� ���� �ʴ°ɷ�,,,

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
        // �θ� ������Ʈ�� ������ ����ó�� (���� ���ſ�)
        if(transform.parent.gameObject == null)
        {
            return;
        }
        _text = GetComponent<Text>();
        _canvas = GetComponentInParent<Canvas>();
        _rectText = GetComponent<RectTransform>();
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = Camera.main;
        
        // ������ ���ڸ� ���ڷ� ��ȯ
        _text.text = _damage.ToString();

        // ������ �� ��ġ y ��
        Vector3 offset = gameObject.transform.position + Vector3.up * (gameObject.GetComponentInParent<NavMeshAgent>().height);
        _y = offset.y;
        // ������ �� ��ġ (������ǥ)
        _rectText.localPosition = new Vector3(0, _y, 0);

        // ���� DOTween �ڵ�
        float power = 1.0f;
        _text.color = _damage > 30 ? Color.red : Color.white;
        _text.text = _damage.ToString();
        
        // null üũ
        if(_text != null)
        {
            // ���� ��������...
            _text.DOFade(0f, 1.0f);
        }
        
        // null üũ
        if(transform != null)
        {
            // �۾� ũ�� ����
            transform.DOPunchScale(Vector3.one * 0.03f, 0.2f, 1, 0);
        }
        Vector3 endPos = transform.position + new Vector3(0, _y, 0);
        endPos.x += (Random.insideUnitCircle * power).x;
        
        // null üũ
        if(transform != null)
        {
            // �۾� ���� ȿ��
            transform.DOJump(endPos, power, 1, 1.0f).OnComplete(() => { gameObject.SetActive(false)/*Destroy(gameObject)*/; });
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
