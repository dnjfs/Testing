using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogManager : MonoBehaviour
{
    //출력할 메시지
    public Text messageText;
    public Image backGround;


    //하얀방 대화창 UI
    public Text whiteRoomMessageText;
    public Image whiteRoomBackGround;
    public Image talkerBakcGround;

    public Button whiteRoomSkipButton;  //하얀방에서의 대화창의 스킵 버튼

    //게임 시작시 메시지
    private string[] startDialogText = {"미로의 모든 길을 지나는 즉시 출구가 열릴 것입니다.",
                                    "괴물들을 피해 열린 출구로 탈출하십시오.",
                                    "그럼 행운을 빕니다."};

    //모든 길을 다 돌았을 때 시스템 메시지
    private string[] createElevatorText = { "출구가 열렸습니다." };

    //하얀방 메시지
    private string theWhiteRoomDialogText =  "탈출을 축하합니다. 그러나 아직 끝은 아닙니다. " +
            "당신이 원하는 해답은 여기서 찾을 수 있을 것입니다.";

    //탈출 후 합격 메시지
    private string passDialogText = "합격을 축하합니다. 출구로 나가시면 부대로 향하는 헬기가 당신을 기다리고 있을 것입니다. " + 
        "괴물로부터 우리 인간의 안위를 지켜주시기 바랍니다.";

    public void StartMessage()
    {
        //게임 시작 시스템 메시지
        TypingEffect(startDialogText);
    }
    
    public void CreateElevatorMessage()
    {
        //엘리베이터 생성 메시지
        TypingEffect(createElevatorText);
    }

    public void EnterWhiteRoom()
    {
        //하얀방 입장 메시지
        whiteRoomMessageText.gameObject.SetActive(true);  //텍스트 활성화
        whiteRoomBackGround.gameObject.SetActive(true); //텍스트 배경 활성화
        talkerBakcGround.gameObject.SetActive(true); //연구원 배경 활성화

        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성(Sequence: Tween들을 시간과 순서에 맞춰 배열하여 하나의 장면 구성)
        seq.Append(whiteRoomBackGround.DOFade(1f, 1f));  //텍스트 배경 페이드 효과(1f 색깔로 1f동안 변경)
        seq.Join(talkerBakcGround.DOFade(1f, 1f));  //연구원 배경 페이드 효과(1f 색깔로 1f동안 변경)

        seq.Append(whiteRoomMessageText.DOText(theWhiteRoomDialogText, 5f));    //시퀀스 끝에 DOText 트윈을 저장

        whiteRoomSkipButton.onClick.AddListener(delegate { this.GetComponent<writtenOath>().OpenReportCard(); });  //스킵 버튼 이벤트 추가(서약서 없이 바로 성적표 나옴)
    }

    //하얀방 입장 메시지를 닫는 함수
    public void CloseWhiteDialog()
    {
        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성(Sequence: Tween들을 시간과 순서에 맞춰 배열하여 하나의 장면 구성)

        seq.Append(whiteRoomBackGround.DOFade(0f, 2f)); //페이드 효과
        seq.Join(whiteRoomMessageText.DOText("", 1f));   //텍스트 초기화

        seq.OnComplete(() => {
            whiteRoomBackGround.gameObject.SetActive(false); //텍스트 배경 비활성화
            whiteRoomMessageText.gameObject.SetActive(false);    //텍스트 비활성화
            talkerBakcGround.gameObject.SetActive(false); //연구원 배경 비활성화

        });

    }

    //탈출 후 합격 메시지
    public void CreatePassMessage()
    {
        whiteRoomMessageText.text = ""; //텍스트 초기화

        whiteRoomMessageText.gameObject.SetActive(true);  //텍스트 활성화
        whiteRoomBackGround.gameObject.SetActive(true); //텍스트 배경 활성화
        talkerBakcGround.gameObject.SetActive(true); //연구원 배경 활성화
        whiteRoomBackGround.gameObject.transform.GetChild(2).gameObject.SetActive(false);  //스킵 버튼 비활성화

        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성(Sequence: Tween들을 시간과 순서에 맞춰 배열하여 하나의 장면 구성)
        seq.Append(whiteRoomBackGround.DOFade(1f, 1f));  //텍스트 배경 페이드 효과(1f 색깔로 1f동안 변경)
        seq.Join(talkerBakcGround.DOFade(1f, 1f));  //연구원 배경 페이드 효과(1f 색깔로 1f동안 변경)

        seq.Append(whiteRoomMessageText.DOText(passDialogText, 5f));    //대사 타이핑 효과
        seq.AppendInterval(2f); //2초 딜레이
        seq.Append(whiteRoomMessageText.DOText("", 1f));    //대사 초기화


        seq.OnComplete(() => {
            whiteRoomMessageText.gameObject.SetActive(false);  //텍스트 비활성화
            whiteRoomBackGround.gameObject.SetActive(false); //텍스트 배경 비활성화
            talkerBakcGround.gameObject.SetActive(false); //연구원 배경 비활성화
        });
    }

    void TypingEffect(string[] textArray)
    {
        //타이핑 효과

        backGround.gameObject.SetActive(true);  //텍스트 배경 활성화
        messageText.gameObject.SetActive(true); //텍스트 활성화

        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성(Sequence: Tween들을 시간과 순서에 맞춰 배열하여 하나의 장면 구성)
        seq.Append(backGround.DOFade(1f, 2f));  //텍스트 배경 페이드 효과(1f 색깔로 2f동안 변경)

        float typingTime = 5f;
        for (int i = 0; i < textArray.Length; i++)
        {
            string currentStr = textArray[i];    //시작 메시지의 출력할 한 문장 저장

            seq.Append(messageText.DOText(currentStr, typingTime));    //시퀀스 끝에 DOText 트윈을 저장
            seq.Append(messageText.DOText("", 0.15f).SetDelay(2f));    //시퀀스 끝에 DOText 트윈을 저장
            typingTime--;
        }

        seq.Append(backGround.DOFade(0f, 2f));  //텍스트 배경 페이드아웃 효과(0f 색깔로 2f동안 변경)
        seq.OnComplete(() => {
            backGround.gameObject.SetActive(false); //텍스트 배경 비활성화
            messageText.gameObject.SetActive(false);    //텍스트 비활성화

        });
    }

}
