using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossStartController : MonoBehaviour
{
    // �÷��̾� �ݶ��̴� Ȯ�ο�
    Collider _playerCollider;
    //CinemachineVirtualCamera _vCam1;    //�� ó�� �÷��̾ ���ߴ� ī�޶�
    //CinemachineVirtualCamera _vCam2;    //�� ���� ���� ������ ���ߴ� ī�޶�
    //CinemachineVirtualCamera _vCam3;    //�� ���� ū ������ ���ߴ� ī�޶�

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
        //_vCam1 = GetComponent<CinemachineVirtualCamera>();
        //_vCam2 = GetComponent<CinemachineVirtualCamera>();
        //_vCam3 = GetComponent<CinemachineVirtualCamera>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �ݶ��̴��� �浹�ߴٸ�

        // 2. ���� �ó׸ӽ�1 �۵�
        GameManager.Cam.BossCamOn1();
        
        // 3. ����Ʈ ����
        GameManager.Quest.QuestProgressValueAdd();

        // 1. ���̽�ƽ ����
        GameManager.Quest.QuestJoystickStop();

        StartCoroutine(BossCreator());

        //���� �ó׸ӽ�3 ����
        StartCoroutine(TurnOffCam());



    }

    IEnumerator BossCreator()
    {
        yield return new WaitForSeconds(5f);
        if (_bossSwan == false)
        {   //���� �ó׸ӽ�2 �۵�
            GameManager.Cam.BossCamOff1();
            GameManager.Cam.BossCamOn2();
            _startPos = _startPosBoss.transform.position;
            // ���� ����
            GameManager.Create.CreateHumanBoss(_startPos, "BossHuman");
            
            _bossSwan = true;
            yield return new WaitForSeconds(4f);
            GameManager.Cam.BossCamOff2();
            GameManager.Cam.BossCamOn3();
        }
    }

    IEnumerator TurnOffCam()
    {
        yield return new WaitForSeconds(3f);
        //������ ķ ����
        GameManager.Cam.BossCamOff3();
    }
}

        


