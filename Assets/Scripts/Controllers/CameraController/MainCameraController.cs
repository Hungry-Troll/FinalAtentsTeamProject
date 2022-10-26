using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject _player;
    Vector3 _oldPos;
    Vector3 _cameraPos;
    Quaternion _cameraRot;
    // Start is called before the first frame update
    void Start()
    {
        SceneCheck();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 따라가는 카메라
        Vector3 delta = _player.transform.position - _oldPos;
        transform.position = transform.position + delta;
        _oldPos = _player.transform.position;
    }

    private void SceneCheck()
    {
        switch (GameManager.Scene._sceneNameEnum)
        {
            case Define.SceneName.Tutorial:
                TutorialStart();
                break;
            case Define.SceneName.Village02:
                Village02Start();
                break;
            case Define.SceneName.DunGeon:
                //DunGeonStart();
                break;
        }
    }

    private void TutorialStart()
    {
        // 듀토리얼맵 카메라 시작 위치
        _cameraPos = new Vector3(-65f, 13f, -63f);
        _cameraRot = Quaternion.Euler(57f, 0f, 0f);
        transform.position = _cameraPos;
        transform.rotation = _cameraRot;

        _player = GameObject.FindWithTag("Player");
        _oldPos = _player.transform.position;
        FirstMainCamPosCal();
    }

    private void Village02Start()
    {
        // 마을맵 카메라 시작 위치
        _player = GameObject.FindWithTag("Player");
        _oldPos = _player.transform.position;
        
        // 카메라 매니저에서 기존 카메라 위치 정보를 가지고 옴 // 추후 카메라 시점 변경시 코드 수정 필요
        _cameraPos = GameManager.Cam._mainCameraPos + _oldPos;
        _cameraRot = Quaternion.Euler(57f, 0f, 0f);
        transform.position = _cameraPos;
        transform.rotation = _cameraRot;
    }
    // 카메라 위치 구하는 함수 // 카메라 시점 변경시 코드 수정 필요
    private void FirstMainCamPosCal()
    {
        // 카메라 벡터 계산해서 카메라매니저에 넣어둠
        GameManager.Cam._mainCameraPos = _cameraPos - _player.transform.position;
    }

    private void DistanceCal()
    {

    }

    private void LastMainCameraPos()
    {


    }
}
