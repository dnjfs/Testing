using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //이 키워드를 추가하여 에디터에서 접근 가능
    private Transform cameraTransform;
    public float cameraSensitivity;

    private float halfScreenWidth; //오른쪽 화면 절반만 터치하여 시점 전환
    private int rightFingerId;
    private Vector2 prevPoint;
    private Vector2 lookInput;
    private float cameraPitch; //pitch 시점

    //Player 정보
    public bool run = false;
    public float speed = 1.0f;
    public float stamina = 1.0f;

    void Start()
    {
        stamina = 1.0f;

        this.rightFingerId = -1; //-1은 추적중이 아닌 손가락
        this.halfScreenWidth = Screen.width / 2;
        this.cameraPitch = 0f;
    }

    void Update()
    {
        GetTouchInput();

        if (run) //달리는 상태
        {
            stamina -= Time.deltaTime;
            speed = 2.0f;
            if (stamina <= 0.0f) //스태미나 부족
            {
                stamina = 0.0f;
                speed = 1.0f;
            }
        }
        else
        {
            stamina += Time.deltaTime / 3.0f;
            speed = 1.0f;
            if (stamina >= 1.0f) //스태미나 100%
                stamina = 1.0f;
        }
    }

    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection * speed;
        if (moveInput.magnitude != 0)
        {
            Vector3 lookForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized; // 카메라가 바라보는 방향
            Vector3 lookRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized; // 카메라의 오른쪽 방향
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // 이동 방향

            transform.position += moveDir * Time.deltaTime * 10f; // 이동
        }
    }

    private void GetTouchInput()
    {
        //몇개의 터치가 입력되는가
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x > this.halfScreenWidth && this.rightFingerId == -1)
                    {
                        this.rightFingerId = t.fingerId;
                        Debug.Log("오른쪽 손가락 입력");
                    }
                    break;

                case TouchPhase.Moved:
                    //이것을 추가하면 시점 원상태 버튼을 누를 때 화면이 돌아가지 않는다
                    if (!EventSystem.current.IsPointerOverGameObject(i))
                    {
                        if (t.fingerId == this.rightFingerId)
                        {

                            //수평
                            this.prevPoint = t.position - t.deltaPosition;
                            this.transform.RotateAround(this.transform.position, Vector3.up, (t.position.x - this.prevPoint.x) * 0.2f);
                            this.prevPoint = t.position;


                            //수직
                            this.lookInput = t.deltaPosition * this.cameraSensitivity * Time.deltaTime;
                            this.cameraPitch = Mathf.Clamp(this.cameraPitch - this.lookInput.y, -20f, 30f); //Pitch의 각도는 위로 10도 ~ 아래로 35도까지
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
                        Debug.Log("오른쪽 손가락 끝");
                    }
                    break;

                case TouchPhase.Canceled:
                    if (t.fingerId == this.rightFingerId)
                    {
                        this.rightFingerId = -1;
                        Debug.Log("오른쪽 손가락 끝");
                    }
                    break;
            }
        }
    }
}