using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    void Start()
    {
        //모바일 해상도 고정 (1920, 1080으로 조정)
        //1920 * 1080 / 1280 * 720
        //Screen.SetResolution(1920, 1080, true);
        //모바일 해상도 고정(16:9로 고정)
        //Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);

    }

    void Update()
    {
        
    }

    public void ChangeStartScene()
    {
        //start 버튼을 누르면 start 씬으로 이동
        SceneManager.LoadScene ("Playing");
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
