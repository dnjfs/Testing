using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMazeType : MonoBehaviour
{
    public Text MazeType;

    void Start()
    {
        // 현재 미로의 종류를 보여줌
        MazeType = this.gameObject.GetComponent<Text>();
        MazeType.text = GameManager.GetGameManager().mazeType;
    }
}
