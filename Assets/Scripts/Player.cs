using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //�� Ű���带 �߰��Ͽ� �����Ϳ��� ���� ����
    private Transform cameraTransform;
    public float cameraSensitivity;

    private float halfScreenWidth; //������ ȭ�� ���ݸ� ��ġ�Ͽ� ���� ��ȯ
    private int rightFingerId;
    private Vector2 prevPoint;
    private Vector2 lookInput;
    private float cameraPitch; //pitch ����

    //Player ����
    public bool run = false;
    public float speed = 1.0f;
    public float stamina = 1.0f;

    void Start()
    {
        stamina = 1.0f;

        this.rightFingerId = -1; //-1�� �������� �ƴ� �հ���
        this.halfScreenWidth = Screen.width / 2;
        this.cameraPitch = 0f;
    }

    void Update()
    {
        GetTouchInput();

        if (run) //�޸��� ����
        {
            stamina -= Time.deltaTime;
            speed = 2.0f;
            if (stamina <= 0.0f) //���¹̳� ����
            {
                stamina = 0.0f;
                speed = 1.0f;
            }
        }
        else
        {
            stamina += Time.deltaTime / 3.0f;
            speed = 1.0f;
            if (stamina >= 1.0f) //���¹̳� 100%
                stamina = 1.0f;
        }
    }

    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection * speed;
        if (moveInput.magnitude != 0)
        {
            Vector3 lookForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized; // ī�޶� �ٶ󺸴� ����
            Vector3 lookRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized; // ī�޶��� ������ ����
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // �̵� ����

            transform.position += moveDir * Time.deltaTime * 10f; // �̵�
        }
    }

    private void GetTouchInput()
    {
        //��� ��ġ�� �ԷµǴ°�
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x > this.halfScreenWidth && this.rightFingerId == -1)
                    {
                        this.rightFingerId = t.fingerId;
                        Debug.Log("������ �հ��� �Է�");
                    }
                    break;

                case TouchPhase.Moved:
                    //�̰��� �߰��ϸ� ���� ������ ��ư�� ���� �� ȭ���� ���ư��� �ʴ´�
                    if (!EventSystem.current.IsPointerOverGameObject(i))
                    {
                        if (t.fingerId == this.rightFingerId)
                        {

                            //����
                            this.prevPoint = t.position - t.deltaPosition;
                            this.transform.RotateAround(this.transform.position, Vector3.up, (t.position.x - this.prevPoint.x) * 0.2f);
                            this.prevPoint = t.position;


                            //����
                            this.lookInput = t.deltaPosition * this.cameraSensitivity * Time.deltaTime;
                            this.cameraPitch = Mathf.Clamp(this.cameraPitch - this.lookInput.y, -20f, 30f); //Pitch�� ������ ���� 10�� ~ �Ʒ��� 35������
                            this.cameraTransform.localRotation = Quaternion.Euler(this.cameraPitch, 0, 0);
                        }
                    }
                    break;

                case TouchPhase.Stationary:
                    if (t.fingerId == this.rightFingerId)
                    {
                        this.lookInput = Vector2.zero;
                    }
                    break;

                case TouchPhase.Ended:
                    if (t.fingerId == this.rightFingerId)
                    {
                        this.rightFingerId = -1;
                        Debug.Log("������ �հ��� ��");
                    }
                    break;

                case TouchPhase.Canceled:
                    if (t.fingerId == this.rightFingerId)
                    {
                        this.rightFingerId = -1;
                        Debug.Log("������ �հ��� ��");
                    }
                    break;
            }
        }
    }
}