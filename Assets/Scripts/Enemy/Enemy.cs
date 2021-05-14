using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    enum Direction { Up, Right, Down, Left, None }; //�̵����� ����
    //private Vector3 direction; //�̵�����
    public float velocity; //�̵��ӵ�

    private bool isNear; //��ó�� �÷��̾ �ִ���
    //private bool isNeedTurn;
    Renderer capsuleColor; //�÷��̾� �߰� �� ���� ����(�ӽ�)
    NavMeshAgent agent; //�ڽ��� agent
    Rigidbody rigid;

    Direction direction;
    Direction nextDirection;
    Vector3 moveDirection; //���� �̵����� ����

    void Start()
    { 
        capsuleColor = gameObject.GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocity * 2f; //�߰� �ӵ� ����

        Invoke("SetMoveDirection", 2f);
        //Invoke("SetIsNeedTurn", 3f);
    }

    void Update()
    {
        //if (isNear)
        //    this.transform.position += new Vector3(direction.x, 0, direction.z) * (velocity*2 * Time.deltaTime); //�̵�
        //else
        //    this.transform.position += new Vector3(direction.x, 0, direction.z) * (velocity * Time.deltaTime);

        if (!isNear) //�÷��̾ �Ѱ����� �ʴ� ����
        {
            //if (isNeedTurn)
            //    FindDirection();

            bool hitFront = Physics.Raycast(transform.position, moveDirection, 4.5f, LayerMask.GetMask("Wall"));
            Debug.DrawRay(transform.position, moveDirection * 4.5f, new Color(1, 0, 0));
            if (!hitFront) //���濡 ���� ���� ���
            {
                rigid.velocity = moveDirection * velocity + new Vector3(0, rigid.velocity.y, 0); //�̵�
                if (moveDirection != Vector3.zero)
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(moveDirection).eulerAngles.y, 0)); //�����̴� ������ �ٶ󺸱�
            }
            else
            {
                SetMoveDirection();
                Debug.Log("��");
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
        yield return new WaitForSeconds(Random.Range(5f, 10f)); //5~10�� �� ���� ����

        int dir = Random.Range(0, 4);
        nextDirection = (Direction)dir;
        //int dir = Random.Range(0, 2);
        //if ((dir == 0 && (int)direction == 0) || (dir == 1 && (int)direction == 2))
        //    Debug.Log("right");
        //else
        //    Debug.Log("left");
        //nextDirection = (Direction)(dir * 2 + 1 - (int)direction % 2); //��.�� <-> ��.��
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

            if (!hitLeft && !hitRight) //���� �շ� �ִٸ�
            {
                Debug.Log("Turn");
                moveDirection = Vector3FromEnum(nextDirection);
                nextDirection = Direction.None;
                StartCoroutine("SetMoveDirectionByTime"); //���� �ð� ���� ������ Ʋ���� �ڷ�ƾ ����
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
            agent.destination = coll.transform.position; //������ ����
            //direction = (target.position - transform.position).normalized;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            isNear = false;
            //�÷��̾ �������Լ� ����� ������ ������ ���� �� �ٽ� �����̵� �ϵ��� ����
            capsuleColor.material.color = Color.white;

            coll.GetComponent<Player>().NearEnemyNum--;
        }
    }

    void OnCollisionEnter(Collision coll) //�浹
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Die"); //���

            //�÷��� Ÿ�� ����
            GameManager.instance.timeScore = GameManager.instance.playTime;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void SetEnemyLevel()
    {
        //���� ���̵��� ���� Enemy �ӵ� ����
        if (GameManager.instance.gameLevel == "easy")  //���� ���̵��� easy��
            velocity = 3f;  //Enemy�� �ӵ��� 3f
        else if (GameManager.instance.gameLevel == "normal")   //���� ���̵��� normal��
            velocity = 5f;  //Enemy�� �ӵ��� 5f
        else if (GameManager.instance.gameLevel == "hard") //���� ���̵��� hard��
            velocity = 7f;  //Enemy�� �ӵ��� 7f
    }
}
