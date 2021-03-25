using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    //Player Prefab을 받을 public 변수
    public GameObject Player;  

    //플레이어가 생성될 좌표
    float[] XPosition = {50f, -50f};
    float YPosition = 12.92469f;
    float[] ZPosition = {38f, -65f};

    // Start()가 실행되기 전 실행
    void Awake()
    {
        //X와 Z의 인덱스 랜덤으로 설정
        int XIndex = Random.Range(0, 2);
        int ZIndex = Random.Range(0, 2);

        //해당 인덱스 번째의 좌표의 위치에 Player 생성
        //GameObject player = (GameObject)Instantiate(Player, new Vector3(XPosition[XIndex],YPosition, ZPosition[ZIndex]), Quaternion.identity);

        //시작 위치에 따라 시작 시야 방향 설정
        //X좌표가 양수이면 -90도 회전 / X좌표가 음수이면 90도 회전
        if (XPosition[XIndex] > 0)
        {
            GameObject player = (GameObject)Instantiate(Player, new Vector3(XPosition[XIndex], YPosition, ZPosition[ZIndex]), Quaternion.Euler(0f, -90f, 0f));
        }
        else
        {
            GameObject player = (GameObject)Instantiate(Player, new Vector3(XPosition[XIndex], YPosition, ZPosition[ZIndex]), Quaternion.Euler(0f, 90f, 0f));
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
