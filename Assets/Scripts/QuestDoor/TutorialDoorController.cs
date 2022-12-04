using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 듀토리얼맵 퀘스트 진행에 따라서 사라지는 비활성화되는 오브젝트
public class TutorialDoorController : MonoBehaviour
{
    public BoxCollider[] _boxCollider;
    public NavMeshObstacle[] _navMeshObstacle;
    // Start is called before the first frame update

    private void Start()
    {
        _boxCollider = new BoxCollider[2];
        _navMeshObstacle = new NavMeshObstacle[2];

        _boxCollider = GetComponentsInChildren<BoxCollider>();
        _navMeshObstacle = GetComponentsInChildren<NavMeshObstacle>();

        // 퀘스트매니저에 정보를 넘겨준다.
        // 이후 퀘스트 진행상황에 따라 비활성화 시킴
        GameManager.Quest._boxCollider = _boxCollider;
        GameManager.Quest._navMeshObstacle = _navMeshObstacle;
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
