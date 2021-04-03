using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingPause : MonoBehaviour
{
    //�Ͻ����� �г�
    public GameObject pausePanel;

    public void GamePause()
    {
        //���� ���� �Ͻ����� ��ư�� �����ٸ�
        //�Ͻ����� â Ȱ��ȭ
        pausePanel.SetActive(true);
        //�ð� ����
        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        //���� ���� �簳 ��ư�� �����ٸ�
        //�Ͻ����� â ��Ȱ��ȭ
        pausePanel.SetActive(false);
        //�ð� �帧
        Time.timeScale = 1f;
    }
}
