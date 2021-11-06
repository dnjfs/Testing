using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorUp : MonoBehaviour
{
    //���������Ͱ� �ö󰡴� �Լ�

    //public GameObject exitElevator_leftDoor;    //Ż�ⱸ ������������ ���� ��
    //public GameObject exitElevator_rightDoor;    //Ż�ⱸ ������������ ������ ��


    GameObject elevator;    //Ż�ⱸ ����������
    GameObject player;   //�÷��̾� ������Ʈ)
    //GameObject maze; //�÷��� ���� �̷�
    //public GameObject corridor; //���� ����

    int elevatorIndex;


    void Start()
    {
        player = GameObject.FindWithTag("Player");

        elevatorIndex = GameManager.instance.elevatorIndex; //���������� �ε��� ��������
        elevator = GameObject.FindWithTag("Elevators").transform.GetChild(elevatorIndex).gameObject;    //Ż�ⱸ ���������� ��������
    }

    //1������ �ö󰡴� �Լ� (���������Ͱ� �����ϸ� �÷��̾� �����̵�
    public void UpToFirstFloor()
    {
        Sequence mySequence = DOTween.Sequence();   //������ ����
        
        mySequence.Append(elevator.transform.DOLocalMoveY(100f, 20f));  //���������� �ö�
        //���������� ���� �����Ÿ�

        //maze = GameObject.Find(GameManager.instance.mazeType+"_maze(Clone)");
        //corridor = GameObject.Find("hallway_modeling");
        //corridor.SetActive(true); //���� ��ü Ȱ��ȭ
        
        mySequence.OnComplete(() => {
            //���������Ͱ� �����ϸ�

            //�� �̵�
            GameObject.FindWithTag("GameSystem").GetComponent<SceneChange>().ChangeCorridorScene();

            //���������� ���� ����
            //player.gameObject.transform.position = new Vector3(152.0f, 8.32f, 495f);    //�÷��̾� �����̵�
            //player.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);    //�÷��̾� ���� ����

            //maze.SetActive(false); //�̷� ��ü ��Ȱ��ȭ

            //���������� �� ����(����)
            //exitElevator_leftDoor.transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�
            //exitElevator_rightDoor.transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�

            //��� ���� ����
            //player.transform.GetChild(0).gameObject.GetComponent<BGAudioPlay>().PlayWhiteRoomBG();
        });
    }

    
}
