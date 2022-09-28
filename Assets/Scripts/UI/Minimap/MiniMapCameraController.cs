using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraController : MonoBehaviour
{
    public Transform _target;
    public float _offsetRatio;
    Camera _cam;

    void Start()
    {
        _target = GameManager.Obj._playerController.transform;
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _targetVector = _target.forward;
        _targetVector.Normalize();
        Vector3 _targetPos = new Vector3(_target.transform.position.x, 60, _target.transform.position.z) + _targetVector * _offsetRatio;
        transform.position = _targetPos;
    }
}
