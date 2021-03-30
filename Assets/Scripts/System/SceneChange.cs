using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneChange : MonoBehaviour
{
    //패널 오브젝트를 담을 변수(게임 설정 패널, 일시정지 패널)
    public GameObject settingPanel;

    void Start()
    {
        //패널 비활성화
        settingPanel.SetActive(false);
    }

    public void ChangeStartScene()
    {
        //start 버튼을 누르면 start 씬으로 이동
        Time.timescale = 1f;
        SceneManager.LoadScene("Start");
    }

    public void ChangePlayingScene()
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

    public void ChangeSettingPanel()
    {
        //Setting 버튼을 누르면 게임 설정 패널 활성화
        settingPanel.SetActive(true);
    }

    public void PauseGame()
    {
        //게임 중 일시정지 버튼을 누르면 게임이 멈추고 일시정지 패널 활성화
        settingPanel.SetActive(true);
        //시간 멈춤
        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {
        //일시정지 패널에서 resume을 누르면 게임 재개
        settingPanel.SetActive(false);
        //시간 흐름
        Time.timeScale = 1f;
    }

    public void CloseSettingPanel()
    {
        //메인화면을 누르면 게임 설정 패널 비활성화
        settingPanel.SetActive(false);
    }
}
