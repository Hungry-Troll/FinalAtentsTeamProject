using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class MonsterManager : MonoBehaviour
{
    // 몬스터 매니저 싱글톤 생성
    public static MonsterManager instance;
    MonsterController _mobCon;
    //몬스터가 죽었을때
    public bool Property_isDie
    { get; set; }
    // 생성할 프리팹
    [SerializeField] GameObject MobPrefab = null;
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
        instance = this;

        for (int i = 0; i < _mobCount; i++)
        {
            CreateMonster(RandomPos(20));
            Debug.Log(_mobPosList[i]);
        }
        _currentTime = new float[_mobCount];
    }

    void Update()
    {
        ReSpawnTime();
    }

    public Vector3 RandomPos(float radius)
    {
        Vector3 _pos = Random.onUnitSphere;
        float r = Random.Range(-radius, radius);
        return (_pos * r) + transform.position;
    }

    public void CreateMonster(Vector3 _pos)
    {
        _pos.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(_pos, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject mob = Instantiate(MobPrefab, hit.point, Quaternion.identity, gameObject.transform);
            _mobCon = mob.transform.GetComponent<MonsterController>();
            _mobCon._mobNum = _mobSetNum++;
            _mobPosList.Add(mob.transform.position);
            Property_isDie = false;
            gameObject.SetActive(true);
        }
    }

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
