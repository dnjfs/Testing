using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFAudioPlay : MonoBehaviour
{
    //����Ʈ ������� ������ ����
    public AudioSource EFSound;

    //������� ũ�Ⱚ
    private float EFvol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs�� ����� �����̴��� ���� ������(�� �ڿ� 1f�� ���� ����: ���� ����ٸ� 1�� �����Ͷ�)
        EFvol = PlayerPrefs.GetFloat("EFvol", 1f);

        //�����̴��� ���� ����Ʈ ������� �ݿ���
        EFSound.volume = EFvol;
    }
}
