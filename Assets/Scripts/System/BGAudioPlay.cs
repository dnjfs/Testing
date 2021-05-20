using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudioPlay : MonoBehaviour
{
    //������� ������� ������ ����
    public AudioSource BGSound;

    //������� ũ�Ⱚ
    private float BGvol = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs�� ����� �����̴��� ���� ������(�� �ڿ� 1f�� ���� ����: ���� ����ٸ� 1�� �����Ͷ�)
        BGvol = PlayerPrefs.GetFloat("BGvol", 1f);

        //�����̴��� ���� ����ȭ�� ������� ������� �ݿ���
        BGSound.volume = BGvol;
    }
}
