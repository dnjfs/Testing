using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤 함수
    //닉네임, 난이도, 타이머, 중복성, 성적 등 관리

    //싱글톤 패턴을 사용하기 위한 전역 변수
    public static GameManager instance;

    public string nickName; //플레이어 닉네임

    public float playTime;  //플레이 시간
    public float timeScore; //최종 플레이 시간

    public string gameLevel; //게임 난이도

    public string mazeType;   //맵 타입(T, E, S)
    public int repetitionCount;   //길 중복도(가장 높은 중복 횟수)
    public int chaseCount; //괴물의 시야에 들어온 횟수

    public int elevatorIndex;   //플레이어가 생성된 곳에서 가장 가까운 엘리베이터
    public bool isFinished;   //미로를 다 돌았는지 확인하는 변수


    //점수 변수들
    public char agility;    //민첩성 등급
    public char accuracy;   //정확성 등급
    public char predictability; //예측성 등급
    public float average;   //평균
    public bool isPass; //합격 여부

    // 게임 시작과 동시에 싱글톤 구성
    void Awake()
    {
        Application.targetFrameRate = 60; //60프레임 고정
        //싱글톤 변수 instance가 이미 있다면
        if (instance)
        {
            DestroyImmediate(gameObject);   //삭제
            return;
        }
        //유일한 인스턴스로 만든다
        instance = this;
        DontDestroyOnLoad(gameObject);  //씬이 바뀌어도 계속 유지시킴

        ResetGameManager(); //변수 초기화
    }

    void Update()
    {
        //타이머 증가
        playTime += Time.deltaTime;
    }

    public static GameManager GetGameManager()
    {
        return instance;
    }

    //GameManager 변수 값 초기화
    public void ResetGameManager()
    {
        //변수 초기화
        nickName = PlayerPrefs.GetString("Name");
        int level = PlayerPrefs.GetInt("Level", 1);   //게임 난이도
        switch(level)
        {
            case 0:
                gameLevel = "easy"; break;
            case 1:
                gameLevel = "normal"; break;
            case 2:
                gameLevel = "hard"; break;
            default:
                gameLevel = "normal"; break;
        }

        mazeType = null;
        playTime = timeScore = average = 0f;
        repetitionCount = 0;    //길 중복횟수 초기화
        chaseCount = 0;
        isFinished = isPass = false;
        agility = accuracy = predictability = 'F';  //등급 F로 초기화
    }
}
