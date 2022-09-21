using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayData
{
    //저장할 내용은 고른 캐릭터, 저장 당시 위치, 소지품, 레벨, 진행 중인 퀘스트
    //
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    PlayData playData = new PlayData();
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

        path = Application.persistentDataPath + "/";
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
        string data = JsonUtility.ToJson(playData);
        File.WriteAllText(path + filename + selectedSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + filename + selectedSlot.ToString());
        playData = JsonUtility.FromJson<PlayData>(data);
    }
}
