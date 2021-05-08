using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnim : MonoBehaviour
{
    Animator anim; //심장 애니메이션

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
    }

    public void SetChasingParam(bool value)
    {
        anim.SetBool("Chasing", value); //파라미터 값 설정
    }

    public void SetAnimSpeed(float inSpeed)
    {
        anim.speed = inSpeed; //애니메이션의 속도 설정
    }
}
