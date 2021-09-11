using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    public GameObject Enemy;    //Enemy 오브젝트
    Transform playerTransform;   //Enemy 생성 좌표의 기준이 될 Player의 좌표

    //Enemy가 생성될 좌표 리스트
    public List<Vector3> doors = new List<Vector3>();

    private float YPosition = -3.8f;    //Y좌표는 동일
    public int openDoorIndex; //Enemy 생성 문 인덱스
    public int createEnemy;    //생성된 Enemy 수

    public GameObject EnemyExitCollider;  //Enemy가 문 밖을 나갔는지 확인할 콜라이더

    void Start()
    {
        createEnemy = 0;

        //Player의 좌표를 가져올 컴포넌트
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();


        //맵 타입에 따라 Enemy가 나올 수 있는 엘리베이터 좌표 추가
        if (GameManager.instance.mazeType == "T")
        {
            doors.Add(new Vector3(-32f, YPosition, -8f));       //1번
            doors.Add(new Vector3(-35f, YPosition, 25f));       //2번
            doors.Add(new Vector3(-35f, YPosition, -63f));      //3번
            doors.Add(new Vector3(63f, YPosition, -30f));        //5번
            doors.Add(new Vector3(25f, YPosition, 63f));        //8번
            doors.Add(new Vector3(-63f, YPosition, 19f));       //10번
            doors.Add(new Vector3(35f, YPosition, 11f));        //11번
            doors.Add(new Vector3(35f, YPosition, -13f));       //12번
        }
        else //E와 S의 문 좌표는 같음
        {
            doors.Add(new Vector3(-27f, YPosition, 0f));
            doors.Add(new Vector3(-27f, YPosition, 31f));
            doors.Add(new Vector3(-36f, YPosition, -66f));
            doors.Add(new Vector3(63f, YPosition, -29f));
            doors.Add(new Vector3(25f, YPosition, 62f));
            doors.Add(new Vector3(27f, YPosition, -36f));
            doors.Add(new Vector3(27f, YPosition, -4f));
            doors.Add(new Vector3(-64f, YPosition, 31f));
        }
    }

    //Enemy 생성 함수(이 함수 호출시 Enemy 생성됨)
    public void CreateEnemy()
    {
        //플레이어 근처 문에서 Enemy 생성

        createEnemy++;  //생성된 Enemy 수 증가

        //플레이어에게서 가장 가까운 문 옆 문 찾기
        float shortDis = Vector3.Distance(playerTransform.position, doors[0]);  //기준 거리(Enemy 생성위치와 플레이어 사이의 거리)
        Vector3 shortDoor = doors[0];   //기준 거리의 문의 좌표를 기준 좌표로 설정
        int shortDoorIndex = 0; //가장 가까운 문 인덱스

        for (int i = 1; i < doors.Count - 2; i++)
        {
            float distance = Vector3.Distance(playerTransform.position, doors[i]);

            if (distance < shortDis) //기준 거리보다 거리가 가까우면
            {

                //더 가까운 거리가 있는지 확인
                for (int j = i + 1; j < doors.Count; j++)
                {
                    float tempDistance = Vector3.Distance(playerTransform.position, doors[j]);

                    if (tempDistance < shortDis) //더 가까운 거리가 있다면 현재 오브젝트로 가까운 거리 갱신(더 가까운 거리가 있기 때문에 플레이어에게서 가장 가까운 거리는 아님)
                    {
                        shortDis = distance;    //가까운 거리 갱신
                        shortDoor = doors[i];  //가까운 좌표 갱신
                        shortDoorIndex = i; //가까운 문 인덱스
                        break;
                    }
                }

            }

        }

        
        switch (shortDoorIndex)
        {
            case 3:
                shortDoorIndex++; break;
            case 4:
                shortDoorIndex  += 3; break;
            case 5:
                shortDoorIndex += 4; break;
            case 6:
                shortDoorIndex += 5;    break;
            case 7:
                shortDoorIndex += 6; break;
            default:
                break;
        }

        openDoorIndex = shortDoorIndex;

        //플레이어와 가장 가까운 문 옆 문에 몬스터 생성(플레이어와 가장 가까운 문의 가장 가까운 문)
        GameObject enemy = (GameObject)Instantiate(Enemy, shortDoor, Quaternion.identity);
        GameObject enemyExitBlock = (GameObject)Instantiate(EnemyExitCollider, shortDoor, Quaternion.identity); //Enemy 생성 위치에 Enemy가 나갔는지 확인하는 블록 생성

        //Enemy 방의 문을 연다.
        this.GetComponent<DoorManager>().OpenEnemyDoor(shortDoorIndex, shortDoorIndex);
    }
    
    //Enemy가 문 밖으로 나갔다면(Enemy가 밖으로 나가면 실행)
    public void ExitEnemyDoor()
    {
        //Enemy 방의 문을 닫는.
        this.GetComponent<DoorManager>().CloseEnemyDoor(openDoorIndex, openDoorIndex);
    }
}
