using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    // ����ī�޶�� ���� �� �ʸ��� ù ��ġ�� �����ؾ� �� �� ���� (1��)
    // ���� �� �ʸ��� ī�޶� ���� ��ġ�� ���ؾ� ��

    public GameObject _mainCamera;
    public GameObject _stateCamera;
    public MainCameraController _mainCameraController;
    public StateCameraController _stateCameraController;
        // Start is called before the first frame update
        public void Init()
    {
        // ���� ī�޶� �ҷ�����
        GameObject mainCamera = GameManager.Resource.GetCamera("Tutorial Main Camera");
        _mainCamera = GameObject.Instantiate<GameObject>(mainCamera);
        _mainCameraController = _mainCamera.AddComponent<MainCameraController>();

        // ����â ī�޶� �ҷ�����
        GameObject stateCamera = GameManager.Resource.GetCamera("stateCamera");
        _stateCamera = GameObject.Instantiate<GameObject>(stateCamera);
        _stateCameraController = _stateCamera.AddComponent<StateCameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
