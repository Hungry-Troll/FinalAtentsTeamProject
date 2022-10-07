using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    float PlayerSpeed;

    public GameObject QuestDoorLevel1;
    public GameObject QuestDoorLevel2;
    int QuestLevel;
    int QuestStory;
    int QuestObjectivesValue;
    int QuestProgressValue;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSpeed = 50f;
        QuestLevel = 0;
        QuestStory = 0;
        QuestObjectivesValue = 0;
        QuestProgressValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(QuestStory == 0 && QuestLevel == 0)
        {
            Debug.Log("���ΰ� �������� ���� 3������ �����!");
            QuestObjectivesValue = 3;
            QuestStory = 1;
        }
        if (QuestStory == 1 && QuestLevel == 1)
        {
            Debug.Log("���ΰ� �������� Ƽ��� 4������ �����!");
            QuestObjectivesValue = 4;
            QuestStory = 2;
        }
        if(QuestObjectivesValue == QuestProgressValue)
        {
            Debug.Log("����Ʈ ����");
            QuestDoorLevel1.SetActive(false);
            QuestProgressValue = 0;
            QuestLevel += 1;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == QuestDoorLevel1)
        {
            Debug.Log("����Ʈ1�� �Ϸ��ϼ���");
            transform.position = QuestDoorLevel1.transform.position + new Vector3(5f,0,0);
        }
        if (col.gameObject == QuestDoorLevel2)
        {
            Debug.Log("����Ʈ2�� �Ϸ��ϼ���");
            transform.position = QuestDoorLevel2.transform.position + new Vector3(5f, 0, 0);
        }
        if (col.collider.CompareTag("Monster"))
        {
            col.gameObject.SetActive(false);
            QuestProgressValue += 1;
            Debug.Log("����" + QuestProgressValue + " / ��ǥ" + QuestObjectivesValue);
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * PlayerSpeed);
        }
    }
}
