using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossStartController : MonoBehaviour
{
    // �÷��̾� �ݶ��̴� Ȯ�ο�
    Collider _playerCollider;
    BoxCollider mine;

    // �ӽ� ���� ��ġ ������ / ����ִ� ���ӿ�����Ʈ�� �÷��̾� ��ġ ����
    public GameObject _startPosBoss;
    public Vector3 _startPos;
    bool _bossSwan;

    void Start()
    {
        // OnTriggerEnter ����� ���� �÷��̾� �ݶ��̴��� ������ ��
        _playerCollider = GameManager.Obj._playerController.GetComponent<Collider>();
        _bossSwan = false;
        mine = GetComponent<BoxCollider>();
        
    }

    public void OnTriggerEnter(Collider other)
    {
        mine.enabled = false;
        // �÷��̾� �ݶ��̴��� �ѹ� �浹�ߴٸ� ���� => �Ȳ��� �����϶����� ��� ȣ��
        // 1. ���̽�ƽ ����
        GameManager.Quest.QuestJoystickStop();
        
        // 2. ���� �ó׸ӽ�1 �۵�
        GameManager.Cam.BossCamOn1();
        
        // 3. ����Ʈ ����
        GameManager.Quest.QuestProgressValueAdd();
        GameManager.Ui.UISetActiveFalse();
        // 4. 3�� �ڿ� ī�޶� ���ư�
        StartCoroutine(BossCreator());
    }

    IEnumerator BossCreator()
    {
        yield return new WaitForSeconds(3f);
        if (_bossSwan == false)
            GameManager.Cam.BossCamOff1();
        {   //���� �ó׸ӽ�2 �۵�
            GameManager.Cam.BossCamOn2();
            _startPos = _startPosBoss.transform.position;
            // ���� ���� 
            GameManager.Create.CreateHumanBoss(_startPos, "BossHuman");
            _bossSwan = true;
            //18�ʵ��� ������ ����԰� ���� ������..
            yield return new WaitForSeconds(17f);
            GameManager.Cam.BossCamOff2();
            GameManager.Cam.BossCamOn3();
            yield return StartCoroutine(TurnOffCam());
        }
    }

    IEnumerator TurnOffCam()
    {
        yield return new WaitForSeconds(5f);
        //������ ķ ����
        GameManager.Cam.BossCamOff3();
        GameManager.Ui.UISetActiveTrue();
    }
}

        


