using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ʻ�ȭ ������ ��ųâ ������ ��ũ��Ʈ
public class PortraitController : MonoBehaviour
{
    Button _button;

    private void Start()
    {
        // FindChild ����Լ��� �̿��ؼ� ��ư�� ã��
        Transform tr = Util.FindChild("Image", gameObject.transform);
        _button = tr.GetComponent<Button>();
        // ��ư�� �ڵ�� ������
        _button.onClick.AddListener(OnPortraitClick);

        // ��¦�̴� ����Ʈ �߰��Ϸ���...
        // ī�޶� ������ ���� >> ī�޶� �Ŵ������� ����
        // ĵ���� ������ ���� 
        // ��ƼŬ ������ �;� ��

        // ��Ʈ����Ʈ�� ��ġ ����
        Canvas portCanvas = GetComponent<Canvas>();
        portCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        portCanvas.worldCamera = GameManager.Cam._uiParticleCam;
        //_portrait.transform.SetParent(_playerHpBar.transform);

    }
    // ��ų ����â ������ �Լ�
    public void OnPortraitClick()
    {
        GameManager.Ui._skillViewController.gameObject.SetActive(true);
    }

    // ���� ��ƼŬ �Ŵ������� �ؾ� �� ��?
    // ������ �� ��Ʈ����Ʈ�� ����Ʈ ����
    // ���� �ٸ� ��ƼŬ�� ������ ī�޶� ���� ���°� ����
}
