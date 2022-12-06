using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager
{
    // 메인카메라는 추후 각 맵마다 첫 위치를 지정해야 될 수 있음 (1안)
    // 추후 각 맵마다 카메라 시작 위치를 정해야 함
    public GameObject _player;

    public GameObject _mainCamera;
    public GameObject _stateCamera;
    public GameObject _miniMapCamera;
    public GameObject _uiParticleCamera;
    public Camera _uiParticleCam;

    // 메인 카메라 대용
    public GameObject _vam1;
    // 웨슬리 카메라
    public GameObject _vam2;
    // 베니스 카메라
    public GameObject _vam3;


    public CinemachineVirtualCamera _Vcam1;
    public CinemachineVirtualCamera _Vcam2;
    public CinemachineVirtualCamera _Vcam3;

    public MainCameraController _mainCameraController;
    public StateCameraController _stateCameraController;
    public MiniMapCameraController _miniMapCameraController;
    public CinemachineController _cinemachineController;

    // 씬 이동을 위한 카메라 위치정보 보관용 변수 >> MainCameraController에서 씬이 종료 될 때 마지막_oldPos를 여기에 넣어줌
    // 이후 다른 씬 로딩 할 때 이 데이터를 메인 카메라에 대입
    public Vector3 _mainCameraPos;

    public void Init()
    {
        GameObject go = new GameObject();
        go.name = "@Camera_Root";

        // 시네머신 브래인 역할 (껍대기)
        GameObject mainCamera = GameManager.Resource.GetCamera("Tutorial Main Camera");
        _mainCamera = GameObject.Instantiate<GameObject>(mainCamera);
        //_mainCameraController = _mainCamera.AddComponent<MainCameraController>();
        _mainCamera.transform.SetParent(go.transform);

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

        // v1 카메라 불러오기
        GameObject v1 = GameManager.Resource.GetCamera("CM vcam1");
        _vam1 = GameObject.Instantiate<GameObject>(v1);
        _Vcam1 = _vam1.GetComponent<CinemachineVirtualCamera>();
        _vam1.transform.SetParent(go.transform);
        // 메인카메라 역활을 v1이 대신 함
        _vam1.AddComponent<MainCameraController>();
        

        // v2 카메라 불러오기
        GameObject v2 = GameManager.Resource.GetCamera("CM vcam2");
        _vam2 = GameObject.Instantiate<GameObject>(v2);
        _Vcam2 = _vam2.GetComponent<CinemachineVirtualCamera>();
        _vam2.transform.SetParent(go.transform);
        _vam2.SetActive(false);

        // v3 카메라 불러오기
        GameObject v3 = GameManager.Resource.GetCamera("CM vcam3");
        _vam3 = GameObject.Instantiate<GameObject>(v3);
        _Vcam3 = _vam3.GetComponent<CinemachineVirtualCamera>();
        _vam3.transform.SetParent(go.transform);
        _vam3.SetActive(false);

        // UI 파티클용 카메라 불러오기
        GameObject uiParticleCamera = GameManager.Resource.GetCamera("UIParticleCamera");
        _uiParticleCamera = GameObject.Instantiate<GameObject>(uiParticleCamera);
        _uiParticleCam = _uiParticleCamera.GetComponent<Camera>();
        _uiParticleCamera.transform.SetParent(go.transform);
    }

    public void WeleyCamOn()
    {
        _vam2.SetActive(true);
    }

    public void WeleyCamOff()
    {
        _vam2.SetActive(false);
    }

    public void VeniceCamOn()
    {
        _vam3.SetActive(true);
    }

    public void VeniceCamOff()
    {
        _vam3.SetActive(false);
    }
}



