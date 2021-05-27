using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffects : MonoBehaviour
{
    //Fade In, Fade Out 효과를 내는 스크립트

    //Fade 효과 총 재생 시간  
    public float FadeTime = 2f;
    //Fade 효과 재생중인 시간
    public float Fadingtime = 0f;

    public void FadeIn(Image FadeInImg)
    {
        //페이드인 함수 실행
        StartCoroutine(coFadeIn(FadeInImg));
    }

    public void FadeOut(Image FadeOutImg)
    {
        //페이드아웃 함수 실행
        StartCoroutine(coFadeOut(FadeOutImg));
    }

    //불투명한 이미지를 투명하게 만드는 코루틴 함수 -> Fade In 효과를 내는 코루틴 함수
    IEnumerator coFadeIn(Image FadeInImg)
    {
        //Fade In 효과를 낼 이미지 활성화
        FadeInImg.gameObject.SetActive(true);

        //Fade In 중인 시간 초기화
        Fadingtime = 0f;
        
        //임시로 색깔을 저장할 변수
        Color tempColor = FadeInImg.color;

        while (tempColor.a > 0f)
        {
            Fadingtime += (Time.timeScale * Time.deltaTime) / FadeTime;

            //투명도 설정
            tempColor.a = Mathf.Lerp(1, 0, Fadingtime);

            //기본 이미지에 바뀐 색깔 저장
            FadeInImg.color = tempColor;

            yield return null;
        }

        //Fade In 효과를 낸 이미지 비활성화
        FadeInImg.gameObject.SetActive(false);

        //코루틴 종료
        yield return null;
    }

    //투명한 이미지를 불투명하게 만드는 코루틴 함수 -> Fade Out 효과를 나타내는 코루틴 함수
    IEnumerator coFadeOut(Image FadeOutImg)
    {
        //Fade Out 효과를 낼 이미지 활성화
        FadeOutImg.gameObject.SetActive(true);

        //Fade Out 중인 시간 초기화
        Fadingtime = 0f;

        //임시로 색깔을 저장할 변수
        Color tempColor = FadeOutImg.color;

        while (tempColor.a < 1f)
        {
            Fadingtime += (Time.timeScale * Time.deltaTime) / FadeTime;

            //투명도 설정
            tempColor.a = Mathf.Lerp(0, 1, Fadingtime);

            //기본 이미지에 바뀐 색깔 저장
            FadeOutImg.color = tempColor;

            yield return null;
        }

        //코루틴 종료
        yield return null;
    }
}
