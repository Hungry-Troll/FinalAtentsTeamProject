using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    // 메인카메라는 추후 각 맵마다 첫 위치를 지정해야 될 수 있음 (1안)
    // 추후 각 맵마다 카메라 시작 위치를 정해야 함

    public GameObject _mainCamera;
    public GameObject _stateCamera;
    public GameObject _miniMapCamera;
    public MainCameraController _mainCameraController;
    public StateCameraController _stateCameraController;
    public MiniMapCameraController _miniMapCameraController;
    // Start is called before the first frame update
    public void Init()
    {
        GameObject go = new GameObject();
        go.name = "@Camera_Root";

        // 메인 카메라 불러오기
        GameObject mainCamera = GameManager.Resource.GetCamera("Tutorial Main Camera");
        _mainCamera = GameObject.Instantiate<GameObject>(mainCamera);
        _mainCameraController = _mainCamera.AddComponent<MainCameraController>();
        _mainCameraController.transform.SetParent(go.transform);

        // 상태창 카메라 불러오기
        GameObject stateCamera = GameManager.Resource.GetCamera("stateCamera");
        _stateCamera = GameObject.Instantiate<GameObject>(stateCamera);
        _stateCameraController = _stateCamera.AddComponent<StateCameraController>();
        _stateCameraController.transform.SetParent(go.transform);

        // 미니맵 카메라 불러오기
        GameObject miniMapCamera = GameManager.Resource.GetCamera("MiniMapCamera");
        _miniMapCamera = GameObject.Instantiate<GameObject>(miniMapCamera);
        _miniMapCameraController = _miniMapCamera.AddComponent<MiniMapCameraController>();
        _miniMapCameraController.transform.SetParent(go.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
