using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsEnemyExitRoom : MonoBehaviour
{
    //�浹���� ��� ��
    void OnTriggerExit (Collider other)
    {
        //�浹�� ������Ʈ�� Enemy���
        if (other.tag == "Monster")
        {
            //other.gameObject.GetComponent<NavMeshAgent>().enabled = true; //NavMesh Ȱ��ȭ
            other.GetComponent<Enemy>().agent.speed = 0;

            GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().ExitEnemyDoor();  //���� �ݴ´�
        }
    }
}
