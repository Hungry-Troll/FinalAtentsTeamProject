using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Practice : MonoBehaviour
{
    Animator MerAnimator;
    public GameObject MerPrefab;
    public GameObject[] MerDialogs;
    public GameObject man_guy_Rig;

    void Awake()
    {
        MerAnimator = MerPrefab.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayStruct;
            if (Physics.Raycast(ray, out RayStruct, 10f))
            {
                if (RayStruct.collider.tag == "Merchant")
                {
                    MerDialogs[0].SetActive(true);
                    MerAnimator.SetTrigger("MeetPlayer");
                    CapsuleCollider collderOfMan_guy_Rig = man_guy_Rig.gameObject.GetComponent<CapsuleCollider>();
                    collderOfMan_guy_Rig.enabled = false;
                };
            }
        }
    }

    public void TalkWithMer()
    {

    }

    public void EndToTalkWithMer()
    {

    }
}
