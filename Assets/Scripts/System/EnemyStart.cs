using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    public GameObject Enemy;

    //Enemy 생성 함수(이 함수 호출시 Enemy 생성됨
    public void CreateEnemy()
    {
        //생성 좌표 수정 필요(플레이어 근처 문 좌표로)
        //지금은 임시로 랜덤 좌표 지정해서 생성

        
        int ranInt = Random.Range(0, 3);    //랜덤 int
        if (ranInt == 0)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(0f, 16f, 37f), Quaternion.identity);
        }
        else if (ranInt == 1)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(14f, 16f, -65f), Quaternion.identity);
        }
        else if(ranInt == 2)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(50f, 16f, -17f), Quaternion.identity);
        }
        
    }
}
