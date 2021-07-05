using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    //��ũ�Ѻ信�� ������ �����ϴ� �Լ�

    public Text levelBtnText;   //���� ��ư �ؽ�Ʈ
    public int currentLevelIndex; //���� ���̵�

    void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("Level", 1);  //ó�� ����� ���̵��� ������

        ShowLevel();    //���̵��� �����ϰ� ȭ�鿡 ������
    }

    public void LeftButton()
    {
        if (currentLevelIndex != 0)     //���� ���̵��� easy�� �ƴ϶��
        {
            currentLevelIndex--;    //�ε��� ����
        }

        ShowLevel();    //���̵��� �����ϰ� ȭ�鿡 ������
    }

    public void RightButton()
    {
        if (currentLevelIndex != 2)     //���� ���̵��� hard �ƴ϶��
        {
            currentLevelIndex++;    //�ε��� ����
        }

        ShowLevel();    //���̵��� �����ϰ� ȭ�鿡 ������
    }

    //���� �ε����� ���̵��� �����ְ� �����ϴ� �Լ�
    public void ShowLevel()
    {
        switch(currentLevelIndex)
        {
            case 0:
                levelBtnText.text = "EASY";
                GameManager.instance.gameLevel = "easy";  //GameManager �̱��濡 ����
                PlayerPrefs.SetInt("Level", 0);
                break;
            case 1:
                levelBtnText.text = "NORMAL";
                GameManager.instance.gameLevel = "normal";  //GameManager �̱��濡 ����
                PlayerPrefs.SetInt("Level", 1);
                break;
            case 2:
                levelBtnText.text = "HARD";
                GameManager.instance.gameLevel = "hard";  //GameManager �̱��濡 ����
                PlayerPrefs.SetInt("Level", 2);
                break;
            default:
                levelBtnText.text = "NORMAL";
                GameManager.instance.gameLevel = "normal";  //GameManager �̱��濡 ����
                PlayerPrefs.SetInt("Level", 1);
                break;
        }
    }
}
