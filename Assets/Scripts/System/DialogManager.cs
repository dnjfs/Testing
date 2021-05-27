using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    //출력할 메시지
    public Text startDialog;
    public Image BackGround;

    //페이드인 효과 연출에 쓸 검정색 이미지
    public Image BlackImage;


    //게임 시작시 메시지
    private string[] startDialogText = {"미로의 모든 길을 지나는 즉시 출구가 열릴 것입니다.",
                                    "괴물들을 피해 열린 출구로 탈출하십시오.",
                                    "그럼 행운을 빕니다."};

    //모든 길을 다 돌았을 때 시스템 메시지
    private string[] createElevatorText = { "출구가 열렸습니다." };

    
    public void StartMessage()
    {
        //게임 시작 시스템 메시지

        //Fade In 효과 실행(Player 오브젝트가 갖고 있는 FadeEffects.cs의 Fade In 함수 실행)
        GameObject.FindWithTag("GameSystem").GetComponent<FadeEffects>().FadeIn(BlackImage);

        //게임 시작 메시지 출력
        StartCoroutine(_typing(startDialogText));
    }
    
    public void CreateElevatorMessage()
    {
        //엘리베이터 생성 메시지
        StartCoroutine(_typing(createElevatorText));
    }

    IEnumerator _typing(string[] Dialog)
    {

        //첫 딜레이 설정(3초)
        yield return new WaitForSeconds(3f);

        //출력할 메시지의 배경 이미지 페이드 아웃 효과 연출(GameSystem 오브젝트의 FadeEffects 스크립트의 FadeOut 함수)
        GameObject.FindWithTag("GameSystem").GetComponent<FadeEffects>().FadeOut(BackGround);

        //시스템 메시지 오브젝트 활성화
        //BackGround.gameObject.SetActive(true);
        startDialog.gameObject.SetActive(true);

        //메시지의 개수만큼 메시지 출력
        for (int j = 0; j < Dialog.Length; j++)
        {
            //타이핑 효과는 각 text의 길이만큼 반복
            for (int i = 0; i <= Dialog[j].Length; i++)
            {
                //Substing(start index, 표현할 글자 개수) -> index부터 i까지의 글자를 표현 == 타이핑 효과
                startDialog.text = Dialog[j].Substring(0, i);

                //한 번에 표현되지 않도록 딜레이를 줌
                yield return new WaitForSeconds(0.15f);
            }

            //다음 메시지 딜레이 설정(3초)
            yield return new WaitForSeconds(3f);
        }

        //출력할 메시지의 배경 이미지 페이드 인 효과 연출(GameSystem 오브젝트의 FadeEffects 스크립트의 FadeIn 함수)
        GameObject.FindWithTag("GameSystem").GetComponent<FadeEffects>().FadeIn(BackGround);
        GameObject.FindWithTag("Player").GetComponent<Player>().SetPlayerLevel();   //난이도에 따른 플레이어 속도 설정

        //시스템 메시지 오브젝트 비활성화
        //BackGround.gameObject.SetActive(false);
        startDialog.gameObject.SetActive(false);

        yield return null;
    }
}
