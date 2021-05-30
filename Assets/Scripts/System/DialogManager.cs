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

    //게임 시작시 메시지
    private string[] startDialogText = {"미로의 모든 길을 지나는 즉시 출구가 열릴 것입니다.",
                                    "괴물들을 피해 열린 출구로 탈출하십시오.",
                                    "그럼 행운을 빕니다."};

    //모든 길을 다 돌았을 때 시스템 메시지
    private string[] createElevatorText = { "출구가 열렸습니다." };


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
