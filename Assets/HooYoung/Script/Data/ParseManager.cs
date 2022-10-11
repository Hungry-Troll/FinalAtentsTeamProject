using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;

public class ParseManager
{
    // 파일 이름 변수
    public static string _fileName;
    // 경로 저장 변수
    public static string _path;

    // ===Data json 파일에서 가져올 리스트===
    // 플레이어 리스트
    public List<PlayerStat> _playerList = new List<PlayerStat>();
    // 펫 리스트
    public List<PetStat> _petList = new List<PetStat>();
    // 몬스터 리스트
    public List<MonsterStat> _monsterList = new List<MonsterStat>();
    // 아이템 리스트
    public List<ItemStatEX> _itemList = new List<ItemStatEX>();
    // 세이브 파일, 하나라 리스트 아님
    public PlayData _save = new PlayData();

    public void Start()
    {

    }

    public void Update()
    {
        
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
            PlayerStat data = new PlayerStat();

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
            PetStat data = new PetStat();

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
            MonsterStat data = new MonsterStat();

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
            ItemStatEX data = new ItemStatEX();

            data.Id = dataList[i]["_Id"].ToString();
            data.Name = dataList[i]["_Name"].ToString();
            data.Type = dataList[i]["_Type"].ToString();
            data.Skill = int.Parse(dataList[i]["_Skill"].ToString());
            data.Info = dataList[i]["_Info"].ToString();
            data.Get_Price = int.Parse(dataList[i]["_Get_Price"].ToString());
            data.Sale_Price = int.Parse(dataList[i]["_Sale_Price"].ToString());

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
        foreach(PlayerStat one in _playerList)
        {
            // 직업 일치하면
            if(one.Job.Equals(Job.ToString()))
            {
                // 레벨 일치하면
                if(one.Lv == Lv)
                {
                    // 게임메니저에 저장하기
                    GameManager.Obj._playerStat.Hp = one.Hp;
                    GameManager.Obj._playerStat.Atk = one.Atk;
                    GameManager.Obj._playerStat.Def = one.Def;
                    GameManager.Obj._playerStat.Lv = one.Lv;
                    GameManager.Obj._playerStat.Max_Hp = one.Max_Hp;
                    // 플레이어 이름은 캐릭터 선택 시 정하기 때문에 그 정보를 게임매니저에 저장하고
                    // 거기서 이름을 가지고 옴
                    GameManager.Obj._playerStat.Name = GameManager.Select._playerName;
                    GameManager.Obj._playerStat.Job = one.Job;
                    GameManager.Obj._playerStat.Exp = one.Exp;
                    GameManager.Obj._playerStat.Lv_Exp = one.Lv_Exp;

                    // 다 넣었으면 탈출하기
                    break;
                }
            }
        }
    }

    // 원하는 펫 데이터 검색해서 GameManager로 넘겨주는 함수
    public void FindPetObjData(Define.Pet PetName)
    {
        foreach (PetStat one in _petList)
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
        foreach (MonsterStat one in _monsterList)
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
    public void FindItemObjData(string itemName ,ItemStatEX itemStatEX)
    {
        foreach(ItemStatEX one in _itemList)
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

                // 다 넣었으면 탈출
                break;
            }
        }
    }
}
