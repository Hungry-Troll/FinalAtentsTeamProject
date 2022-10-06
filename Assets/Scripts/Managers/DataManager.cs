using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

[System.Serializable]
public class PlayData
{
    //저장할 내용은 고른 캐릭터, 저장 당시 위치, 소지품, 레벨, 진행 중인 퀘스트
    
    // 캐릭터(직업)
    [SerializeField]
    private string _job;
    
    // 저장 씬 이름
    [SerializeField]
    private string _scene;
    
    // 인벤토리 담았던 아이템 목록
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
        #region 싱글톤
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
        // path 인식을 못해서 여기 일단 넣어줌...
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";

        // 캐릭터 선택 창에서 담아둔 직업 정보(UiDesign2.cs)
        playData.Job = GameManager.Data.playData.Job;

        // SceneManagerEX 에서 가져온 현재 씬 이름
        playData.Scene = GameManager.Scene.LoadSceneName;
        
        // UiManager 에서 가져온 아이템 리스트
        // 인벤토리 컨트롤러부터 널검사
        if(GameManager.Ui._inventoryController != null)
        {
            // null 아닐때만 저장
            if(GameManager.Ui._inventoryController._item != null)
            {
                foreach(GameObject one in GameManager.Ui._inventoryController._item)
                {
                    // 인벤토리 아이템 이름으로 저장
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
