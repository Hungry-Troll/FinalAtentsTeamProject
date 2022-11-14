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
        // BGM Ãß°¡
        GameManager.Sound.BGMPlay("Sky Is the Limit _ by Supreme Devices (Epic Music World)");
    }
    //¼ÒšÀÆ®¸® È®ÀÎ¿ë
    void Update()
    {
        if(WButtom)
        {
            pickNumber = 1;
            jobName.text = "°­È­ÀÎ°£";
            jobInformation.text = "±ÙÁ¢ÀüÅõ ½ºÅ¸ÀÏ¿¡ ¹ë·±½ºÇüÀÌ Æ¯Â¡";
            characterGeneration("SuperhumanCharacterSelect");
            SkillInfo.gameObject.SetActive(true);
            wSkill.text = "¼³¸í";
            //SkillInfo.sprite = wSkill;
            WButtom = false;
            //¼±ÅÃÇÑ Á÷¾÷À» °ÔÀÓ¸Å´ÏÀú¿¡¼­ °ü¸®
            GameManager.Select._jobName = "Superhuman";
        }

        if(AButtom)
        {
            pickNumber = 2;
            jobName.text = "»çÀÌº¸±×";
            jobInformation.text = "¿ø°Å¸®ÀüÅõ ½ºÅ¸ÀÏ¿¡ Æø¹ßÀûÀÎ µ¥¹ÌÁö°¡ Æ¯Â¡";
            characterGeneration("CyborgCharacterSelect");
            SkillInfo.gameObject.SetActive(true);
            aSkill.text = "¼³¸í";
            //SkillInfo.sprite = aSkill;
            AButtom = false;
            //¼±ÅÃÇÑ Á÷¾÷À» °ÔÀÓ¸Å´ÏÀú¿¡¼­ °ü¸®
            GameManager.Select._jobName = "Cyborg";
        }

        if(SButtom)
        {
            pickNumber = 3;
            jobName.text = "°úÇÐÀÚ";
            jobInformation.text = "¿ø°Å¸®ÀüÅõ ½ºÅ¸ÀÏ¿¡ Àü·«ÇüÀÌ Æ¯Â¡";
            characterGeneration("ScientistCharacterSelect");
            SkillInfo.gameObject.SetActive(true);
            //SkillInfo.sprite = sSkill;
            sSkill.text = "¼³¸í";
            SButtom = false;
            //¼±ÅÃÇÑ Á÷¾÷À» °ÔÀÓ¸Å´ÏÀú¿¡¼­ °ü¸®
            GameManager.Select._jobName = "Scientist";
        }


    }
    
    //Ä³¸¯ÅÍ¸¦ ¸®¼Ò½º·Î ºÒ·¯¿À´Â ÄÚµå
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

    //¿øÇÏ´Â Ä³¸¯ÅÍÀÇ ¹öÆ°ÀÌ ´­¸®°Ô ÇÏ±â À§ÇÑ ÄÚµå
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
    //start¹öÆ°¿¡ ¸¶¿ì½º¿Ã¸®¸é ¾Ö´Ï¸ÞÀÌ¼ÇÀÌ Çàµ¿
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
    //´Ù¸¥ Ä³¸¯ÅÍ¸¦ ´©¸£¸é ¾Ö´Ï¸ÞÀÌ¼ÇÀÌ ¼­¿îÇÏ°Ô Çàµ¿
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
    //Ä³¸¯ÅÍ°¡ ¼±ÅÃÀÌ µÇ¸é ½ºÅ³¿ÀºêÁ§Æ®°¡ º¸ÀÌ°Ô ¼³Á¤
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
    //Ä³¸¯ÅÍ¸¦ ¼±ÅÃÇÏ°í ½ÃÀÛ¹öÆ°À» ´©¸£¸é ´Ð³×ÀÓÃ¢ÀÌ ¶ß°Ô ¼³Á¤
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
            Debug.Log(nickname.text + "Àº °¡´É");
            // Ä³¸¯ÅÍ ÀÌ¸§ ÀúÀåÇÔ
            GameManager.Select._playerName = nickname.text;
            Debug.Log(GameManager.Select._playerName);

            // ´ÙÀ½ ¾ÀÀ¸·Î ³Ñ±è
            GameManager.Scene.LoadScene("SelectPet");
        }
        else
        {
            Debug.Log(nickname.text + "Àº ºÒ°¡´É");
            nickname.text = null;
        }
    }
    public void CancelButton()
    {
        nicknameObject.gameObject.SetActive(false);
    }
    //À¯Àú°¡ ´Ð³×ÀÓÀ» Text¿¡ ÀÛ¼ºÇÏ¸é ¼ýÀÚ,¿µ¾î,ÇÑ±Û¸¸ »ç¿ë°¡´ÉÇÏ°Ô ÇÏ´Â ÄÚµå
    //https://mrbinggrae.tistory.com/175
    private bool CheckNickname()
    {
        return Regex.IsMatch(nickname.text, "^[0-9a-zA-Z°¡-ÆR]*$");
    }
}
