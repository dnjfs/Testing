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
    private HeartAnim heart; //하트 애니메이션 객체

    //Player 정보
    public bool run = false;
    public bool isChased = false; //쫓기는 상태
    public float speed; //플레이어의 기본 속도
    public float currentSpeed;  //현재 속도
    public int NearEnemyNum = 0;
    public int CloseEnemyNum = 0;

    //스태미나 정보
    public Slider staminaBar;   //스태미나를 표시할 UI 슬라이더
    public float stamina;   //플레이어의 스태미나
    public float maxStamina;    //스태미나의 최댓값
    public float increasingStamina;    //스테미나 회복 속도
    public float decreasingStamina; //스태미나 감소 속도

    void Start()
    {
        //스테미너 초기 설정
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();  //스테미나바 오브젝트의 슬라이더 가져옴
        staminaBar.gameObject.SetActive(true);  //스테미나바 슬라이더 활성화
        stamina = maxStamina = 1.0f; //Player의 스테미나는 1.0f
        staminaBar.maxValue = maxStamina;  //슬라이더의 최대값을 최대 스테미나 값으로 변경
        staminaBar.value = maxStamina;     //츨라이더의 값을 최대 스테미나 값으로 변경
        

        this.rightFingerId = -1; //-1은 추적중이 아닌 손가락
        this.halfScreenWidth = Screen.width / 2;
        this.cameraPitch = 0f;

        body = GetComponent<Rigidbody>();
        Heartbeat = this.GetComponent<AudioSource>(); //Player의 AudioSource 컴포넌트
        heart = GameObject.Find("Heart").GetComponent<HeartAnim>();

        

        //플레이어 생성되면 시스템 시작메시지 출력
        GameObject.FindWithTag("GameSystem").GetComponent<DialogManager>().StartMessage();
        Invoke("SetPlayerLevel", 21f);
        //SetPlayerLevel();   //DialogManager에서 처리
    }

    void Update()
    {
        staminaBar.value = stamina;

        GetTouchInput();

        if (run) //달리는 상태
        {
            stamina -= Time.deltaTime / decreasingStamina;
            staminaBar.value = stamina;
            currentSpeed = speed * 2; //달리는 상태일 때 플레이어의 속도는 기존의 두 배로 증가
            if (stamina <= 0.0f) //스태미나 부족
            {
                stamina = 0.0f;
                currentSpeed = speed;   //플레이어 속도는 기본 속도
            }
        }
        else
        {
            stamina += Time.deltaTime / increasingStamina;
            staminaBar.value = stamina;
            currentSpeed = speed;   //플레이어의 속도는 기본 속도
            if (stamina >= maxStamina) //스태미나 100%
                stamina = maxStamina;
        }

        if (NearEnemyNum > 0) //근처에 몬스터가 존재하면
        {
            if (CloseEnemyNum > 0) //가까이에 몬스터가 존재하면
            {
                Heartbeat.pitch = 2.0f; //오디오 출력속도 2배
                heart.SetAnimSpeed(2.0f); //심장박동 애니메이션 2배
            }
            else
            {
                Heartbeat.pitch = 1.0f;
                heart.SetAnimSpeed(1.0f);
            }

            if (!Heartbeat.isPlaying)
                Heartbeat.Play(); //심장 박동소리 출력

            heart.SetChasingParam(true); //심장 애니메이터의 Chasing 파라미터값을 true로 설정
        }
        else
        {
            if (Heartbeat.isPlaying)
                Heartbeat.Stop();

            heart.SetChasingParam(false);
        }
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
            body.velocity = moveDir * currentSpeed; //이동
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

    public void SetPlayerLevel()
    {
        //게임 난이도에 따라 Player 속도 및 스테미나 증가 감소 속도 조절
        if (GameManager.instance.gameLevel == "easy")  //게임 난이도가 easy면
        {
            speed = currentSpeed = 10f;  //Player의 속도는 10f
            increasingStamina = 1.5f;   //스태미나 회복 속도 1.5s
            decreasingStamina = 4.0f;   //스태미나 감소 속도 4s
        }
        else if (GameManager.instance.gameLevel == "normal")   //게임 난이도가 normal면
        {
            speed = currentSpeed = 7f;  //Player의 속도는 7f
            increasingStamina = 2.0f;   //스태미나 회복 속도 2s
            decreasingStamina = 3.0f;   //스태미나 감소 속도 3s
        }
        else if (GameManager.instance.gameLevel == "hard") //게임 난이도가 hard면
        {
            speed = currentSpeed = 5f;  //Player의 속도는 5f
            increasingStamina = 3.0f;   //스태미나 회복 속도 3s
            decreasingStamina = 2.0f;   //스태미나 감소 속도 2s
        }
    }
}