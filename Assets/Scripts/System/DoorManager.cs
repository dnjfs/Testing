using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween을 사용하기 위해 임포트

public class DoorManager : MonoBehaviour
{
    //문 열림 스크립트(수정중)
    
    public GameObject[] leftDoors = new GameObject [12];  //왼쪽 문을 저장할 배열
    public GameObject[] rightDoors = new GameObject[12]; //오른쪽 문을 저장할 배열

    GameObject elevator;    //현재 엘리베이터(처음에 문 좌표 저장할 때 사용)

    //엘리베이터별 문 좌표 불러오기: T맵
    public void T_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            elevator = GameObject.FindWithTag("Elevator_T").transform.GetChild(i - 1).gameObject; //현재 맵의 엘리베이터를 설정

            leftDoors[i - 1] = elevator.transform.GetChild(26).gameObject;  //해당 인덱스 엘리베이터의 왼쪽문 저장(26: 엘리베이터 오브젝트의 왼쪽문 자식 인덱스)
            rightDoors[i - 1] = elevator.transform.GetChild(38).gameObject; //해당 인덱스 엘리베이터의 오른쪽문 저장(38: 엘리베이터 오브젝트의 오른쪽문 자식 인덱스)
        }
    }

    //엘리베이터별 문 좌표 불러오기: E맵
    public void E_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            elevator = GameObject.FindWithTag("Elevator_E").transform.GetChild(i - 1).gameObject; //현재 맵의 엘리베이터를 설정

            leftDoors[i - 1] = elevator.transform.GetChild(0).gameObject;  //해당 인덱스 엘리베이터의 왼쪽문 저장(26: 엘리베이터 오브젝트의 왼쪽문 자식 인덱스)
            rightDoors[i - 1] = elevator.transform.GetChild(1).gameObject; //해당 인덱스 엘리베이터의 오른쪽문 저장(38: 엘리베이터 오브젝트의 오른쪽문 자식 인덱스)
        }
    }

    //엘리베이터별 문 좌표 불러오기: S맵
    public void S_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            elevator = GameObject.FindWithTag("Elevator_S").transform.GetChild(i - 1).gameObject; //현재 맵의 엘리베이터를 설정

            leftDoors[i - 1] = elevator.transform.GetChild(26).gameObject;  //해당 인덱스 엘리베이터의 왼쪽문 저장(26: 엘리베이터 오브젝트의 왼쪽문 자식 인덱스)
            rightDoors[i - 1] = elevator.transform.GetChild(38).gameObject; //해당 인덱스 엘리베이터의 오른쪽문 저장(38: 엘리베이터 오브젝트의 오른쪽문 자식 인덱스)
        }
    }

    //해당 번째의 문 열기
    public void OpenDoor(int leftIndex, int rightIndex)
    {
        leftDoors[leftIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3초간 X 방향으로 3만큼 이동
        rightDoors[rightIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3초간 X 방향으로 -3만큼 이동
    }

    /*
//엘리베이터별로 구성이 달라서 이름으로 검색
leftDoors[i - 1] = elevator.transform.Find("LeftDoor").gameObject;  //해당 인덱스 엘리베이터의 왼쪽문 저장(26: 엘리베이터 오브젝트의 왼쪽문 자식 인덱스)
Debug.Log(i - 1 + "왼쪽문 성공");
rightDoors[i - 1] = elevator.transform.Find("RightDoor").gameObject; //해당 인덱스 엘리베이터의 오른쪽문 저장(38: 엘리베이터 오브젝트의 오른쪽문 자식 인덱스)
Debug.Log(i - 1 + "오른쪽문 성공");
*/

    /*
    leftDoors[i - 1] = elevator.transform.GetChild(26).gameObject;  //해당 인덱스 엘리베이터의 왼쪽문 저장(26: 엘리베이터 오브젝트의 왼쪽문 자식 인덱스)
    Debug.Log(i - 1 + "왼쪽문 성공");
    rightDoors[i - 1] = elevator.transform.GetChild(38).gameObject; //해당 인덱스 엘리베이터의 오른쪽문 저장(38: 엘리베이터 오브젝트의 오른쪽문 자식 인덱스)
    Debug.Log(i - 1 + "오른쪽문 성공");
    */
}
