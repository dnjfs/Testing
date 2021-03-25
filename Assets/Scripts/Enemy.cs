using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Vector3 direction; //이동방향
    public float velocity = 8f; //이동속도

    private bool isNear; //근처에 플레이어가 있는지
    Renderer capsuleColor; //플레이어 발견 시 색깔 변경(임시)

    void Start()
    {
        capsuleColor = gameObject.GetComponent<Renderer>();
        target = GameObject.Find("Player").transform;

        velocity *= Time.deltaTime;
    }

    void Update()
    {
        if (isNear)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(direction).eulerAngles.y, 0)); //움직이는 방향을 바라보기
            this.transform.position += new Vector3(direction.x, 0, direction.z) * velocity; //이동
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            isNear = true;
            capsuleColor.material.color = Color.red;
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            direction = (target.position - transform.position).normalized;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            isNear = false;
            capsuleColor.material.color = Color.white;
        }
    }

    void OnCollisionEnter(Collision coll) //충돌
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Die"); //사망
        }
    }
}
