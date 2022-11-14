using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    float PlayerSpeed;
    QuestData questData;
    //����Ʈ�� �Ϸᰡ �Ǹ� ����Ʈ�� ��Ƽ� ����
    List<QuestData> questCompletionList;

    public List<GameObject> door;
    public GameObject questInfo;
    public GameObject conversationInfo;
    public GameObject questWarningText;
    public Text questTitleText;
    public Text questObjectivesText;
    public Text conversationCharacterNameText;
    public Text conversationText;

    //����Ʈ ����
    int questLevel;
    //���丮 ī��Ʈ
    int questStroyCount;
    //��ǥġ
    int questProgressValue;
    //���� ����Ʈ ��ǥ��
    int conversationCount;

    private void Awake()
    { 
        questCompletionList = new List<QuestData>();
        //����Ʈ�� ��ȭâ�� ��Ȱ��ȭ
        ObjectActive(false);
    }
    void Start()
    {
        PlayerSpeed = 50f;
        //���丮���� ������ �� ���ӿ� ������ ����1�� ����Ʈ�� ������ �ȴ�.
        questLevel = 1;
        questStroyCount = 1;
        questProgressValue = 0;
        conversationCount = 0;
    }

    void Update()
    {
        Move();
        QuestOutput(questLevel);
        //��ǥ���� ��� ������Ʈ�� �Ǿ�� �ϱ� ������ 
        questObjectivesText.text = questData.QuestObjectiveName + " " + questProgressValue + " / <color=red>" + questData.QuestObjectiveScore + "</color>";
        //��ǥġ�� �޼��ϰ� �ȴٸ�.
        if (questProgressValue == questData.QuestObjectiveScore)
        {
            //����Ʈ �Ϸ� ����Ʈ�� ���� ����Ʈ�� ��ü�� �߰�
            questCompletionList.Add(questData);
            //����Ʈ ������ �´� ���� ��Ȱ��ȭ �Ǿ� ���� �ܰ�� �Ѿ�� ����
            door[questLevel - 1].SetActive(false);
            //���� ����Ʈ�� ������ �ǰ� ������
            questLevel++;
            //����Ʈ�� ��ǥ���� �ʱ�ȭ
            questProgressValue = 0;
        }
    }
    public void QuestOutput(int _questLevel)
    {
        //����Ʈ�� ���丮�� �ѹ��� ȣ���ϱ� ���ؼ� questLevel ��ſ� questStroyCount������ ��
        if (questStroyCount == _questLevel)
        {
            //��ü�� �ν��Ͻ�ȭ �Ͽ� ��ü�� �Լ��� �ҷ��� �����͸� ����
            questData = new QuestData();
            questData.ReadQuest(_questLevel);
            //�����͸� �ҷ����� ��ȭâ�� Ȱ��ȭ ��
            conversationInfo.SetActive(true);
            //conversationCount������ questData.QuestStroy�� ����Ʈ�� �����͸� �������� ����
            //conversationCount���� questData.QuestStroy.Count���� Ŀ���� ���̻� ����Ʈ���� ������ �����Ͱ� ����
            if (questData.QuestStroy.Count <= conversationCount)
            {
                //����Ʈ�� ���丮�� �ݺ��Ǿ� ������ �ʰ� questStroyCount�� ���� ���丮�� ���������� �÷����´�.
                questStroyCount++;
                //���丮�� ������ ��ȭâ�� ��Ȱ��ȭ
                conversationInfo.SetActive(false);
                //����Ʈâ�� Ȱ��ȭ
                questInfo.SetActive(true);
                //����Ʈ������ ��ü���� �ҷ��� ���丮 ����Ʈ�� count�� ���ϱ� ���ؼ� ���� ������ �ʱ�ȭ
                conversationCount = 0;
                //����Ʈ Ÿ��Ʋ �ؽ�Ʈ�� Ÿ��Ʋ�� �־��ش�
                questTitleText.text = questData.QuestTitle;
                return;
            }
            //����Ʈ ��ȭ NPC�� �÷��̾��� �̸�(¦��)
            conversationCharacterNameText.text = questData.QuestStroy[conversationCount];
            //����Ʈ�� ����(Ȧ��)
            conversationText.text = questData.QuestStroy[conversationCount+1];
            //�����̽��� ������ �Ǵ� ȭ���� ������ ��ȭ ��ŵ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                conversationCount += 2;
            }
        }
    }
    //����Ʈ ���� ��ȭâ ��Ȱ��ȭ �Լ�
    public void ObjectActive(bool _value)
    {
        questInfo.SetActive(_value);
        conversationInfo.SetActive(_value);
    }
    //�ڷ�ƾ�� ���Ͽ� ����Ʈ �̿Ϸ� �汳ǥ�� �ð� ����
    IEnumerator QuestWarning()
    {
        //2�ʵڿ� ����Ʈ ���â�� ��Ȱ��ȭ
        yield return new WaitForSeconds(2f);
        questWarningText.SetActive(false);
    }
    private void OnCollisionEnter(Collision col)
    {
        //����Ʈ �̿Ϸ�� �������� ���� ������ �ϰ� �Ǹ� ���ǥ��
        if(col.gameObject == door[questLevel-1])
        {
            //�÷��̾ �̿ϷṮ�� �����ϰ� �ȴٸ� ���� x�������� ƨ������ ��
            transform.position = door[questLevel - 1].transform.position + new Vector3(5f,0,0);
            //���UI Ȱ��ȭ  
            questWarningText.SetActive(true);
            //�ڷ�ƾ�� ���Ͽ� ���â�� 2�ʵ��� Ȱ��ȭ �ǰ� ����
            StartCoroutine(QuestWarning());
        }
        //�÷��̾ ���Ϳ��� ��ȣ�ۿ��� ���� ���ǽ� �ִ� ��
        if (col.collider.CompareTag("Monster"))
        {
            //����� ������ ������Ʈ�� ��Ȱ��ȭ �ϰ� �Ǹ� ��ǥ���� ī���� ��
            col.gameObject.SetActive(false);
            if(conversationInfo.activeSelf == false)
            {
                questProgressValue += 1;
            }
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
