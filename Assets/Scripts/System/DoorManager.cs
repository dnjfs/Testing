using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween을 사용하기 위해 임포트

public class DoorManager : MonoBehaviour
{
    //문 열림 스크립트

    public GameObject[] leftDoors = new GameObject[12];  //왼쪽 문을 저장할 배열
    public GameObject[] rightDoors = new GameObject[12]; //오른쪽 문을 저장할 배열

    public bool isMoving;    //문이 열리고 있는 중인지 확인
    public bool isCloseDoor;  //마지막으로 열었는지 닫았는지 확인(열은 상태로 열을 수 없고, 닫은 상태로 닫을 수 없음)

    public GameObject Button;   //엘리베이터 버튼 프리팹을 저장할 변수
    public GameObject Enter; //엘리베이터에 입장했는지 검사하는 트리거
    float YPosition = 1f;   //버튼의 Y좌표는 동일

    void Start()
    {
        isMoving = false;   //문은 움직이지 않는 상태로 초기화
        isCloseDoor = true; //문이 닫혀있는 상태로 초기화
    }

    //엘리베이터별 문 좌표 불러오기: T맵
    public void T_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            GameObject elevator = GameObject.FindWithTag("Elevator_T").transform.GetChild(i - 1).gameObject; //현재 맵의 엘리베이터를 설정

            leftDoors[i - 1] = elevator.transform.GetChild(0).gameObject;  //해당 인덱스 엘리베이터의 왼쪽문 저장(26: 엘리베이터 오브젝트의 왼쪽문 자식 인덱스)
            rightDoors[i - 1] = elevator.transform.GetChild(1).gameObject; //해당 인덱스 엘리베이터의 오른쪽문 저장(38: 엘리베이터 오브젝트의 오른쪽문 자식 인덱스)
        }
    }

    //엘리베이터별 문 좌표 불러오기: E맵
    public void E_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            GameObject elevator = GameObject.FindWithTag("Elevator_E").transform.GetChild(i - 1).gameObject; //현재 맵의 엘리베이터를 설정

            leftDoors[i - 1] = elevator.transform.GetChild(0).gameObject;  //해당 인덱스 엘리베이터의 왼쪽문 저장(26: 엘리베이터 오브젝트의 왼쪽문 자식 인덱스)
            rightDoors[i - 1] = elevator.transform.GetChild(1).gameObject; //해당 인덱스 엘리베이터의 오른쪽문 저장(38: 엘리베이터 오브젝트의 오른쪽문 자식 인덱스)
        }
    }

    //엘리베이터별 문 좌표 불러오기: S맵
    public void S_DoorSetting()
    {
        for (int i = 1; i < 13; i++)
        {
            GameObject elevator = GameObject.FindWithTag("Elevator_S").transform.GetChild(i - 1).gameObject; //현재 맵의 엘리베이터를 설정

            leftDoors[i - 1] = elevator.transform.GetChild(0).gameObject;  //해당 인덱스 엘리베이터의 왼쪽문 저장(26: 엘리베이터 오브젝트의 왼쪽문 자식 인덱스)
            rightDoors[i - 1] = elevator.transform.GetChild(1).gameObject; //해당 인덱스 엘리베이터의 오른쪽문 저장(38: 엘리베이터 오브젝트의 오른쪽문 자식 인덱스)
        }
    }

    //해당 번째의 문 열기
    public void OpenDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && isCloseDoor)  //지금 문이 닫혀있고 움직이지 않는 상태라면
        {
            StartCoroutine(cOpenDoor(leftIndex, rightIndex));   //문 여는 코루틴 함수 실행
        }
    }

    //해당 인덱스 문 닫기
    public void CloseDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && !isCloseDoor)  //지금 문이 닫혀있지 않고 움직이지 않는 상태라면
        {
            StartCoroutine(cCloseDoor(leftIndex, rightIndex));
        }
    }

    //해당 번째의 문 열기(Enemy 문) -> 자동 닫힘 없음
    public void OpenEnemyDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && isCloseDoor)  //지금 문이 닫혀있고 움직이지 않는 상태라면
        {
            StartCoroutine(cOpenEnemyDoor(leftIndex, rightIndex));   //문 여는 코루틴 함수 실행
        }
    }

    //해당 인덱스 문 닫기(Enemy 문) -> Enemy가 나가면 닫기
    public void CloseEnemyDoor(int leftIndex, int rightIndex)
    {
        if (!isMoving && !isCloseDoor)  //지금 문이 닫혀있지 않고 움직이지 않는 상태라면
        {
            StartCoroutine(cCloseEnemyDoor(leftIndex, rightIndex));
        }
    }


    //문 여는 코루틴 함수
    IEnumerator cOpenDoor(int leftIndex, int rightIndex)
    {
        isMoving = true;    //문이 움직이는 중 설정
        isCloseDoor = false;    //문이 닫혀있지 않음으로 설정
        leftDoors[leftIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3초간 X 방향으로 3만큼 이동
        rightDoors[rightIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3초간 X 방향으로 -3만큼 이동

        yield return new WaitForSeconds(3f);    //3초 뒤
        isMoving = false;   //3초 뒤 문이 움직이지 않는 중으로 설정
        //isCloseDoor = false;    //3초 뒤 문이 열려있음으로 설정

        yield return new WaitForSeconds(7f);    //7초 뒤(+위의 3초 웨이트 ==문이 열린지 10초 뒤)
        CloseDoor(leftIndex, rightIndex);   //문 닫는 함수 실행
        //만약 버튼으로 문을 미리 닫았으면 CloseDoor 함수 내의 조건문에 걸려 그냥 넘어갈 것.

        yield return null;

    }

    //문 닫는 코루틴 함수
    IEnumerator cCloseDoor(int leftIndex, int rightIndex)
    {
        isMoving = true;    //문이 움직이는 중 설정
        leftDoors[leftIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3초간 X 방향으로 -3만큼 이동
        rightDoors[rightIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3초간 X 방향으로 3만큼 이동

        yield return new WaitForSeconds(3f);    //3초 뒤
        isMoving = false;   //3초 뒤 문이 움직이지 않는 중으로 설정
        isCloseDoor = true;    //3초 뒤 문이 닫혀있음으로 설정

        yield return null;
    }


    //Enemy문 여는 코루틴 함수(자동 닫힘 없음)
    IEnumerator cOpenEnemyDoor(int leftIndex, int rightIndex)
    {
        isMoving = true;    //문이 움직이는 중 설정
        isCloseDoor = false;    //문이 닫혀있지 않음으로 설정
        leftDoors[leftIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3초간 X 방향으로 3만큼 이동
        rightDoors[rightIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3초간 X 방향으로 -3만큼 이동

        yield return new WaitForSeconds(3f);    //3초 뒤
        isMoving = false;   //3초 뒤 문이 움직이지 않는 중으로 설정
        isCloseDoor = false;    //3초 뒤 문이 열려있음으로 설정

        yield return null;
    }

    //Enemy문 닫는 코루틴 함수(Enemy와 충돌시 닫기)
    IEnumerator cCloseEnemyDoor(int leftIndex, int rightIndex)
    {
        isMoving = true;    //문이 움직이는 중 설정
        leftDoors[leftIndex].transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3초간 X 방향으로 -3만큼 이동
        rightDoors[rightIndex].transform.DOLocalMoveX(3f, 3f).SetRelative();  //3초간 X 방향으로 3만큼 이동

        yield return new WaitForSeconds(3f);    //3초 뒤
        isMoving = false;   //3초 뒤 문이 움직이지 않는 중으로 설정
        isCloseDoor = true;    //3초 뒤 문이 닫혀있음으로 설정

        yield return null;
    }

    //플레이어 좌표에서 가장 가까운 탈출 엘리베이터 지정하는 함수
    public void SetPlayerElevator(Vector3 playerPosition)
    {
        //플레이어 엘리베이터 후보: 4, 6, 7, 9번 -> 인덱스: 3, 5, 6, 8
        List<Vector3> Elevators = new List<Vector3>(); //플레이어 엘리베이터 후보들을 저장할 배열

        //플레이어 출구 엘리베이터 설정(탈출구들의 오른쪽문과 거리 비교)
        Elevators.Add(rightDoors[3].transform.position);
        Elevators.Add(rightDoors[5].transform.position);
        Elevators.Add(rightDoors[6].transform.position);
        Elevators.Add(rightDoors[8].transform.position);

        //기준 거리(첫번째 벡터의 거리)
        float shortDis = Vector3.Distance(playerPosition, Elevators[0]);
        int shortElevatorIndex = 0;   //가장 가까운 엘리베이터의 인덱스를 저장

        for (int i = 1; i < 4; i++)
        {
            float distance = Vector3.Distance(playerPosition, Elevators[i]);  //플레이어와 엘리베이터 거리 계산

            if (distance < shortDis) //기준 거리보다 거리가 가까우면
            {
                shortDis = distance;    //가장 가까운 거리 갱신
                shortElevatorIndex = i;  //가장 가까운 문의 좌표 갱신
            }
        }

        //엘리베이터의 인덱스를 저장
        if (shortElevatorIndex == 0)
            GameManager.instance.elevatorIndex = 3;
        else if (shortElevatorIndex == 1)
            GameManager.instance.elevatorIndex = 5;
        else if (shortElevatorIndex == 2)
            GameManager.instance.elevatorIndex = 6;
        else
            GameManager.instance.elevatorIndex = 8;
    }

    //미로 종류와 엘리베이터 위치에 따라 버튼을 생성하는 함수
    public void CreateElevatorButton()
    {
        string maze = GameManager.instance.mazeType;    //GameManager의 미로 타입을 가져옴
        int elevator = GameManager.instance.elevatorIndex;  //GameManager의 엘리베이터 번호를 가져옴

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
    public void CreateEnterTrigger() //엘리베이터에 탑승했는지 검사하는 트리거 생성
    {
        GameObject elevator = GameObject.FindWithTag("Elevator_"+GameManager.instance.mazeType).transform.GetChild(GameManager.instance.elevatorIndex).gameObject;
        Debug.Log(elevator.name);
        GameObject enter = Instantiate(Enter, elevator.transform.position, Quaternion.identity, elevator.transform);
        //enter.transform.parent = elevator.transform; //Instantiate에서 설정됨
        enter.transform.rotation = elevator.transform.rotation; //부모의 rotation과 맞춰야 로컬 좌표계의 rotation이 0,0,0으로 됨
        enter.transform.Translate(new Vector3(0, -4f, 4f));
    }
}
