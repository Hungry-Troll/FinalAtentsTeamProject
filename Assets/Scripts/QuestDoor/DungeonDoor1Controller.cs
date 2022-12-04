using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 듀토리얼맵 퀘스트 진행에 따라서 사라지는 비활성화되는 오브젝트
public class DungeonDoor1Controller : MonoBehaviour
{
    public BoxCollider _boxCollider;
    public NavMeshObstacle _navMeshObstacle;
    // Start is called before the first frame update

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();

        // 퀘스트매니저에 정보를 넘겨준다.
        // 이후 퀘스트 진행상황에 따라 비활성화 시킴
        // 이전 자료는 제거
        GameManager.Quest._boxCollider[0] = null;
        GameManager.Quest._navMeshObstacle[0] = null;
        GameManager.Quest._boxCollider[0] = _boxCollider;
        GameManager.Quest._navMeshObstacle[0] = _navMeshObstacle;
    }

    // 추후 지나갈 수 없습니다 UI로 대체 해야 됨
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("아직 지나갈 수 없습니다.");
        }
    }

}
