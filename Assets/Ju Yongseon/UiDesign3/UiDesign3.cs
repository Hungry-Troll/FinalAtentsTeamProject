using UnityEngine;
using UnityEngine.UI;

public class UiDesign3 : MonoBehaviour
{
    public GameObject warriorB;
    public GameObject ArcherB;
    public GameObject ScientistB;

    public Text jobName;
    public Text jobInformation;

    SpriteRenderer sr;

    bool WButtom = false;
    bool AButtom = false;
    bool SButtom = false;

    GameObject job;
    GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        sr = warriorB.GetComponent<SpriteRenderer>();
    }
    //�ҽ�Ʈ�� Ȯ�ο�
    void Update()
    {
        if(WButtom)
        {
            Debug.Log("Warrior");

            job = Resources.Load<GameObject>("PeoplePrefabs/man-viking");
            playerObject = GameObject.Instantiate<GameObject>(job);
            playerObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            jobName.text = "����";
            jobInformation.text = "�������� ��Ÿ�Ͽ� �뷱������ Ư¡";
            characterGeneration("viking");

            WButtom = false;
        }

        if(AButtom)
        {
            Debug.Log("Archer");
            jobName.text = "�ü�";
            jobInformation.text = "���Ÿ����� ��Ÿ�Ͽ� �������� �������� Ư¡";
            characterGeneration("soldier");
            AButtom = false;
        }

        if(SButtom)
        {
            Debug.Log("Scientist");
            jobName.text = "������";
            jobInformation.text = "���⸮���� ��Ÿ�Ͽ� �������� Ư¡";
            characterGeneration("doctor");
            SButtom = false;
        }


    }
    
    void characterGeneration(string _name)
    {
        if (playerObject != null)
        {
            Destroy(playerObject);
        }
        job = Resources.Load<GameObject>("PeoplePrefabs/man-" + _name);
        playerObject = GameObject.Instantiate<GameObject>(job);
        playerObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public void WarriorButtom()
    {
        WButtom = true;
        AButtom = false;
        SButtom = false;
    }

    public void ArcherButtom()
    {
        WButtom = false;
        AButtom = true;
        SButtom = false;
    }

    public void ScientistButtom()
    {
        WButtom = false;
        AButtom = false;
        SButtom = true;
    }


}
