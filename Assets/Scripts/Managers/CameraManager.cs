using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    // ����ī�޶�� ���� �� �ʸ��� ù ��ġ�� �����ؾ� �� �� ���� (1��)
    // ���� �� �ʸ��� ī�޶� ���� ��ġ�� ���ؾ� ��
    public GameObject _mainCamera;
    public GameObject _stateCamera;
    public GameObject _miniMapCamera;
    public MainCameraController _mainCameraController;
    public StateCameraController _stateCameraController;
    public MiniMapCameraController _miniMapCameraController;

    // �� �̵��� ���� ī�޶� ��ġ���� ������ ���� >> MainCameraController���� ���� ���� �� �� ������_oldPos�� ���⿡ �־���
    // ���� �ٸ� �� �ε� �� �� �� �����͸� ���� ī�޶� ����
    public Vector3 _mainCameraPos;

    public void Init()
    {
        GameObject go = new GameObject();
        go.name = "@Camera_Root";

        // ���� ī�޶� �ҷ�����
        GameObject mainCamera = GameManager.Resource.GetCamera("Tutorial Main Camera");
        _mainCamera = GameObject.Instantiate<GameObject>(mainCamera);
        _mainCameraController = _mainCamera.AddComponent<MainCameraController>();
        _mainCameraController.transform.SetParent(go.transform);

        // ����â ī�޶� �ҷ�����
        GameObject stateCamera = GameManager.Resource.GetCamera("stateCamera");
        _stateCamera = GameObject.Instantiate<GameObject>(stateCamera);
        _stateCameraController = _stateCamera.AddComponent<StateCameraController>();
        _stateCameraController.transform.SetParent(go.transform);

        // �̴ϸ� ī�޶� �ҷ�����
        GameObject miniMapCamera = GameManager.Resource.GetCamera("MiniMapCamera");
        _miniMapCamera = GameObject.Instantiate<GameObject>(miniMapCamera);
        _miniMapCameraController = _miniMapCamera.AddComponent<MiniMapCameraController>();
        _miniMapCameraController.transform.SetParent(go.transform);
    }
}



