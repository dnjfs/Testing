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
        GameManager.instance.playTime = 0f;
    }

    public void ChangeLankingScene()
    {
        //Lanking 버튼을 누르면 Lanking 씬으로 이동
        SceneManager.LoadScene("Lanking");
    }

    public void Exit()
    {
        //Exit 버튼을 누르면 게임 종료
        Application.Quit();
    }
}
