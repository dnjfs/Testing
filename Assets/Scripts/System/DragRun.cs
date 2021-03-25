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
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        inputVector.x = 0; //x��ǥ ����
        if (inputVector.y < 0) //�Ʒ��δ� ���� �̵��Ұ�
            inputVector.y = 0;
        lever.anchoredPosition = inputVector;

        if (lever.anchoredPosition.y > leverRange * 0.9f) //������ 90%�̻� ����� �� �޸���
            player.run = true;
        else
            player.run = false;
    }

    private void InputControlVector()
    {
        player.Move(Vector2.up);
    }
}
