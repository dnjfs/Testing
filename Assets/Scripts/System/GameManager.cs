using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�̱��� �Լ�
    //�г���, ���̵�, Ÿ�̸�, �ߺ���, ���� �� ����

    //�̱��� ������ ����ϱ� ���� ���� ����
    public static GameManager instance;

    public string nickName; //�÷��̾� �г���

    public float playTime;  //�÷��� �ð�
    public float timeScore; //���� �÷��� �ð�

    public string gameLevel; //���� ���̵�

    public string mazeType;   //�� Ÿ��(T, E, S)
    public int repetitionCount;   //�� �ߺ���(���� ���� �ߺ� Ƚ��)

    public int elevatorIndex;   //�÷��̾ ������ ������ ���� ����� ����������
    public bool isFinished;   //�̷θ� �� ���Ҵ��� Ȯ���ϴ� ����


    //���� ������
    public char agility;    //��ø�� ���
    public char accuracy;   //��Ȯ�� ���
    public char predictability; //������ ���
    public float average;   //���
    public bool isPass; //�հ� ����

    // ���� ���۰� ���ÿ� �̱��� ����
    void Awake()
    {
        //�̱��� ���� instance�� �̹� �ִٸ�
        if (instance)
        {
            DestroyImmediate(gameObject);   //����
            return;
        }
        //������ �ν��Ͻ��� �����
        instance = this;
        DontDestroyOnLoad(gameObject);  //���� �ٲ� ��� ������Ŵ

        ResetGameManager();
    }

    void Update()
    {
        //Ÿ�̸� ����
        playTime += Time.deltaTime;
    }

    //GameManager ���� �� �ʱ�ȭ
    public void ResetGameManager()
    {
        //���� �ʱ�ȭ
        nickName = PlayerPrefs.GetString("Name");
        int level = PlayerPrefs.GetInt("Level", 1);   //���� ���̵�
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
        repetitionCount = 0;    //�� �ߺ�Ƚ�� �ʱ�ȭ
        isFinished = isPass = false;
        agility = accuracy = predictability = 'F';  //��� F�� �ʱ�ȭ
    }
}
