using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator ani;
    public TrailRenderer swordEffect;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(skill1());
        }
    }
    IEnumerator skill1()
    {
        swordEffect.enabled = true;
        ani.SetInteger("playerStat", 5);
        yield return new WaitForSeconds(0.2f);
        ani.SetInteger("playerStat", 51);
        yield return new WaitForSeconds(0.2f);
        ani.SetInteger("playerStat", 52);
        yield return new WaitForSeconds(0.7f);
        swordEffect.enabled = false;
        ani.SetInteger("playerStat", 0);
    }
}
