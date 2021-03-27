using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    //슬라이더 오브젝트를 가져올 변수
    public Slider BGMusic;
    public Slider EFMusic;

    //배경음악과 효과음 오디오를 가져올 변수
    public AudioSource BGAudio;
    public AudioSource EFAudio;

    //오디오의 크기
    private float BGvol = 1f;
    private float EFvol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs에 저장된 값을 가져옴(맨 뒤에 1f를 적은 이유: 값이 비었다면 1을 가져와라)
        BGvol = PlayerPrefs.GetFloat("BGvol", 1f);
        EFvol = PlayerPrefs.GetFloat("EFvol", 1f);

        //저장된 값을 슬라이더에 반영함
        BGMusic.value = BGvol;
        EFMusic.value = EFvol;

        //슬라이더의 값을 오디오에 반영함
        BGAudio.volume = BGMusic.value;
        EFAudio.volume = EFMusic.value;
    }

    // Update is called once per frame
    void Update()
    {
        //배경음악 조절
        SoundSlider(BGMusic, BGAudio, BGvol, "BGvol");
        //효과음 조절
        SoundSlider(EFMusic, EFAudio, EFvol, "EFvol");
    }

    public void SoundSlider(Slider soundSlider, AudioSource audio, float volValue, string volName)
    {
        //슬라이더의 값을 가져와서 오디오의 볼륨으로 설정함
        audio.volume = soundSlider.value;

        //값을 유지하기 위해 float형 변수에 넣은 후 PlayerPrefs()를 이용하여 저장함
        volValue = soundSlider.value;
        PlayerPrefs.SetFloat(volName, volValue);
    }
}
