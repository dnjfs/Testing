using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRun : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Player player;

    [SerializeField]
    private RectTransform lever;

    [SerializeField, Range(100, 200)]
    private float leverRange; //레버 이동 범위

    private RectTransform rectTransform;
    private bool isInput;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isInput) //드래그 하는 동안에만 이동
            InputControlVector();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero; //레버 위치 초기화
        isInput = false;
        player.run = false;
        player.Move(Vector2.zero); //정지
    }

    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        inputVector.x = 0; //x좌표 고정
        if (inputVector.y < 0) //아래로는 레버 이동불가
            inputVector.y = 0;
        lever.anchoredPosition = inputVector;

        if (lever.anchoredPosition.y > leverRange * 0.9f) //레버를 90%이상 당겼을 때 달리기
            player.run = true;
        else
            player.run = false;
    }

    private void InputControlVector()
    {
        player.Move(Vector2.up);
    }
}
