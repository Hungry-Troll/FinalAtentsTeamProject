using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 퀘스트 창
// 퀘스트 타이틀
// 퀘스트 이름 넣어줘야 됨 QuestManagerEX 에서 데이터를 넣어줄 예정
public class QuestInfoController : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    public void Awake()
    {
        // 퀘스트 매니저에 자기 자신의 정보와 텍스트 컴포넌트 정보를 넘겨준다.
        // 퀘스트에 따라서 텍스트를 바꿔야 되기 때문
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
