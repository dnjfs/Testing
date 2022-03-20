using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    //보석에 닿으면 보석이 사라지게 하는 클래스
    //void OnParticleCollision(GameObject other)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")   //만약 플레이어와 닿았다면
        {
            //이 파티클(자기자신) 비활성화
            this.gameObject.SetActive(false);
        }

    }
}
