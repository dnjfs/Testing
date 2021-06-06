using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandom : MonoBehaviour
{
    //Player Prefab을 받을 public 변수
    public GameObject Player;

    //Maze Prefab을 받을 public 변수
    public GameObject T_Map;
    public GameObject E_Map;
    public GameObject S_Map;

    //맵 별 중복도 검사 블록
    public GameObject T_Block;
    public GameObject E_Block;
    public GameObject S_Block;

    //중복도 검사 블록 그룹 좌표(미로 셋 다 동일)
    private float mazeX = -28.05f;
    private float maxeY = -7.17f;
    private float mazeZ = -42.78f;

    //플레이어가 생성될 좌표(X, Z 좌표가 모두 54, -54 중 하나)
    float[] XZPosition = {54f, -54f};
    float YPosition = -1f;

    GameObject GameSystem;   //DoorManager, ElevatorButton 스크립트를 가지고 있는 GameSystem 오브젝트

    // Start()가 실행되기 전 실행
    void Awake()
    {

        GameSystem = GameObject.FindWithTag("GameSystem");

        //맵 생성(1/3 확률로 T, E, S맵 생성)
        int mapIndex = Random.Range(0, 3);
        if (mapIndex == 0)
        {
            GameObject maze = (GameObject)Instantiate(T_Map, new Vector3(0f, 0f, 0f), Quaternion.identity); //T맵 생성
            GameManager.instance.mazeType = "T";    //T맵 정보 저장
            GameObject Repetition = (GameObject)Instantiate(T_Block, new Vector3(mazeX, maxeY, mazeZ), Quaternion.identity); //T맵 중복성 검사 블록 생성
            GameSystem.GetComponent<DoorManager>().T_DoorSetting(); //엘리베이터, 괴생명체 등장 문 정보 설정
        }
        else if (mapIndex == 1)
        {
            GameObject maze = (GameObject)Instantiate(E_Map, new Vector3(0f, 0f, 0f), Quaternion.identity); //E맵 생성
            GameManager.instance.mazeType = "E";    //E맵 정보 저장
            GameObject Repetition = (GameObject)Instantiate(E_Block, new Vector3(mazeX, maxeY, mazeZ), Quaternion.identity); //E맵 중복성 검사 블록 생성
            GameSystem.GetComponent<DoorManager>().E_DoorSetting(); //엘리베이터, 괴생명체 등장 문 정보 설정
        }
        else
        {
            GameObject maze = (GameObject)Instantiate(S_Map, new Vector3(0f, 0f, 0f), Quaternion.identity); //S맵 생성
            GameManager.instance.mazeType = "S";    //S맵 정보 저장
            GameObject Repetition = (GameObject)Instantiate(S_Block, new Vector3(mazeX, maxeY, mazeZ), Quaternion.identity); //S맵 중복성 검사 블록 생성
            GameSystem.GetComponent<DoorManager>().S_DoorSetting(); //엘리베이터, 괴생명체 등장 문 정보 설정
        }


        //플레이어 생성
        //X와 Z의 인덱스 랜덤으로 설정
        int XIndex = Random.Range(0, 2);
        int ZIndex = Random.Range(0, 2);

        //해당 인덱스 번째의 좌표의 위치에 Player 생성
        //GameObject player = (GameObject)Instantiate(Player, new Vector3(XPosition[XIndex],YPosition, ZPosition[ZIndex]), Quaternion.identity);

        //시작 위치에 따라 시작 시야 방향 설정
        //X좌표가 양수이면 180도 회전 / X좌표가 음수이면 회전하지 않은 채로 생성
        if (XZPosition[ZIndex] > 0)
        {
            GameObject player = (GameObject)Instantiate(Player, new Vector3(XZPosition[XIndex], YPosition, XZPosition[ZIndex]), Quaternion.Euler(0f, 180f, 0f));    //플레이어 회전해서 생성
        }
        else
        {
            GameObject player = (GameObject)Instantiate(Player, new Vector3(XZPosition[XIndex], YPosition, XZPosition[ZIndex]), Quaternion.identity);   //플레이어 기본 생성
        }

        //플레이어와 가장 가까운 엘리베이터 설정
        GameSystem.GetComponent<DoorManager>().SetPlayerElevator(new Vector3(XZPosition[XIndex], YPosition, XZPosition[ZIndex]));
        //엘리베이터 버튼 생성
        GameSystem.GetComponent<DoorManager>().CreateElevatorButton();
    }
}
