using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public static TrailEffect _instance;

    public TrailRenderer _trailOne;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use()
    {
        StopCoroutine("Attack");
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        _trailOne.enabled = true;

        yield return new WaitForSeconds(0.3f);
        _trailOne.enabled = false;
    }
}
