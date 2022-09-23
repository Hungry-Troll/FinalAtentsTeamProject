using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public Transform _target;
    public float _offsetRatio;
    Camera _cam;

    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _targetVector = _target.forward;
        _targetVector.Normalize();
        Vector3 _targetPos = new Vector3(_target.transform.position.x, 90, _target.transform.position.z) + _targetVector * _offsetRatio;
        transform.position = _targetPos;
    }
}
