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

        // �����ϱ����� �ʱ�ȭ
        // ����Ʈ �ʱ�ȭ
        _text.DOFade(1f, 0.0f);
        // ��ġ �ʱ�ȭ
        Vector3 offset = new Vector3(0, 0, 0);

        // ������ ���ڸ� ���ڷ� ��ȯ
        _text.text = _damage.ToString();

        // ������ �� ��ġ y ��
        offset = Vector3.up * (gameObject.GetComponentInParent<NavMeshAgent>().height);
        _y = offset.y;
        // ������ �� ��ġ (������ǥ)
        _rectText.localPosition = new Vector3(0, _y, 0);

        // ���� DOTween �ڵ�
        float power = 1.0f;
        _text.color = _damage > 30 ? Color.red : Color.white;
        _text.text = _damage.ToString();

        // ���� ��������...
        _text.DOFade(0f, 1.0f);

        // �۾� ũ�� ����
        transform.DOPunchScale(Vector3.one * 0.03f, 0.2f, 1, 0);

        Vector3 endPos = transform.position + new Vector3(0, _y, 0);
        endPos.x += (Random.insideUnitCircle * power).x;


        // �۾� ���� ȿ��
        transform.DOJump(endPos, power, 1, 1.5f).OnComplete(() =>
        // �۾� ������ ������ ��Ȱ��ȭ�ϰ� ������ؽ�ƮǮ�� �ٽ� ����
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(GameManager.DamText._damageTextPool.transform);
            GameManager.DamText._damageTextList.Add(this.gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶� �������� ȸ��
        transform.rotation = Camera.main.transform.rotation;
    }
}
