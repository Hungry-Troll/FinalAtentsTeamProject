using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

[System.Serializable]
public class PlayData
{
    //������ ������ �� ĳ����, ���� ��� ��ġ, ����ǰ, ����, ���� ���� ����Ʈ
    
    // ĳ����(����)
    [SerializeField]
    private string _job;
    
    // ���� �� �̸�
    [SerializeField]
    private string _scene;
    
    // �κ��丮 ��Ҵ� ������ ���
    [SerializeField]
    private List<string> _itemList = new List<string>();

    public string Job
    {
        get { return _job; }
        set { _job = value; }
    }
    public string Scene
    {
        get { return _scene; }
        set { _scene = value; }
    }
    public List<string> ItemList
    {
        get { return _itemList; }
        set { _itemList = value; }
    }

}
public class DataManager : MonoBehaviour
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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

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

    public void SaveData()
    {
        // path �ν��� ���ؼ� ���� �ϴ� �־���...
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";

        // ĳ���� ���� â���� ��Ƶ� ���� ����(UiDesign2.cs)
        playData.Job = GameManager.Data.playData.Job;

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

    public void LoadData()
    {
        string data = File.ReadAllText(path + filename + selectedSlot.ToString());
        playData = JsonUtility.FromJson<PlayData>(data);
    }
}
