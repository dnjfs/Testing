using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    public GameObject Enemy;    //Enemy ������Ʈ
    Transform playerTransform;   //Enemy ���� ��ǥ�� ������ �� Player�� ��ǥ

    //Enemy�� ������ ��ǥ ����Ʈ
    public List<Vector3> doors = new List<Vector3>();

    private float YPosition = -3.8f;    //Y��ǥ�� ����
    public int openDoorIndex; //Enemy ���� �� �ε���
    public int createEnemy;    //������ Enemy ��

    public GameObject EnemyExitCollider;  //Enemy�� �� ���� �������� Ȯ���� �ݶ��̴�

    void Start()
    {
        createEnemy = 0;

        //Player�� ��ǥ�� ������ ������Ʈ
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();


        //�� Ÿ�Կ� ���� Enemy�� ���� �� �ִ� ���������� ��ǥ �߰�
        if (GameManager.instance.mazeType == "T")
        {
            doors.Add(new Vector3(-32f, YPosition, -8f));       //1��
            doors.Add(new Vector3(-35f, YPosition, 25f));       //2��
            doors.Add(new Vector3(-35f, YPosition, -63f));      //3��
            doors.Add(new Vector3(63f, YPosition, -30f));        //5��
            doors.Add(new Vector3(25f, YPosition, 63f));        //8��
            doors.Add(new Vector3(-63f, YPosition, 19f));       //10��
            doors.Add(new Vector3(35f, YPosition, 11f));        //11��
            doors.Add(new Vector3(35f, YPosition, -13f));       //12��
        }
        else //E�� S�� �� ��ǥ�� ����
        {
            doors.Add(new Vector3(-27f, YPosition, 0f));
            doors.Add(new Vector3(-27f, YPosition, 31f));
            doors.Add(new Vector3(-36f, YPosition, -66f));
            doors.Add(new Vector3(63f, YPosition, -29f));
            doors.Add(new Vector3(25f, YPosition, 62f));
            doors.Add(new Vector3(27f, YPosition, -36f));
            doors.Add(new Vector3(27f, YPosition, -4f));
            doors.Add(new Vector3(-64f, YPosition, 31f));
        }
    }

    //Enemy ���� �Լ�(�� �Լ� ȣ��� Enemy ������)
    public void CreateEnemy()
    {
        //�÷��̾� ��ó ������ Enemy ����

        createEnemy++;  //������ Enemy �� ����

        //�÷��̾�Լ� ���� ����� �� �� �� ã��
        float shortDis = Vector3.Distance(playerTransform.position, doors[0]);  //���� �Ÿ�(Enemy ������ġ�� �÷��̾� ������ �Ÿ�)
        Vector3 shortDoor = doors[0];   //���� �Ÿ��� ���� ��ǥ�� ���� ��ǥ�� ����
        int shortDoorIndex = 0; //���� ����� �� �ε���

        for (int i = 1; i < doors.Count - 2; i++)
        {
            float distance = Vector3.Distance(playerTransform.position, doors[i]);

            if (distance < shortDis) //���� �Ÿ����� �Ÿ��� ������
            {

                //�� ����� �Ÿ��� �ִ��� Ȯ��
                for (int j = i + 1; j < doors.Count; j++)
                {
                    float tempDistance = Vector3.Distance(playerTransform.position, doors[j]);

                    if (tempDistance < shortDis) //�� ����� �Ÿ��� �ִٸ� ���� ������Ʈ�� ����� �Ÿ� ����(�� ����� �Ÿ��� �ֱ� ������ �÷��̾�Լ� ���� ����� �Ÿ��� �ƴ�)
                    {
                        shortDis = distance;    //����� �Ÿ� ����
                        shortDoor = doors[i];  //����� ��ǥ ����
                        shortDoorIndex = i; //����� �� �ε���
                        break;
                    }
                }

            }

        }

        
        switch (shortDoorIndex)
        {
            case 3:
                shortDoorIndex++; break;
            case 4:
                shortDoorIndex  += 3; break;
            case 5:
                shortDoorIndex += 4; break;
            case 6:
                shortDoorIndex += 5;    break;
            case 7:
                shortDoorIndex += 6; break;
            default:
                break;
        }

        openDoorIndex = shortDoorIndex;

        //�÷��̾�� ���� ����� �� �� ���� ���� ����(�÷��̾�� ���� ����� ���� ���� ����� ��)
        GameObject enemy = (GameObject)Instantiate(Enemy, shortDoor, Quaternion.identity);
        GameObject enemyExitBlock = (GameObject)Instantiate(EnemyExitCollider, shortDoor, Quaternion.identity); //Enemy ���� ��ġ�� Enemy�� �������� Ȯ���ϴ� ��� ����

        //Enemy ���� ���� ����.
        this.GetComponent<DoorManager>().OpenEnemyDoor(shortDoorIndex, shortDoorIndex);
    }
    
    //Enemy�� �� ������ �����ٸ�(Enemy�� ������ ������ ����)
    public void ExitEnemyDoor()
    {
        //Enemy ���� ���� �ݴ�.
        this.GetComponent<DoorManager>().CloseEnemyDoor(openDoorIndex, openDoorIndex);
    }
}
