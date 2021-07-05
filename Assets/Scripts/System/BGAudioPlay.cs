using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudioPlay : MonoBehaviour
{
    //������� ������� ������ ����
    public AudioSource BGSound;
    public AudioClip[] BGSounds;    //��� ���ǿ� ����� ����� Ŭ����

    //������� ũ�Ⱚ
    private float BGvol = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs�� ����� �����̴��� ���� ������(�� �ڿ� 1f�� ���� ����: ���� ����ٸ� 1�� �����Ͷ�)
        BGvol = PlayerPrefs.GetFloat("BGvol", 1f);

        //�����̴��� ���� ����ȭ�� ������� ������� �ݿ���
        BGSound.volume = BGvol;

        PlayMazeBG();
    }

    //�̷� ����������� ����ϴ� �Լ�
    public void PlayMazeBG()
    {
        BGSound.Stop();
        BGSound.clip = BGSounds[0];    //�̷� �������� ����� Ŭ�� ����
        BGSound.Play(); //����� ���
    }

    //�Ͼ�� ����������� ����ϴ� �Լ�
    public void PlayWhiteRoomBG()
    {
        BGSound.Stop();
        BGSound.clip = BGSounds[1];    //�Ͼ�� �������� ����� Ŭ�� ����
        BGSound.Play(); //����� ���
    }

    //���� ����������� ����ϴ� �Լ�
    public void PlayEndingBG()
    {
        BGSound.Stop();
        BGSound.clip = BGSounds[2];    //���� �������� ����� Ŭ�� ����
        BGSound.Play(); //����� ���
    }
}
