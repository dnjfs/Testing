using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMazeType : MonoBehaviour
{
    public Text MazeType;

    void Start()
    {
        // ���� �̷��� ������ ������
        MazeType = this.gameObject.GetComponent<Text>();
        MazeType.text = GameManager.GetGameManager().mazeType;
    }
}
