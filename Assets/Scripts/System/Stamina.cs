using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public Player player;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.localScale = new Vector3(player.stamina, 1, 1); //���� ���¹̳� ǥ��
    }
}
