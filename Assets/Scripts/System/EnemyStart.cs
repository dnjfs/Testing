using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    public GameObject Enemy;    //Enemy 오브젝트
    Transform playerTransform;   //Enemy 생성 좌표의 기준이 될 Player의 좌표

    //Enemy가 나올 수 있는 문 좌표 리스트
    public List<Vector3> doors = new List<Vector3>();

    private float YPosition = -3.8f;    //Y좌표는 동일
    
    void Start()
    {
        //Player의 좌표를 가져올 컴포넌트
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();


        //맵 타입에 따라 Enemy가 나올 수 있는 문 좌표 추가
        if (GameManager.instance.mazeType == "T")
        {
            doors.Add(new Vector3(-32f, YPosition, 1.5f));
            doors.Add(new Vector3(-34f, YPosition, 34f));
            doors.Add(new Vector3(-36f, YPosition, -55f));
            doors.Add(new Vector3(55f, YPosition, -30f));
            doors.Add(new Vector3(25f, YPosition, 55f));
            doors.Add(new Vector3(-55f, YPosition, 19f));
            doors.Add(new Vector3(35f, YPosition, 3f));
            doors.Add(new Vector3(35f, YPosition, 22f));
        }
        else //E와 S의 문 좌표는 같음
        {
            doors.Add(new Vector3(-27f, YPosition, 8f));
            doors.Add(new Vector3(-27f, YPosition, 40f));
            doors.Add(new Vector3(-36f, YPosition, -58f));
            doors.Add(new Vector3(55f, YPosition, -30f));
            doors.Add(new Vector3(25f, YPosition, 52f));
            doors.Add(new Vector3(27f, YPosition, -14f));
            doors.Add(new Vector3(27f, YPosition, -45f));
            doors.Add(new Vector3(-55f, YPosition, 31f));
        }
    }

    //Enemy 생성 함수(이 함수 호출시 Enemy 생성됨)
    public void CreateEnemy()
    {
        //플레이어 근처 문에서 Enemy 생성

        //기준 거리(첫번째 벡터의 거리)
        float shortDis = Vector3.Distance(playerTransform.position, doors[0]);
        Vector3 shortDoor = doors[0];   //가장 가까운 문의 좌표를 기준 좌표로 설정

        foreach (Vector3 EnemyPosition in doors)
        {
            float distance = Vector3.Distance(playerTransform.position, EnemyPosition);

            if (distance < shortDis) //기준 거리보다 거리가 가까우면
            {
                shortDis = distance;    //가장 가까운 거리 갱신
                shortDoor = EnemyPosition;  //가장 가까운 문의 좌표 갱신
            }
        }

        //가장 가까운 문의 좌표에 몬스터 생성
        GameObject enemy = (GameObject)Instantiate(Enemy, shortDoor, Quaternion.identity);
    }
}
