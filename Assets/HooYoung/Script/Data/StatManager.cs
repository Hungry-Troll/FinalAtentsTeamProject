using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Text;

// TestMainŬ������ ����Ÿ �Ŵ����� ����
// ��� �����ʹ� ���⸦ ���ؼ� �ε�
public class StatManager
{
    // �̱��� ��ü ��������
    // private PlayerStat _player = PlayerStat.Instance();
    // �ʵ�Ŵ������� ĳ���� ������ addComponenet�� ���� >> ������Ʈ �Ŵ����� ������ �÷��̾� ���ݽ�ũ��Ʈ ������ ��
    public PlayerStat _player;
    public MonsterStat _monster;
    public PetStat _pet;

    // ������ ����Ʈ, CreateItemFile() ���� ���
    private List<ItemStat> _createdItemList;
    private List<ItemStat> _ItemList;
    // SplitJson(string json) ���� ����ϴ� item ��������� json string�迭 
    private string[] _itemJsonArr;

    // �ӽ� ���� ����� Ŭ���� ����
    TempStatEX _tempStat;

    public void Init()
    {
        // �÷��̾� ���ݽ�ũ��Ʈ�� ���ӸŴ������� �������
        // _player = GameManager.Obj._playerStat;
        _pet = GameManager.Obj._petStat;

        // ���� �� ����
        // �ѹ� �����͸� �����ϴµ� ����� �ڵ�
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

        // ������ ������ ����� �ڵ�
        // �ڵ�, �̸�(string), Ÿ��(string), ��ų, ������(string), ���ź��, �Ǹź��
        /*
        // Weapon
        ItemStat Sword = new ItemStat(111, "��", Define.ItemType.Weapon.ToString(), 10, "����� ��", 100, 10);
        ItemStat IronSword = new ItemStat(112, "��ö��", Define.ItemType.Weapon.ToString(), 20, "�ܴ��� ��ö�� ���� ��", 200, 20);
        ItemStat LegendSword = new ItemStat(113, "������ ��", Define.ItemType.Weapon.ToString(), 30, "������ ���� ��� ��", 300, 30);
        ItemStat Revolver = new ItemStat(121, "������", Define.ItemType.Weapon.ToString(), 10, "�ܼ��� ����", 100, 10);
        ItemStat AutomaticRifle = new ItemStat(122, "�ڵ�����", Define.ItemType.Weapon.ToString(), 20, "���� ����� ���� ��", 200, 20);
        ItemStat LaserGun = new ItemStat(123, "������ ��", Define.ItemType.Weapon.ToString(), 30, "������ ���� ��� ��", 300, 30);
        ItemStat LuckyBook = new ItemStat(131, "����� å", Define.ItemType.Weapon.ToString(), 10, "�Ǹ� ����� ��� å", 100, 80);
        ItemStat ThickDictionary = new ItemStat(132, "�β��� ����", Define.ItemType.Weapon.ToString(), 20, "�𼭸��� ��ī�ο� ����", 200, 20);
        ItemStat DevilsProphet = new ItemStat(133, "�Ǹ��� ����", Define.ItemType.Weapon.ToString(), 30, "���� ������ ������ å", 300, 30);
        
        // Armour
        ItemStat SuperSuit = new ItemStat(211, "��ȭ�ΰ� ��Ʈ", Define.ItemType.Armour.ToString(), 5, "��ȭ�ΰ� ���� ��", 100, 10);
        ItemStat EnergyShield = new ItemStat(221, "������ �帷", Define.ItemType.Armour.ToString(), 5, "���̺��� ���� ��", 100, 10);
        ItemStat WhiteCoat = new ItemStat(231, "��� ����", Define.ItemType.Armour.ToString(), 5, "���̾�Ƽ��Ʈ ���� ��", 100, 10);
        
        // Potion(Consumables)
        ItemStat HP_Potion = new ItemStat(301, "ü�� ����", Define.ItemType.Consumables.ToString(), 0, "ü���� 50 ȸ�������ش�", 10, 1);
        ItemStat Recover_Potion = new ItemStat(302, "�����̻� ȸ�� ����", Define.ItemType.Consumables.ToString(), 1, "�����̻��� ȸ�������ش�.", 20, 2);
        ItemStat SpeedUp_Potion = new ItemStat(303, "�̵��ӵ� ���� ����", Define.ItemType.Consumables.ToString(), 2, "10�ʵ��� �̵��ӵ��� ���������ش�.", 30, 3);
        
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


        // �ӽ� ���� ����� ��ü ����
        _tempStat = new TempStatEX();
        // �ε��ϰ� PlayerStat �̱��� ��ü�� �ִ´�

        // enum JOB �� Define enum Job���� ���� >> ���� �ٸ��뼭�� �������� ���

        // �÷��̾� ���� �ε�
        //PlayerStatLoadJson(1, Define.Job.Superhuman);
        // ���� ���� �ε�
        //MonsterStatLoadJson(Define.Monster.Velociraptor);

        // ������ ���� �ε�
        // item json -> _ItemList<ItemStat> �� ��ȯ
        _createdItemList = new List<ItemStat>();
        _ItemList = new List<ItemStat>();
        LoadItemList();
    // ��ġ ������ id �ڵ带 ���� ������ �̸��� �����ϰ� ������
    // id �ڵ� ��� �������� �� ������ �̸����� ����


    // ������, ������ �ڵ�� ã��
    //Debug.Log("id 123 : " + SearchItem(123).Name);
    //Debug.Log("id 123 : " + SearchItem(123).Info);
}

    // json ���� �ε� �� ���ȵ� PlayerStat �̱��� ��ü�� �ִ� �Լ�
    public void PlayerStatLoadJson(int Lv, Define.Job Job)
    {
        // �ҷ��� ���� �̸�
        string fileName = Job.ToString();
        fileName += Lv.ToString();
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/Player/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // PlayerStat static ������ ������ ���� �ֱ�
        // PlayerStat.SetInstance = JsonUtility.FromJson<PlayerStat>(json);

        // PlayerStat�� ������Ʈ�� ���� ���ؼ� StatŬ������ MonoBehaviour ���
        // �׷��� JsonUtility.FromJson �� ����� �� ���⿡ �ӽ� TempStatEX Ŭ������ ���� �����͸� �����ϰ� �ٽ� �÷��̾� ���ݿ� ����
        // �÷��̾� ������ �÷��̾ ������Ʈ�� ����־�� �����̳� �׽�Ʈ�� ���� ������ �����մϴ�.
        _tempStat = JsonUtility.FromJson<TempStatEX>(json);

        GameManager.Obj._playerStat.Hp = _tempStat.Hp;
        GameManager.Obj._playerStat.Atk = _tempStat.Atk;
        GameManager.Obj._playerStat.Def = _tempStat.Def;
        GameManager.Obj._playerStat.Lv = _tempStat.Lv;
        GameManager.Obj._playerStat.Max_Hp = _tempStat.Max_Hp;
        // �÷��̾� �̸��� ĳ���� ���� �� ���ϱ� ������ �� ������ ���ӸŴ����� �����ϰ�
        // �ű⼭ �̸��� ������ ��
        GameManager.Obj._playerStat.Name = GameManager.Select._playerName;
        GameManager.Obj._playerStat.Job = _tempStat.Job;
        GameManager.Obj._playerStat.Exp = _tempStat.Exp;
        GameManager.Obj._playerStat.Lv_Exp = _tempStat.Lv_Exp;

        // ������
        /*
        Debug.Log("�÷��̾� �̸� : " + _player.Name);
        Debug.Log("���� : " + _player.Lv);
        Debug.Log("���� : " + _player.Job);
        Debug.Log("�⺻ ���ݷ� : " + _player.Atk);
        Debug.Log("�ִ� ü�� : " + _player.Max_Hp);
        Debug.Log("������ ����ġ : " + _player.Lv_Exp);
        */
    }

    // ���� �̸����� ���� �ҷ���
    public void MonsterStatLoadJson(string monsterName, MonsterStat monsterStat)
    {
        string fileName = monsterName;
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/Monster/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // PlayerStat static ������ ������ ���� �ֱ�
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

    // �� �̸����� ���� �ҷ���
    public void PetStatLoadJson(Define.Pet name)
    {
        string fileName = name.ToString();
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/Pet/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        _tempStat = JsonUtility.FromJson<TempStatEX>(json);

        // �� ���� ����
        GameManager.Obj._petStat.Hp = _tempStat.Hp;
        GameManager.Obj._petStat.Atk = _tempStat.Atk;
        GameManager.Obj._petStat.Def = _tempStat.Def;
        GameManager.Obj._petStat.Max_Hp = _tempStat.Max_Hp;
        GameManager.Obj._petStat.Name = _tempStat.Name;
        GameManager.Obj._petStat.Speed = _tempStat.Speed;
        GameManager.Obj._petStat.Revive_Time = _tempStat.Revive_Time;
    }
    // ������ ���� �ҷ���
    // ItemStatEX �� �����ϰ� ���� ���̴� ������Ʈ�뵵
    public void ItemStatLoadJson(string itemName, ItemStatEX itemStatEX)
    {
        // �̸����� ��ġ
        ItemStat tempStat = SearchItem(itemName);
        // ������ ������ ����
        itemStatEX.Id = tempStat.Id;
        itemStatEX.Name = tempStat.Name;
        itemStatEX.Type = tempStat.Type;
        itemStatEX.Skill = tempStat.Skill;
        itemStatEX.Info = tempStat.Info;
        itemStatEX.Get_Price = tempStat.Get_Price;
        itemStatEX.Sale_Price = tempStat.Sale_Price;
    }

    // ������ �ε�
    public void LoadItemList()
    {
        string fileName = "Items";
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/Item/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // {}{}{} -> {} {} {} ���� �迭�� ����
        SplitJson(json);

        for(int i = 0; i < _itemJsonArr.Length; i++)
        {
            // {name, type, skill...} �� ��Ʈ�� ItemStat Ÿ������ parsing
            ItemStat item = JsonUtility.FromJson<ItemStat>(_itemJsonArr[i]);
            // ����Ʈ�� �߰�
            _ItemList.Add(item);
        }
    }

    // ������ ���̵�� �˻��ؼ� ��ȯ�ϴ� �Լ�
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

    // �÷��̾� ���� json ���� ���� �� ���� 
    // _Job : JOB Ÿ�� enum
    // ��ȭ�ΰ� : JOB.Superhuman
    // ���̺��� : JOB.Cyborg
    // ���̾�Ƽ��Ʈ : JOB.Scientist
    void CreateFile(int _Lv, Define.Job _Job)
    {
        // ������ �ӽ÷� ���ͷ� �� ����
        /*
        _player.Name = "p1";
        _player.Lv = 2;
        _player.Hp = 100;
        _player.Atk = 0;
        _player.Def = 0;
        _player.Exp = 0;
        _player.Job = "Scientist";
        */

        // ������ ������ ������ ���� setting
        SortStat(_Lv, _Job);

        // static ��ü�� ������ �ִ� ���ȵ� json string ���� ��ȯ
        string json = JsonUtility.ToJson(_player);
        // ���� �̸�
        //string fileName = "playerStat";

        // �ҷ��� ���� �̸�
        string fileName = _Job.ToString();
        fileName += _Lv.ToString();

        // ���
        string path = Application.dataPath + "/Resources/Data/Json/Player/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void CreatePetFile(Define.Pet _Pet)
    {
        // ������ �ӽ÷� ���ͷ� �� ����
        /*
        _pet.Name = _Pet.ToString();
        _pet.Lv = 0;
        _pet.Hp = 160;
        _pet.Atk = 20;
        _pet.Def = 5;
        _pet.Speed = 0;
        _pet.Revive_Time = 60;
        */

        // static ��ü�� ������ �ִ� ���ȵ� json string ���� ��ȯ
        string json = JsonUtility.ToJson(_pet, true);
        // ���� �̸�
        //string fileName = "playerStat";

        // �ҷ��� ���� �̸�
        string fileName = _Pet.ToString();

        // ���
        string path = Application.dataPath + "/Resources/Data/Json/Pet/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // ������ json ���Ͽ� �� �߰��ϴ� �Լ�
    void CreateItemFile(ItemStat _item)
    {

        // ������ �ӽ÷� ���ͷ� �� ����
        /*
        _item.Name = "sword3";
        _item.Type = "Weapon";
        _item.Skill = 10;
        _item.Info = "�׳� ��";
        _item.Get_Price = 10;
        _item.Sale_Price = 5;
        */

        // json���� ��ȯ�� string
        string json;
        // �ҷ��� ���� �̸�
        string fileName = "Items";
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/Item/" + fileName + ".json";
        // ���� ����α�
        FileStream fileStream = new FileStream(path, FileMode.Create);

        // ������ ����Ʈ�� �߰�
        _createdItemList.Add(_item);

        // ����Ʈ null check
        if(_createdItemList.Count > 0)
        {
            foreach(ItemStat one in _createdItemList)
            {
                // static ��ü�� ������ �ִ� ���ȵ� json string ���� ��ȯ
                json = JsonUtility.ToJson(one, true);
                
                byte[] data = Encoding.UTF8.GetBytes(json);
                fileStream.Write(data, 0, data.Length);
            }
        }
        fileStream.Close();
    }

    // ����, ������ �ʱⰪ ���ϴ� �Լ�
    void SortStat(int lv, Define.Job job)
    {
        // ����
        _player.Lv = lv;

        // ������ ü��, ���ݷ�, ���� set
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
        
        // ������ ����ġ set
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
            
            // ������
            //Debug.Log(_itemJsonArr += "}");
        }
    }
}

// �ӽ� ���� ����� Ŭ����
public class TempStatEX
{
    // ���� ü��
    [SerializeField]
    private int _Hp;

    // ���ݷ�
    [SerializeField]
    private int _Atk;

    // ����
    [SerializeField]
    private int _Def;

    // ���� ����
    [SerializeField]
    private int _Lv;

    // �ִ� ü��
    [SerializeField]
    private int _Max_Hp;

    // �̸�
    [SerializeField]
    private string _Name;

    // ����
    [SerializeField]
    private string _Job;

    // ���
    [SerializeField]
    private int _Gold;

    // ���� ����ġ
    [SerializeField]
    private int _Exp;

    // ������ ����ġ
    [SerializeField]
    private int _Lv_Exp;

    // �̵��ӵ�
    [SerializeField]
    private int _Speed;

    // ��Ȱ�ð�
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

