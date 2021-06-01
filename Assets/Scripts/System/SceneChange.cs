﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SceneChange : MonoBehaviour
{

    //후에 하나의 이미지로 수정할까 고민중
    public Image blackImage;    //페이드 효과에 사용할 이미지(검정->투명)
    public Image clearImage;    //페이드 효과에 사용할 이미지(투명->검정)

    void Awake()
    {
        FadeEffects.FadeIn(blackImage);
    }

    public void ChangeStartScene()
    {
        //loading씬 후 start 씬으로 이동
        //(LoadManager.LoadScene이 아니라 LoadingManager 스크립트의 LoadScene)

        Time.timeScale = 1f;
        FadeEffects.FadeOutAndLoadScene(clearImage, "Start");
        //LoadingManager.LoadScene("Start");
    }

    public void ChangePlayingScene()
    {
        //loading씬 후 play 씬으로 이동
        //(LoadManager.LoadScene이 아니라 LoadingManager 스크립트의 LoadScene)

        FadeEffects.FadeOutAndLoadScene(clearImage, "Playing");
        //LoadingManager.LoadScene("Playing");
    }

    public void ChangeRankingScene()
    {
        //loading씬 후 Ranking 씬으로 이동
        //(LoadManager.LoadScene이 아니라 LoadingManager 스크립트의 LoadScene)

        FadeEffects.FadeOutAndLoadScene(clearImage, "Ranking");
        //LoadingManager.LoadScene("Ranking");
    }

    public void Exit()
    {
        //Exit 버튼을 누르면 게임 종료

        Application.Quit();
    }

    /*
    //페이드 효과를 위한 델리게이트 체인
    void OnEnable()
    {
        //델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //씬이 로드될 때마다 Fade In 효과
        FadeEffects.FadeIn(blackImage);
    }

    void OnDisable()
    {
        //델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    */
    
    void OnLevelWasLoade(int level)
    {
        //씬이 로드될 때마다 Fade In 효과
        FadeEffects.FadeIn(blackImage);
    }
    



}
