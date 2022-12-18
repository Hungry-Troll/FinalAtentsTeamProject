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
    public GameObject NameCam;

    // ���� ī�޶� ���
    public GameObject _vam1;
    // ������ ī�޶�
    public GameObject _vam2;
    // ���Ͻ� ī�޶�
    public GameObject _vam3;
    //============================������
    // ���� �÷��̾�
    public GameObject _vam4;
    // ���� ��������
    public GameObject _vam5;
    // ���� ū����
    public GameObject _vam6;


    public CinemachineVirtualCamera _Vcam1;
    public CinemachineVirtualCamera _Vcam2;
    public CinemachineVirtualCamera _Vcam3;
    //===������ ī�޶�
    public CinemachineVirtualCamera _Vcam4;
    public CinemachineVirtualCamera _Vcam5;
    public CinemachineVirtualCamera _Vcam6;

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

        ///=====================================�������� �� �ó׸ӽŵ�
        
        // v4 ī�޶� �ҷ�����
        GameObject v4 = GameManager.Resource.GetCamera("CM vcam4");
        _vam4 = GameObject.Instantiate<GameObject>(v4);
        _Vcam4 = _vam4.GetComponent<CinemachineVirtualCamera>();
        _vam4.transform.SetParent(go.transform);
        _vam4.SetActive(false);

        // v5 ī�޶� �ҷ�����
        GameObject v5 = GameManager.Resource.GetCamera("CM vcam5");
        _vam5 = GameObject.Instantiate<GameObject>(v5);
        _Vcam5 = _vam5.GetComponent<CinemachineVirtualCamera>();
        _vam5.transform.SetParent(go.transform);
        _vam5.SetActive(false);

        // v6 ī�޶� �ҷ�����
        GameObject v6 = GameManager.Resource.GetCamera("CM vcam6");
        _vam6 = GameObject.Instantiate<GameObject>(v6);
        _Vcam6 = _vam6.GetComponent<CinemachineVirtualCamera>();
        _vam6.transform.SetParent(go.transform);
        _vam6.SetActive(false);

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
        if(GameManager.Scene._sceneNameEnum == Define.SceneName.DunGeon)
        {
            _vam3.transform.position = new Vector3((float)60.70, (float)11.51, (float)126.25);
        }
    }

    public void VeniceCamOff()
    {
        _vam3.SetActive(false);
    }

    public void BossCamOn1()
    {
        _vam4.SetActive(true);
    }
    public void BossCamOff1()
    {
        _vam4.SetActive(false);
    }
    public void BossCamOn2()
    {
        _vam5.SetActive(true);
    }

    public void BossCamOff2()
    {
        _vam5.SetActive(false);
    }

    public void BossCamOn3()
    {
        _vam6.SetActive(true);
    }
    public void BossCamOff3()
    {
        _vam6.SetActive(false);
    }

}



