using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Text;

public class TestMain : MonoBehaviour
{
    // 싱글톤 객체 가져오기
    private PlayerStat _player = PlayerStat.Instance();

    private void Start()
    {
        // 생성 및 저장
        CreateFile(2, JOB.Superhuman);

        // 로드하고 PlayerStat 싱글톤 객체에 넣는다
        LoadJson();
    }

    void Update()
    {

    }

    // json 파일 로드 후 스탯들 PlayerStat 싱글톤 객체에 넣는 함수
    void LoadJson()
    {
        // 불러올 파일 이름
        string fileName = "playerStat";
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);

        // PlayerStat static 변수에 각각의 스탯 넣기
        PlayerStat.SetInstance = JsonUtility.FromJson<PlayerStat>(json);

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

    // 플레이어 스탯 json 파일 생성 및 저장 
    // _Job : JOB 타입 enum
    // 강화인간 : JOB.Superhuman
    // 사이보그 : JOB.Cyborg
    // 사이언티스트 : JOB.Scientist
    void CreateFile(int _Lv, JOB _Job)
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
        string fileName = "playerStat";
        // 경로
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName + ".json";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // 레벨, 직업별 초기값 정하는 함수
    void SortStat(int lv, JOB job)
    {
        // 레벨
        _player.Lv = lv;

        // 직업별 체력, 공격력, 방어력 set
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
