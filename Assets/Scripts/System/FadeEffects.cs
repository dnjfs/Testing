using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeEffects : MonoBehaviour
{
    public static float fadeTime = 1f; //Fade 효과 총 재생 시간  

    public static void FadeIn(Image fadeImage)
    {
        //페이드인 함수 실행(어둡다가 밝아짐)
        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성
        seq.OnStart(() =>
        {
            fadeImage.gameObject.SetActive(true); //이미지 활성화
        });
        seq.Append(fadeImage.DOFade(0f, fadeTime));  //0f 색깔로 2f동안 변경

        seq.OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false); //이미지 비활성화
        });        
    }
    
    public static void FadeOut(Image fadeImage)
    {
        //페이드아웃 함수 실행(밝다가 어두워짐)
        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성
        seq.OnStart(() =>
        {
            fadeImage.gameObject.SetActive(true); //이미지 활성화
        });
        seq.Append(fadeImage.DOFade(1f, fadeTime));  //1f 색깔로 2f동안 변경

        seq.OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false); //이미지 비활성화
        });
    }


    //페이드 아웃 후 바로 씬 이동하는 함수(자연스러운 페이드 아웃을 위해 임시로 생성)
    public static void FadeOutAndLoadScene(Image fadeImage, string sceneName)
    {
        //페이드아웃 함수 실행(밝다가 어두워짐)
        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성
        seq.OnStart(() =>
        {
            fadeImage.gameObject.SetActive(true); //이미지 활성화
        });
        seq.Append(fadeImage.DOFade(1f, fadeTime));  //1f 색깔로 2f동안 변경

        seq.OnComplete(() =>
        {
            LoadingManager.LoadScene(sceneName);
            //blackImage.gameObject.SetActive(false); //이미지 비활성화
        });
    }

}
