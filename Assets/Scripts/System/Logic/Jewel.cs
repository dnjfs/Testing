using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    //������ ������ ������ ������� �ϴ� Ŭ����
    //void OnParticleCollision(GameObject other)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")   //���� �÷��̾�� ��Ҵٸ�
        {
            //�� ��ƼŬ(�ڱ��ڽ�) ��Ȱ��ȭ
            this.gameObject.SetActive(false);
        }

    }
}
