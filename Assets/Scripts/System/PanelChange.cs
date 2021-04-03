using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChange : MonoBehaviour
{
    //�г��� ���� �ݴ� �Լ�
    //Ÿ��Ʋ ȭ�鿡�� ���� �гΰ� �г��� �Է� �г���, �÷��� ȭ�鿡�� �Ͻ����� �г��� ���� ����

    //�г� ������Ʈ�� ���� ����
    public GameObject nickNamePanel;
    public GameObject titlePanel;
    public GameObject settingPanel;

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
        //Open ��ư�� ������ ���� ���� �г� Ȱ��ȭ
        settingPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        //Close ��ư�� ������ ���� ���� �г� ��Ȱ��ȭ
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
