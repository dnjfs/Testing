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
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<NavMeshAgent>().enabled = true; //NavMesh Ȱ��ȭ

            GameObject.FindWithTag("System").GetComponent<EnemyStart>().ExitEnemyDoor();  //���� �ݴ´�
        }
    }
}
