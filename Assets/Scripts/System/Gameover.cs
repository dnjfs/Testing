using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    Text record;
    void Start()
    {
        //Gameover ���� �����ϸ� �����ͺ��̽��� ���� ���
        GameObject.Find("GameSystem").GetComponent<RankSystem>().DataWrite(PlayerPrefs.GetString("Name"), (int)GameManager.instance.average, (int)GameManager.instance.timeScore);

        //������ �ֱ� �÷����� ������ �ð��� ���������
        record = GameObject.Find("Record").GetComponent<Text>();
        int time = int.Parse(PlayerPrefs.GetInt("Time").ToString());
        record.text = "Score: " + PlayerPrefs.GetInt("Score") + "  Time: " + (time / 60).ToString("00") + ":" + (time % 60).ToString("00");
    }
}
