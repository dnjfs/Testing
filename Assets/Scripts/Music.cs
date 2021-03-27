using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    //�����̴� ������Ʈ�� ������ ����
    public Slider BGMusic;
    public Slider EFMusic;

    //������ǰ� ȿ���� ������� ������ ����
    public AudioSource BGAudio;
    public AudioSource EFAudio;

    //������� ũ��
    private float BGvol = 1f;
    private float EFvol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs�� ����� ���� ������(�� �ڿ� 1f�� ���� ����: ���� ����ٸ� 1�� �����Ͷ�)
        BGvol = PlayerPrefs.GetFloat("BGvol", 1f);
        EFvol = PlayerPrefs.GetFloat("EFvol", 1f);

        //����� ���� �����̴��� �ݿ���
        BGMusic.value = BGvol;
        EFMusic.value = EFvol;

        //�����̴��� ���� ������� �ݿ���
        BGAudio.volume = BGMusic.value;
        EFAudio.volume = EFMusic.value;
    }

    // Update is called once per frame
    void Update()
    {
        //������� ����
        SoundSlider(BGMusic, BGAudio, BGvol, "BGvol");
        //ȿ���� ����
        SoundSlider(EFMusic, EFAudio, EFvol, "EFvol");
    }

    public void SoundSlider(Slider soundSlider, AudioSource audio, float volValue, string volName)
    {
        //�����̴��� ���� �����ͼ� ������� �������� ������
        audio.volume = soundSlider.value;

        //���� �����ϱ� ���� float�� ������ ���� �� PlayerPrefs()�� �̿��Ͽ� ������
        volValue = soundSlider.value;
        PlayerPrefs.SetFloat(volName, volValue);
    }
}
