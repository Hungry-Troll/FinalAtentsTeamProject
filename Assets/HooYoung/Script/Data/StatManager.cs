using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Text;

// TestMain클래스를 데이타 매니저로 변경
// 모든 데이터는 여기를 통해서 로드
public class StatManager
{
    // 싱글톤 객체 가져오기
    // private PlayerStat _player = PlayerStat.Instance();
    // 필드매니저에서 캐릭터 생성시 addComponenet로 생성 >> 오브젝트 매니저에 저장한 플레이어 스텟스크립트 가지고 옴
    public PlayerStat _player;
    public MonsterStat _monster;
    public PetStat _pet;

    // 아이템 리스트, CreateItemFile() 에서 사용
    private List<ItemStat> _createdItemList;
    private List<ItemStat> _ItemList;
    // SplitJson(string json) 에서 사용하는 item 개별포장된 json string배열 
    private string[] _itemJsonArr;

    // 임시 스텟 저장용 클래스 선언
    TempStatEX _tempStat;

    public void Init()
    {
        // 플레이어 스텟스크립트를 게임매니저에서 가지고옴
        // _player = GameManager.Obj._playerStat;
        _pet = GameManager.Obj._petStat;

        // 생성 및 저장
        // 한번 데이터를 생성하는데 사용한 코드
        //CreateFile(1, Define.Job.Superhuman);
        //CreateFile(2, Define.Job.Superhuman);
        //CreateFile(3, Define.Job.Superhuman);
        //CreateFile(4, Define.Job.Superhuman);
        //CreateFile(1, Define.Job.Cyborg);
        //CreateFile(2, Define.Job.Cyborg);
        //CreateFile(3, Define.Job.Cyborg);
        //CreateFile(4, Define.Job.Cyborg);
        //CreateFile(1, Define.Job.Scientist);
        //CreateFile(2, Define.Job.Scientist);
        //CreateFile(3, Define.Job.Scientist);
        //CreateFile(4, Define.Job.Scientist);
        //CreatePetFile(Define.Pet.Triceratops);
        //CreatePetFile(Define.Pet.Pachycephalosaurus);
        //CreatePetFile(Define.Pet.Brachiosaurus);

        // 아이템 생성에 사용한 코드
        // 코드, 이름(string), 타입(string), 스킬, 상세정보(string), 구매비용, 판매비용
        /*
        // Weapon
        ItemStat Sword = new ItemStat(111, "검", Define.ItemType.Weapon.ToString(), 10, "평범한 검", 100, 10);
        ItemStat IronSword = new ItemStat(112, "강철검", Define.ItemType.Weapon.ToString(), 20, "단단한 강철로 만든 검", 200, 20);
        ItemStat LegendSword = new ItemStat(113, "전설의 검", Define.ItemType.Weapon.ToString(), 30, "포스의 힘이 깃든 검", 300, 30);
        ItemStat Revolver = new ItemStat(121, "리볼버", Define.ItemType.Weapon.ToString(), 10, "단순한 권총", 100, 10);
        ItemStat AutomaticRifle = new ItemStat(122, "자동소총", Define.ItemType.Weapon.ToString(), 20, "연속 사격이 강한 총", 200, 20);
        ItemStat LaserGun = new ItemStat(123, "레이저 총", Define.ItemType.Weapon.ToString(), 30, "포스의 힘이 깃든 총", 300, 30);
        ItemStat LuckyBook = new ItemStat(131, "행운의 책", Define.ItemType.Weapon.ToString(), 10, "판매 비용이 비싼 책", 100, 80);
        ItemStat ThickDictionary = new ItemStat(132, "두꺼운 사전", Define.ItemType.Weapon.ToString(), 20, "모서리가 날카로운 사전", 200, 20);
        ItemStat DevilsProphet = new ItemStat(133, "악마의 예언서", Define.ItemType.Weapon.ToString(), 30, "세계 종말을 예언한 책", 300, 30);
        
        // Armour
        ItemStat SuperSuit = new ItemStat(211, "강화인간 슈트", Define.ItemType.Armour.ToString(), 5, "강화인간 전용 방어구", 100, 10);
        ItemStat EnergyShield = new ItemStat(221, "에너지 장막", Define.ItemType.Armour.ToString(), 5, "사이보그 전용 방어구", 100, 10);
        ItemStat WhiteCoat = new ItemStat(231, "흰색 가운", Define.ItemType.Armour.ToString(), 5, "사이언티스트 전용 방어구", 100, 10);
        
        // Potion(Consumables)
        ItemStat HP_Potion = new ItemStat(301, "체력 물약", Define.ItemType.Consumables.ToString(), 0, "체력을 50 회복시켜준다", 10, 1);
        ItemStat Recover_Potion = new ItemStat(302, "상태이상 회복 물약", Define.ItemType.Consumables.ToString(), 1, "상태이상을 회복시켜준다.", 20, 2);
        ItemStat SpeedUp_Potion = new ItemStat(303, "이동속도 증가 물약", Define.ItemType.Consumables.ToString(), 2, "10초동안 이동속도를 증가시켜준다.", 30, 3);
        
        CreateItemFile(Sword);
        CreateItemFile(IronSword);
        CreateItemFile(LegendSword);
        CreateItemFile(Revolver);
        CreateItemFile(AutomaticRifle);
        CreateItemFile(LaserGun);
        CreateItemFile(LuckyBook);
        CreateItemFile(ThickDictionary);
        CreateItemFile(DevilsProphet);
        
        CreateItemFile(SuperSuit);
        CreateItemFile(EnergyShield);
        CreateItemFile(WhiteCoat);
        
        CreateItemFile(HP_Potion);
        CreateItemFile(Recover_Potion);
        CreateItemFile(SpeedUp_Potion);
        */


        // 임시 스텟 저장용 객체 선언
        _tempStat = new TempStatEX();
        // 로드하고 PlayerStat 싱글톤 객체에 넣는다

        // enum JOB 을 Define enum Job으로 통합 >> 추후 다른대서도 공용으로 사용

        // 플레이어 스텟 로드
        //PlayerStatLoadJson(1, Define.Job.Superhuman);
        // 몬스터 스텟 로드
        //MonsterStatLoadJson(Define.Monster.Velociraptor);

        // 아이템 스텟 로드
        // item json -> _ItemList<ItemStat> 로 변환
        _createdItemList = new List<ItemStat>();
        _ItemList = new List<ItemStat>();
        LoadItemList();
    // 서치 아이템 id 코드를 기존 아이템 이름과 동일하게 변경함
    // id 코드 대신 직관적인 각 프리팹 이름으로 변경


    // 디버깅용, 아이템 코드로 찾음
    //Debug.Log("id 123 : " + SearchItem(123).Name);
    //Debug.Log("id 123 : " + SearchItem(123).Info);
}

    // json 파일 로드 후 스탯들 PlayerStat 싱글톤 객체에 넣는 함수
    public void PlayerStatLoadJson(int Lv, Define.Job Job)
    {
        // 불러올 파일 이름
        string fileName = Job.ToString();
        fileName += Lv.ToString();
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/Player/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // PlayerStat static 변수에 각각의 스탯 넣기
        // PlayerStat.SetInstance = JsonUtility.FromJson<PlayerStat>(json);

        // PlayerStat을 컴포넌트로 쓰기 위해서 Stat클래스를 MonoBehaviour 사용
        // 그러면 JsonUtility.FromJson 를 사용할 수 없기에 임시 TempStatEX 클래스를 만들어서 데이터를 저장하고 다시 플레이어 스텟에 적용
        // 플레이어 스텟은 플레이어가 컴포넌트로 들고있어야 수정이나 테스트가 쉽기 때문에 수정합니다.
        _tempStat = JsonUtility.FromJson<TempStatEX>(json);

        GameManager.Obj._playerStat.Hp = _tempStat.Hp;
        GameManager.Obj._playerStat.Atk = _tempStat.Atk;
        GameManager.Obj._playerStat.Def = _tempStat.Def;
        GameManager.Obj._playerStat.Lv = _tempStat.Lv;
        GameManager.Obj._playerStat.Max_Hp = _tempStat.Max_Hp;
        // 플레이어 이름은 캐릭터 선택 시 정하기 때문에 그 정보를 게임매니저에 저정하고
        // 거기서 이름을 가지고 옴
        GameManager.Obj._playerStat.Name = GameManager.Select._playerName;
        GameManager.Obj._playerStat.Job = _tempStat.Job;
        GameManager.Obj._playerStat.Exp = _tempStat.Exp;
        GameManager.Obj._playerStat.Lv_Exp = _tempStat.Lv_Exp;

        // 디버깅용
        /*
        Debug.Log("플레이어 이름 : " + _player.Name);
        Debug.Log("레벨 : " + _player.Lv);
        Debug.Log("직업 : " + _player.Job);
        Debug.Log("기본 공격력 : " + _player.Atk);
        Debug.Log("최대 체력 : " + _player.Max_Hp);
        Debug.Log("레벨업 경험치 : " + _player.Lv_Exp);
        */
    }

    // 몬스터 이름으로 스텟 불러옴
    public void MonsterStatLoadJson(string monsterName, MonsterStat monsterStat)
    {
        string fileName = monsterName;
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/Monster/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // PlayerStat static 변수에 각각의 스탯 넣기
        // PlayerStat.SetInstance = JsonUtility.FromJson<PlayerStat>(json);

        _tempStat = JsonUtility.FromJson<TempStatEX>(json);

        monsterStat.Hp = _tempStat.Hp;
        monsterStat.Atk = _tempStat.Atk;
        monsterStat.Def = _tempStat.Def;
        monsterStat.Lv = _tempStat.Lv;
        monsterStat.Max_Hp = _tempStat.Max_Hp;
        monsterStat.Name = _tempStat.Name;
        monsterStat.Gold = _tempStat.Gold;
        monsterStat.Exp = _tempStat.Exp;
        monsterStat.Speed = _tempStat.Speed;
    }

    // 펫 이름으로 스텟 불러옴
    public void PetStatLoadJson(Define.Pet name)
    {
        string fileName = name.ToString();
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/Pet/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        _tempStat = JsonUtility.FromJson<TempStatEX>(json);

        // 펫 스텟 대입
        GameManager.Obj._petStat.Hp = _tempStat.Hp;
        GameManager.Obj._petStat.Atk = _tempStat.Atk;
        GameManager.Obj._petStat.Def = _tempStat.Def;
        GameManager.Obj._petStat.Max_Hp = _tempStat.Max_Hp;
        GameManager.Obj._petStat.Name = _tempStat.Name;
        GameManager.Obj._petStat.Speed = _tempStat.Speed;
        GameManager.Obj._petStat.Revive_Time = _tempStat.Revive_Time;
    }
    // 아이템 스텟 불러옴
    // ItemStatEX 는 순수하게 스텟 붙이는 컴포넌트용도
    public void ItemStatLoadJson(string itemName, ItemStatEX itemStatEX)
    {
        // 이름으로 서치
        ItemStat tempStat = SearchItem(itemName);
        // 가져온 데이터 대입
        itemStatEX.Id = tempStat.Id;
        itemStatEX.Name = tempStat.Name;
        itemStatEX.Type = tempStat.Type;
        itemStatEX.Skill = tempStat.Skill;
        itemStatEX.Info = tempStat.Info;
        itemStatEX.Get_Price = tempStat.Get_Price;
        itemStatEX.Sale_Price = tempStat.Sale_Price;
    }

    // 아이템 로드
    public void LoadItemList()
    {
        string fileName = "Items";
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/Item/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // {}{}{} -> {} {} {} 각각 배열에 저장
        SplitJson(json);

        for(int i = 0; i < _itemJsonArr.Length; i++)
        {
            // {name, type, skill...} 한 세트씩 ItemStat 타입으로 parsing
            ItemStat item = JsonUtility.FromJson<ItemStat>(_itemJsonArr[i]);
            // 리스트에 추가
            _ItemList.Add(item);
        }
    }

    // 아이템 아이디로 검색해서 반환하는 함수
    public ItemStat SearchItem(string findId)
    {
        foreach(ItemStat one in _ItemList)
        {
            if(one.Id == findId)
            {
                return one;
            }
        }
        return null;
    }

    // 플레이어 스탯 json 파일 생성 및 저장 
    // _Job : JOB 타입 enum
    // 강화인간 : JOB.Superhuman
    // 사이보그 : JOB.Cyborg
    // 사이언티스트 : JOB.Scientist
    void CreateFile(int _Lv, Define.Job _Job)
    {
        // 디버깅용 임시로 리터럴 값 넣음
        /*
        _player.Name = "p1";
        _player.Lv = 2;
        _player.Hp = 100;
        _player.Atk = 0;
        _player.Def = 0;
        _player.Exp = 0;
        _player.Job = "Scientist";
        */

        // 레벨별 직업별 갈리는 스탯 setting
        SortStat(_Lv, _Job);

        // static 객체가 가지고 있던 스탯들 json string 으로 변환
        string json = JsonUtility.ToJson(_player);
        // 파일 이름
        //string fileName = "playerStat";

        // 불러올 파일 이름
        string fileName = _Job.ToString();
        fileName += _Lv.ToString();

        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/Player/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void CreatePetFile(Define.Pet _Pet)
    {
        // 디버깅용 임시로 리터럴 값 넣음
        /*
        _pet.Name = _Pet.ToString();
        _pet.Lv = 0;
        _pet.Hp = 160;
        _pet.Atk = 20;
        _pet.Def = 5;
        _pet.Speed = 0;
        _pet.Revive_Time = 60;
        */

        // static 객체가 가지고 있던 스탯들 json string 으로 변환
        string json = JsonUtility.ToJson(_pet, true);
        // 파일 이름
        //string fileName = "playerStat";

        // 불러올 파일 이름
        string fileName = _Pet.ToString();

        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/Pet/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // 아이템 json 파일에 값 추가하는 함수
    void CreateItemFile(ItemStat _item)
    {

        // 디버깅용 임시로 리터럴 값 넣음
        /*
        _item.Name = "sword3";
        _item.Type = "Weapon";
        _item.Skill = 10;
        _item.Info = "그냥 검";
        _item.Get_Price = 10;
        _item.Sale_Price = 5;
        */

        // json으로 변환될 string
        string json;
        // 불러올 파일 이름
        string fileName = "Items";
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/Item/" + fileName + ".json";
        // 파일 열어두기
        FileStream fileStream = new FileStream(path, FileMode.Create);

        // 아이템 리스트에 추가
        _createdItemList.Add(_item);

        // 리스트 null check
        if(_createdItemList.Count > 0)
        {
            foreach(ItemStat one in _createdItemList)
            {
                // static 객체가 가지고 있던 스탯들 json string 으로 변환
                json = JsonUtility.ToJson(one, true);
                
                byte[] data = Encoding.UTF8.GetBytes(json);
                fileStream.Write(data, 0, data.Length);
            }
        }
        fileStream.Close();
    }

    // 레벨, 직업별 초기값 정하는 함수
    void SortStat(int lv, Define.Job job)
    {
        // 레벨
        _player.Lv = lv;

        // 직업별 체력, 공격력, 방어력 set
        switch(job)
        {
            case Define.Job.Superhuman:
                _player.Max_Hp = (int)(lv * 0.5 * 100) + 50;
                _player.Atk = lv * 5;
                _player.Def = 10;
                _player.Job = "Superhuman";
                break;
            case Define.Job.Cyborg:
                _player.Max_Hp = (int)(lv * 0.5 * 80) + 40;
                _player.Atk = lv * 5;
                _player.Def = 5;
                _player.Job = "Cyborg";
                break;
            case Define.Job.Scientist:
                _player.Max_Hp = (int)(lv * 0.5 * 60) + 30;
                _player.Atk = lv * 5;
                _player.Def = 5;
                _player.Job = "Scientist";
                break;
        }
        
        // 레벨업 경험치 set
        switch(lv)
        {
            case 1:
                _player.Lv_Exp = 100;
                break;
            case 2:
                _player.Lv_Exp = 300;
                break;
            case 3:
                _player.Lv_Exp = 500;
                break;
            case 4:
                _player.Lv_Exp = 0;
                break;
        }
    }

    void SplitJson(string jsonSentence)
    {
        _itemJsonArr = jsonSentence.Trim().Split("}");

        for(int i = 0; i < _itemJsonArr.Length-1; i++)
        {
            _itemJsonArr[i] += "}";
            
            // 디버깅용
            //Debug.Log(_itemJsonArr += "}");
        }
    }
}

// 임시 스텟 저장용 클래스
public class TempStatEX
{
    // 현재 체력
    [SerializeField]
    private int _Hp;

    // 공격력
    [SerializeField]
    private int _Atk;

    // 방어력
    [SerializeField]
    private int _Def;

    // 현재 레벨
    [SerializeField]
    private int _Lv;

    // 최대 체력
    [SerializeField]
    private int _Max_Hp;

    // 이름
    [SerializeField]
    private string _Name;

    // 직업
    [SerializeField]
    private string _Job;

    // 골드
    [SerializeField]
    private int _Gold;

    // 현재 경험치
    [SerializeField]
    private int _Exp;

    // 레벨업 경험치
    [SerializeField]
    private int _Lv_Exp;

    // 이동속도
    [SerializeField]
    private int _Speed;

    // 부활시간
    [SerializeField]
    private int _Revive_Time;

    public int Hp
    {
        get { return _Hp; }
        set { _Hp = value; }
    }

    public int Atk
    {
        get { return _Atk; }
        set { _Atk = value; }
    }

    public int Def
    {
        get { return _Def; }
        set { _Def = value; }
    }

    public int Lv
    {
        get { return _Lv; }
        set { _Lv = value; }
    }

    public int Max_Hp
    {
        get { return _Max_Hp; }
        set { _Max_Hp = value; }
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

    public int Gold
    {
        get { return _Gold; }
        set { _Gold = value; }
    }

    public int Exp
    {
        get { return _Exp; }
        set { _Exp = value; }
    }

    public int Lv_Exp
    {
        get { return _Lv_Exp; }
        set { _Lv_Exp = value; }
    }

    public int Speed
    {
        get { return _Speed; }
        set { _Speed = value; }
    }

    public int Revive_Time
    {
        get { return _Revive_Time; }
        set { _Revive_Time = value; }
    }
}

