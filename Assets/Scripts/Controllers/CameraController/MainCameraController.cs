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
        //듀토리얼맵 카메라 시작 위치
        _cameraPos = new Vector3(-65f, 13f, -63f);
        _cameraRot = Quaternion.Euler(57f, 0f, 0f);
        transform.position = _cameraPos;
        transform.rotation = _cameraRot;

        _player = GameObject.FindWithTag("Player");
        _oldPos = _player.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        // 플레이어 따라가는 카메라
        Vector3 delta = _player.transform.position - _oldPos;
        transform.position = transform.position + delta;
        _oldPos = _player.transform.position;
    }
}
