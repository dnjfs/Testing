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
    private Vector3 moveDir; //플레이어 이동 방향

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
        // 스크린으로 입력된 지점을 캔버스의 조이스틱UI 기준으로 계산 (멀티 해상도 대응)
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localCursor);
        var inputVector = localCursor;

        player.run = false;
        if (inputVector.y > leverRange + 80.0f) //레버를 위로 많이 당겼을 때 달리기
        {
            player.run = true;
            inputVector.x = 0; //x좌표 고정
            if (inputVector.y > leverRange + 150.0f) //y좌표 제한
                inputVector.y = leverRange + 150.0f;
        }
        else if (inputVector.magnitude > leverRange) //레버의 범위 제한
            inputVector = inputVector.normalized * leverRange;

        lever.anchoredPosition = inputVector; //레버 이미지의 위치 조정
        moveDir = inputVector.normalized; //이동 방향 설정, 벡터 normalized로 이동속도는 일정하게
    }

    private void InputControlVector()
    {
        player.Move(moveDir);
    }
}
