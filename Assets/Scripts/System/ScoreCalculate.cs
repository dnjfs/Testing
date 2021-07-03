using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculate : MonoBehaviour
{
    //점수 계산 스크립트

    //민첩성 계산 함수
    public char GetAgility()
    {
        char grade; //반환할 등급
        float playTime = GameManager.instance.timeScore;

        if (playTime <= 600.0f)     //10분 이내면 A등급
        {
            grade = 'A';
        }
        else if (playTime > 600.0f && playTime <= 1200.0f)  //11분~20분: B등급
        {
            grade = 'B';
        }
        else if (playTime > 1200.0f && playTime <= 1800.0f) //21분~30분: C등급
        {
            grade = 'C';
        }
        else if (playTime > 1800.0f && playTime <= 2400.0f) //31분~40분: D등급
        {
            grade = 'D';
        }
        else if (playTime > 2400.0f && playTime <= 3000.0f) //41분~50분: E등급
        {
            grade = 'E';
        }
        else //50분 이상: F등급
        {
            grade = 'F';
        }

        GameManager.instance.agility = grade;   //점수 저장

        return grade;
    }

    //정확성 계산 함수
    public char GetAccuracy()
    {
        char grade; //반환할 등급
        int mostRepetition = GameManager.instance.repetitionCount;

        if (mostRepetition <= 5) //1~5번 중복: A등급
        {
            grade = 'A';
        }
        else if (mostRepetition > 5 && mostRepetition <= 10)   //6~10번 중복: B등급
        {
            grade = 'B';
        }
        else if (mostRepetition > 10 && mostRepetition <= 20) //11~20번 중복: C등급
        {
            grade = 'C';
        }
        else if (mostRepetition > 20 && mostRepetition <= 30) //21~30번 중복: D등급
        {
            grade = 'D';
        }
        else if (mostRepetition > 30 && mostRepetition <= 40) //31~40번 중복: E등급
        {
            grade = 'E';
        }
        else //41번 이상 중복: F등급
        {
            grade = 'F';
        }

        GameManager.instance.accuracy = grade;   //점수 저장

        return grade;
    }

    //예측성 계산 함수
    public char GetPredictability()
    {
        char grade; //반환할 등급
        int mostRepetition = GameManager.instance.chaseCount;    //플레이어 근처에 괴생명체가 접근한 횟수

        if (mostRepetition <= 5) //1~5번 접근: A등급
        {
            grade = 'A';
        }
        else if (mostRepetition > 5 && mostRepetition <= 10)   //6~10번 접근: B등급
        {
            grade = 'B';
        }
        else if (mostRepetition > 10 && mostRepetition <= 20) //11~20번 접근: C등급
        {
            grade = 'C';
        }
        else if (mostRepetition > 20 && mostRepetition <= 25) //21~25번 접근: D등급
        {
            grade = 'D';
        }
        else if (mostRepetition > 25 && mostRepetition <= 30) //25~30번 접근: E등급
        {
            grade = 'E';
        }
        else //31번 이상 접근: F등급
        {
            grade = 'F';
        }

        GameManager.instance.predictability = grade;     //점수 저장

        return grade;
    }

    //등급을 점수로 바꾸는 함수
    public int GradeToScore(char grade)
    {
        int result;

        switch (grade)
        {
            case 'A':
                result = 100; break;
            case 'B':
                result = 80; break;
            case 'C':
                result = 60; break;
            case 'D':
                result = 40; break;
            case 'E':
                result = 20; break;
            case 'F':
                result = 0; break;
            default:
                result = 0; break;
        }

        return result;
    }

    //평균 점수 반환하는 함수
    public float GetAverage()
    {
        char AgilityGrade = GetAgility();
        char AccuracyGrade = GetAccuracy();
        char PredictabilityGrade = GetPredictability();

        int AgilityScore = GradeToScore(AgilityGrade);
        int AccuracyScore = GradeToScore(AccuracyGrade);
        int PredictabilityScore = GradeToScore(PredictabilityGrade);

        float average = ((float)(AgilityScore + AccuracyScore + PredictabilityScore)) / 3.0f;

        //점수 저장
        GameManager.instance.average = average;

        return average;
    }
}
