using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    enum Direction { Up, Right, Down, Left, None }; //이동방향 열거
    //private Vector3 direction; //이동방향
    public float velocity; //이동속도

    private bool isNear; //근처에 플레이어가 있는지
    //private bool isNeedTurn;
    Renderer capsuleColor; //플레이어 발견 시 색깔 변경(임시)
    NavMeshAgent agent; //자신의 agent
    Rigidbody rigid;

    Direction direction;
    Direction nextDirection;
    Vector3 moveDirection; //실제 이동방향 벡터

    void Start()
    { 
        capsuleColor = gameObject.GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocity * 2f; //추격 속도 설정

        Invoke("SetMoveDirection", 2f);
        //Invoke("SetIsNeedTurn", 3f);
    }

    void Update()
    {
        //if (isNear)
        //    this.transform.position += new Vector3(direction.x, 0, direction.z) * (velocity*2 * Time.deltaTime); //이동
        //else
        //    this.transform.position += new Vector3(direction.x, 0, direction.z) * (velocity * Time.deltaTime);

        if (!isNear) //플레이어를 쫓고있지 않는 상태
        {
            //if (isNeedTurn)
            //    FindDirection();

            bool hitFront = Physics.Raycast(transform.position, moveDirection, 4.5f, LayerMask.GetMask("Wall"));
            Debug.DrawRay(transform.position, moveDirection * 4.5f, new Color(1, 0, 0));
            if (!hitFront) //전방에 벽이 없는 경우
            {
                rigid.velocity = moveDirection * velocity + new Vector3(0, rigid.velocity.y, 0); //이동
                if (moveDirection != Vector3.zero)
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(moveDirection).eulerAngles.y, 0)); //움직이는 방향을 바라보기
            }
            else
            {
                SetMoveDirection();
                Debug.Log("벽");
            }
        }
    }

    //void FindDirection()
    //{
    //    Vector3 center = transform.position;
    //    float bound = 5f;
    //    Debug.DrawRay(center, (transform.forward*2f-transform.right) * bound/2, new Color(1, 0, 0));
    //    Debug.DrawRay(center, -transform.right * bound, new Color(1, 0, 0));
    //    Debug.DrawRay(center, transform.forward * bound, new Color(1, 0, 0));
    //    Debug.DrawRay(center, transform.right * bound, new Color(1, 0, 0));
    //    bool hitLeft = Physics.Raycast(center, -transform.right, bound, LayerMask.GetMask("Wall"));
    //    bool hitFront = Physics.Raycast(center, transform.forward, bound, LayerMask.GetMask("Wall"));
    //    bool hitRight = Physics.Raycast(center, transform.right, bound, LayerMask.GetMask("Wall"));

    //    if (!hitLeft)
    //        Debug.Log("Turn Left");
    //    if (!hitFront)
    //        Debug.Log("Front");
    //    if (!hitRight)
    //        Debug.Log("Turn Right");
    //}

    void SetMoveDirection()
    {
        direction = (Direction)Random.Range(0, 4); //랜덤한 방향 지정
        moveDirection = Vector3FromEnum(direction);

        StopAllCoroutines();
        StartCoroutine("SetMoveDirectionByTime"); //일정 시간 이후 방향을 틀도록 코루틴 실행
    }

    Vector3 Vector3FromEnum(Direction state)
    {
        Vector3 dir = Vector3.zero;

        switch (state)
        {
            case Direction.Up:      dir = Vector3.forward;  break;
            case Direction.Right:   dir = Vector3.right;    break;
            case Direction.Down:    dir = Vector3.back;     break;
            case Direction.Left:    dir = Vector3.left;     break;

            default:                dir = Vector3.zero;     break;
        }

        return dir;
    }
    
    IEnumerator SetMoveDirectionByTime()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f)); //5~10초 후 방향 설정

        int dir = Random.Range(0, 4);
        nextDirection = (Direction)dir;
        //int dir = Random.Range(0, 2);
        //if ((dir == 0 && (int)direction == 0) || (dir == 1 && (int)direction == 2))
        //    Debug.Log("right");
        //else
        //    Debug.Log("left");
        //nextDirection = (Direction)(dir * 2 + 1 - (int)direction % 2); //상.하 <-> 좌.우
        StartCoroutine("CheckBlockedNextMoveDirection");
    }

    IEnumerator CheckBlockedNextMoveDirection()
    {
        while (true)
        {
            Vector3 body = transform.position;
            Vector3 leftRay = Vector3FromEnum(nextDirection)*2f + Vector3FromEnum((Direction)(Mathf.Repeat((int)nextDirection+1, 4)));
            Vector3 rightRay = Vector3FromEnum(nextDirection)*2f + Vector3FromEnum((Direction)(Mathf.Repeat((int)nextDirection-1, 4)));
            float bound = 5f;
            Debug.DrawRay(body, leftRay * bound*2, new Color(1, 0, 0));
            //Debug.DrawRay(body, Vector3FromEnum(nextDirection) * bound * 2, new Color(1, 0, 0));
            Debug.DrawRay(body, rightRay * bound*2, new Color(1, 0, 0));
            bool hitLeft = Physics.Raycast(body, leftRay, bound * 3/2, LayerMask.GetMask("Wall"));
            //bool hitFront = Physics.Raycast(body, Vector3FromEnum(nextDirection) * 2f, bound * 2, LayerMask.GetMask("Wall"));
            bool hitRight = Physics.Raycast(body, rightRay, bound * 3/2, LayerMask.GetMask("Wall"));

            if (!hitLeft && !hitRight) //벽이 뚫려 있다면
            {
                Debug.Log("Turn");
                moveDirection = Vector3FromEnum(nextDirection);
                nextDirection = Direction.None;
                StartCoroutine("SetMoveDirectionByTime"); //일정 시간 이후 방향을 틀도록 코루틴 실행
                break;
            }
            yield return null;
        }
    }

    //void SetIsNeedTurn()
    //{
    //    isNeedTurn = true;
    //}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            isNear = true;
            capsuleColor.material.color = Color.red;

            coll.GetComponent<Player>().NearEnemyNum++;
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            agent.destination = coll.transform.position; //목적지 설정
            //direction = (target.position - transform.position).normalized;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            isNear = false;
            //플레이어가 괴물에게서 벗어나도 마지막 목적지 도착 후 다시 랜덤이동 하도록 구현
            capsuleColor.material.color = Color.white;

            coll.GetComponent<Player>().NearEnemyNum--;
        }
    }

    void OnCollisionEnter(Collision coll) //충돌
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Die"); //사망

            //플레이 타임 저장
            GameManager.instance.timeScore = GameManager.instance.playTime;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void SetEnemyLevel()
    {
        //게임 난이도에 따라 Enemy 속도 조절
        if (GameManager.instance.gameLevel == "easy")  //게임 난이도가 easy면
            velocity = 3f;  //Enemy의 속도는 3f
        else if (GameManager.instance.gameLevel == "normal")   //게임 난이도가 normal면
            velocity = 5f;  //Enemy의 속도는 5f
        else if (GameManager.instance.gameLevel == "hard") //게임 난이도가 hard면
            velocity = 7f;  //Enemy의 속도는 7f
    }
}
