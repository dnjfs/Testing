using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    void Start()
    {
        //Gameover ���� �����ϸ� �����ͺ��̽��� ���� ���
        GameObject.Find("GameSystem").GetComponent<RankSystem>().DataWrite(PlayerPrefs.GetString("Name"), (int)GameManager.instance.average, (int)GameManager.instance.timeScore);

        //������ �ֱ� �÷����� ������ �ð��� ���������
    }
}
