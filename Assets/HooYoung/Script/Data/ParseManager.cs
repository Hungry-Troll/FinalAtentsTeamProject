using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;

//                                       * 참고
// <    MonoBehaviour 상속        -          상속 받지 않은 일반 클래스    >
//
// <    PlayerStat                      -           TempStatEx                      >
// <    PetStat                          -           TempPetStat                      >
// <    MonsterStat                   -           TempMonsterStat               >
// <    ItemStatEx                    -           TempItemStat, ItemStat     >
// <    SkillStat                        -           TempSkillStat                      >

public class ParseManager
{
    // 파일 이름 변수
    public static string _fileName;
    // 경로 저장 변수
    public static string _path;

    // ===Data json 파일에서 가져올 리스트===
    // 플레이어 리스트
    public List<TempStatEX> _playerList = new List<TempStatEX>();
    // 펫 리스트
    public List<TempPetStat> _petList = new List<TempPetStat>();
    // 몬스터 리스트
    public List<TempMonsterStat> _monsterList = new List<TempMonsterStat>();
    // 아이템 리스트
    public List<TempItemStat> _itemList = new List<TempItemStat>();
    // 세이브 파일, 하나라 리스트 아님
    public PlayData _save = new PlayData();

    public void Init()
    {
        LoadJson();
    }
    // Data json 파일 로드해서 각 리스트로 넘겨주는 함수
    public void LoadJson()
    {
        _fileName = "Data";
        _path = Application.dataPath + "/Resources/Data/Json/" + _fileName + ".json";

        // 파일 가져와서 읽는 코드
        FileStream fileStream = new FileStream(_path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // 가장 큰 {} -> 전체 데이터, 밑 예시처럼 사용
        JsonData rootData = JsonMapper.ToObject(json);
        
        // 예시) {["Player" : {"_Id" : 11, "_Job" : "boss", } , {...}, {...} ...]}    /-> boss 출력
        //Debug.Log(rootData["Player"][0]["_Job"].ToString());

        // 플레이어, 펫, 몬스터, 아이템 , 세이브 각 스탯 리스트로 변환
        ParsePlayerStat(rootData["Player"]);
        ParsePetStat(rootData["Pet"]);
        ParseMonsterStat(rootData["Monster"]);
        ParseItemStat(rootData["Item"]);
        ParseSave(rootData["Save"]);

        // 테스트 코드 / 값 확인 용
        //foreach(string one in _save.ItemList)
        //{
        //    Debug.Log("이름 : " + one);
        //}
    }

    //======================================
    // 여기서부터 리스트 변환 함수

    // 플레이어 리스트 생성하는 함수
    // json -> List<PlayerStat> _playerList 플레이어 리스트로 변환
    public void ParsePlayerStat(JsonData dataList)
    {
        // json {} 오브젝트 개수 만큼 원소에 값 채워넣음
        for(int i = 0; i < dataList.Count; i++)
        {
            // 리스트 원소 생성
            TempStatEX data = new TempStatEX();

            data.Hp = int.Parse(dataList[i]["_Hp"].ToString());
            data.Atk = int.Parse(dataList[i]["_Atk"].ToString());
            data.Def = int.Parse(dataList[i]["_Def"].ToString());
            data.Lv = int.Parse(dataList[i]["_Lv"].ToString());
            data.Max_Hp = int.Parse(dataList[i]["_Max_Hp"].ToString());
            // 플레이어 이름은 여기선 "", Search 함수에서 넣어줄 것임
            data.Name = "";
            data.Job = dataList[i]["_Job"].ToString();
            data.Exp = int.Parse(dataList[i]["_Exp"].ToString());
            data.Lv_Exp = int.Parse(dataList[i]["_Lv_Exp"].ToString());
            data.Gold = int.Parse(dataList[i]["_Gold"].ToString());

            // 리스트에 원소 추가
            _playerList.Add(data);
        }
    }

    // 펫 리스트 생성
    public void ParsePetStat(JsonData dataList)
    {
        // json {} 오브젝트 개수 만큼 원소에 값 채워넣음
        for (int i = 0; i < dataList.Count; i++)
        {
            // 리스트 원소 생성
            TempPetStat data = new TempPetStat();

            data.Name = dataList[i]["_Name"].ToString();
            data.Hp = int.Parse(dataList[i]["_Hp"].ToString());
            data.Atk = int.Parse(dataList[i]["_Atk"].ToString());
            data.Def = int.Parse(dataList[i]["_Def"].ToString());
            data.Lv = int.Parse(dataList[i]["_Lv"].ToString());
            data.Max_Hp = int.Parse(dataList[i]["_Max_Hp"].ToString());
            data.Speed = int.Parse(dataList[i]["_Speed"].ToString());
            data.Revive_Time = int.Parse(dataList[i]["_Revive_Time"].ToString());

            // 리스트에 원소 추가
            _petList.Add(data);
        }
    }

    // 몬스터 리스트 생성
    public void ParseMonsterStat(JsonData dataList)
    {
        // json {} 오브젝트 개수 만큼 원소에 값 채워넣음
        for (int i = 0; i < dataList.Count; i++)
        {
            // 리스트 원소 생성
            TempMonsterStat data = new TempMonsterStat();

            data.Hp = int.Parse(dataList[i]["_Hp"].ToString());
            data.Atk = int.Parse(dataList[i]["_Atk"].ToString());
            data.Def = int.Parse(dataList[i]["_Def"].ToString());
            data.Lv = int.Parse(dataList[i]["_Lv"].ToString());
            data.Max_Hp = int.Parse(dataList[i]["_Max_Hp"].ToString());
            data.Name = dataList[i]["_Name"].ToString();
            data.Gold = int.Parse(dataList[i]["_Gold"].ToString());
            data.Exp = int.Parse(dataList[i]["_Exp"].ToString());
            data.Speed = int.Parse(dataList[i]["_Speed"].ToString());

            // 리스트에 원소 추가
            _monsterList.Add(data);
        }
    }

    // 아이템 리스트 생성
    public void ParseItemStat(JsonData dataList)
    {
        // json {} 오브젝트 개수 만큼 원소에 값 채워넣음
        for (int i = 0; i < dataList.Count; i++)
        {
            // 리스트 원소 생성
            TempItemStat data = new TempItemStat();

            data.Id = dataList[i]["_Id"].ToString();
            data.Name = dataList[i]["_Name"].ToString();
            data.Type = dataList[i]["_Type"].ToString();
            data.Skill = int.Parse(dataList[i]["_Skill"].ToString());
            data.Info = dataList[i]["_Info"].ToString();
            data.Get_Price = int.Parse(dataList[i]["_Get_Price"].ToString());
            data.Sale_Price = int.Parse(dataList[i]["_Sale_Price"].ToString());
            data.Count = int.Parse((dataList[i]["_Count"]).ToString());

            // 리스트에 원소 추가
            _itemList.Add(data);
        }
    }

    // 세이브 데이터 변환(PlayData 타입 객체 하나)
    public void ParseSave(JsonData dataList)
    {
        _save.Name = dataList[0]["_Name"].ToString();
        _save.Job = dataList[0]["_Job"].ToString(); ;
        _save.Pet = dataList[0]["_Pet"].ToString(); ;
        _save.Scene = dataList[0]["_Scene"].ToString();
        _save.Weapon = dataList[0]["_Weapon"].ToString();
        _save.Gold = int.Parse(dataList[0]["_Gold"].ToString());
        
        for(int i = 0; i < dataList[0]["_Item_List"].Count; i++)
        {
            _save.ItemList.Add(dataList[0]["_Item_List"][i].ToString());
        }
    }

    //======================================
    // 여기서부터 검색 함수

    // 원하는 플레이어 데이터 검색해서 GameManager로 넘겨주는 함수
    public void FindPlayerObjData(int Lv, Define.Job Job)
    {
        foreach (TempStatEX one in _playerList)
        {
            // 직업 일치하면
            if (one.Job.Equals(Job.ToString()))
            {
                // 레벨 일치하면
                if (one.Lv == Lv)
                {
                    // 게임메니저에 저장하기
                    GameManager.Obj._playerStat.Hp = one.Hp;
                    // 무기가 있으면 무시 수치를 가지고 와서 계산
                    if (GameManager.Ui._inventoryController._weapon != null)
                    {
                        GameManager.Obj._playerStat.Atk = one.Atk + GameManager.Ui._inventoryController._weaponStat.GetComponent<ItemStatEX>().Skill;
                    }
                    else
                    {
                        GameManager.Obj._playerStat.Atk = one.Atk;
                    }
                    GameManager.Obj._playerStat.Def = one.Def;
                    GameManager.Obj._playerStat.Lv = one.Lv;
                    GameManager.Obj._playerStat.Max_Hp = one.Max_Hp;
                    // 플레이어 이름은 캐릭터 선택 시 정하기 때문에 그 정보를 게임매니저에 저장하고
                    // 거기서 이름을 가지고 옴
                    GameManager.Obj._playerStat.Name = GameManager.Select._playerName;
                    GameManager.Obj._playerStat.Job = one.Job;
                    GameManager.Obj._playerStat.Exp = one.Exp;
                    GameManager.Obj._playerStat.Lv_Exp = one.Lv_Exp;
                    GameManager.Obj._playerStat.Gold = one.Gold;

                    // 다 넣었으면 탈출하기
                    break;
                }
            }
        }
    }


    public TempStatEX FindPlayerObjData2(int Lv, Define.Job Job)
    {
        TempStatEX tmp = new TempStatEX();
        foreach (TempStatEX one in _playerList)
        {
            // 직업 일치하면
            if (one.Job.Equals(Job.ToString()))
            {
                // 레벨 일치하면
                if (one.Lv == Lv)
                {
                    // 게임메니저에 저장하기
                    tmp.Hp = one.Hp;
                    tmp.Atk = one.Atk;
                    tmp.Def = one.Def;
                    tmp.Lv = one.Lv;
                    tmp.Max_Hp = one.Max_Hp;
                    // 플레이어 이름은 캐릭터 선택 시 정하기 때문에 그 정보를 게임매니저에 저장하고
                    // 거기서 이름을 가지고 옴
                    GameManager.Obj._playerStat.Name = GameManager.Select._playerName;
                    tmp.Job = one.Job;
                    tmp.Exp = one.Exp;
                    tmp.Lv_Exp = one.Lv_Exp;
                    tmp.Gold = one.Gold;

                    return tmp;
                    // 다 넣었으면 탈출하기;
                }
            }
            //return null;
        }
        return null;
    }

    // 원하는 펫 데이터 검색해서 GameManager로 넘겨주는 함수
    public void FindPetObjData(Define.Pet PetName)
    {
        foreach (TempPetStat one in _petList)
        {
            // 이름 일치하면
            if (one.Name.Equals(PetName.ToString()))
            {
                // 펫 스텟 대입
                GameManager.Obj._petStat.Hp = one.Hp;
                GameManager.Obj._petStat.Atk = one.Atk;
                GameManager.Obj._petStat.Def = one.Def;
                GameManager.Obj._petStat.Max_Hp = one.Max_Hp;
                GameManager.Obj._petStat.Name = one.Name;
                GameManager.Obj._petStat.Speed = one.Speed;
                GameManager.Obj._petStat.Revive_Time = one.Revive_Time;

                // 다 넣었으면 탈출하기
                break;
            }
        }
    }

    // 원하는 몬스터 데이터 검색해서 스탯 붙여주는 함수
    public void FindMonsterObjData(Define.Monster MonsterName, MonsterStat monsterStat)
    {
        foreach (TempMonsterStat one in _monsterList)
        {
            // 이름 일치하면
            if (one.Name.Equals(MonsterName.ToString()))
            {
                // 몬스터 스텟 대입
                monsterStat.Hp = one.Hp;
                monsterStat.Atk = one.Atk;
                monsterStat.Def = one.Def;
                monsterStat.Lv = one.Lv;
                monsterStat.Max_Hp = one.Max_Hp;
                monsterStat.Name = one.Name;
                monsterStat.Gold = one.Gold;
                monsterStat.Exp = one.Exp;
                monsterStat.Speed = one.Speed;

                // 다 넣었으면 탈출하기
                break;
            }
        }
    }

    // 아이템 검색해서 넘겨줌
    // 현재 사용중인 방식대로 작성했지만 그냥 이름만 검색해서 반환하는 방법도 있음
    public void FindItemObjData(string itemName , TempItemStat itemStatEX)
    {
        foreach(TempItemStat one in _itemList)
        {
            // 이름으로 검색
            if(one.Id.Equals(itemName))
            {
                // 가져온 객체에 복사
                itemStatEX.Id = one.Id;
                itemStatEX.Name = one.Name;
                itemStatEX.Type = one.Type;
                itemStatEX.Skill = one.Skill;
                itemStatEX.Info = one.Info;
                itemStatEX.Get_Price = one.Get_Price;
                itemStatEX.Sale_Price = one.Sale_Price;
                itemStatEX.Count = one.Count;

                // 다 넣었으면 탈출
                break;
            }
        }
    }

    //======================================
    // 여기서부터 타입 변경 함수(Temp <-> Stat)

    // 플레이어 스탯 PlayerStat -> TempPlayerStat 으로 변경하는 함수
    // 오버로딩(1 / 2)
    public TempPlayerStat SwitchPlayerStatType(PlayerStat originStat, TempPlayerStat tempStat)
    {
        // null check
        if(originStat != null)
        {
            tempStat.Name = originStat.Name;
            tempStat.Hp = originStat.Hp;
            tempStat.Atk = originStat.Atk;
            tempStat.Def = originStat.Def;
            tempStat.Lv = originStat.Lv;
            tempStat.Max_Hp = originStat.Max_Hp;
            tempStat.Job = originStat.Job;
            tempStat.Exp = originStat.Exp;
            tempStat.Lv_Exp = originStat.Lv_Exp;
            tempStat.Gold = originStat.Gold;
        }
        return tempStat;
    }

    // 플레이어 스탯 TempPlayerStat -> PlayerStat 으로 변경하는 함수
    // 오버로딩(2 / 2)
    public PlayerStat SwitchPlayerStatType(TempPlayerStat tempStat, PlayerStat originStat)
    {
        // null check
        if (originStat != null)
        {
            originStat.Name = tempStat.Name;
            originStat.Hp = tempStat.Hp;
            originStat.Atk = tempStat.Atk;
            originStat.Def = tempStat.Def;
            originStat.Lv = tempStat.Lv;
            originStat.Max_Hp = tempStat.Max_Hp;
            originStat.Job = tempStat.Job;
            originStat.Exp = tempStat.Exp;
            originStat.Lv_Exp = tempStat.Lv_Exp;
            originStat.Gold = tempStat.Gold;
        }
        return originStat;
    }
}
