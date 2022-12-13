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
    //저장할 내용은 고른 캐릭터, 저장 당시 위치, 소지품, 레벨, 진행 중인 퀘스트

    // 플레이어 객체
    [SerializeField]
    private TempPlayerStat _Player;

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

    [SerializeField]
    private string _Weapon;

    // 갖고 있는 골드 수량
    [SerializeField]
    private int _Gold;

    // 플레이어 보유 스킬 목록
    // SkillStat과 동일하지만 MonoBehaviour 상속 받지 않은 클래스
    [SerializeField]
    private List<TempSkillStat> _SkillInfo = new List<TempSkillStat>();

    // 보유 스킬 포인트
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

public class DataManager //: MonoBehaviour 게임매니저에서 관리하도록 변경
{
    //public static DataManager instance;
    public PlayData playData = new PlayData();
    string path;
    string filename = "save";
    public int selectedSlot;
    // 씬을 이동할때는 씬 로드가 필요 없으므로 bool 변수 하나를 추가하고 LoadData() 함수 매개변수로 사용
    bool _sceneLoad;
    private List<TempSkillStat> _skillInfo;

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

    // 저장 : PlayData 타입으로 저장 후 json 파일 생성
    public void SaveData()
    {
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";

        // 플레이어 객체 ObjectManager에서 가져와서 저장
        if(GameManager.Obj._playerStat != null)
        {
            // 타입만 TempPlayerStat 으로 바꿔서 저장(MonoBehaviour 상속받지 않은 스탯)
            playData.Player 
                = GameManager.Parse.SwitchPlayerStatType(GameManager.Obj._playerStat, new TempPlayerStat());
        }

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
        playData.Scene = GameManager.Scene._LoadSceneName;

        // UiManager  _inventoryController._weapon 은 장착아이템
        // 장착 아이템도 저장 필요
        if (GameManager.Ui._inventoryController._weapon != null)
        {
            // null 아닐 때만 담기
            playData.Weapon = GameManager.Ui._inventoryController._weapon.name;
        }

        // UiManager 에서 가져온 아이템 리스트
        // 인벤토리 컨트롤러부터 널검사
        if (GameManager.Ui._inventoryController != null)
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

        // 현재 가진 골드 저장
        playData.Gold = GameManager.Obj._goldController.GoldAmount;

        // 스킬 목록 저장
        // 컨트롤러부터 null 체크
        if(GameManager.Ui._skillViewController != null)
        {
            // _skillStat 변수 null 체크
            if(GameManager.Ui._skillViewController._playerSkillList != null)
            {
                // 저장
                foreach(TempSkillStat one in GameManager.Ui._skillViewController._playerSkillList)
                {
                    // 기존의 저장한 내용 없으면
                    if(_skillInfo == null)
                    {
                        // 그냥 저장하면 됨
                        playData.SkillInfo.Add(one);
                    }
                    else
                    {
                        // 중복 체크 변수
                        bool isDuplicate = false;
                        // 저장된 TempSkillStat 객체 만큼
                        for (int i = 0; i < _skillInfo.Count; i++)
                        {
                            // 중복되나요?
                            isDuplicate = GameManager.Skill.CompareSkillStat(_skillInfo[i], one);
                            if (isDuplicate)
                            {
                                // 네
                                // -> 다음 반복으로 넘어가세요(foreach문)
                                break;
                            }
                            // 아니요
                            // -> 일단 다음 반복으로 넘어가세요(for문)
                        }
                        // for문을 다 돌았는데도 isDuplicate 가 false -> 중복 개체가 발견되지 않음
                        if(!isDuplicate)
                        {
                            // 그럼 저장
                            playData.SkillInfo.Add(one);
                        }
                    }
                }
            }
            // 테스트용
            else
            {
                TempSkillStat tmpStat = new TempSkillStat();
                playData.SkillInfo.Add(tmpStat);
            }
        }

        // 보유 스킬 포인트 저장
        playData.SkillPoint = GameManager.Select._skillPoint;


        string json = JsonUtility.ToJson(playData, true);
        //JsonData jsonData = JsonMapper.ToJson(playData);
        //File.WriteAllText(path, jsonData.ToString());


        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // save 로드 : DataManager의 멤버 playData 로 받음
    // 씬을 이동할때는 씬 로드가 필요 없으므로 bool 변수 하나를 추가하고 LoadData() 함수 매개변수로 사용
    public void LoadData(bool _sceneLoad)
    {
        //string data = File.ReadAllText(path + filename + selectedSlot.ToString());
        //playData = JsonUtility.FromJson<PlayData>(data);

        // path
        path = Application.dataPath + "/Resources/Data/Json/Save/" + filename + ".json";

        // save 에서 가져올 씬 이름 없으면 Title 로 가도록
        // 임시 변수들
        TempPlayerStat player = null;
        string scene = "Title";
        string name = "None";
        string job = "None";
        // 스킬 뷰 슬롯 위치 확인할 때 사용할 직업별 Enum 값, 없다면 default는 강화인간
        Define.Job jobCode = 0;
        string pet = "None";
        string weapon = "None";
        List<string> itemList = new List<string>();
        int gold = 0;
        List<TempSkillStat> skillInfo = new List<TempSkillStat>();
        int skillPoint = 0;


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

        // save 파일에서 가져온 플레이어 객체(TempPlayerStat)
        if(playData.Player != null)
        {
            player = playData.Player;
            // 플레이어 생성되는 시기가 데이터 로드보다 뒤에 있으므로 FieldManager에서 개별 실행
            // 타입변경해서 ObjectManager의 _playerStat에 넣어주기(TempPlayerStat -> PlayerStat)
            GameManager.Parse.SwitchPlayerStatType(player, GameManager.Obj._playerStat);
        }

        // save 파일에서 가져온 플레이어 이름
        if(playData.Name != null)
        {
            // null 아닐 때만 넣어주기
            name = playData.Name;
            //GameManager.Select._playerName = playData.Name;
        }

        // save 파일에서 가져온 직업
        if (playData.Job != null)
        {
            // null 아닐 때만 넣어주기
            job = playData.Job;
            // 스킬 로드할 때 사용할 변수
            jobCode = Util.SortJob(job);
            //GameManager.Select._jobName = playData.Job;
        }

        // save 파일에서 가져온 펫
        if (playData.Pet != null)
        {
            // null 아닐 때만 넣어주기
            pet = playData.Pet;
            //GameManager.Select._petName = playData.Pet;
        }

        if (playData.Weapon != null)
        {
            // null 아닐 때만 넣어주기
            weapon = playData.Weapon;
        }

        // save 파일에서 가져온 인벤토리 리스트
        if (playData.ItemList != null)
        {
            // null 아닐 때만 넣어주기
            itemList = playData.ItemList;
            // 아이템 리스트를 스트링으로 저장해놓은것을 다시 인벤토리에 넣어야 됨
            //GameManager.Select._itemList = playData.ItemList;
            // 인벤토리 아이템 로드
            InventoryLoad();

            // 인벤토리에 바로 넣는것 테스트
            //GameManager.Ui._inventoryController._item = playData.ItemList;
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

        // 무기 착용 여기서
        // 함수 안에 널체크 있지만 한 번 더
        //if(playData.Weapon != null && !playData.Weapon.Trim().Equals(""))
        //{
        //    EquipWeaponLoad();
        //}

        // save에서 가져온 골드 넣어주기
        gold = playData.Gold;
        if(GameManager.Obj._playerStat != null)
        {
            GameManager.Obj._playerStat.Gold = gold;
        }

        // 스킬 목록 가져오기
        // 널 체크
        if(playData.SkillInfo != null)
        {
            skillInfo = playData.SkillInfo;
            // DataManager의 멤버, save 할 때 리스트 대조해야해서 필요함 
            _skillInfo = playData.SkillInfo;
            // 사용할 곳 있을 것 같아서... 일단 뷰 컨트롤러에도 넣어두기
            GameManager.Ui._skillViewController._playerSkillList = skillInfo;
            // 스킬 슬롯 담길 리스트
            List<Ui_SceneSkillSlot> _sceneSkillSlot = new List<Ui_SceneSkillSlot>();
            // 리스트에 로드
            _sceneSkillSlot = GameManager.Ui._sceneButton.GetComponent<Ui_SceneAttackButton>().LoadSceneSkillSlots();

            for(int i = 0; i < skillInfo.Count; i++)
            {
                // 스킬 이미지 이름(skill1, skill2, ... skill9) / SkillId의 이름에서 첫 글자만 소문자
                string skillImageName = skillInfo[i].Id.Replace('S', 's');
                // 스킬 목록 창에서 위치 확인할 때 사용할 SkillId의 숫자(ex) Skill4 -> 4로 변환)
                int skillIdNumber = int.Parse(skillInfo[i].Id.Replace("Skill", ""));
                // 슬롯 컬러 고정값
                Color tmpColor;
                tmpColor.a = 0.7f;
                tmpColor.r = 1.0f;
                tmpColor.g = 1.0f;
                tmpColor.b = 1.0f;

                switch(skillInfo[i].SkillSlotNumber)
                {
                    // 스킬뷰(스킬 목록 창)
                    case -1:
                        // 스킬뷰의 몇 번째 슬롯인지 판별
                        int skillViewSlotNumber = skillIdNumber - ((int)jobCode);
                        switch(skillViewSlotNumber)
                        {
                            // 첫 번째 슬롯
                            case 1:
                                GameManager.Ui._skillViewController._skillLevel.skill1 = skillInfo[i].SkillLevel;
                                GameManager.Ui._skillViewController._skill1LevelText.text = skillInfo[i].SkillLevel.ToString();
                                break;
                            // 두 번째 슬롯
                            case 2:
                                GameManager.Ui._skillViewController._skillLevel.skill2 = skillInfo[i].SkillLevel;
                                GameManager.Ui._skillViewController._skill2LevelText.text = skillInfo[i].SkillLevel.ToString();
                                break;
                            // 세 번째 슬롯
                            case 3:
                                GameManager.Ui._skillViewController._skillLevel.skill3 = skillInfo[i].SkillLevel;
                                GameManager.Ui._skillViewController._skill3LevelText.text = skillInfo[i].SkillLevel.ToString();
                                break;
                        }
                        break;
                    
                    // 스킬 버튼 1(공격버튼)
                    case 0:
                        _sceneSkillSlot[0]._uiImage.sprite = GameManager.Resource.GetImage(skillImageName);
                        _sceneSkillSlot[0]._skillText.gameObject.SetActive(false);
                        _sceneSkillSlot[0]._uiImage.color = tmpColor;
                        break;
                    // 스킬 버튼 2(공격버튼)
                    case 1:
                        _sceneSkillSlot[1]._uiImage.sprite = GameManager.Resource.GetImage(skillImageName);
                        _sceneSkillSlot[1]._skillText.gameObject.SetActive(false);
                        _sceneSkillSlot[1]._uiImage.color = tmpColor;
                        break;
                    // 스킬 버튼 3(공격버튼)
                    case 2:
                        _sceneSkillSlot[2]._uiImage.sprite = GameManager.Resource.GetImage(skillImageName);
                        _sceneSkillSlot[2]._skillText.gameObject.SetActive(false);
                        _sceneSkillSlot[2]._uiImage.color = tmpColor;
                        break;
                }
            }
        }

        // save 파일에서 가져온 스킬 포인트
        skillPoint = playData.SkillPoint;
        // skillViewController 널 체크
        if(GameManager.Ui._skillViewController != null)
        {
            // 넣어주기
            GameManager.Ui._skillViewController._skillLevel.skillPoint = skillPoint;
            // 스킬 레벨별 이미지 활성화
            GameManager.Ui._skillViewController.SkillLevelCheck();
            // 스킬 포인트 있다면 레벨업 버튼 활성화 시켜줌
            GameManager.Ui._skillViewController.ButtonInteractableTrue(true);
        }

        //=========여기서 씬 전환==========
        if (_sceneLoad == true)
        {
            // 마지막에 저장된 씬으로 이동
            GameManager.Scene.LoadScene(scene);

            // 디버깅용
            //Debug.Log("save 파일 플레이어 이름 : " + name);
            //Debug.Log("save 파일 직업 : " + job);
            //Debug.Log("save 파일 펫 : " + pet);
            //Debug.Log("save 파일 씬 : " + scene);
        }
    }

    //playData에서 인벤토리 데이터 가지고 오는 함수
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
                // 스텟 스크립트를 넣고
                ItemStatEX _itemStatEX = Item.AddComponent<ItemStatEX>();
                // 스텟 스크립트에 json 파일 스텟 적용
                GameManager.Stat.ItemStatLoadJson(Item.name, _itemStatEX);
                // 인벤토리에 넣기
                GameManager.Item.InventoryItemAdd(Item, false);
            }
        }
    }

    // playData에서 장착아이템 가지고 오는 함수 LoaData()에 넣고 싶은대 플레이어 정보를 LoadData보다 늦게 불러와서 따로 불러와야됨 (필드매니저에서 사용)
    // 추후 수정
    public void EquipWeaponLoad()
    {
        // 무기 없으면 탈출
        if(playData.Weapon == null || playData.Weapon.Trim().Equals(""))
            return;
        
        string tempName = playData.Weapon;
        // UI매니저 인벤토리컨트롤러에서 WeaponImage 게임오브젝트를 재귀함수로 찾음 
        Transform findTr = Util.FindChild("WeaponImage", GameManager.Ui._inventoryController.transform);
        // 찾은 게임오브젝트에서 이미지컴포넌트를 빼옴
        Image findImage = findTr.GetComponent<Image>();
        // 리소스매니저에서 이미지 찾음
        Sprite weaponImage = GameManager.Resource.GetImage(tempName);
        // 찾은 이미지 넣어줌
        findImage.sprite = weaponImage;
        // 이미지 활성화
        findImage.gameObject.SetActive(true);

        // 무기를 찾음 
        GameObject weaponTemp = GameManager.Resource.GetEquipItem(tempName);
        GameObject weapon = Util.Instantiate(weaponTemp);
        // 무기 장착 (인벤토리에서 들고있는 무기 / 계산용 / 실제로 캐릭터가 들고잇지 않음)
        GameManager.Ui._inventoryController._weapon = weapon;
        // 스텟 스크립트를 넣고
        ItemStatEX itemStatEX = weapon.AddComponent<ItemStatEX>();
        // 스텟 스크립트에 json 파일 스텟 적용
        GameManager.Stat.ItemStatLoadJson(tempName, itemStatEX);
        // UI 매니저에 장착 무기 스텟도 넣어 놓음
        GameManager.Ui._inventoryController._weaponStat = itemStatEX;

        // 무기 착용 (실제 캐릭터가 들고있는 것)
        GameManager.Weapon.TempEquipWeapon(tempName, GameManager.Obj._playerController.transform);
        // 플레이어 스크립트에 스텟 더함
        GameManager.Obj._playerStat.Atk += itemStatEX.Skill;
        // 플레이어 스크립트를 이용해서 인벤토리에 있는 캐릭터창에 공격력 방어력을 보여줌
        GameManager.Ui.InventoryStatUpdate();
    }

    // 플레이어 스탯 업데이트 해주는 함수
    public void UpdatePlayerStat()
    {
        GameManager.Parse.SwitchPlayerStatType(playData.Player, GameManager.Obj._playerStat);
    }
}
