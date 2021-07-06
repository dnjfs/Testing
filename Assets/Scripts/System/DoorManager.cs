using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween�� ����ϱ� ���� ����Ʈ

public class DoorManager : MonoBehaviour
{
    //�� ���� ��ũ��Ʈ

    public GameObject[] leftDoors = new GameObject[12];  //���� ���� ������ �迭
    public GameObject[] rightDoors = new GameObject[12]; //������ ���� ������ �迭

    public bool isMoving;    //���� ������ �ִ� ������ Ȯ��
    public bool isCloseDoor;  //���������� �������� �ݾҴ��� Ȯ��(���� ���·� ���� �� ����, ���� ���·� ���� �� ����)

    public GameObject Button;   //���������� ��ư �������� ������ ����
    public GameObject Enter; //���������Ϳ� �����ߴ��� �˻��ϴ� Ʈ����
    float YPosition = 1f;   //��ư�� Y��ǥ�� ����

    void Start()
    {
        isMoving = false;   //���� �������� �ʴ� ���·� �ʱ�ȭ
        isCloseDoor = true; //���� �����ִ� ���·� �ʱ�ȭ
    }

    //���������ͺ� �� ��ǥ �ҷ�����: T��
    public void T_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            GameObject elevator = GameObject.FindWithTag("Elevator_T").transform.GetChild(i - 1).gameObject; //���� ���� ���������͸� ����

            leftDoors[i - 1] = elevator.transform.GetChild(0).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
            rightDoors[i - 1] = elevator.transform.GetChild(1).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
        }
    }

    //���������ͺ� �� ��ǥ �ҷ�����: E��
    public void E_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            GameObject elevator = GameObject.FindWithTag("Elevator_E").transform.GetChild(i - 1).gameObject; //���� ���� ���������͸� ����

            leftDoors[i - 1] = elevator.transform.GetChild(0).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
            rightDoors[i - 1] = elevator.transform.GetChild(1).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
        }
    }

    //���������ͺ� �� ��ǥ �ҷ�����: S��
    public void S_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            GameObject elevator = GameObject.FindWithTag("Elevator_S").transform.GetChild(i - 1).gameObject; //���� ���� ���������͸� ����

            leftDoors[i - 1] = elevator.transform.GetChild(0).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
            rightDoors[i - 1] = elevator.transform.GetChild(1).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
        }
    }

    //�ش� ��°�� �� ����
    public void OpenDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && isCloseDoor)  //���� ���� �����ְ� �������� �ʴ� ���¶��
        {
            StartCoroutine(cOpenDoor(leftIndex, rightIndex));   //�� ���� �ڷ�ƾ �Լ� ����
        }
    }

    //�ش� �ε��� �� �ݱ�
    public void CloseDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && !isCloseDoor)  //���� ���� �������� �ʰ� �������� �ʴ� ���¶��
        {
            StartCoroutine(cCloseDoor(leftIndex, rightIndex));
        }
    }

    //�ش� ��°�� �� ����(Enemy ��) -> �ڵ� ���� ����
    public void OpenEnemyDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && isCloseDoor)  //���� ���� �����ְ� �������� �ʴ� ���¶��
        {
            StartCoroutine(cOpenEnemyDoor(leftIndex, rightIndex));   //�� ���� �ڷ�ƾ �Լ� ����
        }
    }

    //�ش� �ε��� �� �ݱ�(Enemy ��) -> Enemy�� ������ �ݱ�
    public void CloseEnemyDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && !isCloseDoor)  //���� ���� �������� �ʰ� �������� �ʴ� ���¶��
        {
            StartCoroutine(cCloseEnemyDoor(leftIndex, rightIndex));
        }
    }


    //�� ���� �ڷ�ƾ �Լ�
    IEnumerator cOpenDoor(int leftIndex, int rightIndex)
    {
        isMoving = true;    //���� �����̴� �� ����
        isCloseDoor = false;    //���� �������� �������� ����
        leftDoors[leftIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�
        rightDoors[rightIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�

        yield return new WaitForSeconds(3f);    //3�� ��
        isMoving = false;   //3�� �� ���� �������� �ʴ� ������ ����
        //isCloseDoor = false;    //3�� �� ���� ������������ ����

        yield return new WaitForSeconds(7f);    //7�� ��(+���� 3�� ����Ʈ ==���� ������ 10�� ��)
        CloseDoor(leftIndex, rightIndex);   //�� �ݴ� �Լ� ����
        //���� ��ư���� ���� �̸� �ݾ����� CloseDoor �Լ� ���� ���ǹ��� �ɷ� �׳� �Ѿ ��.

        yield return null;

    }

    //�� �ݴ� �ڷ�ƾ �Լ�
    IEnumerator cCloseDoor(int leftIndex, int rightIndex)
    {
        isMoving = true;    //���� �����̴� �� ����
        leftDoors[leftIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�
        rightDoors[rightIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�

        yield return new WaitForSeconds(3f);    //3�� ��
        isMoving = false;   //3�� �� ���� �������� �ʴ� ������ ����
        isCloseDoor = true;    //3�� �� ���� ������������ ����

        yield return null;
    }


    //Enemy�� ���� �ڷ�ƾ �Լ�(�ڵ� ���� ����)
    IEnumerator cOpenEnemyDoor(int leftIndex, int rightIndex)
    {
        isMoving = true;    //���� �����̴� �� ����
        isCloseDoor = false;    //���� �������� �������� ����
        leftDoors[leftIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�
        rightDoors[rightIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�

        yield return new WaitForSeconds(3f);    //3�� ��
        isMoving = false;   //3�� �� ���� �������� �ʴ� ������ ����
        isCloseDoor = false;    //3�� �� ���� ������������ ����

        yield return null;
    }

    //Enemy�� �ݴ� �ڷ�ƾ �Լ�(Enemy�� �浹�� �ݱ�)
    IEnumerator cCloseEnemyDoor(int leftIndex, int rightIndex)
    {
        isMoving = true;    //���� �����̴� �� ����
        leftDoors[leftIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�
        rightDoors[rightIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�

        yield return new WaitForSeconds(3f);    //3�� ��
        isMoving = false;   //3�� �� ���� �������� �ʴ� ������ ����
        isCloseDoor = true;    //3�� �� ���� ������������ ����

        yield return null;
    }

    //�÷��̾� ��ǥ���� ���� ����� Ż�� ���������� �����ϴ� �Լ�
    public void SetPlayerElevator(Vector3 playerPosition)
    {
        //�÷��̾� ���������� �ĺ�: 4, 6, 7, 9�� -> �ε���: 3, 5, 6, 8
        List<Vector3> Elevators = new List<Vector3>(); //�÷��̾� ���������� �ĺ����� ������ �迭

        //�÷��̾� �ⱸ ���������� ����(Ż�ⱸ���� �����ʹ��� �Ÿ� ��)
        Elevators.Add(rightDoors[3].transform.position);
        Elevators.Add(rightDoors[5].transform.position);
        Elevators.Add(rightDoors[6].transform.position);
        Elevators.Add(rightDoors[8].transform.position);

        //���� �Ÿ�(ù��° ������ �Ÿ�)
        float shortDis = Vector3.Distance(playerPosition, Elevators[0]);
        int shortElevatorIndex = 0;   //���� ����� ������������ �ε����� ����

        for (int i = 1; i < 4; i++)
        {
            float distance = Vector3.Distance(playerPosition, Elevators[i]);  //�÷��̾�� ���������� �Ÿ� ���

            if (distance < shortDis) //���� �Ÿ����� �Ÿ��� ������
            {
                shortDis = distance;    //���� ����� �Ÿ� ����
                shortElevatorIndex = i;  //���� ����� ���� ��ǥ ����
            }
        }

        //������������ �ε����� ����
        if (shortElevatorIndex == 0)
            GameManager.instance.elevatorIndex = 3;
        else if (shortElevatorIndex == 1)
            GameManager.instance.elevatorIndex = 5;
        else if (shortElevatorIndex == 2)
            GameManager.instance.elevatorIndex = 6;
        else
            GameManager.instance.elevatorIndex = 8;
    }

    //�̷� ������ ���������� ��ġ�� ���� ��ư�� �����ϴ� �Լ�
    public void CreateElevatorButton()
    {
        string maze = GameManager.instance.mazeType;    //GameManager�� �̷� Ÿ���� ������
        int elevator = GameManager.instance.elevatorIndex;  //GameManager�� ���������� ��ȣ�� ������

        if (maze.Equals("T"))    //���� �̷ΰ� T���̶��
        {
            if (elevator == 3)  //���� ���������� �ε����� 3�̶��
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(38.4f, YPosition, -58.9f), Quaternion.identity);    //��ư ȸ�� ���� ����
            }
            else if (elevator == 5) //���� ���������� �ε����� 5���
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(58.3f, YPosition, 33.7f), Quaternion.Euler(0f, -90f, 0f));    //��ư ȸ���ؼ� ����
            }
            else if (elevator == 6) //���� ���������� �ε����� 6�̶��
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(-20.2f, YPosition, 58.9f), Quaternion.Euler(0f, -180f, 0f));    //��ư ȸ���ؼ� ����
            }
            else if (elevator == 8) //���� ���������� �ε����� 8�̶��
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(-57.9f, YPosition, -25.85f), Quaternion.Euler(0f, 90f, 0f));    //��ư ȸ���ؼ� ����
            }
        }
        else    //���� �̷ΰ� E, S���̶��
        {
            if (elevator == 3)  //���� ���������� �ε����� 3�̶��
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(38f, YPosition, -60.65f), Quaternion.identity);    //��ư ȸ�� ���� ����
            }
            else if (elevator == 5) //���� ���������� �ε����� 5���
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(57.9f, YPosition, 32f), Quaternion.Euler(0f, -90f, 0f));    //��ư ȸ���ؼ� ����
            }
            else if (elevator == 6) //���� ���������� �ε����� 6�̶��
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(-20.6f, YPosition, 57.15f), Quaternion.Euler(0f, -180f, 0f));    //��ư ȸ���ؼ� ����
            }
            else if (elevator == 8) //���� ���������� �ε����� 8�̶��
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(-58.2f, YPosition, -27.65f), Quaternion.Euler(0f, 90f, 0f));    //��ư ȸ���ؼ� ����
            }
        }
    }
    public void CreateEnterTrigger() //���������Ϳ� ž���ߴ��� �˻��ϴ� Ʈ���� ����
    {
        GameObject elevator = GameObject.FindWithTag("Elevator_"+GameManager.instance.mazeType).transform.GetChild(GameManager.instance.elevatorIndex).gameObject;
        Debug.Log(elevator.name);
        GameObject enter = Instantiate(Enter, elevator.transform.position, Quaternion.identity, elevator.transform);
        //enter.transform.parent = elevator.transform; //Instantiate���� ������
        enter.transform.rotation = elevator.transform.rotation; //�θ��� rotation�� ����� ���� ��ǥ���� rotation�� 0,0,0���� ��
        enter.transform.Translate(new Vector3(0, -4f, 4f));
    }
}
