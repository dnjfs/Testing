using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Vector3 direction; //�̵�����
    public float velocity = 5f; //�̵��ӵ�

    private bool isNear; //��ó�� �÷��̾ �ִ���
    Renderer capsuleColor; //�÷��̾� �߰� �� ���� ����(�ӽ�)

    void Start()
    {
        capsuleColor = gameObject.GetComponent<Renderer>();

        Invoke("Think", 3f);
    }

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(direction).eulerAngles.y, 0)); //�����̴� ������ �ٶ󺸱�
        if (isNear)
            this.transform.position += new Vector3(direction.x, 0, direction.z) * (velocity*2 * Time.deltaTime); //�̵�
        else
            this.transform.position += new Vector3(direction.x, 0, direction.z) * (velocity * Time.deltaTime);
    }

    void Think()
    {
        direction.x = Random.Range(-1.0f, 1.0f);
        direction.z = Random.Range(-1.0f, 1.0f);
        direction = direction.normalized; //normalized�Ͽ� �̵��ӵ��� �����ϰ� ����

        Invoke("Think", Random.Range(5f, 10f)); //5~10�� ���� �ݺ�
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

    void OnCollisionEnter(Collision coll) //�浹
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Die"); //���
            SceneManager.LoadScene("GameOver");
        }
    }
}
