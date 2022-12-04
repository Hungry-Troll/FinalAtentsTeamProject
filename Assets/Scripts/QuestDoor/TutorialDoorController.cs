using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���丮��� ����Ʈ ���࿡ ���� ������� ��Ȱ��ȭ�Ǵ� ������Ʈ
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

        // ����Ʈ�Ŵ����� ������ �Ѱ��ش�.
        // ���� ����Ʈ �����Ȳ�� ���� ��Ȱ��ȭ ��Ŵ
        GameManager.Quest._boxCollider = _boxCollider;
        GameManager.Quest._navMeshObstacle = _navMeshObstacle;
    }

    // ���� ������ �� �����ϴ� UI�� ��ü �ؾ� ��
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("���� ������ �� �����ϴ�.");
        }
    }

}
