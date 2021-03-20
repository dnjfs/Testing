using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public Player player;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.localScale = new Vector3(player.stamina, 1, 1); //남은 스태미나 표시
    }
}
