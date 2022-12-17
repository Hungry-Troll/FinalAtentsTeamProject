using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Ui_Npc ������ ��Ʈ�ѷ��� �����ؼ� ����Ʈ�� �����ϰԵ�
// 
public class WesleyController : MonoBehaviour
{
    Animator WesleyAnimator;
    public GameObject WesleyPrefab;
    public GameObject DialogsOfWesleyPanel;
    CapsuleCollider capsuleCollider;

    // ��ȭâ�� ����
    public Text WesleyDialog0;
    // ��ȭ�� �ڷ�ƾ ���� (�������ſ�)
    public Coroutine _coTalk;

    ParticleSystem _heart;

    void Awake()
    {
        WesleyAnimator = WesleyPrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
        // ����Ʈ �Ŵ������� ������ ��Ʈ�ѷ��� �������
        GameManager.Quest._wesleyController = GetComponent<WesleyController>();
        _coTalk = null;
        // ����Ʈ ��Ʈ ����
        _heart = Util.FindChild("QuestHeart", transform).GetComponent<ParticleSystem>();
        // ����Ʈ ��Ʈ ��
        QuestHeartOff();
    }

    private void Start()
    {
        // ���� ���� ����Ʈ ������ �ٸ���
        switch (GameManager.Scene._sceneNameEnum)
        {
            case Define.SceneName.Tutorial:
                GameManager.Quest.QuestStart(GameManager.QuestData._questLevel); //1���� ����Ʈ ����
                break;
            case Define.SceneName.Village02:
                RadioQuest();
                break;
            case Define.SceneName.DunGeon:
                // �������� ���� �� ����Ʈ ������ �ʿ���
                RadioQuest();
                // ����Ʈ �� ������ ��
                // GameManager.Quest.QuestDataGetValue(GameManager.QuestData._questLevel);
                // ���� // ����Ʈ ��ǥ��
                //GameManager.Quest.QuestInfoText();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayStruct;
            if (Physics.Raycast(ray, out RayStruct, Mathf.Infinity))
            {
                // Ÿ���� �������̰� , ����Ʈ ��ǥġ�� �޼����� ���
                if (RayStruct.collider.tag == "Wesley" && GameManager.Quest._questProgressValue >= GameManager.Quest._questObjectiveScore)
                {
                    // ����Ʈ ������ ���� �ൿ �з�
                    switch (GameManager.QuestData._questLevel)
                    {
                        // ����Ʈ ����1
                        case 1:
                            TutorialQuest();
                            // �Լ� ������ ����Ʈ ����2
                            // ù ��° ���� ���̰� ������ Ŭ������ �� ������ ���� ȭ��ǥ ���ֱ�
                            GameManager.Ui._directionArrowController.OffAllArrows();
                            break;
                        case 2:
                            TutorialQuest();
                            GameManager.Quest.TutorialDoorOpen();
                            // �Լ� ������ ����Ʈ ����3
                            break;
                    }
                }
            }
        }
    }

    public void EndToTalkWithWesley()
    {
        capsuleCollider.enabled = true;
        // ��� UI Ŵ
        GameManager.Ui.UISetActiveTrue();
        // ��� �˾�
        // ó�� ���� ���۽ÿ��� �켱 ������ ����
        if ((GameManager.QuestData._questLevel == 1))
        {
            GameManager.Ui.PopUpLocation("�� ���Ϸ���");
            // �˾� close
            StartCoroutine(GameManager.Ui.ClosePopUpLocation());
        }
        else
        {
            // ������ NPC ����ī�޶� OFF
            GameManager.Cam.WeleyCamOff();
        }
    }

    // ȸ��� ����
    public void OpeningVideoPlay()
    {
        capsuleCollider.enabled = true;
        GameObject tutorialVideo = GameManager.Create.CreateUi("UI_TutorialVideo", gameObject);
        // ��ŵ ��ư�� ���� ������Ʈ �Ѱ��ֱ�
        GameManager.Ui._skipButtonController.UI_TutorialVideo = tutorialVideo;

        // ������ NPC ����ī�޶� OFF
        GameManager.Cam.WeleyCamOff();
    }

    public IEnumerator WesleyDialog0Coroutine(char[] _Arr)
    {
        for (int i = 0; i < _Arr.Length; i++)
        {
            WesleyDialog0.text += _Arr[i];
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(WesleyDialog0Coroutine(_Arr));
    }

    public void WeleyDialogStart(string conversationText)
    {
        // ���� �ؽ�Ʈ �ʱ�ȭ
        WesleyDialog0.text = string.Empty;
        if(_coTalk != null)
        {
            StopCoroutine(_coTalk);
        }
        // �ؽ�Ʈ ��ȯ
        char[] ArrOfWesleyDialog0 = conversationText.ToCharArray();
        // �ڷ�ƾ����
        _coTalk = StartCoroutine(WesleyDialog0Coroutine(ArrOfWesleyDialog0));
    }

    // ���丮��� ����Ʈ
    public void TutorialQuest()
    {
        WesleyAnimator.SetTrigger("MeetPlayer");
        // ����Ʈ �Ϸ�
        GameManager.Quest.QuestCompletion();
        // ���ο� ����Ʈ ����
        GameManager.Quest.QuestStart(GameManager.QuestData._questLevel);
        // ������ NPC ����ī�޶� ON
        GameManager.Cam.WeleyCamOn();
        // �ݶ��̴��� ��Ȱ��ȭ�ϴ� ������ �� ���� �����
        // ù ��° ��ȭâ�� �ִ� ���¿��� ������ Ŭ���غ��� �� �� ����.                   
        capsuleCollider.enabled = false;
        // ���丮�� 2���� ����Ʈ ������ ���� �ӽ� �ڵ�
        if (GameManager.QuestData._questLevel == 2)
        {
            GameManager.Quest.Property_QuestProgressValue++;
        }
    }

    // ������ ����Ʈ
    public void RadioQuest()
    {
        // ����Ʈ �Ϸ�
        GameManager.Quest.QuestCompletion();
        // ���ο� ����Ʈ ����
        GameManager.Quest.QuestStart(GameManager.QuestData._questLevel);
    }

    public void QuestHeartOn()
    {
        _heart.Play();
    }

    public void QuestHeartOff()
    {
        _heart.Stop();
    }
}