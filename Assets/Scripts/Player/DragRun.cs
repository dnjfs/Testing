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
    private float leverRange; //���� �̵� ����
    private Vector3 moveDir; //�÷��̾� �̵� ����

    private RectTransform rectTransform;
    private bool isInput;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isInput) //�巡�� �ϴ� ���ȿ��� �̵�
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
        lever.anchoredPosition = Vector2.zero; //���� ��ġ �ʱ�ȭ
        isInput = false;
        player.run = false;
        player.Move(Vector2.zero); //����
    }

    public void ControlJoystickLever(PointerEventData eventData)
    {
        // ��ũ������ �Էµ� ������ ĵ������ ���̽�ƽUI �������� ��� (��Ƽ �ػ� ����)
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localCursor);
        var inputVector = localCursor;

        player.run = false;
        if (inputVector.y > leverRange + 80.0f) //������ ���� ���� ����� �� �޸���
        {
            player.run = true;
            inputVector.x = 0; //x��ǥ ����
            if (inputVector.y > leverRange + 150.0f) //y��ǥ ����
                inputVector.y = leverRange + 150.0f;
        }
        else if (inputVector.magnitude > leverRange) //������ ���� ����
            inputVector = inputVector.normalized * leverRange;

        lever.anchoredPosition = inputVector; //���� �̹����� ��ġ ����
        moveDir = inputVector.normalized; //�̵� ���� ����, ���� normalized�� �̵��ӵ��� �����ϰ�
    }

    private void InputControlVector()
    {
        player.Move(moveDir);
    }
}
