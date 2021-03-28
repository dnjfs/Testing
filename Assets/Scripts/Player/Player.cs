using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

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

    private AudioSource Heartbeat;
    private Rigidbody body; //이동을 위한 Rigidbody

    //Player 정보
    public bool run = false;
    public float speed = 10.0f;
    public float stamina = 1.0f;
    public int NearEnemyNum = 0;
    public int CloseEnemyNum = 0;

    public Slider staminaBar;   //스태미나를 표시할 UI 슬라이더

    void Start()
    {
        stamina = 1.0f;
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        staminaBar.gameObject.SetActive(true);
 
        staminaBar.maxValue = stamina;  //슬라이더의 최대값을 기본 스테미나 값으로 변경
        staminaBar.value = stamina;     //츨라이더의 값을 현재 스테미나 값으로 변경

        this.rightFingerId = -1; //-1은 추적중이 아닌 손가락
        this.halfScreenWidth = Screen.width / 2;
        this.cameraPitch = 0f;

        body = GetComponent<Rigidbody>();
        Heartbeat = this.GetComponent<AudioSource>(); //Player의 AudioSource 컴포넌트
    }

    void Update()
    {
        staminaBar.value = stamina;

        GetTouchInput();

        if (run) //달리는 상태
        {
            stamina -= Time.deltaTime / 2.0f;
            staminaBar.value = stamina;
            speed = 20.0f;
            if (stamina <= 0.0f) //스태미나 부족
            {
                stamina = 0.0f;
                speed = 10.0f;
            }
        }
        else
        {
            stamina += Time.deltaTime / 3.0f;
            staminaBar.value = stamina;
            speed = 10.0f;
            if (stamina >= 1.0f) //스태미나 100%
                stamina = 1.0f;
        }

        if (NearEnemyNum > 0) //근처에 몬스터가 존재하면
        {
            if (CloseEnemyNum > 0) //가까이에 몬스터가 존재하면
                Heartbeat.pitch = 2.0f; //오디오 출력속도 2배
            else
                Heartbeat.pitch = 1.0f;

            if (!Heartbeat.isPlaying)
                Heartbeat.Play(); //심장 박동소리 출력
        }
        else if (Heartbeat.isPlaying)
            Heartbeat.Stop();
    }

    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection;
        if (moveInput.magnitude != 0)
        {
            Vector3 lookForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized; // 카메라가 바라보는 방향
            Vector3 lookRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized; // 카메라의 오른쪽 방향
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // 이동 방향

            //transform.position += moveDir * Time.deltaTime * speed; // 이동
            //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            body.velocity = moveDir * speed; //이동
        }
        else
            body.velocity = Vector3.zero;
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