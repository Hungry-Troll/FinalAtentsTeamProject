using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class UiDesign3 : MonoBehaviour
{
    public Text jobName;
    public Text jobInformation;
    public InputField nickname;

    public Text wSkill;
    public Text aSkill;
    public Text sSkill;

    public Image SkillInfo;


    Animator ani;
    
    GameObject job;
    GameObject playerObject;
    GameObject nicknameObject;

    bool WButtom = false;
    bool AButtom = false;
    bool SButtom = false;

    int pickNumber;


    void Start()
    {
        pickNumber = 0;
        nicknameObject = GameObject.Find("Canvas/NicknameObject");
        Debug.Log(nicknameObject);
        nicknameObject.gameObject.SetActive(false);
        SkillInfo.gameObject.SetActive(false);
        // BGM �߰�
        GameManager.Sound.BGMPlay("Sky Is the Limit _ by Supreme Devices (Epic Music World)");
    }
    //�Қ�Ʈ�� Ȯ�ο�
    void Update()
    {
        if(WButtom)
        {
            pickNumber = 1;
            jobName.text = "��ȭ�ΰ�";
            jobInformation.text = "�������� ��Ÿ�Ͽ� �뷱������ Ư¡";
            characterGeneration("SuperhumanCharacterSelect");
            SkillInfo.gameObject.SetActive(true);
            wSkill.text = "����";
            //SkillInfo.sprite = wSkill;
            WButtom = false;
            //������ ������ ���ӸŴ������� ����
            GameManager.Select._jobName = "Superhuman";
        }

        if(AButtom)
        {
            pickNumber = 2;
            jobName.text = "���̺���";
            jobInformation.text = "���Ÿ����� ��Ÿ�Ͽ� �������� �������� Ư¡";
            characterGeneration("CyborgCharacterSelect");
            SkillInfo.gameObject.SetActive(true);
            aSkill.text = "����";
            //SkillInfo.sprite = aSkill;
            AButtom = false;
            //������ ������ ���ӸŴ������� ����
            GameManager.Select._jobName = "Cyborg";
        }

        if(SButtom)
        {
            pickNumber = 3;
            jobName.text = "������";
            jobInformation.text = "���Ÿ����� ��Ÿ�Ͽ� �������� Ư¡";
            characterGeneration("ScientistCharacterSelect");
            SkillInfo.gameObject.SetActive(true);
            //SkillInfo.sprite = sSkill;
            sSkill.text = "����";
            SButtom = false;
            //������ ������ ���ӸŴ������� ����
            GameManager.Select._jobName = "Scientist";
        }


    }
    
    //ĳ���͸� ���ҽ��� �ҷ����� �ڵ�
    void characterGeneration(string _name)
    {
        Debug.Log(_name);
        if (playerObject != null)
        {
            Destroy(playerObject);
        }
        job = Resources.Load<GameObject>("Prefabs/Character_Prefab/" + _name);
        playerObject = GameObject.Instantiate<GameObject>(job);
        playerObject.transform.position = new Vector3(0, 0, 0);
        playerObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        ani = playerObject.GetComponent<Animator>();
        ani.SetInteger("Index", 1);
    }

    //���ϴ� ĳ������ ��ư�� ������ �ϱ� ���� �ڵ�
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
    //start��ư�� ���콺�ø��� �ִϸ��̼��� �ൿ
    public void StartPointerEnter()
    {
        if(ani != null)
        {
            ani.SetInteger("Index",2);
        }
    }
    public void StartPointerExit()
    {
        if(ani != null)
        {
            ani.SetInteger("Index", 0);
        }
    }
    //�ٸ� ĳ���͸� ������ �ִϸ��̼��� �����ϰ� �ൿ
    public void PickPointerEnter(int _number)
    {
        if(ani != null)
        {
            if(pickNumber != _number)
            {
                ani.SetInteger("Index", 3);
            }
        }
    }
    public void PickPointerExit()
    {
        Debug.Log("0");
        if (ani != null)
        {
            
            ani.SetInteger("Index", 0);
        }
    }
    //ĳ���Ͱ� ������ �Ǹ� ��ų������Ʈ�� ���̰� ����
    public void SkillCheckPointerEnter()
    {
        if (SkillInfo.sprite != null)
        {
            SkillInfo.gameObject.SetActive(true);
        }
        
    }
    public void SkillCheckPointerExit()
    {
        SkillInfo.gameObject.SetActive(false);
    }
    //ĳ���͸� �����ϰ� ���۹�ư�� ������ �г���â�� �߰� ����
    public void StartButton()
    {
        if(pickNumber !=0)
        {
            nicknameObject.gameObject.SetActive(true);
        }
    }
    public void NicknameconfirmedButton()
    {
        if (CheckNickname() == true)
        {
            Debug.Log(nickname.text + "�� ����");
            // ĳ���� �̸� ������
            GameManager.Select._playerName = nickname.text;
            Debug.Log(GameManager.Select._playerName);

            // ���� ������ �ѱ�
            GameManager.Scene.LoadScene("SelectPet");
        }
        else
        {
            Debug.Log(nickname.text + "�� �Ұ���");
            nickname.text = null;
        }
    }
    public void CancelButton()
    {
        nicknameObject.gameObject.SetActive(false);
    }
    //������ �г����� Text�� �ۼ��ϸ� ����,����,�ѱ۸� ��밡���ϰ� �ϴ� �ڵ�
    //https://mrbinggrae.tistory.com/175
    private bool CheckNickname()
    {
        return Regex.IsMatch(nickname.text, "^[0-9a-zA-Z��-�R]*$");
    }
}
