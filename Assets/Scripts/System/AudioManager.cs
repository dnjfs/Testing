using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //�����̴� ������Ʈ�� ������ ����
    public Slider BGSlider;
    public Slider EFSlider;
    
    //����ȭ�� ������� ������� ������ ����
    public AudioSource StartBG;

    //������� ũ�Ⱚ
    private float BGvol = 1f;
    private float EFvol = 1f; 

    // Start is called before the first frame update
    void Start()
    {
        
        // PlayerPrefs�� ����� ���� ������(�� �ڿ� 1f�� ���� ����: ���� ����ٸ� 1�� �����Ͷ�)
        BGvol = PlayerPrefs.GetFloat("BGvol", 1f);
        EFvol = PlayerPrefs.GetFloat("EFvol", 1f);

        //����� ���� �����̴��� �ݿ���
        BGSlider.value = BGvol;
        EFSlider.value = EFvol;

        //�����̴��� ���� ����ȭ�� ������� ������� �ݿ���
        StartBG.volume = BGSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        //�����̴��� ���� �����ͼ� ������� �������� ������
        StartBG.volume = BGSlider.value;

        //������� �� ����
        BGSoundSlider();
        //ȿ���� �� ����
        EFSoundSlider();
    }

    public void BGSoundSlider()
    {
        //BackGround Sound ���� �Լ�
        
        //���� �����ϱ� ���� float�� ������ ���� �� PlayerPrefs()�� �̿��Ͽ� ������
        BGvol = BGSlider.value;
        PlayerPrefs.SetFloat("BGvol", BGvol);
    }

    public void EFSoundSlider()
    {
        //Effect Sound ���� �Լ�

        //���� �����ϱ� ���� float�� ������ ���� �� PlayerPrefs()�� �̿��Ͽ� ������
        EFvol = EFSlider.value;
        PlayerPrefs.SetFloat("EFvol", EFvol);
    }
}
