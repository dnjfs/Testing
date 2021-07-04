using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnter : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player") //�÷��̾ ���������Ϳ� �����ϸ�
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster")) //��� ������ �÷��̾ �߰�
                obj.GetComponent<Enemy>().ChasePlayer();
        }
    }
}
