using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSetting : MonoBehaviour
{
    //Ÿ��Ʋ ȭ�鿡�� ���� �г��� ���� ����

    //�г� ������Ʈ�� ���� ����
    //public GameObject titlePanel;
    public GameObject settingPanel;

    /*
    // Start is called before the first frame update
    void Start()
    {
        
        //�г� Ȱ��ȭ, ��Ȱ��ȭ
        settingPanel.SetActive(false);
        if (GameManager.instance.nickName == null)
        {
            //���� �г����� ���ٸ�
            //start ���� �г��� â���� ����
            nickNamePanel.SetActive(true);
            titlePanel.SetActive(false);
        }
        else
        {
            //�г����� �ִٸ�
            //Start ���� ���� ȭ������ ����
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
        //Open ��ư�� ������ ���� ���� �г� Ȱ��ȭ
        settingPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        //Close ��ư�� ������ ���� ���� �г� ��Ȱ��ȭ
        settingPanel.SetActive(false);
    }
}
