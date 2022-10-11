using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

[System.Serializable]
public class PlayData
{
    //저장할 내용은 고른 캐릭터, 저장 당시 위치, 소지품, 레벨, 진행 중인 퀘스트
    
    // 플레이어 이름
    [SerializeField]
    private string _Name;

    // 캐릭터(직업)
    [SerializeField]
    private string _Job;
    
    // 펫
    [SerializeField]
    private string _Pet;

    // 저장 씬 이름
    [SerializeField]
    private string _Scene;
    
    // 인벤토리 담았던 아이템 목록
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
public class DataManager //: MonoBehaviour 게임매니저에서 관리하도록 변경
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
/*        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);*/
        #endregion// 싱글톤을 게임매니저에서 관리하도록 변경

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

    // 저장 : PlayData 타입으로 저장 후 json 파일 생성
    public void SaveData()
    {
        // path 인식을 못해서 여기 일단 넣어줌...
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";

        // 플레이어 이름 SelectManager에서 가져와서 저장
        if(GameManager.Select._playerName != null)
        {
            // null 아닐 때만 담기
            playData.Name = GameManager.Select._playerName;
        }

        // 캐릭터 선택 창에서 담아둔 직업 정보(UiDesign2.cs)
        //playData.Job = GameManager.Data.playData.Job;
        // 캐릭터 선택창 변경(UiDesign2 -> UiDesign3), SelectManager 에서 가져오도록 변경
        if(GameManager.Select._jobName != null)
        {
            // null 아닐 때만 담기
            playData.Job = GameManager.Select._jobName;
        }

        // 펫 이름 저장
        if (GameManager.Select._petName != null)
        {
            // null 아닐 때만 담기
            playData.Pet = GameManager.Select._petName;
        }

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

    // save 로드 : DataManager의 멤버 playData 로 받음
    public void LoadData()
    {
        //string data = File.ReadAllText(path + filename + selectedSlot.ToString());
        //playData = JsonUtility.FromJson<PlayData>(data);

        // path
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";

        // save 에서 가져올 씬 이름 없으면 Title 로 가도록
        // 임시 변수들
        string scene = "Title";
        string name = "None";
        string job = "None";
        string pet = "None";
        List<string> itemList = new List<string>();

        // 파일 가져와서 읽는 코드
        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        playData = JsonUtility.FromJson<PlayData>(json);

        // save 파일에서 가져온 씬
        if(playData.Scene != null)
        {
            // null 아닐 때만 넣어주기
            scene = playData.Scene;
        }

        // save 파일에서 가져온 플레이어 이름
        if (playData.Name != null)
        {
            // null 아닐 때만 넣어주기
            name = playData.Name;
            GameManager.Select._playerName = playData.Name;
        }

        // save 파일에서 가져온 직업
        if (playData.Job != null)
        {
            // null 아닐 때만 넣어주기
            job = playData.Job;
            GameManager.Select._jobName = playData.Job;
        }

        // save 파일에서 가져온 펫
        if (playData.Pet != null)
        {
            // null 아닐 때만 넣어주기
            pet = playData.Pet;
            GameManager.Select._petName = playData.Pet;
        }

        // save 파일에서 가져온 인벤토리 리스트
        if (playData.ItemList != null)
        {
            // null 아닐 때만 넣어주기
            itemList = playData.ItemList;
            GameManager.Select._itemList = playData.ItemList;

            // InventoryController에 넣을 때 사용할 코드, 아이템 리스트 저장하는 코드
            // 나중에 수정 혹은 삭제
            /*
            foreach(string one in itemList)
            {
                // null 검사 시 오류 방지 try ~ catch 문
                try
                {
                    if ((GameManager.Resource.GetfieldItem(one)) != null)
                    {
                        // 인벤토리에 아이템 넣기... 시도했으나 실패, 나중에 수정 혹은 다른 방식
                        //GameManager.Ui._inventoryController._item.Add(GameManager.Resource.GetfieldItem(one));
                    }
                }
                catch(System.Exception e)
                {
                    // 임시 코드, 나중에 처리 코드 추가
                    Debug.Log("오류 : " + e.Message);
                }
            }
            */
        }


        //=========여기서 씬 전환==========

        // 마지막에 저장된 씬으로 이동
        GameManager.Scene.LoadScene(scene);

        // 디버깅용
        //Debug.Log("save 파일 플레이어 이름 : " + name);
        //Debug.Log("save 파일 직업 : " + job);
        //Debug.Log("save 파일 펫 : " + pet);
        //Debug.Log("save 파일 씬 : " + scene);
    }
}
