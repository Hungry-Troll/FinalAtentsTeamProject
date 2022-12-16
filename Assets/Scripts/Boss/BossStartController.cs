using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossStartController : MonoBehaviour
{
    // �÷��̾� �ݶ��̴� Ȯ�ο�
    Collider _playerCollider;
    BoxCollider _thisObj;

    // �ӽ� ���� ��ġ ������ / ����ִ� ���ӿ�����Ʈ�� �÷��̾� ��ġ ����
    public GameObject _startPosBoss;
    public Vector3 _startPos;
    bool _bossSwan;

    void Start()
    {
        // OnTriggerEnter ����� ���� �÷��̾� �ݶ��̴��� ������ ��
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
        _bossSwan = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �ݶ��̴��� �浹�ߴٸ�

        // 1. ���̽�ƽ ����
        GameManager.Quest.QuestJoystickStop();

        // 2. ���� �ó׸ӽ�1 �۵�
        GameManager.Cam.BossCamOn1();
        
        // 3. ����Ʈ ����
        GameManager.Quest.QuestProgressValueAdd();

        StartCoroutine(BossCreator());

        //���� �ó׸ӽ�3 ����
        GameManager.Cam.BossCamOff1();
        StartCoroutine(TurnOffCam());
    }

    IEnumerator BossCreator()
    {
        yield return new WaitForSeconds(5f);
        if (_bossSwan == false)
        {   //���� �ó׸ӽ�2 �۵�
            GameManager.Cam.BossCamOff1();
            Destroy(GameManager.Cam._Vcam4);
            GameManager.Cam.BossCamOn2();
            _startPos = _startPosBoss.transform.position;
            // ���� ����
            GameManager.Create.CreateHumanBoss(_startPos, "BossHuman");
            
            _bossSwan = true;
            yield return new WaitForSeconds(5f);
            GameManager.Cam.BossCamOff1();
            GameManager.Cam.BossCamOff2();
            GameManager.Cam.BossCamOn3();
        }
    }

    IEnumerator TurnOffCam()
    {
        yield return new WaitForSeconds(5f);
        //������ ķ ����
        GameManager.Cam.BossCamOff1();
        GameManager.Cam.BossCamOff3();
    }
}

        


