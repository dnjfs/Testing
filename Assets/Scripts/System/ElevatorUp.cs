using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorUp : MonoBehaviour
{
    //���������Ͱ� �ö󰡴� �Լ�

    GameObject elevator;    //Ż�ⱸ ����������
    int elevatorIndex;

    public GameObject player;   //�÷��̾� ������Ʈ(�÷��̾� ������Ʈ�� ���� �������� �ʰ� �ö󰡴� ��� ã�� ������ ���)


    void Start()
    {
        elevatorIndex = GameManager.instance.elevatorIndex; //���������� �ε��� ��������

        //�� �� Ż�ⱸ ���������� ��������
        if (GameManager.instance.mazeType == "T")       //���� �̷ΰ� T���
        {
            elevator = GameObject.FindWithTag("Elevator_T").transform.GetChild(elevatorIndex).gameObject;
        }
        else if (GameManager.instance.mazeType == "E")  //���� �̷ΰ� E���
        {
            elevator = GameObject.FindWithTag("Elevator_E").transform.GetChild(elevatorIndex).gameObject;
        }
        else        //���� �̷ΰ� S���
        {
            elevator = GameObject.FindWithTag("Elevator_S").transform.GetChild(elevatorIndex).gameObject;
        }

    }

    //1������ �ö󰡴� �Լ� (���������Ϳ� �÷��̾ �����̵� ��Ű�� ���ڿ������� ���� ���� �̵�)
    public void UpToFirstFloor()
    {
        elevator.transform.DOLocalMoveY(100f, 20f);
        player.transform.DOLocalMoveY(100f, 20f);
    }

    
}
