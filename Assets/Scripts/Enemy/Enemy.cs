using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum Direction { Up, Right, Down, Left, None }; //이동방향 열거
    public float velocity; //이동속도

    public bool isNear; //근처에 플레이어가 있는지
    public NavMeshAgent agent; //자신의 agent
    Rigidbody rigid;

    Direction direction; //현재 바라보는 방향
    Direction nextDirection;
    Vector3 moveDirection; //실제 이동방향 벡터

    //생성 Enemy 번호(몇 번째 Enemy인지)
    public int EnemyNumber;

    void Start()
    {
        EnemyNumber = GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().createEnemy;  //Enemy 번호 지정

        SetEnemyLevel();
        rigid = GetComponent<Rigidbody>();
        isNear = false;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocity;
        agent.destination = GameObject.FindWithTag("Player").transform.position;

        Invoke("SetMoveDirection", 5f); //생성되고 5초 후 이동
    }

    void Update()
    {
        if (!isNear) //플레이어를 쫓고있지 않는 상태
        {
            float bound = 4.5f;
            Debug.DrawRay(transform.position, moveDirection * bound, new Color(1, 0, 0));

            RaycastHit enemyColl;
            if (Physics.Raycast(transform.position, moveDirection, out enemyColl, bound, LayerMask.GetMask("Enemy"))) //적끼리 충돌한 경우
            {
                if(enemyColl.collider.GetType() == typeof(CapsuleCollider)) //레이캐스트에 닿은 콜라이더가 캡슐 콜라이더일 때
                    TurnBack(); //뒤돌아보기
            }

            bool hitFront = Physics.Raycast(transform.position, moveDirection, bound, LayerMask.GetMask("Wall"));
            if (!hitFront) //전방에 벽이 없는 경우
            {
                rigid.velocity = moveDirection * velocity + new Vector3(0, rigid.velocity.y, 0); //이동
                if (moveDirection != Vector3.zero)
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(moveDirection).eulerAngles.y, 0)); //움직이는 방향을 바라보기
            }
            else
            {
                SetMoveDirection();
                //Debug.Log("벽");
            }
        }
        else if (Vector3.Distance(this.transform.position, agent.destination) < 1.0f) //마지막으로 발견된 플레이어의 위치만큼 이동 후 다시 랜덤이동
        {
            isNear = false;
            agent.speed = 0;
        }
    }

    void TurnBack()
    {
        //Debug.Log("적끼리 충돌");
        direction = (Direction)(Mathf.Repeat((int)direction + 2, 4)); //뒤로 돌기
        moveDirection = Vector3FromEnum(direction);
    }

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
        yield return new WaitForSeconds(Random.Range(0.5f, 1f)); //0.5~1초 후 방향 설정

        int dir = Random.Range(0, 2);
        nextDirection = (Direction)(dir * 2 + 1 - (int)direction % 2); //상.하 <-> 좌.우
        StartCoroutine("CheckBlockedNextMoveDirection");

        StartCoroutine("NoTurnLongTime"); //5~10초동안 방향이동 한번도 안하면 방향 재설정
    }

    IEnumerator CheckBlockedNextMoveDirection()
    {
        while (true)
        {
            Vector3 rayDirection = Vector3FromEnum(nextDirection);
            Vector3 leftBody = this.transform.position + Vector3FromEnum((Direction)(Mathf.Repeat((int)nextDirection - 1, 4)))*2f;
            Vector3 rightBody = this.transform.position + Vector3FromEnum((Direction)(Mathf.Repeat((int)nextDirection + 1, 4)))*2f;
            float bound = 10f;
            Debug.DrawRay(leftBody, rayDirection * bound, new Color(0, 0, 1));
            Debug.DrawRay(rightBody, rayDirection * bound, new Color(0, 0, 1));
            bool hitLeft = Physics.Raycast(leftBody, rayDirection, bound, LayerMask.GetMask("Wall"));
            bool hitRight = Physics.Raycast(rightBody, rayDirection, bound, LayerMask.GetMask("Wall"));

            if (!hitLeft && !hitRight) //벽이 뚫려 있다면
            {
                //Debug.Log("Turn");
                direction = nextDirection; //방향 재설정
                moveDirection = Vector3FromEnum(nextDirection);
                nextDirection = Direction.None;
                StartCoroutine("SetMoveDirectionByTime"); //일정 시간 이후 방향을 틀도록 코루틴 실행

                StopCoroutine("NoTurnLongTime"); //방향이동 오랫동안 안했을때 실행되는 코루틴 종료
                break;
            }
            yield return null;
        }
    }

    IEnumerator NoTurnLongTime()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));

        SetMoveDirection();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            StopAllCoroutines();
            isNear = true;
            agent.speed = velocity * 2f;

            coll.GetComponent<Player>().CloseEnemyNum++;
            GameManager.instance.chaseCount++; //예측성 카운트
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            agent.destination = coll.transform.position; //목적지 설정
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Player>().CloseEnemyNum--;
        }
    }

    public void SetEnemyLevel()
    {
        //게임 난이도에 따라 Enemy 속도 조절
        if (GameManager.instance.gameLevel == "easy")  //게임 난이도가 easy면
            velocity = 3f;  //Enemy의 속도는 3f
        else if (GameManager.instance.gameLevel == "normal")   //게임 난이도가 normal면
            velocity = 4f;  //Enemy의 속도는 4f
        else if (GameManager.instance.gameLevel == "hard") //게임 난이도가 hard면
            velocity = 5f;  //Enemy의 속도는 5f
    }

    public void SpeedBoostEnemy()
    {
        velocity *= 1.5f;
    }

    public void ChasePlayer()
    {
        StopAllCoroutines();
        isNear = true;
        agent.destination = GameObject.FindWithTag("Player").transform.position;
        if (agent.speed == 0)
            agent.speed = velocity * 2f;
    }
}
