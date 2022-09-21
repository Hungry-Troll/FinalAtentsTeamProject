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
    //소스트리 확인용
    void Update()
    {
        if(WButtom)
        {
            Debug.Log("Warrior");

            job = Resources.Load<GameObject>("PeoplePrefabs/man-viking");
            playerObject = GameObject.Instantiate<GameObject>(job);
            playerObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            jobName.text = "전사";
            jobInformation.text = "근접전투 스타일에 밸런스형이 특징";
            characterGeneration("viking");

            WButtom = false;
        }

        if(AButtom)
        {
            Debug.Log("Archer");
            jobName.text = "궁수";
            jobInformation.text = "원거리전투 스타일에 폭발적인 데미지가 특징";
            characterGeneration("soldier");
            AButtom = false;
        }

        if(SButtom)
        {
            Debug.Log("Scientist");
            jobName.text = "과학자";
            jobInformation.text = "원기리전투 스타일에 전략형이 특징";
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
