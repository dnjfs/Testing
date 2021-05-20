using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFAudioPlay : MonoBehaviour
{
    //이펙트 오디오를 가져올 변수
    public AudioSource EFSound;

    //오디오의 크기값
    private float EFvol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs에 저장된 슬라이더의 값을 가져옴(맨 뒤에 1f를 적은 이유: 값이 비었다면 1을 가져와라)
        EFvol = PlayerPrefs.GetFloat("EFvol", 1f);

        //슬라이더의 값을 이펙트 오디오에 반영함
        EFSound.volume = EFvol;
    }
}
