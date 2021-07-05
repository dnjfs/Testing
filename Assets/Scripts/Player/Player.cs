using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] //�� Ű���带 �߰��Ͽ� �����Ϳ��� ���� ����
    private Transform cameraTransform;
    private Image fog; //���� ���¹̳ʸ� �����ִ� �Ȱ�

    private float halfScreenWidth; //������ ȭ�� ���ݸ� ��ġ�Ͽ� ���� ��ȯ
    private int rightFingerId;
    private Vector2 prevPoint;
    private Vector2 lookInput;
    private float cameraPitch; //pitch ����

    public AudioClip WalkSound, RunSound;
    private AudioSource Heartbeat, MoveAudio, BGAudio;
    private Rigidbody body; //�̵��� ���� Rigidbody
    private HeartAnim heart; //��Ʈ �ִϸ��̼� ��ü
    private VideoPlayer AttackVideo;
    private AudioSource AttackAudio;

    //Player ����
    public bool run = false;
    public bool isChased = false; //�ѱ�� ����
    public float speed; //�÷��̾��� �⺻ �ӵ�
    public float currentSpeed;  //���� �ӵ�
    public int NearEnemyNum = 0;
    public int CloseEnemyNum = 0;

    //���¹̳� ����
    public Slider staminaBar;   //���¹̳��� ǥ���� UI �����̴�
    public float stamina;   //�÷��̾��� ���¹̳�
    public float maxStamina;    //���¹̳��� �ִ�
    private float increasingStamina = 1f;    //���׹̳� ȸ�� �ӵ�
    private float decreasingStamina = 1f; //���¹̳� ���� �ӵ�

    void Start()
    {
        //���׹̳� �ʱ� ����
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();  //���׹̳��� ������Ʈ�� �����̴� ������
        staminaBar.gameObject.SetActive(true);  //���׹̳��� �����̴� Ȱ��ȭ
        stamina = maxStamina = 1.0f; //Player�� ���׹̳��� 1.0f
        staminaBar.maxValue = maxStamina;  //�����̴��� �ִ밪�� �ִ� ���׹̳� ������ ����
        staminaBar.value = maxStamina;     //�����̴��� ���� �ִ� ���׹̳� ������ ����


        this.rightFingerId = -1; //-1�� �������� �ƴ� �հ���
        this.halfScreenWidth = Screen.width / 2;
        this.cameraPitch = 0f;

        body = GetComponent<Rigidbody>();
        Heartbeat = this.GetComponent<AudioSource>(); //Player�� AudioSource ������Ʈ
        MoveAudio = this.gameObject.AddComponent<AudioSource>(); //AudioSource ������Ʈ �߰�
        MoveAudio.volume = 0.4f;
        BGAudio = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>(); //�������
        heart = GameObject.Find("Heart").GetComponent<HeartAnim>();
        fog = GameObject.Find("Fog").GetComponent<Image>();
        AttackVideo = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
        AttackAudio = GameObject.Find("VideoPlayer").GetComponent<AudioSource>();
        AttackVideo.Prepare(); //���� �غ�

        //�÷��̾� �����Ǹ� �ý��� ���۸޽��� ���
        GameObject.FindWithTag("GameSystem").GetComponent<DialogManager>().StartMessage();
        Invoke("SetPlayerLevel", 21f);
        //SetPlayerLevel();   //DialogManager���� ó��
    }

    void Update()
    {
        staminaBar.value = stamina;

        GetTouchInput();

        if (run) //�޸��� ����
        {
            stamina -= Time.deltaTime / decreasingStamina;
            staminaBar.value = stamina;
            currentSpeed = speed * 2; //�޸��� ������ �� �÷��̾��� �ӵ��� ������ �� ��� ����
            if (stamina <= 0.0f) //���¹̳� ����
            {
                stamina = 0.0f;
                currentSpeed = speed;   //�÷��̾� �ӵ��� �⺻ �ӵ�
            }
        }
        else
        {
            stamina += Time.deltaTime / increasingStamina;
            staminaBar.value = stamina;
            currentSpeed = speed;   //�÷��̾��� �ӵ��� �⺻ �ӵ�
            if (stamina >= maxStamina) //���¹̳� 100%
                stamina = maxStamina;
        }

        fog.color = new Color(1f, 1f, 1f, 0.5f - stamina); //���� ���¹̳ʿ� ���� �Ȱ��� ���� ���� (���¹̳ʰ� ���� ������ ������ �ѿ��� ��)

        if (NearEnemyNum > 0) //��ó�� ���Ͱ� �����ϸ�
        {
            if (CloseEnemyNum > 0) //�����̿� ���Ͱ� �����ϸ�
            {
                Heartbeat.pitch = 2.0f; //����� ��¼ӵ� 2��
                heart.SetAnimSpeed(2.0f); //����ڵ� �ִϸ��̼� 2��
            }
            else
            {
                Heartbeat.pitch = 1.0f;
                heart.SetAnimSpeed(1.0f);
            }

            if (!Heartbeat.isPlaying)
                Heartbeat.Play(); //���� �ڵ��Ҹ� ���

            BGAudio.volume = 0; //BGAudio.mute = true;
            heart.SetChasingParam(true); //���� �ִϸ������� Chasing �Ķ���Ͱ��� true�� ����
        }
        else
        {
            if (Heartbeat.isPlaying)
                Heartbeat.Stop();

            BGAudio.volume = 1; //BGAudio.mute = false;
            heart.SetChasingParam(false);
        }
    }

    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection;
        if (moveInput.magnitude != 0)
        {
            Vector3 lookForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized; // ī�޶� �ٶ󺸴� ����
            Vector3 lookRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized; // ī�޶��� ������ ����
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // �̵� ����

            //transform.position += moveDir * Time.deltaTime * speed; // �̵�
            //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            body.velocity = moveDir * currentSpeed; //�̵�

            if (body.velocity != Vector3.zero) //�̵���
            {
                if (currentSpeed == speed * 2) //�޸��� ����
                    MoveSoundPlay(RunSound);
                else //�ȴ� ����
                    MoveSoundPlay(WalkSound);
            }
        }
        else
            body.velocity = Vector3.zero;
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
                            this.lookInput = t.deltaPosition * Time.deltaTime * 3.0f;
                            this.cameraPitch = Mathf.Clamp(this.cameraPitch - this.lookInput.y, -20f, 30f); //Pitch�� ������ ���� 20�� ~ �Ʒ��� 30������
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

    public void SetPlayerLevel()
    {
        speed = 8f;  //Player�� �ӵ��� 8f
        //���� ���̵��� ���� Player ���׹̳� ���� ���� �ӵ� ����
        if (GameManager.instance.gameLevel == "easy")  //���� ���̵��� easy��
        {
            //speed = 10f;  //Player�� �ӵ��� 10f
            increasingStamina = 1.5f;   //���¹̳� ȸ�� �ӵ� 1.5s
            decreasingStamina = 4.0f;   //���¹̳� ���� �ӵ� 4s
        }
        else if (GameManager.instance.gameLevel == "normal")   //���� ���̵��� normal��
        {
            //speed = 7f;  //Player�� �ӵ��� 7f
            increasingStamina = 2.0f;   //���¹̳� ȸ�� �ӵ� 2s
            decreasingStamina = 3.0f;   //���¹̳� ���� �ӵ� 3s
        }
        else if (GameManager.instance.gameLevel == "hard") //���� ���̵��� hard��
        {
            //speed = 5f;  //Player�� �ӵ��� 5f
            increasingStamina = 3.0f;   //���¹̳� ȸ�� �ӵ� 3s
            decreasingStamina = 2.0f;   //���¹̳� ���� �ӵ� 2s
        }
    }

    public void SpeedBoostPlayer()
    {
        speed *= 1.5f;
    }

    private void MoveSoundPlay(AudioClip audio)
    {
        if (MoveAudio.clip != audio) //�ٸ� �߼Ҹ��� ����ؾ��ϴ� ���
        {
            if (MoveAudio.isPlaying) //����� ȿ������ ���� ������ ���
                return;

            MoveAudio.clip = audio; //ȿ���� ��ü
        }

        if (!MoveAudio.isPlaying)
            MoveAudio.Play(); //ȿ���� ���
    }

    void OnCollisionEnter(Collision coll) //�浹
    {
        if (coll.gameObject.tag == "Monster")
        {
            if (AttackAudio.isPlaying) //���� ���� ȿ������ ����ǰ� �ִٸ� �̹� ���� ����
                return;

            GameObject.Find("Canvas").transform.Find("VideoRender").gameObject.SetActive(true); //RawImage Ȱ��ȭ
            AttackVideo.Play(); //���� ���� ���� ����
            AttackAudio.Play(); //���� ���� ȿ���� ����
            Heartbeat.volume = 0; //����Ҹ� ���Ұ�

            GameManager.instance.timeScore = GameManager.instance.playTime; //�÷��� Ÿ�� ����
            Invoke("ChangeGameoverScene", 1.2f); //1.2�� �� �� ��ȯ
        }
    }
    void ChangeGameoverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}