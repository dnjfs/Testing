using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandom : MonoBehaviour
{
    //Player Prefab을 받을 public 변수
    public GameObject Player;
    public GameObject T_Map;
    public GameObject E_Map;
    public GameObject S_Map;

    //플레이어가 생성될 좌표(X, Z 좌표가 모두 54, -54 중 하나)
    float[] XZPosition = {54f, -54f};
    float YPosition = -1f;

    // Start()가 실행되기 전 실행
    void Awake()
    {
        //맵 생성(1/3 확률로 T, E, S맵 생성)
        int mapIndex = Random.Range(0, 3);
        if (mapIndex == 0)
        {
            GameObject maze = (GameObject)Instantiate(T_Map, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (mapIndex == 1)
        {
            GameObject maze = (GameObject)Instantiate(E_Map, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
        }
        else
        {
            GameObject maze = (GameObject)Instantiate(S_Map, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
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
            GameObject player = (GameObject)Instantiate(Player, new Vector3(XZPosition[XIndex], YPosition, XZPosition[ZIndex]), Quaternion.Euler(0f, 180f, 0f));
        }
        else
        {
            GameObject player = (GameObject)Instantiate(Player, new Vector3(XZPosition[XIndex], YPosition, XZPosition[ZIndex]), Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
