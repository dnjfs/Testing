using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenTheWhiteDoor : MonoBehaviour
{
    public GameObject whiteLeftDoor;    //하얀 방의 왼쪽 문
    public GameObject whiteRightDoor;   //하얀 방의 오른쪽 문

    void OnTriggerEnter(Collider other)
    {
        //만약 플레이어와 부딪혔다면
        if (other.gameObject.tag == "Player")
        {
            //하얀 방의 문 열림
            whiteLeftDoor.transform.DOLocalMoveX(5f, 3f).SetRelative();  //3초간 X 방향으로 5만큼 이동
            whiteRightDoor.transform.DOLocalMoveX(-6.5f, 3f).SetRelative();  //3초간 X 방향으로 -5만큼 이동
        }
    }

    //하얀 방의 문을 닫는 함수
    public void CloseTheWhiteDoor()
    {
        whiteLeftDoor.transform.DOLocalMoveX(-5f, 3f).SetRelative();  //3초간 X 방향으로 -5만큼 이동
        whiteRightDoor.transform.DOLocalMoveX(6.5f, 3f).SetRelative();  //3초간 X 방향으로 5만큼 이동
    }
}
