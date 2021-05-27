using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneChange : MonoBehaviour
{
    public void ChangeStartScene()
    {
        //start 버튼을 누르면 start 씬으로 이동
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }

    public void ChangePlayingScene()
    {
        //start 버튼을 누르면 play 씬으로 이동
        SceneManager.LoadScene ("Playing");
        GameManager.instance.playTime = 0f; //플레이 시간 초기화
        
    }

    public void ChangeRankingScene()
    {
        //Ranking 버튼을 누르면 Ranking 씬으로 이동
        SceneManager.LoadScene("Ranking");
    }

    public void Exit()
    {
        //Exit 버튼을 누르면 게임 종료
        Application.Quit();
    }
}
