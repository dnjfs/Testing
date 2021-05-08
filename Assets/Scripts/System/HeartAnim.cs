using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnim : MonoBehaviour
{
    Animator anim; //���� �ִϸ��̼�

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
    }

    public void SetChasingParam(bool value)
    {
        anim.SetBool("Chasing", value); //�Ķ���� �� ����
    }

    public void SetAnimSpeed(float inSpeed)
    {
        anim.speed = inSpeed; //�ִϸ��̼��� �ӵ� ����
    }
}
