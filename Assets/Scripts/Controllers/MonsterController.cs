using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : MonoBehaviour
{
    public Define info;
    private Animator ani;

    public Transform _target;
    public Vector3 _mobpos;
    float _speed;
    float _rotateSpeed;
    float _attack;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetInteger("state", 0);
        _mobpos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _speed = 1.0f;
        _rotateSpeed = 360.0f;
        _attack = 2f;
        TargetSystem();
    }
    public void TargetSystem()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _speed = _speed * Time.deltaTime;
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance <= 5.0f && distance > _attack)
        {
            ani.SetInteger("state", 1);
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);

            Quaternion lookRotation = Quaternion.LookRotation(_target.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
        }
        else if (distance <= _attack)
        {
            ani.SetInteger("state", 2);
            _speed = 0.0f;
        }
        else if(this.transform.position == _mobpos)
        {
            ani.SetInteger("state", 0);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _mobpos, _speed);
            Quaternion lookRotation = Quaternion.LookRotation(_mobpos - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
        }
    }
}
