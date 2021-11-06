using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRandom : MonoBehaviour
{
    //Player Prefab을 받을 public 변수
    public GameObject Player;

    //플레이어가 생성될 좌표(X, Z 좌표가 모두 54, -54 중 하나)
    float[] XZPosition = {54f, -54f};
    float YPosition = -1f;

    GameObject GameSystem;   //DoorManager, ElevatorButton 스크립트를 가지고 있는 GameSystem 오브젝트

    // Start()가 실행되기 전 실행
    void Awake()
    {

        GameSystem = GameObject.FindWithTag("GameSystem");
        //현재 맵 정보 저장(테스트할때 바로 Maze 맵으로 실행했을 경우를 위해)
        if (SceneManager.GetActiveScene().name == "Maze_T")
        {
            GameManager.instance.mazeType = "T";
        }
        else if (SceneManager.GetActiveScene().name == "Maze_E")
        {
            GameManager.instance.mazeType = "E";
        }
        else
        {
            GameManager.instance.mazeType = "S";
        }

        GameSystem.GetComponent<DoorManager>().DoorSetting(); //엘리베이터, 괴생명체 등장 문 정보 설정

        //플레이어 생성
        //X와 Z의 인덱스 랜덤으로 설정
        int XIndex = Random.Range(0, 2);
        int ZIndex = Random.Range(0, 2);

        //해당 인덱스 번째의 좌표의 위치에 Player 생성 코드

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
        GameSystem.GetComponent<DoorManager>().CreateEnterTrigger(); //엘리베이터 출입검사 트리거 생성
    }
}
