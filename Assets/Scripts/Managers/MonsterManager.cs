using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using static Define;

public class MonsterManager : MonoBehaviour
{
    //���� �Ŵ��� �̱��� ����
    public static MonsterManager instance;
    MonsterController _mobCon;
    //���Ͱ� �׾�����
    public bool Property_isDie
    { get; set; }
    // ������ ������
    [SerializeField] GameObject MobPrefab = null;
    // ��ü�� ������ ����Ʈ
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
            CreateMonster();
            Debug.Log(_mobPosList[i]);
        }
        _currentTime = new float[_mobCount];
    }

    void Update()
    {
        ReSpawnTime();
    }

    public void CreateMonster()
    {
        GameObject mob = Instantiate(MobPrefab, gameObject.transform);
        _mobCon = mob.transform.GetComponent<MonsterController>();
        _mobCon._mobNum = _mobSetNum++;
        mob.transform.position = RandomPos(40);
        _mobPosList.Add(mob.transform.position);
        Property_isDie = false;
        gameObject.SetActive(true);

    }

    //onUnitSphere�Լ��� ����Ͽ� ������ �� ���� �ȿ��� ���� ����
    public Vector3 RandomPos(float radius)
    {
        Vector3 _pos = Random.onUnitSphere;
        //���̴� 0���� ����
        _pos.y = 0.0f;
        float r = Random.Range(0.0f, radius);
        return (_pos * r) + transform.position;
    }

    public void ReSpawnTime()
    {
        for (int j = 0; j < _mobCount; j++)
        {
            if (GameObject.Find("MobSpawner").transform.GetChild(j).gameObject.activeSelf == false)
            {
                Debug.Log("������!!!");
                _currentTime[j] += Time.deltaTime;
                if (_currentTime[j] >= 7)
                {
                    this.Property_isDie = false;
                    GameObject.Find("MobSpawner").transform.GetChild(j).gameObject.SetActive(true);
                    GameObject.Find("MobSpawner").transform.GetChild(j).transform.position = _mobPosList[j];
                    Debug.Log("������222!!!");
                    _currentTime[j] = 0.0f;
                }
            }
        }
    }
}
