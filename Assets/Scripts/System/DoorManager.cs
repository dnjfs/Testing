using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween을 사용하기 위해 임포트

public class DoorManager : MonoBehaviour
{
    //문 열림 스크립트(수정중)
    //만약 GameManager의 맵 타입이 T, E, S라면
    //각각의 엘리베이터 저장, 탈출구 문 저장?
    /*
    GameObject left;
    GameObject right;

    void Start()
    {
        left = GameObject.FindWithTag("Left");
        right = GameObject.FindWithTag("Right");
        OpenDoor(left, right);
    }

    public void OpenDoor(GameObject leftDoor, GameObject rightDoor)
    {
        leftDoor.transform.DOLocalMoveX(3f, 3f).SetRelative();  //3초간 X 방향으로 3만큼 이동
        rightDoor.transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3초간 X 방향으로 -3만큼 이동
    }
    */
}
