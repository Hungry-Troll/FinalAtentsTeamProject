using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ������ġ ������
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    // ������ġ ������
    [SerializeField, Range(10, 150)]
    private float leverRange;
    // ���� ���� Ȯ�ο�
    public JoystickState _joystickState;
    // ��ǲ ����
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
    // ���� �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        _joystickState = JoystickState.InputTrue;
    }
    // ���� �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        _joystickState = JoystickState.InputTrue;
    }
    // ���� �巡�� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 inputPos = Vector2.zero;
        Vector2 inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = Vector2.zero;
        inputDirection = inputVector / leverRange;
        _joystickState = JoystickState.InputFalse;
    }
    // ���̽�ƽ �����̴� �Լ�
    public void ControlJoystickLever(PointerEventData eventData)
    {
        Vector2 inputPos = eventData.position - (rectTransform.anchoredPosition * 4) - new Vector2(20, 20);
        Vector2 inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }
    // ���̽�ƽ�� ���� ���
    public Vector2 InputControlVector()
    {
        _joystickState = JoystickState.InputTrue;
       // Debug.Log("���̽�ƽ : " + inputDirection);
        return inputDirection;
    }

}