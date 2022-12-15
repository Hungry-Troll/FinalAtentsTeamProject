using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartController : MonoBehaviour
{
    // �÷��̾� �ݶ��̴� Ȯ�ο�
    Collider _playerCollider;

    // �ӽ� ���� ��ġ ������ / ����ִ� ���ӿ�����Ʈ�� �÷��̾� ��ġ ����
    public GameObject _startPosBoss;
    public Vector3 _startPos;
    bool _bossSwan;

    // Start is called before the first frame update
    void Start()
    {
        // OnTriggerEnter ����� ���� �÷��̾� �ݶ��̴��� ������ ��
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
        _bossSwan = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �ݶ��̴��� �浹�ߴٸ�
        // ���� ����
        if(_bossSwan == false)
        {
            _startPos = _startPosBoss.transform.position;
            // ���� ����
            GameManager.Create.CreateHumanBoss(_startPos, "BossHuman");
            // ����Ʈ ����
            GameManager.Quest.QuestProgressValueAdd();
            _bossSwan = true;
            // ���̽�ƽ ����
            GameManager.Quest.QuestJoystickStop();
        }
        // ���� �ó׸ӽ� �۵�
    }
}
