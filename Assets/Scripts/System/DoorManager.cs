using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween�� ����ϱ� ���� ����Ʈ

public class DoorManager : MonoBehaviour
{
    //�� ���� ��ũ��Ʈ(������)
    
    public GameObject[] leftDoors = new GameObject [12];  //���� ���� ������ �迭
    public GameObject[] rightDoors = new GameObject[12]; //������ ���� ������ �迭

    GameObject elevator;    //���� ����������(ó���� �� ��ǥ ������ �� ���)

    //���������ͺ� �� ��ǥ �ҷ�����: T��
    public void T_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            elevator = GameObject.FindWithTag("Elevator_T").transform.GetChild(i - 1).gameObject; //���� ���� ���������͸� ����

            leftDoors[i - 1] = elevator.transform.GetChild(26).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
            rightDoors[i - 1] = elevator.transform.GetChild(38).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
        }
    }

    //���������ͺ� �� ��ǥ �ҷ�����: E��
    public void E_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            elevator = GameObject.FindWithTag("Elevator_E").transform.GetChild(i - 1).gameObject; //���� ���� ���������͸� ����

            leftDoors[i - 1] = elevator.transform.GetChild(0).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
            rightDoors[i - 1] = elevator.transform.GetChild(1).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
        }
    }

    //���������ͺ� �� ��ǥ �ҷ�����: S��
    public void S_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            elevator = GameObject.FindWithTag("Elevator_S").transform.GetChild(i - 1).gameObject; //���� ���� ���������͸� ����

            leftDoors[i - 1] = elevator.transform.GetChild(26).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
            rightDoors[i - 1] = elevator.transform.GetChild(38).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
        }
    }

    //�ش� ��°�� �� ����
    public void OpenDoor(int leftIndex, int rightIndex)
    {
        leftDoors[leftIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�
        rightDoors[rightIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�
    }

    /*
//���������ͺ��� ������ �޶� �̸����� �˻�
leftDoors[i - 1] = elevator.transform.Find("LeftDoor").gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
Debug.Log(i - 1 + "���ʹ� ����");
rightDoors[i - 1] = elevator.transform.Find("RightDoor").gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
Debug.Log(i - 1 + "�����ʹ� ����");
*/

    /*
    leftDoors[i - 1] = elevator.transform.GetChild(26).gameObject;  //�ش� �ε��� ������������ ���ʹ� ����(26: ���������� ������Ʈ�� ���ʹ� �ڽ� �ε���)
    Debug.Log(i - 1 + "���ʹ� ����");
    rightDoors[i - 1] = elevator.transform.GetChild(38).gameObject; //�ش� �ε��� ������������ �����ʹ� ����(38: ���������� ������Ʈ�� �����ʹ� �ڽ� �ε���)
    Debug.Log(i - 1 + "�����ʹ� ����");
    */
}
