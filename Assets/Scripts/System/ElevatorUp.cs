using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorUp : MonoBehaviour
{
    //엘리베이터가 올라가는 함수

    public GameObject exitElevator_leftDoor;    //탈출구 엘리베이터의 왼쪽 문
    public GameObject exitElevator_rightDoor;    //탈출구 엘리베이터의 오른쪽 문


    GameObject elevator;    //탈출구 엘리베이터
    GameObject player;   //플레이어 오브젝트)
    GameObject maze; //플레이 중인 미로
    public GameObject corridor; //엔딩 복도

    int elevatorIndex;


    void Start()
    {
        player = GameObject.FindWithTag("Player");

        elevatorIndex = GameManager.instance.elevatorIndex; //엘리베이터 인덱스 가져오기

        //맵 별 탈출구 엘리베이터 가져오기
        if (GameManager.instance.mazeType == "T")       //현재 미로가 T라면
        {
            elevator = GameObject.FindWithTag("Elevator_T").transform.GetChild(elevatorIndex).gameObject;
        }
        else if (GameManager.instance.mazeType == "E")  //현재 미로가 E라면
        {
            elevator = GameObject.FindWithTag("Elevator_E").transform.GetChild(elevatorIndex).gameObject;
        }
        else        //현재 미로가 S라면
        {
            elevator = GameObject.FindWithTag("Elevator_S").transform.GetChild(elevatorIndex).gameObject;
        }        
    }

    //1층까지 올라가는 함수 (엘리베이터가 도착하면 플레이어 순간이동(이때 엘리베이터 깜빡거림)
    public void UpToFirstFloor()
    {
        Sequence mySequence = DOTween.Sequence();   //시퀀스 생성
        
        mySequence.Append(elevator.transform.DOLocalMoveY(100f, 20f));  //엘리베이터 올라감
        //엘리베이터 조명 깜빡거림

        maze = GameObject.Find(GameManager.instance.mazeType+"_maze(Clone)");
        //corridor = GameObject.Find("hallway_modeling");
        corridor.SetActive(true); //복도 객체 활성화

        mySequence.OnComplete(() => {
            //엘리베이터가 도착하면
            //엘리베이터 조명 꺼짐
            player.gameObject.transform.position = new Vector3(152.0f, 8.32f, 495f);    //플레이어 순간이동
            player.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);    //플레이어 방향 조정

            maze.SetActive(false); //미로 객체 비활성화

            //엘리베이터 문 열림(영구)
            exitElevator_leftDoor.transform.DOLocalMoveX(3f, 3f).SetRelative();  //3초간 X 방향으로 3만큼 이동
            exitElevator_rightDoor.transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3초간 X 방향으로 -3만큼 이동

            //배경 음악 변경
            player.transform.GetChild(0).gameObject.GetComponent<BGAudioPlay>().PlayWhiteRoomBG();
        });
    }

    
}
