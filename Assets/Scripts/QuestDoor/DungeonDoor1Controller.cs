using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���丮��� ����Ʈ ���࿡ ���� ������� ��Ȱ��ȭ�Ǵ� ������Ʈ
public class DungeonDoor1Controller : MonoBehaviour
{
    public BoxCollider _boxCollider;
    public NavMeshObstacle _navMeshObstacle;
    // Start is called before the first frame update

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();

        // ����Ʈ�Ŵ����� ������ �Ѱ��ش�.
        // ���� ����Ʈ �����Ȳ�� ���� ��Ȱ��ȭ ��Ŵ
        // ���� �ڷ�� ����
        GameManager.Quest._boxCollider[0] = null;
        GameManager.Quest._navMeshObstacle[0] = null;
        GameManager.Quest._boxCollider[0] = _boxCollider;
        GameManager.Quest._navMeshObstacle[0] = _navMeshObstacle;
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
