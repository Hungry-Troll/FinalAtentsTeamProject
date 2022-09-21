using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCameraController : MonoBehaviour
{
    Vector3 _cameraPos;
    Quaternion _cameraRot;
    // Start is called before the first frame update
    void Start()
    {
        // 상태창 플레이어 프리팹
        transform.position = GameManager.Ui._statePlayerObj.transform.position;
        transform.SetParent(GameManager.Ui._statePlayerObj.transform);

        // 상태창 카메라 시작 위치
        _cameraPos = new Vector3(0f, 1f, 2f);
        _cameraRot = Quaternion.Euler(3f, 180f, 0f);
        transform.position += _cameraPos;
        transform.rotation = _cameraRot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
