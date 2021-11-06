using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_TheWhiteRoom : MonoBehaviour
{
    public GameObject joyStick;

    void OnTriggerEnter(Collider other)
    {
        //만약 플레이어와 부딪혔다면
        if (other.gameObject.tag == "Player")
        {
            //조이스틱 없앰
            joyStick.gameObject.SetActive(false);   //플레이어 조작 비활성화
            GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //플레이어 멈춤
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(-106f, 7.46f, 87f);    //플레이어 위치 옮김
            GameObject.FindWithTag("Player").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f); //플레이어 회전 초기화

            //하얀 방의 문을 닫음
            GameObject.FindWithTag("WhiteDoorOpenBlock").GetComponent<OpenTheWhiteDoor>().CloseTheWhiteDoor();

            //연구원 대화창 출력
            GameObject.FindWithTag("GameSystem").GetComponent<Dialog_Corridor>().EnterWhiteRoomMessage();
        }
    }
}
