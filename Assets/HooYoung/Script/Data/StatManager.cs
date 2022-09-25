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

    // 임시 스텟 저장용 클래스 선언
    TempStatEX _tempStat;

    public void Init()
    {
        // 플레이어 스텟스크립트를 게임매니저에서 가지고옴
        _player = GameManager.Obj._playerStat;

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

        // 임시 스텟 저장용 객체 선언
        _tempStat = new TempStatEX();
        // 로드하고 PlayerStat 싱글톤 객체에 넣는다

        // enum JOB 을 Define enum Job으로 통합 >> 추후 다른대서도 공용으로 사용

        // 플레이어 스텟 로드
        PlayerStatLoadJson(1, Define.Job.Superhuman);
        // 몬스터 스텟 로드
        MonsterStatLoadJson(Define.Monster.Velociraptor);
    }

    // json 파일 로드 후 스탯들 PlayerStat 싱글톤 객체에 넣는 함수
    void PlayerStatLoadJson(int Lv, Define.Job Job)
    {
        // 불러올 파일 이름
        string fileName = Job.ToString();
        fileName += Lv.ToString();
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

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

        _player.Hp = _tempStat.Hp;
        _player.Atk = _tempStat.Atk;
        _player.Def = _tempStat.Def;
        _player.Lv = _tempStat.Lv;
        _player.Max_Hp = _tempStat.Max_Hp;
        _player.Name = _tempStat.Name;
        _player.Job = _tempStat.Job;
        _player.Exp = _tempStat.Exp;
        _player.Lv_Exp = _tempStat.Lv_Exp;

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
    void MonsterStatLoadJson(Define.Monster name)
    {
        string fileName = name.ToString();
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // PlayerStat static 변수에 각각의 스탯 넣기
        // PlayerStat.SetInstance = JsonUtility.FromJson<PlayerStat>(json);


        _tempStat = JsonUtility.FromJson<TempStatEX>(json);

        // 몬스터 스텟 대입
        for(int i = 0; i < GameManager.Obj._mobStatList.Count; i++)
        {
            GameManager.Obj._mobStatList[i].Hp = _tempStat.Hp;
            GameManager.Obj._mobStatList[i].Atk = _tempStat.Atk;
            GameManager.Obj._mobStatList[i].Def = _tempStat.Def;
            GameManager.Obj._mobStatList[i].Lv = _tempStat.Lv;
            GameManager.Obj._mobStatList[i].Max_Hp = _tempStat.Max_Hp;
            GameManager.Obj._mobStatList[i].Name = _tempStat.Name;
            GameManager.Obj._mobStatList[i].Gold = _tempStat.Gold;
            GameManager.Obj._mobStatList[i].Exp = _tempStat.Exp;
            GameManager.Obj._mobStatList[i].Speed = _tempStat.Speed;
        }
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
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
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
}

