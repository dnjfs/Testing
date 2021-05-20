using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterCreate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //플레이어 생성되면 시스템 시작메시지 출력
        GameObject.FindWithTag("GameSystem").GetComponent<DialogManager>().StartMessage();
    }
}
