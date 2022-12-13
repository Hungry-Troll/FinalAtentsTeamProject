using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;

//                                       * ����
// <    MonoBehaviour ���        -          ��� ���� ���� �Ϲ� Ŭ����    >
//
// <    PlayerStat                      -           TempStatEx                      >
// <    PetStat                          -           TempPetStat                      >
// <    MonsterStat                   -           TempMonsterStat               >
// <    ItemStatEx                    -           TempItemStat, ItemStat     >
// <    SkillStat                        -           TempSkillStat                      >

public class ParseManager
{
    // ���� �̸� ����
    public static string _fileName;
    // ��� ���� ����
    public static string _path;

    // ===Data json ���Ͽ��� ������ ����Ʈ===
    // �÷��̾� ����Ʈ
    public List<TempStatEX> _playerList = new List<TempStatEX>();
    // �� ����Ʈ
    public List<TempPetStat> _petList = new List<TempPetStat>();
    // ���� ����Ʈ
    public List<TempMonsterStat> _monsterList = new List<TempMonsterStat>();
    // ������ ����Ʈ
    public List<TempItemStat> _itemList = new List<TempItemStat>();
    // ���̺� ����, �ϳ��� ����Ʈ �ƴ�
    public PlayData _save = new PlayData();

    public void Init()
    {
        LoadJson();
    }
    // Data json ���� �ε��ؼ� �� ����Ʈ�� �Ѱ��ִ� �Լ�
    public void LoadJson()
    {
        _fileName = "Data";
        _path = Application.dataPath + "/Resources/Data/Json/" + _fileName + ".json";

        // ���� �����ͼ� �д� �ڵ�
        FileStream fileStream = new FileStream(_path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // ���� ū {} -> ��ü ������, �� ����ó�� ���
        JsonData rootData = JsonMapper.ToObject(json);
        
        // ����) {["Player" : {"_Id" : 11, "_Job" : "boss", } , {...}, {...} ...]}    /-> boss ���
        //Debug.Log(rootData["Player"][0]["_Job"].ToString());

        // �÷��̾�, ��, ����, ������ , ���̺� �� ���� ����Ʈ�� ��ȯ
        ParsePlayerStat(rootData["Player"]);
        ParsePetStat(rootData["Pet"]);
        ParseMonsterStat(rootData["Monster"]);
        ParseItemStat(rootData["Item"]);
        ParseSave(rootData["Save"]);

        // �׽�Ʈ �ڵ� / �� Ȯ�� ��
        //foreach(string one in _save.ItemList)
        //{
        //    Debug.Log("�̸� : " + one);
        //}
    }

    //======================================
    // ���⼭���� ����Ʈ ��ȯ �Լ�

    // �÷��̾� ����Ʈ �����ϴ� �Լ�
    // json -> List<PlayerStat> _playerList �÷��̾� ����Ʈ�� ��ȯ
    public void ParsePlayerStat(JsonData dataList)
    {
        // json {} ������Ʈ ���� ��ŭ ���ҿ� �� ä������
        for(int i = 0; i < dataList.Count; i++)
        {
            // ����Ʈ ���� ����
            TempStatEX data = new TempStatEX();

            data.Hp = int.Parse(dataList[i]["_Hp"].ToString());
            data.Atk = int.Parse(dataList[i]["_Atk"].ToString());
            data.Def = int.Parse(dataList[i]["_Def"].ToString());
            data.Lv = int.Parse(dataList[i]["_Lv"].ToString());
            data.Max_Hp = int.Parse(dataList[i]["_Max_Hp"].ToString());
            // �÷��̾� �̸��� ���⼱ "", Search �Լ����� �־��� ����
            data.Name = "";
            data.Job = dataList[i]["_Job"].ToString();
            data.Exp = int.Parse(dataList[i]["_Exp"].ToString());
            data.Lv_Exp = int.Parse(dataList[i]["_Lv_Exp"].ToString());
            data.Gold = int.Parse(dataList[i]["_Gold"].ToString());

            // ����Ʈ�� ���� �߰�
            _playerList.Add(data);
        }
    }

    // �� ����Ʈ ����
    public void ParsePetStat(JsonData dataList)
    {
        // json {} ������Ʈ ���� ��ŭ ���ҿ� �� ä������
        for (int i = 0; i < dataList.Count; i++)
        {
            // ����Ʈ ���� ����
            TempPetStat data = new TempPetStat();

            data.Name = dataList[i]["_Name"].ToString();
            data.Hp = int.Parse(dataList[i]["_Hp"].ToString());
            data.Atk = int.Parse(dataList[i]["_Atk"].ToString());
            data.Def = int.Parse(dataList[i]["_Def"].ToString());
            data.Lv = int.Parse(dataList[i]["_Lv"].ToString());
            data.Max_Hp = int.Parse(dataList[i]["_Max_Hp"].ToString());
            data.Speed = int.Parse(dataList[i]["_Speed"].ToString());
            data.Revive_Time = int.Parse(dataList[i]["_Revive_Time"].ToString());

            // ����Ʈ�� ���� �߰�
            _petList.Add(data);
        }
    }

    // ���� ����Ʈ ����
    public void ParseMonsterStat(JsonData dataList)
    {
        // json {} ������Ʈ ���� ��ŭ ���ҿ� �� ä������
        for (int i = 0; i < dataList.Count; i++)
        {
            // ����Ʈ ���� ����
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

            // ����Ʈ�� ���� �߰�
            _monsterList.Add(data);
        }
    }

    // ������ ����Ʈ ����
    public void ParseItemStat(JsonData dataList)
    {
        // json {} ������Ʈ ���� ��ŭ ���ҿ� �� ä������
        for (int i = 0; i < dataList.Count; i++)
        {
            // ����Ʈ ���� ����
            TempItemStat data = new TempItemStat();

            data.Id = dataList[i]["_Id"].ToString();
            data.Name = dataList[i]["_Name"].ToString();
            data.Type = dataList[i]["_Type"].ToString();
            data.Skill = int.Parse(dataList[i]["_Skill"].ToString());
            data.Info = dataList[i]["_Info"].ToString();
            data.Get_Price = int.Parse(dataList[i]["_Get_Price"].ToString());
            data.Sale_Price = int.Parse(dataList[i]["_Sale_Price"].ToString());
            data.Count = int.Parse((dataList[i]["_Count"]).ToString());

            // ����Ʈ�� ���� �߰�
            _itemList.Add(data);
        }
    }

    // ���̺� ������ ��ȯ(PlayData Ÿ�� ��ü �ϳ�)
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
    // ���⼭���� �˻� �Լ�

    // ���ϴ� �÷��̾� ������ �˻��ؼ� GameManager�� �Ѱ��ִ� �Լ�
    public void FindPlayerObjData(int Lv, Define.Job Job)
    {
        foreach (TempStatEX one in _playerList)
        {
            // ���� ��ġ�ϸ�
            if (one.Job.Equals(Job.ToString()))
            {
                // ���� ��ġ�ϸ�
                if (one.Lv == Lv)
                {
                    // ���Ӹ޴����� �����ϱ�
                    GameManager.Obj._playerStat.Hp = one.Hp;
                    // ���Ⱑ ������ ���� ��ġ�� ������ �ͼ� ���
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
                    // �÷��̾� �̸��� ĳ���� ���� �� ���ϱ� ������ �� ������ ���ӸŴ����� �����ϰ�
                    // �ű⼭ �̸��� ������ ��
                    GameManager.Obj._playerStat.Name = GameManager.Select._playerName;
                    GameManager.Obj._playerStat.Job = one.Job;
                    GameManager.Obj._playerStat.Exp = one.Exp;
                    GameManager.Obj._playerStat.Lv_Exp = one.Lv_Exp;
                    GameManager.Obj._playerStat.Gold = one.Gold;

                    // �� �־����� Ż���ϱ�
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
            // ���� ��ġ�ϸ�
            if (one.Job.Equals(Job.ToString()))
            {
                // ���� ��ġ�ϸ�
                if (one.Lv == Lv)
                {
                    // ���Ӹ޴����� �����ϱ�
                    tmp.Hp = one.Hp;
                    tmp.Atk = one.Atk;
                    tmp.Def = one.Def;
                    tmp.Lv = one.Lv;
                    tmp.Max_Hp = one.Max_Hp;
                    // �÷��̾� �̸��� ĳ���� ���� �� ���ϱ� ������ �� ������ ���ӸŴ����� �����ϰ�
                    // �ű⼭ �̸��� ������ ��
                    GameManager.Obj._playerStat.Name = GameManager.Select._playerName;
                    tmp.Job = one.Job;
                    tmp.Exp = one.Exp;
                    tmp.Lv_Exp = one.Lv_Exp;
                    tmp.Gold = one.Gold;

                    return tmp;
                    // �� �־����� Ż���ϱ�;
                }
            }
            //return null;
        }
        return null;
    }

    // ���ϴ� �� ������ �˻��ؼ� GameManager�� �Ѱ��ִ� �Լ�
    public void FindPetObjData(Define.Pet PetName)
    {
        foreach (TempPetStat one in _petList)
        {
            // �̸� ��ġ�ϸ�
            if (one.Name.Equals(PetName.ToString()))
            {
                // �� ���� ����
                GameManager.Obj._petStat.Hp = one.Hp;
                GameManager.Obj._petStat.Atk = one.Atk;
                GameManager.Obj._petStat.Def = one.Def;
                GameManager.Obj._petStat.Max_Hp = one.Max_Hp;
                GameManager.Obj._petStat.Name = one.Name;
                GameManager.Obj._petStat.Speed = one.Speed;
                GameManager.Obj._petStat.Revive_Time = one.Revive_Time;

                // �� �־����� Ż���ϱ�
                break;
            }
        }
    }

    // ���ϴ� ���� ������ �˻��ؼ� ���� �ٿ��ִ� �Լ�
    public void FindMonsterObjData(Define.Monster MonsterName, MonsterStat monsterStat)
    {
        foreach (TempMonsterStat one in _monsterList)
        {
            // �̸� ��ġ�ϸ�
            if (one.Name.Equals(MonsterName.ToString()))
            {
                // ���� ���� ����
                monsterStat.Hp = one.Hp;
                monsterStat.Atk = one.Atk;
                monsterStat.Def = one.Def;
                monsterStat.Lv = one.Lv;
                monsterStat.Max_Hp = one.Max_Hp;
                monsterStat.Name = one.Name;
                monsterStat.Gold = one.Gold;
                monsterStat.Exp = one.Exp;
                monsterStat.Speed = one.Speed;

                // �� �־����� Ż���ϱ�
                break;
            }
        }
    }

    // ������ �˻��ؼ� �Ѱ���
    // ���� ������� ��Ĵ�� �ۼ������� �׳� �̸��� �˻��ؼ� ��ȯ�ϴ� ����� ����
    public void FindItemObjData(string itemName , TempItemStat itemStatEX)
    {
        foreach(TempItemStat one in _itemList)
        {
            // �̸����� �˻�
            if(one.Id.Equals(itemName))
            {
                // ������ ��ü�� ����
                itemStatEX.Id = one.Id;
                itemStatEX.Name = one.Name;
                itemStatEX.Type = one.Type;
                itemStatEX.Skill = one.Skill;
                itemStatEX.Info = one.Info;
                itemStatEX.Get_Price = one.Get_Price;
                itemStatEX.Sale_Price = one.Sale_Price;
                itemStatEX.Count = one.Count;

                // �� �־����� Ż��
                break;
            }
        }
    }

    //======================================
    // ���⼭���� Ÿ�� ���� �Լ�(Temp <-> Stat)

    // �÷��̾� ���� PlayerStat -> TempPlayerStat ���� �����ϴ� �Լ�
    // �����ε�(1 / 2)
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

    // �÷��̾� ���� TempPlayerStat -> PlayerStat ���� �����ϴ� �Լ�
    // �����ε�(2 / 2)
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
