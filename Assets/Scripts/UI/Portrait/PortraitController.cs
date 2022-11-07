using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 초상화 누르면 스킬창 열리는 스크립트
public class PortraitController : MonoBehaviour
{
    Button _button;

    private void Start()
    {
        // FindChild 재귀함수를 이용해서 버튼을 찾고
        Transform tr = Util.FindChild("Image", gameObject.transform);
        _button = tr.GetComponent<Button>();
        // 버튼을 코드로 연결함
        _button.onClick.AddListener(OnPortraitClick);

        // 반짝이는 이펙트 추가하려면...
        // 카메라 가지고 오고 >> 카메라 매니저에서 생성
        // 캔버스 가지고 오고 
        // 파티클 가지고 와야 됨

        // 포트레이트도 위치 변경
        Canvas portCanvas = GetComponent<Canvas>();
        portCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        portCanvas.worldCamera = GameManager.Cam._uiParticleCam;
        //_portrait.transform.SetParent(_playerHpBar.transform);

    }
    // 스킬 상태창 열리는 함수
    public void OnPortraitClick()
    {
        GameManager.Ui._skillViewController.gameObject.SetActive(true);
    }

    // 추후 파티클 매니저에서 해야 될 듯?
    // 레벨업 시 포트레이트에 이펙트 생성
    // 추후 다른 파티클이 없으면 카메라도 같이 끄는게 좋음
}
