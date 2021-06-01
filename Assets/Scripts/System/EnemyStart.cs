using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    public GameObject Enemy;    //Enemy ������Ʈ
    Transform playerTransform;   //Enemy ���� ��ǥ�� ������ �� Player�� ��ǥ

    //Enemy�� ���� �� �ִ� �� ��ǥ ����Ʈ
    public List<Vector3> doors = new List<Vector3>();

    private float YPosition = -3.8f;    //Y��ǥ�� ����
    
    void Start()
    {
        //Player�� ��ǥ�� ������ ������Ʈ
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();


        //�� Ÿ�Կ� ���� Enemy�� ���� �� �ִ� �� ��ǥ �߰�
        if (GameManager.instance.mazeType == "T")
        {
            doors.Add(new Vector3(-32f, YPosition, 1.5f));
            doors.Add(new Vector3(-34f, YPosition, 34f));
            doors.Add(new Vector3(-36f, YPosition, -55f));
            doors.Add(new Vector3(55f, YPosition, -30f));
            doors.Add(new Vector3(25f, YPosition, 55f));
            doors.Add(new Vector3(-55f, YPosition, 19f));
            doors.Add(new Vector3(35f, YPosition, 3f));
            doors.Add(new Vector3(35f, YPosition, 22f));
        }
        else //E�� S�� �� ��ǥ�� ����
        {
            doors.Add(new Vector3(-27f, YPosition, 8f));
            doors.Add(new Vector3(-27f, YPosition, 40f));
            doors.Add(new Vector3(-36f, YPosition, -58f));
            doors.Add(new Vector3(55f, YPosition, -30f));
            doors.Add(new Vector3(25f, YPosition, 52f));
            doors.Add(new Vector3(27f, YPosition, -14f));
            doors.Add(new Vector3(27f, YPosition, -45f));
            doors.Add(new Vector3(-55f, YPosition, 31f));
        }
    }

    //Enemy ���� �Լ�(�� �Լ� ȣ��� Enemy ������)
    public void CreateEnemy()
    {
        //�÷��̾� ��ó ������ Enemy ����

        //���� �Ÿ�(ù��° ������ �Ÿ�)
        float shortDis = Vector3.Distance(playerTransform.position, doors[0]);
        Vector3 shortDoor = doors[0];   //���� ����� ���� ��ǥ�� ���� ��ǥ�� ����

        foreach (Vector3 EnemyPosition in doors)
        {
            float distance = Vector3.Distance(playerTransform.position, EnemyPosition);

            if (distance < shortDis) //���� �Ÿ����� �Ÿ��� ������
            {
                shortDis = distance;    //���� ����� �Ÿ� ����
                shortDoor = EnemyPosition;  //���� ����� ���� ��ǥ ����
            }
        }

        //���� ����� ���� ��ǥ�� ���� ����
        GameObject enemy = (GameObject)Instantiate(Enemy, shortDoor, Quaternion.identity);
    }
}
