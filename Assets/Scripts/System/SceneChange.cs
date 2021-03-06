using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SceneChange : MonoBehaviour
{
    //씬 전환 스크립트
    public Image blackImage;    //페이드 효과에 사용할 이미지(검정->투명)
    public Image clearImage;    //페이드 효과에 사용할 이미지(투명->검정)

    void Awake()
    {
        FadeEffects.FadeIn(blackImage, 1f);
    }

    public void ChangeStartScene()
    {
        //loading씬 후 start 씬으로 이동
        //(LoadManager.LoadScene이 아니라 LoadingManager 스크립트의 LoadScene)

        Time.timeScale = 1f;
        FadeEffects.FadeOutAndLoadScene(clearImage, "Start", 0.5f);
        //LoadingManager.LoadScene("Start");
    }

    public void ChangePlayingScene()
    {
        //loading씬 후 play 씬으로 이동
        //(LoadManager.LoadScene이 아니라 LoadingManager 스크립트의 LoadScene)
        GameManager.instance.ResetGameManager();    //게임 기록 초기화
        FadeEffects.FadeOutAndLoadScene(clearImage, "Playing", 0.5f);
        //LoadingManager.LoadScene("Playing");
    }

    public void ChangeRankingScene()
    {
        //loading씬 후 Ranking 씬으로 이동
        //(LoadManager.LoadScene이 아니라 LoadingManager 스크립트의 LoadScene)

        FadeEffects.FadeOutAndLoadScene(clearImage, "Ranking", 0.5f);
        //LoadingManager.LoadScene("Ranking");
    }

    public void ChangeGameOverScene()
    {
        FadeEffects.FadeOutAndLoadScene(clearImage, "GameOver", 0.5f);
    }

    public void Exit()
    {
        //Exit 버튼을 누르면 게임 종료
        Application.Quit();
    }
    
    void OnLevelWasLoade(int level)
    {
        //씬이 로드될 때마다 Fade In 효과
        FadeEffects.FadeIn(blackImage, 0.5f);
    }
    



}
