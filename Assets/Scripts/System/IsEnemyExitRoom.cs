using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsEnemyExitRoom : MonoBehaviour
{
    //생성 블록 번호(몇 번째 Enemy인지)
    public int EnemyBlockNumber;
    
    void Start()
    {
        EnemyBlockNumber = GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().createEnemy; //Enemy 번호 지정
    }
    
    //충돌에서 벗어날 때
    void OnTriggerExit (Collider other)
    {
        //충돌한 오브젝트가 Enemy라면
        if (other.tag == "Monster")
        {
            //Enemy 번호가 Enemy 생성 블록 번호와 일치하다면
            if (other.GetComponent<Enemy>().EnemyNumber == EnemyBlockNumber)
            {
                //other.gameObject.GetComponent<NavMeshAgent>().enabled = true; //NavMesh 활성화
                other.GetComponent<Enemy>().agent.speed = 0;

                GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().ExitEnemyDoor();  //문을 닫는다
            }
        }
    }
}
