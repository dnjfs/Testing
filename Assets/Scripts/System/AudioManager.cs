using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //슬라이더 오브젝트를 가져올 변수
    public Slider BGSlider;
    public Slider EFSlider;
    
    //시작화면 배경음악 오디오를 가져올 변수
    public AudioSource StartBG;

    //오디오의 크기값
    private float BGvol = 1f;
    private float EFvol = 1f; 

    // Start is called before the first frame update
    void Start()
    {
        
        // PlayerPrefs에 저장된 값을 가져옴(맨 뒤에 1f를 적은 이유: 값이 비었다면 1을 가져와라)
        BGvol = PlayerPrefs.GetFloat("BGvol", 1f);
        EFvol = PlayerPrefs.GetFloat("EFvol", 1f);

        //저장된 값을 슬라이더에 반영함
        BGSlider.value = BGvol;
        EFSlider.value = EFvol;

        //슬라이더의 값을 시작화면 배경음악 오디오에 반영함
        StartBG.volume = BGSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        //슬라이더의 값을 가져와서 오디오의 볼륨으로 설정함
        StartBG.volume = BGSlider.value;

        //배경음악 값 조절
        BGSoundSlider();
        //효과음 값 조절
        EFSoundSlider();
    }

    public void BGSoundSlider()
    {
        //BackGround Sound 조절 함수
        
        //값을 유지하기 위해 float형 변수에 넣은 후 PlayerPrefs()를 이용하여 저장함
        BGvol = BGSlider.value;
        PlayerPrefs.SetFloat("BGvol", BGvol);
    }

    public void EFSoundSlider()
    {
        //Effect Sound 조절 함수

        //값을 유지하기 위해 float형 변수에 넣은 후 PlayerPrefs()를 이용하여 저장함
        EFvol = EFSlider.value;
        PlayerPrefs.SetFloat("EFvol", EFvol);
    }
}
