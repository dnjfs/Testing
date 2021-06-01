using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    
    public static string changeScene;   //전환할 씬
    public Slider ProgressBar;  //진행 바
    public Image BlackImage;    //페이드 효과를 위한 검정 이미지
    public Image ClearImage;    //페이드 효과를 위한 투명 이미지

    private void Start()
    {
        FadeEffects.FadeIn(BlackImage, 0.5f); //페이드인 화면
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        changeScene = sceneName;    //전환할 씬 저장
        SceneManager.LoadScene("Loading");  //로딩 씬으로 이동
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //LoadSceneAsync(): 비동기 방식으로 씬을 불러옴(불러오는 도중 다른 작업 가능)
        AsyncOperation op = SceneManager.LoadSceneAsync(changeScene);   
        op.allowSceneActivation = false;

        float timer = 0.0f; //시간 초기화
        ProgressBar.value = 0.0f;   //진행도 슬라이더 초기화

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                //준비가 다 되지 않았다면
                //Mathf.Lerp(float 시작점, float 종료점, float 거리비율): 시작점부터 종료점까지의 거리 비율 반환
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, op.progress, timer);
                if (ProgressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, 1.0f, timer);
                
                if (ProgressBar.value == 1.0f)
                {
                    //준비가 다 되었다면
                    op.allowSceneActivation = true;

                    //FadeEffects.FadeOut(ClearImage, 0.5f); //페이드아웃 화면
                    yield break;
                }
            }
        }
    }
}
