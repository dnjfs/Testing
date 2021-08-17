using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsEnemyExitRoom : MonoBehaviour
{
    //충돌에서 벗어날 때
    void OnTriggerExit (Collider other)
    {
        //충돌한 오브젝트가 Enemy라면
        if (other.tag == "Monster")
        {
            //other.gameObject.GetComponent<NavMeshAgent>().enabled = true; //NavMesh 활성화
            other.GetComponent<Enemy>().agent.speed = 0;

            GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().ExitEnemyDoor();  //문을 닫는다
        }
    }
}
