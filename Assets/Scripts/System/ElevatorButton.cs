using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public GameObject Button;   //엘리베이터 버튼 프리팹을 저장할 변수

    float YPosition = 1f;   //버튼의 Y좌표는 동일

    string maze;
    int elevator;

    //미로 종류와 엘리베이터 위치에 따라 버튼을 생성하는 함수
    public void CreateElevatorButton()
    {
        maze = GameManager.instance.mazeType;    //GameManager의 미로 타입을 가져옴
        elevator = GameManager.instance.elevatorIndex;  //GameManager의 엘리베이터 번호를 가져옴

        if (maze.Equals("T"))    //만약 미로가 T맵이라면
        {
            if (elevator == 3)  //만약 엘리베이터 인덱스가 3이라면
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(38.4f, YPosition, -58.9f), Quaternion.identity);    //버튼 회전 없이 생성
            }
            else if (elevator == 5) //만약 엘리베이터 인덱스가 5라면
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(58.3f, YPosition, 33.7f), Quaternion.Euler(0f, -90f, 0f));    //버튼 회전해서 생성
            }
            else if (elevator == 6) //만약 엘리베이터 인덱스가 6이라면
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(-20.2f, YPosition, 58.9f), Quaternion.Euler(0f, -180f, 0f));    //버튼 회전해서 생성
            }
            else if (elevator == 8) //만약 엘리베이터 인덱스가 8이라면
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(-57.9f, YPosition, -25.85f), Quaternion.Euler(0f, 90f, 0f));    //버튼 회전해서 생성
            }
        }
        else    //만약 미로가 E, S맵이라면
        {
            if (elevator == 3)  //만약 엘리베이터 인덱스가 3이라면
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(38f, YPosition, -60.65f), Quaternion.identity);    //버튼 회전 없이 생성
            }
            else if (elevator == 5) //만약 엘리베이터 인덱스가 5라면
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(57.9f, YPosition, 32f), Quaternion.Euler(0f, -90f, 0f));    //버튼 회전해서 생성
            }
            else if (elevator == 6) //만약 엘리베이터 인덱스가 6이라면
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(-20.6f, YPosition, 57.15f), Quaternion.Euler(0f, -180f, 0f));    //버튼 회전해서 생성
            }
            else if (elevator == 8) //만약 엘리베이터 인덱스가 8이라면
            {
                GameObject button = (GameObject)Instantiate(Button, new Vector3(-58.2f, YPosition, -27.65f), Quaternion.Euler(0f, 90f, 0f));    //버튼 회전해서 생성
            }
        }
    }
}
