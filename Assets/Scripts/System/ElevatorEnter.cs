using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnter : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player") //플레이어가 엘리베이터에 입장하면
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster")) //모든 괴물이 플레이어를 추격
                obj.GetComponent<Enemy>().ChasePlayer();
        }
    }
}
