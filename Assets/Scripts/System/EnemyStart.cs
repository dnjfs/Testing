using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        //�ش� �ε��� ��°�� ��ǥ�� ��ġ�� Enemy ����
        GameObject enemy1 = (GameObject)Instantiate(Enemy, new Vector3(0f, 16f, 37f), Quaternion.identity);
        GameObject enemy2 = (GameObject)Instantiate(Enemy, new Vector3(14f, 16f, -65f), Quaternion.identity);
        GameObject enemy3 = (GameObject)Instantiate(Enemy, new Vector3(50f, 16f, -17f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
