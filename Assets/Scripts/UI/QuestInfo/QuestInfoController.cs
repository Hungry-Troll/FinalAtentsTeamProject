using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ����Ʈ â
// ����Ʈ Ÿ��Ʋ
// ����Ʈ �̸� �־���� �� QuestManagerEX ���� �����͸� �־��� ����
public class QuestInfoController : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    public void Awake()
    {
        // ����Ʈ �Ŵ����� �ڱ� �ڽ��� ������ �ؽ�Ʈ ������Ʈ ������ �Ѱ��ش�.
        // ����Ʈ�� ���� �ؽ�Ʈ�� �ٲ�� �Ǳ� ����
        GameManager.Quest._questInfo = this.gameObject;

        Transform tmp1 = Util.FindChild("QuestTileText", transform);
        GameManager.Quest._questTitleText = tmp1.GetComponent<Text>();
        Transform tmp2 = Util.FindChild("QuestObjectiveText", transform);
        GameManager.Quest._questObjectivesText = tmp2.GetComponent<Text>();

        this.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
