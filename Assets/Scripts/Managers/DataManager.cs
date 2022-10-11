using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

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

}
public class DataManager //: MonoBehaviour ���ӸŴ������� �����ϵ��� ����
{
    public static DataManager instance;
    public PlayData playData = new PlayData();
    string path;
    string filename = "save";
    public int selectedSlot;

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

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    // ���� : PlayData Ÿ������ ���� �� json ���� ����
    public void SaveData()
    {
        // path �ν��� ���ؼ� ���� �ϴ� �־���...
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
        playData.Scene = GameManager.Scene.LoadSceneName;
        
        // UiManager ���� ������ ������ ����Ʈ
        // �κ��丮 ��Ʈ�ѷ����� �ΰ˻�
        if(GameManager.Ui._inventoryController != null)
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

        string json = JsonUtility.ToJson(playData, true);
        //File.WriteAllText(path + filename + selectedSlot.ToString(), data);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // save �ε� : DataManager�� ��� playData �� ����
    public void LoadData()
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
        List<string> itemList = new List<string>();

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
            GameManager.Select._playerName = playData.Name;
        }

        // save ���Ͽ��� ������ ����
        if (playData.Job != null)
        {
            // null �ƴ� ���� �־��ֱ�
            job = playData.Job;
            GameManager.Select._jobName = playData.Job;
        }

        // save ���Ͽ��� ������ ��
        if (playData.Pet != null)
        {
            // null �ƴ� ���� �־��ֱ�
            pet = playData.Pet;
            GameManager.Select._petName = playData.Pet;
        }

        // save ���Ͽ��� ������ �κ��丮 ����Ʈ
        if (playData.ItemList != null)
        {
            // null �ƴ� ���� �־��ֱ�
            itemList = playData.ItemList;
            GameManager.Select._itemList = playData.ItemList;

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


        //=========���⼭ �� ��ȯ==========

        // �������� ����� ������ �̵�
        GameManager.Scene.LoadScene(scene);

        // ������
        //Debug.Log("save ���� �÷��̾� �̸� : " + name);
        //Debug.Log("save ���� ���� : " + job);
        //Debug.Log("save ���� �� : " + pet);
        //Debug.Log("save ���� �� : " + scene);
    }
}
