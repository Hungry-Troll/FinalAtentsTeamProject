using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    float PlayerSpeed;
    QuestData questData;
    //퀘스트가 완료가 되면 리스트에 담아서 저장
    List<QuestData> questCompletionList;

    public List<GameObject> door;
    public GameObject questInfo;
    public GameObject conversationInfo;
    public GameObject questWarningText;
    public Text questTitleText;
    public Text questObjectivesText;
    public Text conversationCharacterNameText;
    public Text conversationText;

    //퀘스트 레벨
    int questLevel;
    //스토리 카운트
    int questStroyCount;
    //목표치
    int questProgressValue;
    //현재 퀘스트 목표량
    int conversationCount;

    private void Awake()
    { 
        questCompletionList = new List<QuestData>();
        //퀘스트와 대화창을 비활성화
        ObjectActive(false);
    }
    void Start()
    {
        PlayerSpeed = 50f;
        //듀토리얼이 끝나고 본 게임에 들어오면 레벨1의 퀘스트가 실행이 된다.
        questLevel = 1;
        questStroyCount = 1;
        questProgressValue = 0;
        conversationCount = 0;
    }

    void Update()
    {
        Move();
        QuestOutput(questLevel);
        //목표량은 계속 업데이트가 되어야 하기 때문에 
        questObjectivesText.text = questData.QuestObjectiveName + " " + questProgressValue + " / <color=red>" + questData.QuestObjectiveScore + "</color>";
        //목표치가 달성하게 된다면.
        if (questProgressValue == questData.QuestObjectiveScore)
        {
            //퀘스트 완료 리스트에 현재 퀘스트의 객체를 추가
            questCompletionList.Add(questData);
            //퀘스트 레벨에 맞는 문이 비활성화 되어 다음 단계로 넘어갈수 있음
            door[questLevel - 1].SetActive(false);
            //다음 퀘스트가 실행이 되게 레벨업
            questLevel++;
            //퀘스트의 목표량을 초기화
            questProgressValue = 0;
        }
    }
    public void QuestOutput(int _questLevel)
    {
        //퀘스트의 스토리를 한번만 호출하기 위해서 questLevel 대신에 questStroyCount변수와 비교
        if (questStroyCount == _questLevel)
        {
            //객체를 인스턴스화 하여 객체의 함수를 불러와 데이터를 저장
            questData = new QuestData();
            questData.ReadQuest(_questLevel);
            //데이터를 불러오면 대화창을 활성화 함
            conversationInfo.SetActive(true);
            //conversationCount변수는 questData.QuestStroy의 리스트의 데이터를 가져오는 변수
            //conversationCount값이 questData.QuestStroy.Count보다 커지면 더이상 리스트에서 가져올 데이터가 없음
            if (questData.QuestStroy.Count <= conversationCount)
            {
                //퀘스트의 스토리가 반복되어 나오지 않게 questStroyCount는 다음 스토리의 레벨값으로 올려놓는다.
                questStroyCount++;
                //스토리가 끝나면 대화창은 비활성화
                conversationInfo.SetActive(false);
                //퀘스트창은 활성화
                questInfo.SetActive(true);
                //퀘스트데이터 객체에서 불러온 스토리 리스트의 count를 비교하기 위해서 만든 변수를 초기화
                conversationCount = 0;
                //퀘스트 타이틀 텍스트에 타이틀의 넣어준다
                questTitleText.text = questData.QuestTitle;
                return;
            }
            //퀘스트 대화 NPC나 플레이어의 이름(짝수)
            conversationCharacterNameText.text = questData.QuestStroy[conversationCount];
            //퀘스트의 내용(홀수)
            conversationText.text = questData.QuestStroy[conversationCount+1];
            //스페이스를 누르면 또는 화면을 누르면 대화 스킵
            if (Input.GetKeyDown(KeyCode.Space))
            {
                conversationCount += 2;
            }
        }
    }
    //퀘스트 정보 대화창 비활성화 함수
    public void ObjectActive(bool _value)
    {
        questInfo.SetActive(_value);
        conversationInfo.SetActive(_value);
    }
    //코루틴을 통하여 퀘스트 미완료 경교표시 시간 설정
    IEnumerator QuestWarning()
    {
        //2초뒤에 퀘스트 경고창이 비활성화
        yield return new WaitForSeconds(2f);
        questWarningText.SetActive(false);
    }
    private void OnCollisionEnter(Collision col)
    {
        //퀘스트 미완료시 각레벨의 문에 접촉을 하게 되면 경고표시
        if(col.gameObject == door[questLevel-1])
        {
            //플레이어가 미완료문에 접촉하게 된다면 문의 x방향으로 튕겨지게 됨
            transform.position = door[questLevel - 1].transform.position + new Vector3(5f,0,0);
            //경고UI 활성화  
            questWarningText.SetActive(true);
            //코루틴을 통하여 경고창을 2초동안 활성화 되게 만듬
            StartCoroutine(QuestWarning());
        }
        //플레이어가 몬스터와의 상호작용을 통한 조건식 넣는 곳
        if (col.collider.CompareTag("Monster"))
        {
            //현재는 접촉한 오브젝트가 비활성화 하게 되면 목표량이 카운터 됨
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
