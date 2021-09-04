using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorUp : MonoBehaviour
{
    //���������Ͱ� �ö󰡴� �Լ�

    public GameObject exitElevator_leftDoor;    //Ż�ⱸ ������������ ���� ��
    public GameObject exitElevator_rightDoor;    //Ż�ⱸ ������������ ������ ��


    GameObject elevator;    //Ż�ⱸ ����������
    GameObject player;   //�÷��̾� ������Ʈ)

    int elevatorIndex;


    void Start()
    {
        player = GameObject.FindWithTag("Player");

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

    //1������ �ö󰡴� �Լ� (���������Ͱ� �����ϸ� �÷��̾� �����̵�(�̶� ���������� �����Ÿ�)
    public void UpToFirstFloor()
    {
        Sequence mySequence = DOTween.Sequence();   //������ ����
        
        mySequence.Append(elevator.transform.DOLocalMoveY(100f, 20f));  //���������� �ö�
        //���������� ���� �����Ÿ�

        mySequence.OnComplete(() => {
            //���������Ͱ� �����ϸ�
            //���������� ���� ����
            player.gameObject.transform.position = new Vector3(152.0f, 8.32f, 495f);    //�÷��̾� �����̵�
            player.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);    //�÷��̾� ���� ����

            //���������� �� ����(����)
            exitElevator_leftDoor.transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�
            exitElevator_rightDoor.transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�

            //��� ���� ����
            player.transform.GetChild(0).gameObject.GetComponent<BGAudioPlay>().PlayWhiteRoomBG();
        });
    }

    
}
