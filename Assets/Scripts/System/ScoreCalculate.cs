using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculate : MonoBehaviour
{
    //���� ��� ��ũ��Ʈ

    //��ø�� ��� �Լ�
    public char GetAgility()
    {
        char grade; //��ȯ�� ���
        float playTime = GameManager.instance.timeScore;

        if (playTime <= 600.0f)     //10�� �̳��� A���
        {
            grade = 'A';
        }
        else if (playTime > 600.0f && playTime <= 1200.0f)  //11��~20��: B���
        {
            grade = 'B';
        }
        else if (playTime > 1200.0f && playTime <= 1800.0f) //21��~30��: C���
        {
            grade = 'C';
        }
        else if (playTime > 1800.0f && playTime <= 2400.0f) //31��~40��: D���
        {
            grade = 'D';
        }
        else if (playTime > 2400.0f && playTime <= 3000.0f) //41��~50��: E���
        {
            grade = 'E';
        }
        else //50�� �̻�: F���
        {
            grade = 'F';
        }

        GameManager.instance.agility = grade;   //���� ����

        return grade;
    }

    //��Ȯ�� ��� �Լ�
    public char GetAccuracy()
    {
        char grade; //��ȯ�� ���
        int mostRepetition = GameManager.instance.repetitionCount;

        if (mostRepetition <= 5) //1~5�� �ߺ�: A���
        {
            grade = 'A';
        }
        else if (mostRepetition > 5 && mostRepetition <= 10)   //6~10�� �ߺ�: B���
        {
            grade = 'B';
        }
        else if (mostRepetition > 10 && mostRepetition <= 20) //11~20�� �ߺ�: C���
        {
            grade = 'C';
        }
        else if (mostRepetition > 20 && mostRepetition <= 30) //21~30�� �ߺ�: D���
        {
            grade = 'D';
        }
        else if (mostRepetition > 30 && mostRepetition <= 40) //31~40�� �ߺ�: E���
        {
            grade = 'E';
        }
        else //41�� �̻� �ߺ�: F���
        {
            grade = 'F';
        }

        GameManager.instance.accuracy = grade;   //���� ����

        return grade;
    }

    //������ ��� �Լ�
    public char GetPredictability()
    {
        char grade; //��ȯ�� ���
        int mostRepetition = GameManager.instance.chaseCount;    //�÷��̾� ��ó�� ������ü�� ������ Ƚ��

        if (mostRepetition <= 5) //1~5�� ����: A���
        {
            grade = 'A';
        }
        else if (mostRepetition > 5 && mostRepetition <= 10)   //6~10�� ����: B���
        {
            grade = 'B';
        }
        else if (mostRepetition > 10 && mostRepetition <= 20) //11~20�� ����: C���
        {
            grade = 'C';
        }
        else if (mostRepetition > 20 && mostRepetition <= 25) //21~25�� ����: D���
        {
            grade = 'D';
        }
        else if (mostRepetition > 25 && mostRepetition <= 30) //25~30�� ����: E���
        {
            grade = 'E';
        }
        else //31�� �̻� ����: F���
        {
            grade = 'F';
        }

        GameManager.instance.predictability = grade;     //���� ����

        return grade;
    }

    //����� ������ �ٲٴ� �Լ�
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

    //��� ���� ��ȯ�ϴ� �Լ�
    public float GetAverage()
    {
        char AgilityGrade = GetAgility();
        char AccuracyGrade = GetAccuracy();
        char PredictabilityGrade = GetPredictability();

        int AgilityScore = GradeToScore(AgilityGrade);
        int AccuracyScore = GradeToScore(AccuracyGrade);
        int PredictabilityScore = GradeToScore(PredictabilityGrade);

        float average = ((float)(AgilityScore + AccuracyScore + PredictabilityScore)) / 3.0f;

        //���� ����
        GameManager.instance.average = average;

        return average;
    }
}
