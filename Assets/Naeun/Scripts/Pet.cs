using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Pet : MonoBehaviour
{
    Animator animator;
    NavMeshAgent nav;
    Transform player;
    Transform target;
    Vector3 pos;
    public bool isChase;
    private void Awake()
    {
        pos = transform.position;
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        Invoke("ChaseStart", 1);
    }
    void Start()
    {
        //  오브젝트매니저에서 게임 시작할 때 생성한 플레이어 정보를 이용해서 연결
        player= GameManager.Obj._playerController.transform;
        //player = GameObject.Find("Player").transform;
        target = GameObject.FindGameObjectWithTag("Monster").transform;
    }
    void ChaseStart()
    {
        isChase = true;

    }

    void Update()
    {
        if (isChase)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            float mondis = Vector3.Distance(transform.position, target.transform.position);
            Debug.Log(mondis);
            Debug.Log(distance);
            transform.LookAt(player);

            if (distance >= 5f)
            {
                animator.SetInteger("Pet_ani", 1);
                nav.SetDestination(player.transform.position);
                transform.LookAt(player);
            }
            else if (mondis <= 3f)
            {
                Debug.Log("공격");
                animator.SetInteger("Pet_ani", 2);
                transform.LookAt(target);
                Debug.Log(mondis);
            }
            else if (distance < 5f)
            {
                animator.SetInteger("Pet_ani", 0);
                transform.LookAt(player);
                Debug.Log(distance);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isDead");
            nav.enabled = false;
            isChase = false;
            Destroy(this.gameObject, 2f);

        }
    }
}
