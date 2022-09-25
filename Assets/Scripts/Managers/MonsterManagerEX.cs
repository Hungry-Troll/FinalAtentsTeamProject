using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using static Define;

public class MonsterManagerEX
{
    // 몬스터 매니저 싱글톤 생성 >> 게임매니저로 연결
    // public static MonsterManager instance;
    MonsterController _mobCon;
    //몬스터가 죽었을때
    public bool Property_isDie
    { get; set; }
    // 생성할 프리팹
    // 하단 CreateMonster() RandomPos() 주석 풀고 나서 같이 처리
    //[SerializeField] GameObject MobPrefab = null;

    // 객체를 저장할 리스트
    public List<Vector3> _mobPosList
    { get; set; }
    public int _mobindex
    {
        get
        { return _mobCount; }
    }

    public int _mobCount;
    int _mobSetNum = 0;

    float[] _currentTime;

    void Awake()
    {
        _mobPosList = new List<Vector3>();
        _mobCount = 5;
        // 몬스터 매니저 싱글톤 생성 >> 게임매니저로 연결
        //instance = this;

        for (int i = 0; i < _mobCount; i++)
        {
            // CreateMonster();
            Debug.Log(_mobPosList[i]);
        }
        _currentTime = new float[_mobCount];
    }

    void Update()
    {
        ReSpawnTime();
    }

    // 몬스터 생성코드 >> 추후 필드매니저를 통해서 생성
    // 던전 나오면 적용

    //public void CreateMonster()
    //{
    //    GameObject mob = Instantiate(MobPrefab, gameObject.transform);
    //    _mobCon = mob.transform.GetComponent<MonsterController>();
    //    _mobCon._mobNum = _mobSetNum++;
    //    mob.transform.position = RandomPos(40);
    //    _mobPosList.Add(mob.transform.position);
    //    Property_isDie = false;
    //    gameObject.SetActive(true);

    //}


    // 몬스터 위치 코드 >> 추후 필드매니저를 통해서 생성하는 것으로 변경
    // 던전 나오면 적용

    //onUnitSphere함수를 사용하여 임의의 원 범위 안에서 몬스터 스폰
    public Vector3 RandomPos(float radius, Transform transform)
    {
        Vector3 _pos = Random.onUnitSphere;
        //높이는 0으로 설정
        _pos.y = 0.0f;
        float r = Random.Range(0.0f, radius);
        return (_pos * r) + transform.position;
    }


    // 리스폰 시간

    public void ReSpawnTime()
    {
        for (int j = 0; j < _mobCount; j++)
        {
            if (GameObject.Find("MobSpawner").transform.GetChild(j).gameObject.activeSelf == false)
            {
                Debug.Log("리스폰!!!");
                _currentTime[j] += Time.deltaTime;
                if (_currentTime[j] >= 7)
                {
                    this.Property_isDie = false;
                    GameObject.Find("MobSpawner").transform.GetChild(j).gameObject.SetActive(true);
                    GameObject.Find("MobSpawner").transform.GetChild(j).transform.position = _mobPosList[j];
                    Debug.Log("리스폰222!!!");
                    _currentTime[j] = 0.0f;
                }
            }
        }
    }
}
