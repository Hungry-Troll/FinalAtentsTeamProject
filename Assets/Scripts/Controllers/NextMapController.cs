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
        if(other == _playerCollider)
        {
            GameManager.Data.SaveData();
            GameManager.Scene.LoadScene("Village02");
            // ���� �����͸� �����Ѵ�.
            // ���� ������ �Ѿ��.
            // ������ �����͸� �ҷ��´�.
        }
    }
}
