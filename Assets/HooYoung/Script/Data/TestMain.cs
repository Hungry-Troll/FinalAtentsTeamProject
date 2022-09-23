using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Text;

public class TestMain : MonoBehaviour
{
    // �̱��� ��ü ��������
    private PlayerStat _player = PlayerStat.Instance();

    private void Start()
    {
        // ���� �� ����
        CreateFile(2, JOB.Superhuman);

        // �ε��ϰ� PlayerStat �̱��� ��ü�� �ִ´�
        LoadJson();
    }

    void Update()
    {

    }

    // json ���� �ε� �� ���ȵ� PlayerStat �̱��� ��ü�� �ִ� �Լ�
    void LoadJson()
    {
        // �ҷ��� ���� �̸�
        string fileName = "playerStat";
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // PlayerStat static ������ ������ ���� �ֱ�
        PlayerStat.SetInstance = JsonUtility.FromJson<PlayerStat>(json);

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

    // �÷��̾� ���� json ���� ���� �� ���� 
    // _Job : JOB Ÿ�� enum
    // ��ȭ�ΰ� : JOB.Superhuman
    // ���̺��� : JOB.Cyborg
    // ���̾�Ƽ��Ʈ : JOB.Scientist
    void CreateFile(int _Lv, JOB _Job)
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
        string fileName = "playerStat";
        // ���
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // ����, ������ �ʱⰪ ���ϴ� �Լ�
    void SortStat(int lv, JOB job)
    {
        // ����
        _player.Lv = lv;

        // ������ ü��, ���ݷ�, ���� set
        switch(job)
        {
            case JOB.Superhuman:
                _player.Max_Hp = (int)(lv * 0.5 * 100) + 50;
                _player.Atk = lv * 5;
                _player.Def = 10;
                _player.Job = "Superhuman";
                break;
            case JOB.Cyborg:
                _player.Max_Hp = (int)(lv * 0.5 * 80) + 40;
                _player.Atk = lv * 5;
                _player.Def = 5;
                _player.Job = "Cyborg";
                break;
            case JOB.Scientist:
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
