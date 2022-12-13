using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using LitJson;

[System.Serializable]
public class PlayData
{
    //������ ������ �� ĳ����, ���� ��� ��ġ, ����ǰ, ����, ���� ���� ����Ʈ

    // �÷��̾� ��ü
    [SerializeField]
    private TempPlayerStat _Player;

    // �÷��̾� �̸�
    [SerializeField]
    private string _Name;

    // ĳ����(����)
    [SerializeField]
    private string _Job;
    
    // ��
    [SerializeField]
    private string _Pet;

    // ���� �� �̸�
    [SerializeField]
    private string _Scene;
    
    // �κ��丮 ��Ҵ� ������ ���
    [SerializeField]
    private List<string> _Item_List = new List<string>();

    [SerializeField]
    private string _Weapon;

    // ���� �ִ� ��� ����
    [SerializeField]
    private int _Gold;

    // �÷��̾� ���� ��ų ���
    // SkillStat�� ���������� MonoBehaviour ��� ���� ���� Ŭ����
    [SerializeField]
    private List<TempSkillStat> _SkillInfo = new List<TempSkillStat>();

    // ���� ��ų ����Ʈ
    [SerializeField]
    private int _SkillPoint;

    public TempPlayerStat Player
    {
        get { return _Player; }
        set { _Player = value; }
    }

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    public string Job
    {
        get { return _Job; }
        set { _Job = value; }
    }
    public string Pet
    {
        get { return _Pet; }
        set { _Pet = value; }
    }
    public string Scene
    {
        get { return _Scene; }
        set { _Scene = value; }
    }
    public List<string> ItemList
    {
        get { return _Item_List; }
        set { _Item_List = value; }
    }
    public string Weapon
    {
        get { return _Weapon; }
        set { _Weapon = value; }
    }
    public int Gold
    {
        get { return _Gold; }
        set { _Gold = value; }
    }
    public List<TempSkillStat> SkillInfo
    {
        get { return _SkillInfo; }
        set { _SkillInfo = value; }
    }
    public int SkillPoint
    {
        get { return _SkillPoint; }
        set { _SkillPoint = value; }
    }
}

public class DataManager //: MonoBehaviour ���ӸŴ������� �����ϵ��� ����
{
    //public static DataManager instance;
    public PlayData playData = new PlayData();
    string path;
    string filename = "save";
    public int selectedSlot;
    // ���� �̵��Ҷ��� �� �ε尡 �ʿ� �����Ƿ� bool ���� �ϳ��� �߰��ϰ� LoadData() �Լ� �Ű������� ���
    bool _sceneLoad;
    private List<TempSkillStat> _skillInfo;

    // Start is called before the first frame update
    void Awake()
    {
        #region �̱���
/*        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);*/
        #endregion// �̱����� ���ӸŴ������� �����ϵ��� ����

        //path = Application.persistentDataPath + "/";
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";
    }

    void Init()
    {
        //path = Application.persistentDataPath + "/";
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";
    }

    // ���� : PlayData Ÿ������ ���� �� json ���� ����
    public void SaveData()
    {
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";

        // �÷��̾� ��ü ObjectManager���� �����ͼ� ����
        if(GameManager.Obj._playerStat != null)
        {
            // Ÿ�Ը� TempPlayerStat ���� �ٲ㼭 ����(MonoBehaviour ��ӹ��� ���� ����)
            playData.Player 
                = GameManager.Parse.SwitchPlayerStatType(GameManager.Obj._playerStat, new TempPlayerStat());
        }

        // �÷��̾� �̸� SelectManager���� �����ͼ� ����
        if(GameManager.Select._playerName != null)
        {
            // null �ƴ� ���� ���
            playData.Name = GameManager.Select._playerName;
        }

        // ĳ���� ���� â���� ��Ƶ� ���� ����(UiDesign2.cs)
        //playData.Job = GameManager.Data.playData.Job;
        // ĳ���� ����â ����(UiDesign2 -> UiDesign3), SelectManager ���� ���������� ����
        if(GameManager.Select._jobName != null)
        {
            // null �ƴ� ���� ���
            playData.Job = GameManager.Select._jobName;
        }

        // �� �̸� ����
        if (GameManager.Select._petName != null)
        {
            // null �ƴ� ���� ���
            playData.Pet = GameManager.Select._petName;
        }

        // SceneManagerEX ���� ������ ���� �� �̸�
        playData.Scene = GameManager.Scene._LoadSceneName;

        // UiManager  _inventoryController._weapon �� ����������
        // ���� �����۵� ���� �ʿ�
        if (GameManager.Ui._inventoryController._weapon != null)
        {
            // null �ƴ� ���� ���
            playData.Weapon = GameManager.Ui._inventoryController._weapon.name;
        }

        // UiManager ���� ������ ������ ����Ʈ
        // �κ��丮 ��Ʈ�ѷ����� �ΰ˻�
        if (GameManager.Ui._inventoryController != null)
        {
            // null �ƴҶ��� ����
            if(GameManager.Ui._inventoryController._item != null)
            {
                foreach(GameObject one in GameManager.Ui._inventoryController._item)
                {
                    // �κ��丮 ������ �̸����� ����
                    playData.ItemList.Add(one.name);
                }
            }
        }

        // ���� ���� ��� ����
        playData.Gold = GameManager.Obj._goldController.GoldAmount;

        // ��ų ��� ����
        // ��Ʈ�ѷ����� null üũ
        if(GameManager.Ui._skillViewController != null)
        {
            // _skillStat ���� null üũ
            if(GameManager.Ui._skillViewController._playerSkillList != null)
            {
                // ����
                foreach(TempSkillStat one in GameManager.Ui._skillViewController._playerSkillList)
                {
                    // ������ ������ ���� ������
                    if(_skillInfo == null)
                    {
                        // �׳� �����ϸ� ��
                        playData.SkillInfo.Add(one);
                    }
                    else
                    {
                        // �ߺ� üũ ����
                        bool isDuplicate = false;
                        // ����� TempSkillStat ��ü ��ŭ
                        for (int i = 0; i < _skillInfo.Count; i++)
                        {
                            // �ߺ��ǳ���?
                            isDuplicate = GameManager.Skill.CompareSkillStat(_skillInfo[i], one);
                            if (isDuplicate)
                            {
                                // ��
                                // -> ���� �ݺ����� �Ѿ����(foreach��)
                                break;
                            }
                            // �ƴϿ�
                            // -> �ϴ� ���� �ݺ����� �Ѿ����(for��)
                        }
                        // for���� �� ���Ҵµ��� isDuplicate �� false -> �ߺ� ��ü�� �߰ߵ��� ����
                        if(!isDuplicate)
                        {
                            // �׷� ����
                            playData.SkillInfo.Add(one);
                        }
                    }
                }
            }
            // �׽�Ʈ��
            else
            {
                TempSkillStat tmpStat = new TempSkillStat();
                playData.SkillInfo.Add(tmpStat);
            }
        }

        // ���� ��ų ����Ʈ ����
        playData.SkillPoint = GameManager.Select._skillPoint;


        string json = JsonUtility.ToJson(playData, true);
        //JsonData jsonData = JsonMapper.ToJson(playData);
        //File.WriteAllText(path, jsonData.ToString());


        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // save �ε� : DataManager�� ��� playData �� ����
    // ���� �̵��Ҷ��� �� �ε尡 �ʿ� �����Ƿ� bool ���� �ϳ��� �߰��ϰ� LoadData() �Լ� �Ű������� ���
    public void LoadData(bool _sceneLoad)
    {
        //string data = File.ReadAllText(path + filename + selectedSlot.ToString());
        //playData = JsonUtility.FromJson<PlayData>(data);

        // path
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";

        // save ���� ������ �� �̸� ������ Title �� ������
        // �ӽ� ������
        TempPlayerStat player = null;
        string scene = "Title";
        string name = "None";
        string job = "None";
        // ��ų �� ���� ��ġ Ȯ���� �� ����� ������ Enum ��, ���ٸ� default�� ��ȭ�ΰ�
        Define.Job jobCode = 0;
        string pet = "None";
        string weapon = "None";
        List<string> itemList = new List<string>();
        int gold = 0;
        List<TempSkillStat> skillInfo = new List<TempSkillStat>();
        int skillPoint = 0;


        // ���� �����ͼ� �д� �ڵ�
        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        playData = JsonUtility.FromJson<PlayData>(json);

        // save ���Ͽ��� ������ ��
        if(playData.Scene != null)
        {
            // null �ƴ� ���� �־��ֱ�
            scene = playData.Scene;
        }

        // save ���Ͽ��� ������ �÷��̾� ��ü(TempPlayerStat)
        if(playData.Player != null)
        {
            player = playData.Player;
            // �÷��̾� �����Ǵ� �ñⰡ ������ �ε庸�� �ڿ� �����Ƿ� FieldManager���� ���� ����
            // Ÿ�Ժ����ؼ� ObjectManager�� _playerStat�� �־��ֱ�(TempPlayerStat -> PlayerStat)
            GameManager.Parse.SwitchPlayerStatType(player, GameManager.Obj._playerStat);
        }

        // save ���Ͽ��� ������ �÷��̾� �̸�
        if(playData.Name != null)
        {
            // null �ƴ� ���� �־��ֱ�
            name = playData.Name;
            //GameManager.Select._playerName = playData.Name;
        }

        // save ���Ͽ��� ������ ����
        if (playData.Job != null)
        {
            // null �ƴ� ���� �־��ֱ�
            job = playData.Job;
            // ��ų �ε��� �� ����� ����
            jobCode = Util.SortJob(job);
            //GameManager.Select._jobName = playData.Job;
        }

        // save ���Ͽ��� ������ ��
        if (playData.Pet != null)
        {
            // null �ƴ� ���� �־��ֱ�
            pet = playData.Pet;
            //GameManager.Select._petName = playData.Pet;
        }

        if (playData.Weapon != null)
        {
            // null �ƴ� ���� �־��ֱ�
            weapon = playData.Weapon;
        }

        // save ���Ͽ��� ������ �κ��丮 ����Ʈ
        if (playData.ItemList != null)
        {
            // null �ƴ� ���� �־��ֱ�
            itemList = playData.ItemList;
            // ������ ����Ʈ�� ��Ʈ������ �����س������� �ٽ� �κ��丮�� �־�� ��
            //GameManager.Select._itemList = playData.ItemList;
            // �κ��丮 ������ �ε�
            InventoryLoad();

            // �κ��丮�� �ٷ� �ִ°� �׽�Ʈ
            //GameManager.Ui._inventoryController._item = playData.ItemList;
            // InventoryController�� ���� �� ����� �ڵ�, ������ ����Ʈ �����ϴ� �ڵ�
            // ���߿� ���� Ȥ�� ����
            /*
            foreach(string one in itemList)
            {
                // null �˻� �� ���� ���� try ~ catch ��
                try
                {
                    if ((GameManager.Resource.GetfieldItem(one)) != null)
                    {
                        // �κ��丮�� ������ �ֱ�... �õ������� ����, ���߿� ���� Ȥ�� �ٸ� ���
                        //GameManager.Ui._inventoryController._item.Add(GameManager.Resource.GetfieldItem(one));
                    }
                }
                catch(System.Exception e)
                {
                    // �ӽ� �ڵ�, ���߿� ó�� �ڵ� �߰�
                    Debug.Log("���� : " + e.Message);
                }
            }
            */
        }

        // ���� ���� ���⼭
        // �Լ� �ȿ� ��üũ ������ �� �� ��
        //if(playData.Weapon != null && !playData.Weapon.Trim().Equals(""))
        //{
        //    EquipWeaponLoad();
        //}

        // save���� ������ ��� �־��ֱ�
        gold = playData.Gold;
        if(GameManager.Obj._playerStat != null)
        {
            GameManager.Obj._playerStat.Gold = gold;
        }

        // ��ų ��� ��������
        // �� üũ
        if(playData.SkillInfo != null)
        {
            skillInfo = playData.SkillInfo;
            // DataManager�� ���, save �� �� ����Ʈ �����ؾ��ؼ� �ʿ��� 
            _skillInfo = playData.SkillInfo;
            // ����� �� ���� �� ���Ƽ�... �ϴ� �� ��Ʈ�ѷ����� �־�α�
            GameManager.Ui._skillViewController._playerSkillList = skillInfo;
            // ��ų ���� ��� ����Ʈ
            List<Ui_SceneSkillSlot> _sceneSkillSlot = new List<Ui_SceneSkillSlot>();
            // ����Ʈ�� �ε�
            _sceneSkillSlot = GameManager.Ui._sceneButton.GetComponent<Ui_SceneAttackButton>().LoadSceneSkillSlots();

            for(int i = 0; i < skillInfo.Count; i++)
            {
                // ��ų �̹��� �̸�(skill1, skill2, ... skill9) / SkillId�� �̸����� ù ���ڸ� �ҹ���
                string skillImageName = skillInfo[i].Id.Replace('S', 's');
                // ��ų ��� â���� ��ġ Ȯ���� �� ����� SkillId�� ����(ex) Skill4 -> 4�� ��ȯ)
                int skillIdNumber = int.Parse(skillInfo[i].Id.Replace("Skill", ""));
                // ���� �÷� ������
                Color tmpColor;
                tmpColor.a = 0.7f;
                tmpColor.r = 1.0f;
                tmpColor.g = 1.0f;
                tmpColor.b = 1.0f;

                switch(skillInfo[i].SkillSlotNumber)
                {
                    // ��ų��(��ų ��� â)
                    case -1:
                        // ��ų���� �� ��° �������� �Ǻ�
                        int skillViewSlotNumber = skillIdNumber - ((int)jobCode);
                        switch(skillViewSlotNumber)
                        {
                            // ù ��° ����
                            case 1:
                                GameManager.Ui._skillViewController._skillLevel.skill1 = skillInfo[i].SkillLevel;
                                GameManager.Ui._skillViewController._skill1LevelText.text = skillInfo[i].SkillLevel.ToString();
                                break;
                            // �� ��° ����
                            case 2:
                                GameManager.Ui._skillViewController._skillLevel.skill2 = skillInfo[i].SkillLevel;
                                GameManager.Ui._skillViewController._skill2LevelText.text = skillInfo[i].SkillLevel.ToString();
                                break;
                            // �� ��° ����
                            case 3:
                                GameManager.Ui._skillViewController._skillLevel.skill3 = skillInfo[i].SkillLevel;
                                GameManager.Ui._skillViewController._skill3LevelText.text = skillInfo[i].SkillLevel.ToString();
                                break;
                        }
                        break;
                    
                    // ��ų ��ư 1(���ݹ�ư)
                    case 0:
                        _sceneSkillSlot[0]._uiImage.sprite = GameManager.Resource.GetImage(skillImageName);
                        _sceneSkillSlot[0]._skillText.gameObject.SetActive(false);
                        _sceneSkillSlot[0]._uiImage.color = tmpColor;
                        break;
                    // ��ų ��ư 2(���ݹ�ư)
                    case 1:
                        _sceneSkillSlot[1]._uiImage.sprite = GameManager.Resource.GetImage(skillImageName);
                        _sceneSkillSlot[1]._skillText.gameObject.SetActive(false);
                        _sceneSkillSlot[1]._uiImage.color = tmpColor;
                        break;
                    // ��ų ��ư 3(���ݹ�ư)
                    case 2:
                        _sceneSkillSlot[2]._uiImage.sprite = GameManager.Resource.GetImage(skillImageName);
                        _sceneSkillSlot[2]._skillText.gameObject.SetActive(false);
                        _sceneSkillSlot[2]._uiImage.color = tmpColor;
                        break;
                }
            }
        }

        // save ���Ͽ��� ������ ��ų ����Ʈ
        skillPoint = playData.SkillPoint;
        // skillViewController �� üũ
        if(GameManager.Ui._skillViewController != null)
        {
            // �־��ֱ�
            GameManager.Ui._skillViewController._skillLevel.skillPoint = skillPoint;
            // ��ų ������ �̹��� Ȱ��ȭ
            GameManager.Ui._skillViewController.SkillLevelCheck();
            // ��ų ����Ʈ �ִٸ� ������ ��ư Ȱ��ȭ ������
            GameManager.Ui._skillViewController.ButtonInteractableTrue(true);
        }

        //=========���⼭ �� ��ȯ==========
        if (_sceneLoad == true)
        {
            // �������� ����� ������ �̵�
            GameManager.Scene.LoadScene(scene);

            // ������
            //Debug.Log("save ���� �÷��̾� �̸� : " + name);
            //Debug.Log("save ���� ���� : " + job);
            //Debug.Log("save ���� �� : " + pet);
            //Debug.Log("save ���� �� : " + scene);
        }
    }

    //playData���� �κ��丮 ������ ������ ���� �Լ�
    public void InventoryLoad()
    {
        for (int i = 0; i < playData.ItemList.Count; i++)
        {
            string temp = playData.ItemList[i];
            GameObject tempItem = GameManager.Resource.GetfieldItem(temp);
            GameObject Item = Util.Instantiate(tempItem);
            if (Item != null)
            {
                Item.AddComponent<ItemController>();
                // ���� ��ũ��Ʈ�� �ְ�
                ItemStatEX _itemStatEX = Item.AddComponent<ItemStatEX>();
                // ���� ��ũ��Ʈ�� json ���� ���� ����
                GameManager.Stat.ItemStatLoadJson(Item.name, _itemStatEX);
                // �κ��丮�� �ֱ�
                GameManager.Item.InventoryItemAdd(Item, false);
            }
        }
    }

    // playData���� ���������� ������ ���� �Լ� LoaData()�� �ְ� ������ �÷��̾� ������ LoadData���� �ʰ� �ҷ��ͼ� ���� �ҷ��;ߵ� (�ʵ�Ŵ������� ���)
    // ���� ����
    public void EquipWeaponLoad()
    {
        // ���� ������ Ż��
        if(playData.Weapon == null || playData.Weapon.Trim().Equals(""))
            return;
        
        string tempName = playData.Weapon;
        // UI�Ŵ��� �κ��丮��Ʈ�ѷ����� WeaponImage ���ӿ�����Ʈ�� ����Լ��� ã�� 
        Transform findTr = Util.FindChild("WeaponImage", GameManager.Ui._inventoryController.transform);
        // ã�� ���ӿ�����Ʈ���� �̹���������Ʈ�� ����
        Image findImage = findTr.GetComponent<Image>();
        // ���ҽ��Ŵ������� �̹��� ã��
        Sprite weaponImage = GameManager.Resource.GetImage(tempName);
        // ã�� �̹��� �־���
        findImage.sprite = weaponImage;
        // �̹��� Ȱ��ȭ
        findImage.gameObject.SetActive(true);

        // ���⸦ ã�� 
        GameObject weaponTemp = GameManager.Resource.GetEquipItem(tempName);
        GameObject weapon = Util.Instantiate(weaponTemp);
        // ���� ���� (�κ��丮���� ����ִ� ���� / ���� / ������ ĳ���Ͱ� ������� ����)
        GameManager.Ui._inventoryController._weapon = weapon;
        // ���� ��ũ��Ʈ�� �ְ�
        ItemStatEX itemStatEX = weapon.AddComponent<ItemStatEX>();
        // ���� ��ũ��Ʈ�� json ���� ���� ����
        GameManager.Stat.ItemStatLoadJson(tempName, itemStatEX);
        // UI �Ŵ����� ���� ���� ���ݵ� �־� ����
        GameManager.Ui._inventoryController._weaponStat = itemStatEX;

        // ���� ���� (���� ĳ���Ͱ� ����ִ� ��)
        GameManager.Weapon.TempEquipWeapon(tempName, GameManager.Obj._playerController.transform);
        // �÷��̾� ��ũ��Ʈ�� ���� ����
        GameManager.Obj._playerStat.Atk += itemStatEX.Skill;
        // �÷��̾� ��ũ��Ʈ�� �̿��ؼ� �κ��丮�� �ִ� ĳ����â�� ���ݷ� ������ ������
        GameManager.Ui.InventoryStatUpdate();
    }

    // �÷��̾� ���� ������Ʈ ���ִ� �Լ�
    public void UpdatePlayerStat()
    {
        GameManager.Parse.SwitchPlayerStatType(playData.Player, GameManager.Obj._playerStat);
    }
}
