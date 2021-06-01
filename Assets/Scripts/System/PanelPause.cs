using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPause : MonoBehaviour
{
    //�Ͻ����� �г�
    public GameObject pausePanel;

    //�Ͻ������� �����
    private AudioSource BGAudio;
    private AudioSource EFAudio;

    void Start()
    {
        //������ǰ� ȿ������ ����� �ҽ� ������Ʈ�� ������
        BGAudio = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        EFAudio = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

    public void GamePause()
    {
        //���� ���� �Ͻ����� ��ư�� �����ٸ�
        //�Ͻ����� â Ȱ��ȭ
        pausePanel.SetActive(true);
        //�ð� ����
        Time.timeScale = 0f;
        //�������, ȿ���� �Ͻ�����
        BGAudio.Pause();
        EFAudio.Pause();
    }

    public void GameResume()
    {
        //���� ���� �簳 ��ư�� �����ٸ�
        //�Ͻ����� â ��Ȱ��ȭ
        pausePanel.SetActive(false);
        //�ð� �帧
        Time.timeScale = 1f;
        //�������, ȿ���� �ٽ� ���
        BGAudio.UnPause();
        EFAudio.UnPause();
    }
}
