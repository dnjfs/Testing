using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Vector3 direction; //이동방향
    public float velocity = 5f; //이동속도

    private bool isNear; //근처에 플레이어가 있는지
    Renderer capsuleColor; //플레이어 발견 시 색깔 변경(임시)

    void Start()
    {
        capsuleColor = gameObject.GetComponent<Renderer>();

        Invoke("Think", 3f);
    }

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(direction).eulerAngles.y, 0)); //움직이는 방향을 바라보기
        if (isNear)
            this.transform.position += new Vector3(direction.x, 0, direction.z) * (velocity*2 * Time.deltaTime); //이동
        else
            this.transform.position += new Vector3(direction.x, 0, direction.z) * (velocity * Time.deltaTime);
    }

    void Think()
    {
        direction.x = Random.Range(-1.0f, 1.0f);
        direction.z = Random.Range(-1.0f, 1.0f);
        direction = direction.normalized; //normalized하여 이동속도를 일정하게 만듦

        Invoke("Think", Random.Range(5f, 10f)); //5~10초 마다 반복
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            target = coll.transform;
            isNear = true;
            capsuleColor.material.color = Color.red;

            coll.GetComponent<Player>().NearEnemyNum++;
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

            coll.GetComponent<Player>().NearEnemyNum--;
        }
    }

    void OnCollisionEnter(Collision coll) //충돌
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Die"); //사망
            SceneManager.LoadScene("GameOver");
        }
    }
}
