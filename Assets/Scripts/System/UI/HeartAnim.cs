using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnim : MonoBehaviour
{
    Player player;
    Animator anim; //심장 애니메이션

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player.isChased) //플레이어가 쫓기는 상태
            anim.SetBool("Chasing", true);
        else
            anim.SetBool("Chasing", false);
    }
}
