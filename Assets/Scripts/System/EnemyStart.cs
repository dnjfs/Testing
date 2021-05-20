using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    public GameObject Enemy;    //Enemy ������Ʈ
    Transform playerTransform;   //Enemy ���� ��ǥ�� ������ �� Player�� ��ǥ

    //Enemy�� ���� �� �ִ� �� ��ǥ ����Ʈ
    public List<Vector3> doors = new List<Vector3>();
    
    void Start()
    {
        //Player�� ��ǥ�� ������ ������Ʈ
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();


        //�� Ÿ�Կ� ���� Enemy�� ���� �� �ִ� �� ��ǥ �߰�
        if (GameManager.instance.mazeType == "T")
        {
            doors.Add(new Vector3(-32f, -4f, 1.5f));
            doors.Add(new Vector3(-34f, -4f, 34f));
            doors.Add(new Vector3(-36f, -4f, -55f));
            doors.Add(new Vector3(55f, -4f, -30f));
            doors.Add(new Vector3(25f, -4f, 55f));
            doors.Add(new Vector3(-55f, -4f, 19f));
            doors.Add(new Vector3(35f, -4f, 3f));
            doors.Add(new Vector3(35f, -4f, 22f));
        }
        else //E�� S�� �� ��ǥ�� ����
        {
            doors.Add(new Vector3(-27f, -4f, 8f));
            doors.Add(new Vector3(-27f, -4f, 40f));
            doors.Add(new Vector3(-36f, -4f, -58f));
            doors.Add(new Vector3(55f, -4f, -30f));
            doors.Add(new Vector3(25f, -4f, 52f));
            doors.Add(new Vector3(27f, -4f, -14f));
            doors.Add(new Vector3(27f, -4f, -45f));
            doors.Add(new Vector3(-55f, -4f, 31f));
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
