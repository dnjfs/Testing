using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Vector3 direction; //�̵�����
    public float velocity = 8f; //�̵��ӵ�

    private bool isNear; //��ó�� �÷��̾ �ִ���
    Renderer capsuleColor; //�÷��̾� �߰� �� ���� ����(�ӽ�)

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
            this.transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(direction).eulerAngles.y, 0)); //�����̴� ������ �ٶ󺸱�
            this.transform.position += new Vector3(direction.x, 0, direction.z) * velocity; //�̵�
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

    void OnCollisionEnter(Collision coll) //�浹
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Die"); //���
        }
    }
}
