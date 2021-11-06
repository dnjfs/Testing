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
        GameManager.instance.ResetGameManager();    //게임 기록 초기화

        //맵 생성(1/3 확률로 T, E, S맵 생성)
        int mapIndex = Random.Range(0, 3);
        if (mapIndex == 0)
        {
            GameManager.instance.mazeType = "T";    //T맵 정보 저장
            FadeEffects.FadeOutAndLoadScene(clearImage, "Maze_T", 0.5f);    //씬 이동
        }
        else if (mapIndex == 1)
        {
            GameManager.instance.mazeType = "E";    //E맵 정보 저장
            FadeEffects.FadeOutAndLoadScene(clearImage, "Maze_E", 0.5f);    //씬 이동
        }
        else
        {
            GameManager.instance.mazeType = "S";    //S맵 정보 저장
            FadeEffects.FadeOutAndLoadScene(clearImage, "Maze_S", 0.5f);    //씬 이동
        }
    }

    public void ChangeCorridorScene()
    {
        //복도 씬으로 이동
        FadeEffects.FadeOutAndLoadScene(clearImage, "Corridor", 0.5f);
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



}
