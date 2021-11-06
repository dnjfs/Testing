using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPause : MonoBehaviour
{
    //일시정지 패널
    public GameObject pausePanel;

    //일시정지할 오디오
    private AudioSource BGAudio;
    private AudioSource EFAudio;

    void Start()
    {
        //배경음악과 효과음의 오디오 소스 컴포넌트를 가져옴
        BGAudio = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        EFAudio = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

    public void GamePause()
    {
        //만약 게임 일시정지 버튼을 눌렀다면
        //일시정지 창 활성화
        pausePanel.SetActive(true);
        //시간 멈춤
        Time.timeScale = 0f;
        //배경음악, 효과음 일시정지
        BGAudio.Pause();
        EFAudio.Pause();
    }

    public void GameResume()
    {
        //만약 게임 재개 버튼을 눌렀다면
        //일시정지 창 비활성화
        pausePanel.SetActive(false);
        //시간 흐름
        Time.timeScale = 1f;
        //배경음악, 효과음 다시 재생
        BGAudio.UnPause();
        EFAudio.UnPause();
    }
}
