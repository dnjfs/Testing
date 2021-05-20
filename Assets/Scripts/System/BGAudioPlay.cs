using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudioPlay : MonoBehaviour
{
    //배경음악 오디오를 가져올 변수
    public AudioSource BGSound;

    //오디오의 크기값
    private float BGvol = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs에 저장된 슬라이더의 값을 가져옴(맨 뒤에 1f를 적은 이유: 값이 비었다면 1을 가져와라)
        BGvol = PlayerPrefs.GetFloat("BGvol", 1f);

        //슬라이더의 값을 시작화면 배경음악 오디오에 반영함
        BGSound.volume = BGvol;
    }
}
