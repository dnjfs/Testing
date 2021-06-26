using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorUp : MonoBehaviour
{
    //엘리베이터가 올라가는 함수

    GameObject elevator;    //탈출구 엘리베이터
    int elevatorIndex;

    public GameObject player;   //플레이어 오브젝트(플레이어 오브젝트를 직접 움직이지 않고 올라가는 방법 찾기 전까지 사용)


    void Start()
    {
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

    //1층까지 올라가는 함수 (엘리베이터와 플레이어를 순간이동 시키면 부자연스러워 직접 위로 이동)
    public void UpToFirstFloor()
    {
        elevator.transform.DOLocalMoveY(100f, 20f);
        player.transform.DOLocalMoveY(100f, 20f);
    }

    
}
