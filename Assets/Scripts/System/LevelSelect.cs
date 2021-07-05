using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    //스크롤뷰에서 레벨을 선택하는 함수

    public Text levelBtnText;   //레벨 버튼 텍스트
    public int currentLevelIndex; //현재 난이도

    void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("Level", 1);  //처음 저장된 난이도를 가져옴

        ShowLevel();    //난이도를 저장하고 화면에 보여줌
    }

    public void LeftButton()
    {
        if (currentLevelIndex != 0)     //현재 난이도가 easy가 아니라면
        {
            currentLevelIndex--;    //인덱스 감소
        }

        ShowLevel();    //난이도를 저장하고 화면에 보여줌
    }

    public void RightButton()
    {
        if (currentLevelIndex != 2)     //현재 난이도가 hard 아니라면
        {
            currentLevelIndex++;    //인덱스 증가
        }

        ShowLevel();    //난이도를 저장하고 화면에 보여줌
    }

    //현재 인덱스의 난이도를 보여주고 저장하는 함수
    public void ShowLevel()
    {
        switch(currentLevelIndex)
        {
            case 0:
                levelBtnText.text = "EASY";
                GameManager.instance.gameLevel = "easy";  //GameManager 싱글톤에 저장
                PlayerPrefs.SetInt("Level", 0);
                break;
            case 1:
                levelBtnText.text = "NORMAL";
                GameManager.instance.gameLevel = "normal";  //GameManager 싱글톤에 저장
                PlayerPrefs.SetInt("Level", 1);
                break;
            case 2:
                levelBtnText.text = "HARD";
                GameManager.instance.gameLevel = "hard";  //GameManager 싱글톤에 저장
                PlayerPrefs.SetInt("Level", 2);
                break;
            default:
                levelBtnText.text = "NORMAL";
                GameManager.instance.gameLevel = "normal";  //GameManager 싱글톤에 저장
                PlayerPrefs.SetInt("Level", 1);
                break;
        }
    }
}
