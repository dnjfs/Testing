using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChange : MonoBehaviour
{
    //패널을 열고 닫는 함수
    //타이틀 화면에서 설정 패널과 닉네임 입력 패널을, 플레이 화면에서 일시정지 패널을 열고 닫음

    //패널 오브젝트를 담을 변수
    public GameObject titlePanel;
    public GameObject settingPanel;
    public GameObject nickNamePanel;

    // Start is called before the first frame update
    void Awake()
    {
        //패널 활성화, 비활성화
        titlePanel.SetActive(true);
        settingPanel.SetActive(false);
        nickNamePanel.SetActive(false);
    }

    public void OpenTitlePanel()
    {
        titlePanel.SetActive(true);
    }

    public void CloseTitlePanel()
    {
        titlePanel.SetActive(false);
    }

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

    public void OpenNickNamePanel()
    {
        nickNamePanel.SetActive(true);
    }

    public void CloseNickNamePanel()
    {
        nickNamePanel.SetActive(false);
    }
}
