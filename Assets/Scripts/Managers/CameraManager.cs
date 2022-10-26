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

    // 씬 이동을 위한 카메라 위치정보 보관용 변수 >> MainCameraController에서 씬이 종료 될 때 마지막_oldPos를 여기에 넣어줌
    // 이후 다른 씬 로딩 할 때 이 데이터를 메인 카메라에 대입
    public Vector3 _mainCameraPos;

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
}



