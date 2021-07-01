using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_TheWhiteRoom : MonoBehaviour
{
    int dialogNum;  //탈출 대화창 출력 횟수

    //하얀방 메시지
    private string[] theWhiteRoomDialogText = { "탈출을 축하합니다. 그러나 아직 끝은 아닙니다. " +
            "당신이 원하는 해답은 여기서 찾을 수 있을 것입니다."};

    void Start()
    {
        dialogNum = 0;  //초기화
    }

    void OnTriggerEnter(Collider other)
    {
        //만약 플레이어와 부딪혔다면
        if (other.gameObject.tag == "Player")
        {
            if (dialogNum == 0) //대화창이 한 번도 출력되지 않았다면
            {
                //하얀 방의 문을 닫음
                GameObject.FindWithTag("WhiteDoorOpenBlock").GetComponent<OpenTheWhiteDoor>().CloseTheWhiteDoor();

                //연구원 대화창 출력
                GameObject.FindWithTag("GameSystem").GetComponent<DialogManager>().EnterWhiteRoom();
            }
        }
    }
}
