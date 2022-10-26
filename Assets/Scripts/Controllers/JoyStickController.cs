using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 레버위치 구현용
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    // 레버위치 조정용
    [SerializeField, Range(10, 150)]
    private float leverRange;
    // 레버 상태 확인용
    public JoystickState _joystickState;
    // 인풋 방향
    public Vector2 inputDirection;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _joystickState = JoystickState.InputFalse;
    }

    // Update is called once per frame
    private void Update()
    {
        if(_joystickState == JoystickState.InputTrue)
        {
            InputControlVector();
        }
    }
    // 레버 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        _joystickState = JoystickState.InputTrue;
    }
    // 레버 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        _joystickState = JoystickState.InputTrue;
    }
    // 레버 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 inputPos = Vector2.zero;
        Vector2 inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = Vector2.zero;
        inputDirection = inputVector / leverRange;
        _joystickState = JoystickState.InputFalse;
    }
    // 조이스틱 움직이는 함수
    public void ControlJoystickLever(PointerEventData eventData)
    {
        Vector2 inputPos = eventData.position - (rectTransform.anchoredPosition * 4) - new Vector2(20, 20);
        Vector2 inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }
    // 조이스틱의 방향 계산
    public Vector2 InputControlVector()
    {
        _joystickState = JoystickState.InputTrue;
       // Debug.Log("조이스틱 : " + inputDirection);
        return inputDirection;
    }

}
