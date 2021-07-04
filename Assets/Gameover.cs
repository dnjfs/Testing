using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    Text record;
    void Start()
    {
        //Gameover 씬에 진입하면 데이터베이스에 점수 기록
        GameObject.Find("GameSystem").GetComponent<RankSystem>().DataWrite(PlayerPrefs.GetString("Name"), (int)GameManager.instance.average, (int)GameManager.instance.timeScore);

        //씬에서 최근 플레이의 점수와 시간도 보여줘야함
        record = GameObject.Find("Record").GetComponent<Text>();
        int time = int.Parse(PlayerPrefs.GetInt("Time").ToString());
        record.text = "Score: " + PlayerPrefs.GetInt("Score") + "  Time: " + (time / 60).ToString("00") + ":" + (time % 60).ToString("00");
    }
}
