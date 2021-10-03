using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum Direction { Up, Right, Down, Left, None }; //�̵����� ����
    public float velocity; //�̵��ӵ�

    public bool isNear; //��ó�� �÷��̾ �ִ���
    public NavMeshAgent agent; //�ڽ��� agent
    Rigidbody rigid;

    Direction direction; //���� �ٶ󺸴� ����
    Direction nextDirection;
    Vector3 moveDirection; //���� �̵����� ����

    //���� Enemy ��ȣ(�� ��° Enemy����)
    public int EnemyNumber;

    void Start()
    {
        EnemyNumber = GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().createEnemy;  //Enemy ��ȣ ����

        SetEnemyLevel();
        rigid = GetComponent<Rigidbody>();
        isNear = false;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocity;
        agent.destination = GameObject.FindWithTag("Player").transform.position;

        Invoke("SetMoveDirection", 5f); //�����ǰ� 5�� �� �̵�
    }

    void Update()
    {
        if (!isNear) //�÷��̾ �Ѱ����� �ʴ� ����
        {
            float bound = 4.5f;
            Debug.DrawRay(transform.position, moveDirection * bound, new Color(1, 0, 0));

            RaycastHit enemyColl;
            if (Physics.Raycast(transform.position, moveDirection, out enemyColl, bound, LayerMask.GetMask("Enemy"))) //������ �浹�� ���
            {
                if(enemyColl.collider.GetType() == typeof(CapsuleCollider)) //����ĳ��Ʈ�� ���� �ݶ��̴��� ĸ�� �ݶ��̴��� ��
                    TurnBack(); //�ڵ��ƺ���
            }

            bool hitFront = Physics.Raycast(transform.position, moveDirection, bound, LayerMask.GetMask("Wall"));
            if (!hitFront) //���濡 ���� ���� ���
            {
                rigid.velocity = moveDirection * velocity + new Vector3(0, rigid.velocity.y, 0); //�̵�
                if (moveDirection != Vector3.zero)
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(moveDirection).eulerAngles.y, 0)); //�����̴� ������ �ٶ󺸱�
            }
            else
            {
                SetMoveDirection();
                //Debug.Log("��");
            }
        }
        else if (Vector3.Distance(this.transform.position, agent.destination) < 1.0f) //���������� �߰ߵ� �÷��̾��� ��ġ��ŭ �̵� �� �ٽ� �����̵�
        {
            isNear = false;
            agent.speed = 0;
        }
    }

    void TurnBack()
    {
        //Debug.Log("������ �浹");
        direction = (Direction)(Mathf.Repeat((int)direction + 2, 4)); //�ڷ� ����
        moveDirection = Vector3FromEnum(direction);
    }

    void SetMoveDirection()
    {
        direction = (Direction)Random.Range(0, 4); //������ ���� ����
        moveDirection = Vector3FromEnum(direction);

        StopAllCoroutines();
        StartCoroutine("SetMoveDirectionByTime"); //���� �ð� ���� ������ Ʋ���� �ڷ�ƾ ����
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
        yield return new WaitForSeconds(Random.Range(0.5f, 1f)); //0.5~1�� �� ���� ����

        int dir = Random.Range(0, 2);
        nextDirection = (Direction)(dir * 2 + 1 - (int)direction % 2); //��.�� <-> ��.��
        StartCoroutine("CheckBlockedNextMoveDirection");

        StartCoroutine("NoTurnLongTime"); //5~10�ʵ��� �����̵� �ѹ��� ���ϸ� ���� �缳��
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

            if (!hitLeft && !hitRight) //���� �շ� �ִٸ�
            {
                //Debug.Log("Turn");
                direction = nextDirection; //���� �缳��
                moveDirection = Vector3FromEnum(nextDirection);
                nextDirection = Direction.None;
                StartCoroutine("SetMoveDirectionByTime"); //���� �ð� ���� ������ Ʋ���� �ڷ�ƾ ����

                StopCoroutine("NoTurnLongTime"); //�����̵� �������� �������� ����Ǵ� �ڷ�ƾ ����
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
            GameManager.instance.chaseCount++; //������ ī��Ʈ
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            agent.destination = coll.transform.position; //������ ����
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
        //���� ���̵��� ���� Enemy �ӵ� ����
        if (GameManager.instance.gameLevel == "easy")  //���� ���̵��� easy��
            velocity = 3f;  //Enemy�� �ӵ��� 3f
        else if (GameManager.instance.gameLevel == "normal")   //���� ���̵��� normal��
            velocity = 4f;  //Enemy�� �ӵ��� 4f
        else if (GameManager.instance.gameLevel == "hard") //���� ���̵��� hard��
            velocity = 5f;  //Enemy�� �ӵ��� 5f
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
