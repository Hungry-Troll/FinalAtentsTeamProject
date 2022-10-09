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
    public float repeatRate = 3f;
    private float timer = 0;
    

    private void Awake()
    {
        pos = transform.position;
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // 인보크를 사용하지 않아도 정상작동 하지 않나용?
        //Invoke("ChaseStart", 1);
        ChaseStart();
    }
    void Start()
    {
        //  오브젝트매니저에서 게임 시작할 때 생성한 플레이어 정보를 이용해서 연결
        //player= GameManager.Obj._playerController.transform;
        player = GameObject.Find("Player").transform;
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
            //Debug.Log(mondis);
            //Debug.Log(distance);
            transform.LookAt(player);

            if (distance >= 5f)
            {
                //Debug.Log("걷자");
                animator.SetInteger("Pet_ani", 1);      //걷기
                nav.SetDestination(player.transform.position);
                transform.LookAt(player);
            }
            //몬스터와의 거리가 3 이하면 공격하기
            if (mondis <= 3f)       
            {
                
                    Debug.Log("공격" );
                    animator.SetInteger("Pet_ani", 2);
                    transform.LookAt(target);
                    //Debug.Log(mondis);    
                    int rnd = Random.Range(0, 100);
                if (timer < 0 && rnd>50)
                {
                    Debug.Log("스킬" + rnd);
                    animator.SetInteger("Pet_ani", 3);
                    transform.LookAt(target);
                    timer = repeatRate;
                }
                timer -= Time.deltaTime;
            }

            //몬스터와의 거리가 3이상, 플레이어와의 거리가 5이하면 멈추기
            if (mondis > 3f && distance < 5 )   
            {
                //Debug.Log("멈춰");
                //animator.ResetTrigger("Skill");
                animator.SetInteger("Pet_ani", 0);
                transform.LookAt(player);
                //Debug.Log(distance);
            }
                    
                
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isDead");
            Debug.Log("죽기");
            nav.enabled = false;
            isChase = false;
            StartCoroutine("Dead");
            
            //StopCoroutine("Dead");
            //StartCoroutine("Revival");
            
        }

    }
    IEnumerator Dead()
    {

        yield return new WaitForSeconds(5f);
        Debug.Log("비활성화");
        gameObject.SetActive(false);
        
        //StopCoroutine("Dead");
        gameObject.SetActive(true);
        StartCoroutine("Revival");
        
        
    }
    IEnumerator Revival()
    {   
        //StopCoroutine("Dead");
        yield return new WaitForSeconds(2f);
        
        Debug.Log("활성화");
        animator.SetInteger("Pet_ani", 0);
        nav.enabled = true;
        isChase = true;
        StopCoroutine("Revival");

    }

}


