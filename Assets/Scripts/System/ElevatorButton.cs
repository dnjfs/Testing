using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public GameObject Button;   //���������� ��ư �������� ������ ����

    float YPosition = 1f;   //��ư�� Y��ǥ�� ����

    string maze;
    int elevator;

    //�̷� ������ ���������� ��ġ�� ���� ��ư�� �����ϴ� �Լ�
    public void CreateElevatorButton()
    {
        maze = GameManager.instance.mazeType;    //GameManager�� �̷� Ÿ���� ������
        elevator = GameManager.instance.elevatorIndex;  //GameManager�� ���������� ��ȣ�� ������

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
}
