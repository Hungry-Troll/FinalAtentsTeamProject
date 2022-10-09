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

        // �κ�ũ�� ������� �ʾƵ� �����۵� ���� �ʳ���?
        //Invoke("ChaseStart", 1);
        ChaseStart();
    }
    void Start()
    {
        //  ������Ʈ�Ŵ������� ���� ������ �� ������ �÷��̾� ������ �̿��ؼ� ����
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
                //Debug.Log("����");
                animator.SetInteger("Pet_ani", 1);      //�ȱ�
                nav.SetDestination(player.transform.position);
                transform.LookAt(player);
            }
            //���Ϳ��� �Ÿ��� 3 ���ϸ� �����ϱ�
            if (mondis <= 3f)       
            {
                
                    Debug.Log("����" );
                    animator.SetInteger("Pet_ani", 2);
                    transform.LookAt(target);
                    //Debug.Log(mondis);    
                    int rnd = Random.Range(0, 100);
                if (timer < 0 && rnd>50)
                {
                    Debug.Log("��ų" + rnd);
                    animator.SetInteger("Pet_ani", 3);
                    transform.LookAt(target);
                    timer = repeatRate;
                }
                timer -= Time.deltaTime;
            }

            //���Ϳ��� �Ÿ��� 3�̻�, �÷��̾���� �Ÿ��� 5���ϸ� ���߱�
            if (mondis > 3f && distance < 5 )   
            {
                //Debug.Log("����");
                //animator.ResetTrigger("Skill");
                animator.SetInteger("Pet_ani", 0);
                transform.LookAt(player);
                //Debug.Log(distance);
            }
                    
                
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isDead");
            Debug.Log("�ױ�");
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
        Debug.Log("��Ȱ��ȭ");
        gameObject.SetActive(false);
        
        //StopCoroutine("Dead");
        gameObject.SetActive(true);
        StartCoroutine("Revival");
        
        
    }
    IEnumerator Revival()
    {   
        //StopCoroutine("Dead");
        yield return new WaitForSeconds(2f);
        
        Debug.Log("Ȱ��ȭ");
        animator.SetInteger("Pet_ani", 0);
        nav.enabled = true;
        isChase = true;
        StopCoroutine("Revival");

    }

}


