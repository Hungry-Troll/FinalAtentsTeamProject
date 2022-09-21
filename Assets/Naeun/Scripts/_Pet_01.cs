using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class _Pet_01 : MonoBehaviour
{

    Animator animator;                                          
    //Vector3 targetPos;
    //
    public float moveSpeed;                                      
    public float rotSpeed;
    Transform target;
    float distance;

    public Transform player;
    NavMeshAgent nav;
    public bool isChase;
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        Invoke("ChaseStart", 1);
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Monster").transform;
        // 플레이어 정보는 게임매니저 오브젝트 매니저에서 스크립트 타입으로 들고있음.
        PlayerController playerController = GameManager.Obj._playerController;
        player = playerController.transform;
    }

    void ChaseStart()
    {
        isChase = true;
        
    }
    void Update()
    {
        if (isChase)
        {
            Vector3 movePos = player.transform.position - (new Vector3(-2, 0, 2));
            
            //Debug.Log($"{movePos} // {transform.position}");

            float disdis = Vector3.Distance(transform.position, movePos);
            //Debug.Log(disdis);
            if(target != null)
            {
                distance = Vector3.Distance(target.transform.position, movePos);
            }
            //Debug.Log(distance);

            if (distance <= 3f)
            {
                //Debug.Log("공격");
                transform.LookAt(target.transform);
                animator.SetInteger("aniIndex", 2);
            }else if (transform.position == movePos || disdis < 0.8f )
            {
                animator.SetInteger("aniIndex", 0);
                transform.LookAt(player);
            }
            else
            {
                animator.SetInteger("aniIndex", 1);
                nav.SetDestination(movePos);

                //Vector3 rot = nav.steeringTarget - movePos;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rot), Time.deltaTime * rotSpeed);

                transform.LookAt(player);


            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {  
            //animator.SetBool("isDead", true);
            animator.SetTrigger("isDead");

            isChase =false;
            nav.enabled = false;
            //gameObject.SetActive(false);
            Destroy(this.gameObject, 2f);
        }

    }


}
