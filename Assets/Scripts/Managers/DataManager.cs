using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

[System.Serializable]
public class PlayData
{
    //������ ������ �� ĳ����, ���� ��� ��ġ, ����ǰ, ����, ���� ���� ����Ʈ
    
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
        playData.Gold = GameManager.Obj._playerStat.Gold;


        string json = JsonUtility.ToJson(playData, true);
        //File.WriteAllText(path + filename + selectedSlot.ToString(), data);

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
        string scene = "Title";
        string name = "None";
        string job = "None";
        string pet = "None";
        string weapon = "None";
        List<string> itemList = new List<string>();
        int gold = 0;


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

        // save ���Ͽ��� ������ �÷��̾� �̸�
        if (playData.Name != null)
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

        // save���� ������ ��� �־��ֱ�
        gold = playData.Gold;

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
        if(playData.Weapon.Trim().Equals(""))
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
}
