using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    //스크롤뷰에서 레벨을 선택하는 함수 - 수정중
    //코드 수정, 주석 추가해야함
    //슬라이드 뿐만 아니라 버튼 클릭으로 이동도 가능하게? 아니면 슬라이드만이나 버튼으로만 이동하게 구현??

    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
    }

    /*
    public string currentLevel; //현재 난이도

    void Start()
    {
        //GameManager 싱글톤의 gameLevel 가져옴(현재 레벨)

    }

    public void LeftButton()
    {
        //현재 레벨 텍스트를 가져옴

        //만약 현재 레벨이 hard나 nomal이라면 옆으로 이동
        //else 이동 불가
        //현재 레벨 텍스트 가져와서 GameManager 싱글톤의 gameLevel에 저장(string)
    }

    public void RightButton()
    {
        //현재 레벨 텍스트를 가져옴
        //만약 현재 레벨이 easy나 nomal이라면 옆으로 이동
        //else 이동 불가
        //현재 레벨 텍스트 가져와서 GameManager 싱글톤의 gameLevel에 저장(string)
    }*/

}
