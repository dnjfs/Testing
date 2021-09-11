using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsEnemyExitRoom : MonoBehaviour
{
    //���� ��� ��ȣ(�� ��° Enemy����)
    public int EnemyBlockNumber;
    
    void Start()
    {
        EnemyBlockNumber = GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().createEnemy; //Enemy ��ȣ ����
    }
    
    //�浹���� ��� ��
    void OnTriggerExit (Collider other)
    {
        //�浹�� ������Ʈ�� Enemy���
        if (other.tag == "Monster")
        {
            //Enemy ��ȣ�� Enemy ���� ��� ��ȣ�� ��ġ�ϴٸ�
            if (other.GetComponent<Enemy>().EnemyNumber == EnemyBlockNumber)
            {
                //other.gameObject.GetComponent<NavMeshAgent>().enabled = true; //NavMesh Ȱ��ȭ
                other.GetComponent<Enemy>().agent.speed = 0;

                GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().ExitEnemyDoor();  //���� �ݴ´�
            }
        }
    }
}
