using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseByPlayer : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Player>().CloseEnemyNum++;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Player>().CloseEnemyNum--;
        }
    }
}
