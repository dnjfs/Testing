using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  //Max()를 사용하기 위해 명시

public class Repetition : MonoBehaviour
{
    //public GameObject[] Block = new GameObject[78]; //중복 검사할 블록 배열
    public int[] Count = new int[78];   //블록별 중복 횟수를 저장할 배열

    public int MaxValue;

    void Start()
    {
        /*
        //카운트할 블록 가져옴
        for (int i = 0; i < 78; i++)
        {
            Block[i] = GameObject.Find(("Cube (" + i + ")").ToString()); 
        }*/

        MaxValue = 0; //최댓값 초기화
    }

    void Update()
    {
        MaxValue = Count.Max(); //최댓값 반환
    }

    public void addCount(int index)
    {
        //인덱스번째의 카운트 배열 값을 증가시키는 함수
        //블록이 충돌처리 되면 자신의 인덱스로 이 함수 호출
        Count[index]++;
    }

    public int MaxCount()
    {
        //가장 많은 중복 카운트값을 구하는 함수
        //배열 중 가장 높은 값을 구하여 반환
        return MaxValue;

        //실시간으로 GameManager 싱글톤 값 변경하는게 나을까? 아니면 죽거나 게임 끝났을 때 값 변경하는게 나을까?
    }
}
