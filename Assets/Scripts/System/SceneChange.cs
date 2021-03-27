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
    //패널 활성화 여부를 나타내는 bool 변수
    public bool isActive;

    void Start()
    {
        //모바일 해상도 고정 (1920, 1080으로 조정)
        //1920 * 1080 / 1280 * 720
        //Screen.SetResolution(1920, 1080, true);
        //모바일 해상도 고정(16:9로 고정)
        //Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);

        //패널 비활성화
        settingPanel.SetActive(false);
        isActive = false;

    }

    void Update()
    {
        /*
        //만약 게임 설정 패널이 활성화되었고 게임 설정 패널이 아닌 밖을 터치하면, 게임 설정 패널 비활성화
        //슬라이드처럼 사라지게? 가능할까
        //settingPanel.SetActive(false);
        if (Input.touchCount > 0)
        {
            //터치한 위치를 가져옴
            Vector2 pos = Input.GetTouch(0).position;

        }
        */
    }

    public void ChangeStartScene()
    {
        //start 버튼을 누르면 start 씬으로 이동
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
        isActive = true;
    }

    public void PauseGame()
    {
        //게임 중 일시정지 버튼을 누르면 게임이 멈추고 일시정지 패널 활성화
        //패널 활성화
        settingPanel.SetActive(true);
        isActive = true;
        //시간 멈춤
        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {
        //일시정지 패널에서 resume을 누르면 게임 재개
        //패널 비활성화
        settingPanel.SetActive(false);
        isActive = false;
        //시간 흐름
        Time.timeScale = 1f;
    }

    public void CloseSettingPanel()
    {
        //메인화면을 누르면 게임 설정 패널 비활성화
        settingPanel.SetActive(false);
        isActive = false;
    }
}
