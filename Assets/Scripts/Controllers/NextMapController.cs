using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �� �̵� ���� ��Ʈ�ѷ�
public class NextMapController : MonoBehaviour
{
    // ���ο� ������ �Ѿ�� ��Ʈ�ѷ� �Ŵ����� ������ �� ���� ����
    // �÷��̾� �ݶ��̴� Ȯ�ο�
    Collider _playerCollider;

    void Start()
    {
        // OnTriggerEnter ����� ���� �÷��̾� �ݶ��̴��� ������ ��
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �ݶ��̴��� �浹�ߴٸ�
        if (other == _playerCollider)
        {
            // ���� �� üũ
            SceneCheck();
        }
    }
    // ���� �� üũ �� ���� �´� �Լ� ����
    private void SceneCheck()
    {
        switch (GameManager.Scene._sceneNameEnum)
        {
            case Define.SceneName.Tutorial:
                MoveToVillage02();
                break;
            case Define.SceneName.Village02:
                MoveToDunGeon();
                break;
        }
    }
    // ������ �� �̵��ϴ� �Լ�
    public void MoveToVillage02()
    {   
         GameManager.Data.SaveData_1();
         GameManager.Scene.LoadScene("Village02");
         // ���� �����͸� �����Ѵ�.
         // ���� ������ �Ѿ��.
         // ������ �����͸� �ҷ��´�.
    }   
    // �������� �� �̵��ϴ� �Լ�
    public void MoveToDunGeon()
    {
        GameManager.Data.SaveData_1();
        GameManager.Scene.LoadScene("DunGeon");
    }

}
