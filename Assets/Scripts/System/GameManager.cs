using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤 함수
    //닉네임, 사운드, 난이도, 타이머, 중복성, 성적 등 관리

    //싱글톤 패턴을 사용하기 위한 전역 변수
    public static GameManager instance;

    //플레이어 닉네임
    public string nickName;


    // 게임 시작과 동시에 싱글톤 구성
    void Awake()
    {
        //싱글톤 변수 instance가 이미 있다면
        if (instance)
        {
            DestroyImmediate(gameObject);   //삭제
            return;
        }
        //유일한 인스턴스로 만든다
        instance = this;
        DontDestroyOnLoad(gameObject);  //씬이 바뀌어도 계속 유지시킴

        nickName = null;
    }
}
