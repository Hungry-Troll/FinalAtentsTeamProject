using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 대화에 쓰이는 UI는 판넬 안에 텍스트와 버튼을 미리 제작한 원본을 따로 만들고
// 그것을 복제해서 써야 문제가 없이 진행할 수 있음.
public class VeniceController : MonoBehaviour
{
    Animator VeniceAnimator;
    public GameObject VenicePrefab;
    public GameObject Shop;
    public GameObject[] DialogOfVenicePanels;
    CapsuleCollider capsuleCollider;
    public Text VeniceDialog0;
    public Text VeniceDialog1;
    string StrVeniceDialog0;
    string StrVeniceDialog1;
    char[] ArrOfStrVeniceDialog0;
    char[] ArrOfStrVeniceDialog1;

    TextMesh _tmesh;
    ParticleSystem _heart;

    void Awake()
    {
        VeniceAnimator = VenicePrefab.GetComponent<Animator>();
        capsuleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();

        GameManager.Quest._veniceController = GetComponent<VeniceController>();

        // 이름연결
        _tmesh = Util.FindChild("Name", transform).GetComponent<TextMesh>();
        // 퀘스트 하트 연결
        _heart = Util.FindChild("QuestHeart", transform).GetComponent<ParticleSystem>();
        // 퀘스트 하트 끔
        QuestHeartOff();
    }

    private void Start()
    {
        StrVeniceDialog0 = string.Empty;
        StrVeniceDialog1 = string.Empty;
        StrVeniceDialog0 = "안녕하세요. 무엇을 도와드릴까요?";
        StrVeniceDialog1 = "주변에 위험한 동물이 많아요. 조심하세요.";
        ArrOfStrVeniceDialog0 = StrVeniceDialog0.ToCharArray();
        ArrOfStrVeniceDialog1 = StrVeniceDialog1.ToCharArray();
        // 베니스 컨트롤러 오브젝트 매니저에서 관리 >> shop 연결을 위해
        // 연결된 shop은 아이템 판매시 버튼 텍스트 변경용도로 사용
        GameManager.Obj._veniceController = GetComponent<VeniceController>();
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
                // 태그 설정은 프리팹이 아니라 프리팹 안의 man-samurai-black_Rig를 이용할 것.
                if (RayStruct.collider.tag == "Merchant")
                {
                    VeniceAnimator.SetTrigger("MeetPlayer");
                    // 콜라이더를 비활성화하는 이유는 이 줄을 지우고
                    // 첫 번째 대화창이 있는 상태에서 상인을 클릭해보면 알 수 있음.                   
                    DialogOfVenicePanels[0].SetActive(true);
                    GameManager.Cam.VeniceCamOn();
                    StartCoroutine(VeniceDialog0Coroutine(ArrOfStrVeniceDialog0));
                    capsuleCollider.enabled = false;
                    VeniceAnimator.SetInteger("restoreInt", 1);
                    // 모든 UI 끔
                    GameManager.Ui.UISetActiveFalse();
                    // 방향 안내 화살표 UI 끔
                    GameManager.Ui._directionArrowController.OffArrow("ToVenice");
                };
            }
        }

        NameLookCam();
    }

    IEnumerator VeniceDialog0Coroutine(char[] _Arr)
    {
        VeniceDialog0.text = string.Empty;
        for (int i = 0; i < _Arr.Length; i++)
        {            
            VeniceDialog0.text += _Arr[i];
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(VeniceDialog0Coroutine(_Arr));
    }

    IEnumerator VeniceDialog1Coroutine(char[] _Arr)
    {
        VeniceDialog1.text = string.Empty;
        for (int i = 0; i < _Arr.Length; i++)
        {             
            VeniceDialog1.text += _Arr[i];
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(VeniceDialog1Coroutine(_Arr));
    }

    // 이 밑으로는 회의로 어떻게 만들 건지 결정해도 되는 부분
    public void TalkWithVenice()
    {
        DialogOfVenicePanels[0].SetActive(false);
        VeniceAnimator.SetTrigger("Talk");
        DialogOfVenicePanels[1].SetActive(true);
        StartCoroutine(VeniceDialog1Coroutine(ArrOfStrVeniceDialog1));
        VeniceAnimator.SetInteger("restoreInt", 1);
    }
    public void BackToDialog0()
    {
        DialogOfVenicePanels[1].SetActive(false);
        DialogOfVenicePanels[0].SetActive(true);
        StartCoroutine(VeniceDialog0Coroutine(ArrOfStrVeniceDialog0));
    }
    public void OpenShop()
    {
        Shop.SetActive(true);
        // 상점 골드 창 킴
        GameManager.Ui.goldDisplayShopOnOff(true);
        // 인벤토리 킴
        GameManager.Ui.InventoryOpen();
    }
    public void CloseShop()
    {
        Shop.SetActive(false);
        // 하트 끔
        QuestHeartOff();
        // 구매 취소 버튼 상점 꺼지면 같이 끔
        GameManager.Ui._buyCancel.SetActive(false);
        // 상점 골드 창 끔
        GameManager.Ui.goldDisplayShopOnOff(false);
        GameManager.Ui.InventoryClose();
    }
    public void EndToTalkWithVenice()
    {
        DialogOfVenicePanels[0].SetActive(false);
        capsuleCollider.enabled = true;
        GameManager.Cam.VeniceCamOff();

        // 모든 UI 킴
        GameManager.Ui.UISetActiveTrue();

        CloseShop();
    }

    public void NameLookCam()
    {
        _tmesh.transform.rotation = Camera.main.transform.rotation;
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
