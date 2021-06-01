using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSetting : MonoBehaviour
{
    //타이틀 화면에서 설정 패널을 열고 닫음

    //패널 오브젝트를 담을 변수
    //public GameObject titlePanel;
    public GameObject settingPanel;

    /*
    // Start is called before the first frame update
    void Start()
    {
        
        //패널 활성화, 비활성화
        settingPanel.SetActive(false);
        if (GameManager.instance.nickName == null)
        {
            //만약 닉네임이 없다면
            //start 씬을 닉네임 창으로 생성
            nickNamePanel.SetActive(true);
            titlePanel.SetActive(false);
        }
        else
        {
            //닉네임이 있다면
            //Start 씬을 시작 화면으로 생성
            titlePanel.SetActive(true);
            nickNamePanel.SetActive(false);
        }
        
    }
    */
    /*
    public void OpenTitlePanel()
    {
        titlePanel.SetActive(true);
    }

    public void CloseTitlePanel()
    {
        titlePanel.SetActive(false);
    }
    */
    public void OpenSettingPanel()
    {
        //Open 버튼을 누르면 게임 설정 패널 활성화
        settingPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        //Close 버튼을 누르면 게임 설정 패널 비활성화
        settingPanel.SetActive(false);
    }
}
