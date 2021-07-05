using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ending : MonoBehaviour
{
    //합격이면 탈출 애니메이션, 불합격이면 게임오버 씬으로 이동하는 스크립트

    public GameObject joyStick; //플레이어 조이스틱

    GameObject player;
    public GameObject endingLeftDoor;   //탈출구 왼쪽 문
    public GameObject endingRightDoor;   //탈출구 오른쪽 문

    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
    }

    public void FailOrPass()    //연구원 대화창 출력 후)
    {
        //배경 음악 변경
        player.transform.GetChild(0).gameObject.GetComponent<BGAudioPlay>().PlayEndingBG();

        //GameManager의 합격 여부를 가져옴
        if (GameManager.instance.isPass)    //합격이라면
        {
            IsPass();   //합격 기능 수행
        }
        else
        {
            IsFail();   //불합격 기능 수행
        }
    }


    //합격이라면 대화창이 나오고 출구가 열리고 건물 밖으로 나가게 되는 엔딩
    public void IsPass()
    {
        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성
        seq.OnStart(() => {
            this.GetComponent<DialogManager>().CreatePassMessage(); //합격 메시지 출력
            //화면 터치 기능 막기

            //출구가 열림
            endingLeftDoor.transform.DOLocalMoveX(5f, 3f).SetRelative();  //3초간 X 방향으로 5만큼 이동
            endingRightDoor.transform.DOLocalMoveX(-6.5f, 3f).SetRelative();  //3초간 X 방향으로 -5만큼 이동
        });

        //밖으로 이동(플레이어 조작 막음 -> 조작 이미지 비활성화)
        player.transform.DOLocalMoveZ(30f, 5f).SetRelative(); //3초간 Z 방향으로 30만큼 이동

        seq.OnComplete(() => {
            //랭킹 씬으로 이동(이 전에 랭킹 저장?)
            //this.GetComponent<SceneChange>().ChangeRankingScene();    //텀 주고 이동 
        });
    }

    //불합격이라면 게임오버 씬으로 이동
    public void IsFail()
    {
        //게임오버 씬으로 이동
        this.GetComponent<SceneChange>().ChangeGameOverScene();
    }
}
