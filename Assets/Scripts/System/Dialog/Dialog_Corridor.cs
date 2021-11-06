using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Dialog_Corridor : MonoBehaviour
{
    //출력할 메시지
    public Text messageText;
    public Image backGround;

    public Button whiteRoomSkipButton;  //대화창 스킵 버튼

    public Image fog;   //fog 이미지(비활성화 할 것)

    //하얀방 메시지
    private string theWhiteRoomDialogText = "탈출을 축하합니다. 그러나 아직 끝은 아닙니다. \n" +
            "당신이 원하는 해답은 여기서 찾을 수 있을 것입니다.";

    //탈출 후 합격 메시지
    private string passDialogText = "합격을 축하합니다. \n" + "출구로 나가시면 부대로 향하는 헬기가 당신을 기다리고 있을 것입니다. \n" +
        "괴물로부터 우리 인간의 안위를 지켜주시기 바랍니다.";



    public void EnterWhiteRoomMessage()
    {
        fog.gameObject.SetActive(false);    //안개 비활성화

        //하얀방 입장 메시지
        messageText.text = ""; //텍스트 초기화
        messageText.gameObject.SetActive(true);  //텍스트 활성화
        backGround.gameObject.SetActive(true); //텍스트 배경 활성화

        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성
        //seq.Append(backGround.DOFade(1f, 1f));  //텍스트 배경 페이드 효과
        seq.Append(messageText.DOText(theWhiteRoomDialogText, 5f));    //텍스트 출력
        //whiteRoomSkipButton.onClick.AddListener(delegate { this.GetComponent<writtenOath>().OpenReportCard(); });  //스킵 버튼 이벤트 추가(서약서 없이 바로 성적표 나옴)
    }

    //하얀방 입장 메시지를 비활성화 하는 함수
    public void CloseWhiteDialog()
    {
        backGround.gameObject.SetActive(false); //텍스트 배경 비활성화
        messageText.gameObject.SetActive(false);    //텍스트 비활성화
    }

    //탈출 후 합격 메시지 출력
    public void CreatePassMessage()
    {
        messageText.text = ""; //텍스트 초기화
        messageText.gameObject.SetActive(true);  //텍스트 활성화
        backGround.gameObject.SetActive(true); //텍스트 배경 활성화
        whiteRoomSkipButton.gameObject.SetActive(false);  //스킵 버튼 비활성화

        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성(Sequence: Tween들을 시간과 순서에 맞춰 배열하여 하나의 장면 구성)

        seq.Append(messageText.DOText(passDialogText, 5f));    //대사 타이핑 효과
        seq.AppendInterval(2f); //2초 딜레이
        seq.Append(messageText.DOText("", 1f));    //대사 초기화


        seq.OnComplete(() => {
            messageText.gameObject.SetActive(false);  //텍스트 비활성화
            backGround.gameObject.SetActive(false); //텍스트 배경 비활성화
        });
    }
}
