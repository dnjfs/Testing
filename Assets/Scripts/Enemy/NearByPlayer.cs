using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearByPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Player>().NearEnemyNum++;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Player>().NearEnemyNum--;
        }
    }
}
