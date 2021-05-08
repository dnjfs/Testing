using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnim : MonoBehaviour
{
    Player player;
    Animator anim; //���� �ִϸ��̼�

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player.isChased) //�÷��̾ �ѱ�� ����
            anim.SetBool("Chasing", true);
        else
            anim.SetBool("Chasing", false);
    }
}
