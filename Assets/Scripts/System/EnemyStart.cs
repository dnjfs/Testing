using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    public GameObject Enemy;

    //Enemy ���� �Լ�(�� �Լ� ȣ��� Enemy ������
    public void CreateEnemy()
    {
        //���� ��ǥ ���� �ʿ�(�÷��̾� ��ó �� ��ǥ��)
        //������ �ӽ÷� ���� ��ǥ �����ؼ� ����

        
        int ranInt = Random.Range(0, 3);    //���� int
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
