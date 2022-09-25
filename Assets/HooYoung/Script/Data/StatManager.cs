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

    // �ӽ� ���� ����� Ŭ���� ����
    TempStatEX _tempStat;

    public void Init()
    {
        // �÷��̾� ���ݽ�ũ��Ʈ�� ���ӸŴ������� �������
        _player = GameManager.Obj._playerStat;

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

        // �ӽ� ���� ����� ��ü ����
        _tempStat = new TempStatEX();
        // �ε��ϰ� PlayerStat �̱��� ��ü�� �ִ´�

        // enum JOB �� Define enum Job���� ���� >> ���� �ٸ��뼭�� �������� ���

        // �÷��̾� ���� �ε�
        PlayerStatLoadJson(1, Define.Job.Superhuman);
        // ���� ���� �ε�
        MonsterStatLoadJson(Define.Monster.Velociraptor);
    }

    // json ���� �ε� �� ���ȵ� PlayerStat �̱��� ��ü�� �ִ� �Լ�
    void PlayerStatLoadJson(int Lv, Define.Job Job)
    {
        // �ҷ��� ���� �̸�
        string fileName = Job.ToString();
        fileName += Lv.ToString();
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

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

        _player.Hp = _tempStat.Hp;
        _player.Atk = _tempStat.Atk;
        _player.Def = _tempStat.Def;
        _player.Lv = _tempStat.Lv;
        _player.Max_Hp = _tempStat.Max_Hp;
        _player.Name = _tempStat.Name;
        _player.Job = _tempStat.Job;
        _player.Exp = _tempStat.Exp;
        _player.Lv_Exp = _tempStat.Lv_Exp;

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
    void MonsterStatLoadJson(Define.Monster name)
    {
        string fileName = name.ToString();
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // PlayerStat static ������ ������ ���� �ֱ�
        // PlayerStat.SetInstance = JsonUtility.FromJson<PlayerStat>(json);


        _tempStat = JsonUtility.FromJson<TempStatEX>(json);

        // ���� ���� ����
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
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
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

