using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween�� ����ϱ� ���� ����Ʈ

public class DoorManager : MonoBehaviour
{
    //�� ���� ��ũ��Ʈ
    
    public GameObject[] leftDoors = new GameObject [12];  //���� ���� ������ �迭
    public GameObject[] rightDoors = new GameObject[12]; //������ ���� ������ �迭

    bool isMoving;    //���� ������ �ִ� ������ Ȯ��
    bool isCloseDoor;  //���������� �������� �ݾҴ��� Ȯ��(���� ���·� ���� �� ����, ���� ���·� ���� �� ����)

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

            leftDoors[i - 1] = elevator.transform.GetChild(26).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
            rightDoors[i - 1] = elevator.transform.GetChild(38).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
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

            leftDoors[i - 1] = elevator.transform.GetChild(26).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
            rightDoors[i - 1] = elevator.transform.GetChild(38).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
        }
    }

    //�ش� ��°�� �� ����
    public void OpenDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && isCloseDoor)  //���� ���� �����ְ� �������� �ʴ� ���¶��
        {
            isMoving = true;    //���� �����̴� �� ����
            leftDoors[leftIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�
            rightDoors[rightIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�

            Invoke("isMoving = false", 3f);  //3�� �� ���� �������� �ʴ� ������ ����
            Invoke("isCloseDoor = false", 3f);  //3�� �� ���� ������������ ����
        }

        Invoke("CloseDoor", 10f);   //10�� �� �� ����
        //���� ��ư���� �̸� ������ CloseDoor �Լ� ���� ���ǹ��� �ɷ� �Ѿ ��.
    }

    //�ش� �ε��� �� �ݱ�
    public void CloseDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && !isCloseDoor)  //���� ���� �������� �ʰ� �������� �ʴ� ���¶��
        {
            isMoving = true;    //���� �����̴� �� ����
            leftDoors[leftIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�
            rightDoors[rightIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�

            Invoke("isMoving = false", 3f);  //3�� �� ���� �������� �ʴ� ������ ����
            Invoke("isCloseDoor = true", 3f);  //3�� �� ���� ������������ ����
        }
    }

    //�÷��̾� ��ǥ���� ���� ����� Ż�� ���������� ����
    public void SetPlayerElevator(Vector3 playerPosition)
    {
        //�÷��̾� ���������� �ĺ�: 4, 6, 7, 9�� -> �ε���: 3, 5, 6, 8
        List <Vector3> Elevators = new List<Vector3>(); //�÷��̾� ���������� �ĺ����� ������ �迭

        //�÷��̾� �ⱸ ���������� ����
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
}
