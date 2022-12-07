using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Define;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 게임매니저 사용 코드 예시 :
    /// Resource 매니저 사용
    /// GameManager.Resource.(  )   ( )는 public 변수 함수 모든 코드 접근가능
    /// UI 매니저 사용
    /// GameManager.Ui.(  )   ( )는 public 변수 함수 모든 코드 접근가능
    /// </summary>
    // 게임 매니저 싱글톤 생성
    static GameManager _instance;
    public static GameManager Instance 
    { 
        get { return _instance; }
    }
    /// 매니저 생성
    ResourceManager _resource = new ResourceManager();
    SceneManagerEX _scene = new SceneManagerEX();
    UiManager _ui = new UiManager();
    ObjectManager _obj = new ObjectManager();
    CameraManager _cam = new CameraManager();
    MonsterManagerEX _mob = new MonsterManagerEX();
    StatManager _stat = new StatManager();
    SoundManagerEX _sound = new SoundManagerEX();
    SelectManager _select = new SelectManager();
    WeaponManager _weapon = new WeaponManager();
    DataManager _data = new DataManager();
    CreateManager _create = new CreateManager();
    ParseManager _parse = new ParseManager();
    ItemManager _item = new ItemManager();
    EffectManagerEX _effect = new EffectManagerEX();
    SkillManager _skill = new SkillManager();
    QuestDataManager _questData = new QuestDataManager();
    QuestManagerEX _quest = new QuestManagerEX();

    public static ResourceManager Resource 
    { 
        get{ return _instance._resource; } 
    }
    public static SceneManagerEX Scene 
    { 
        get { return _instance._scene; } 
    }
    public static UiManager Ui 
    { 
        get { return _instance._ui; } 
    }
    public static ObjectManager Obj
    {
        get { return _instance._obj; }
    }
    public static CameraManager Cam
    {
        get { return _instance._cam; }
    }
    public static MonsterManagerEX Mob
    {
        get { return _instance._mob; }
    }
    public static StatManager Stat
    {
        get { return _instance._stat; }
    }
    public static SoundManagerEX Sound
    {
        get { return _instance._sound; }
    }
    public static SelectManager Select
    {
        get { return _instance._select; }
    }
    public static WeaponManager Weapon
    {
        get { return _instance._weapon; }
    }
    public static DataManager Data
    {
        get { return _instance._data; }
    }
    public static CreateManager Create
    {
        get { return _instance._create; }
    }
    public static ParseManager Parse
    {
        get { return _instance._parse; }
    }
    public static ItemManager Item
    {
        get { return _instance._item; }
    }
    public static EffectManagerEX Effect
    {
        get { return _instance._effect; }
    }
    public static SkillManager Skill
    {
        get { return _instance._skill; }
    }
    public static QuestDataManager QuestData
    {
        get { return _instance._questData; }
    }
    public static QuestManagerEX Quest
    {
        get { return _instance._quest; }
    }


    // Start is called before the first frame update
    private void Awake()
    {
        if(_instance == null)
        {
           _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // 게임매니저에서 리소스 매니저/사운드 매니저 Init(Awake 함수 대체)
        GameManager.Resource.Init();
        GameManager.Sound.Init();
        // 퀘스트 데이터 불러옴
        GameManager.QuestData.Init();
        // 퀘스트매니저 Init() 함수 추가
        GameManager.Quest.Init();
    }
}

