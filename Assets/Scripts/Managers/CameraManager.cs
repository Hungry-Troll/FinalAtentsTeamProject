using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager
{
    // ����ī�޶�� ���� �� �ʸ��� ù ��ġ�� �����ؾ� �� �� ���� (1��)
    // ���� �� �ʸ��� ī�޶� ���� ��ġ�� ���ؾ� ��
    public GameObject _player;

    public GameObject _mainCamera;
    public GameObject _stateCamera;
    public GameObject _miniMapCamera;
    public GameObject _uiParticleCamera;
    public Camera _uiParticleCam;

    // ���� ī�޶� ���
    public GameObject _vam1;
    // ������ ī�޶�
    public GameObject _vam2;
    // ���Ͻ� ī�޶�
    public GameObject _vam3;


    public CinemachineVirtualCamera _Vcam1;
    public CinemachineVirtualCamera _Vcam2;
    public CinemachineVirtualCamera _Vcam3;

    public MainCameraController _mainCameraController;
    public StateCameraController _stateCameraController;
    public MiniMapCameraController _miniMapCameraController;
    public CinemachineController _cinemachineController;

    // �� �̵��� ���� ī�޶� ��ġ���� ������ ���� >> MainCameraController���� ���� ���� �� �� ������_oldPos�� ���⿡ �־���
    // ���� �ٸ� �� �ε� �� �� �� �����͸� ���� ī�޶� ����
    public Vector3 _mainCameraPos;

    public void Init()
    {
        GameObject go = new GameObject();
        go.name = "@Camera_Root";

        // �ó׸ӽ� �귡�� ���� (�����)
        GameObject mainCamera = GameManager.Resource.GetCamera("Tutorial Main Camera");
        _mainCamera = GameObject.Instantiate<GameObject>(mainCamera);
        //_mainCameraController = _mainCamera.AddComponent<MainCameraController>();
        _mainCamera.transform.SetParent(go.transform);

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

        // v1 ī�޶� �ҷ�����
        GameObject v1 = GameManager.Resource.GetCamera("CM vcam1");
        _vam1 = GameObject.Instantiate<GameObject>(v1);
        _Vcam1 = _vam1.GetComponent<CinemachineVirtualCamera>();
        _vam1.transform.SetParent(go.transform);
        // ����ī�޶� ��Ȱ�� v1�� ��� ��
        _vam1.AddComponent<MainCameraController>();
        

        // v2 ī�޶� �ҷ�����
        GameObject v2 = GameManager.Resource.GetCamera("CM vcam2");
        _vam2 = GameObject.Instantiate<GameObject>(v2);
        _Vcam2 = _vam2.GetComponent<CinemachineVirtualCamera>();
        _vam2.transform.SetParent(go.transform);
        _vam2.SetActive(false);

        // v3 ī�޶� �ҷ�����
        GameObject v3 = GameManager.Resource.GetCamera("CM vcam3");
        _vam3 = GameObject.Instantiate<GameObject>(v3);
        _Vcam3 = _vam3.GetComponent<CinemachineVirtualCamera>();
        _vam3.transform.SetParent(go.transform);
        _vam3.SetActive(false);

        // UI ��ƼŬ�� ī�޶� �ҷ�����
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



