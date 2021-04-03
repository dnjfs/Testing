using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingPause : MonoBehaviour
{
    //일시정지 패널
    public GameObject pausePanel;

    public void GamePause()
    {
        //만약 게임 일시정지 버튼을 눌렀다면
        //일시정지 창 활성화
        pausePanel.SetActive(true);
        //시간 멈춤
        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        //만약 게임 재개 버튼을 눌렀다면
        //일시정지 창 비활성화
        pausePanel.SetActive(false);
        //시간 흐름
        Time.timeScale = 1f;
    }
}
