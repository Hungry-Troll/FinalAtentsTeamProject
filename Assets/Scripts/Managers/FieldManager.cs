using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    //게임 시작시 Awake , Start, Update 사용 용도 매니저
    PlayerController _player;
    _Pet_01 _pet;
    ItemController _item;

    public GameObject _startPosObject;
    public Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        // 게임매니저에서 Ui매니저 Init(Awake 함수 대체)
        // Ui 불러옴
        GameManager.Ui.Init();
        GameManager.Cam.Init();
        // 플레이어 캐릭터 생성
        // 추후 플레이어 선택창에서 string 으로 이름만 받아오면 됨
        // 시작위치는 맵마다 다르게 해야 됨
        _startPos = _startPosObject.transform.position;
        //Obj매니저에서 플레이어스크립트를 들고있게함
        GameManager.Obj._playerController = CreatePlayerCharacter(_startPos, "player");
        CreatePet(_startPos, "Fox");
        //아이템 생성용 테스트 코드
        for (int i = 0; i < GameManager.Resource._fieldItem.Count; i++)
        {
            Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
            _item = CreateFieldItem(_player.transform.position + tempPos, GameManager.Resource._fieldItem[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public PlayerController CreatePlayerCharacter(Vector3 origin , string playerName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPlayerChar = GameManager.Resource.GetCharacter(playerName);
            if (temPlayerChar != null)
            {
                GameObject player = GameObject.Instantiate<GameObject>(temPlayerChar,hit.point,Quaternion.identity);
                _player = player.AddComponent<PlayerController>();
                return _player;
            }
        }
        return null;
    }
    public _Pet_01 CreatePet(Vector3 origin, string petName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temPet = GameManager.Resource.GetPet(petName);
            if (temPet != null)
            {
                GameObject pet = GameObject.Instantiate<GameObject>(temPet, hit.point, Quaternion.identity);
                _pet = pet.AddComponent<_Pet_01>();
                return _pet;
            }
        }
        return null;
    }
    public ItemController CreateFieldItem(Vector3 origin, string fieldItemName)
    {
        // 위에서 레이를 쏴서 지형 높이에 따른 캐릭터 생성 코드
        origin.y += 100f;
        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject temfieldItem = GameManager.Resource.GetfieldItem(fieldItemName);
            if (temfieldItem != null)
            {
                string tempName = temfieldItem.name;
                GameObject fieldItem = GameObject.Instantiate<GameObject>(temfieldItem, hit.point, Quaternion.identity);
                _item = fieldItem.AddComponent<ItemController>();
                _item.name = tempName;
                return _item;
            }
        }
        return null;
    }
}
