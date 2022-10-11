using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;

public class ParseManager
{
    // ���� �̸� ����
    public static string _fileName;
    // ��� ���� ����
    public static string _path;

    // ===Data json ���Ͽ��� ������ ����Ʈ===
    // �÷��̾� ����Ʈ
    public List<PlayerStat> _playerList = new List<PlayerStat>();
    // �� ����Ʈ
    public List<PetStat> _petList = new List<PetStat>();
    // ���� ����Ʈ
    public List<MonsterStat> _monsterList = new List<MonsterStat>();
    // ������ ����Ʈ
    public List<ItemStatEX> _itemList = new List<ItemStatEX>();
    // ���̺� ����, �ϳ��� ����Ʈ �ƴ�
    public PlayData _save = new PlayData();

    public void Start()
    {

    }

    public void Update()
    {
        
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
            PlayerStat data = new PlayerStat();

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
            PetStat data = new PetStat();

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
            ItemStatEX data = new ItemStatEX();

            data.Id = dataList[i]["_Id"].ToString();
            data.Name = dataList[i]["_Name"].ToString();
            data.Type = dataList[i]["_Type"].ToString();
            data.Skill = int.Parse(dataList[i]["_Skill"].ToString());
            data.Info = dataList[i]["_Info"].ToString();
            data.Get_Price = int.Parse(dataList[i]["_Get_Price"].ToString());
            data.Sale_Price = int.Parse(dataList[i]["_Sale_Price"].ToString());

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
        foreach(PlayerStat one in _playerList)
        {
            // ���� ��ġ�ϸ�
            if(one.Job.Equals(Job.ToString()))
            {
                // ���� ��ġ�ϸ�
                if(one.Lv == Lv)
                {
                    // ���Ӹ޴����� �����ϱ�
                    GameManager.Obj._playerStat.Hp = one.Hp;
                    GameManager.Obj._playerStat.Atk = one.Atk;
                    GameManager.Obj._playerStat.Def = one.Def;
                    GameManager.Obj._playerStat.Lv = one.Lv;
                    GameManager.Obj._playerStat.Max_Hp = one.Max_Hp;
                    // �÷��̾� �̸��� ĳ���� ���� �� ���ϱ� ������ �� ������ ���ӸŴ����� �����ϰ�
                    // �ű⼭ �̸��� ������ ��
                    GameManager.Obj._playerStat.Name = GameManager.Select._playerName;
                    GameManager.Obj._playerStat.Job = one.Job;
                    GameManager.Obj._playerStat.Exp = one.Exp;
                    GameManager.Obj._playerStat.Lv_Exp = one.Lv_Exp;

                    // �� �־����� Ż���ϱ�
                    break;
                }
            }
        }
    }

    // ���ϴ� �� ������ �˻��ؼ� GameManager�� �Ѱ��ִ� �Լ�
    public void FindPetObjData(Define.Pet PetName)
    {
        foreach (PetStat one in _petList)
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
        foreach (MonsterStat one in _monsterList)
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
    public void FindItemObjData(string itemName ,ItemStatEX itemStatEX)
    {
        foreach(ItemStatEX one in _itemList)
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

                // �� �־����� Ż��
                break;
            }
        }
    }
}
