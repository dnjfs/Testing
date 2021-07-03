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
    public int chaseCount; //������ �þ߿� ���� Ƚ��

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

        //���� �ʱ�ȭ
        nickName = PlayerPrefs.GetString("Name");
        mazeType = null;
        playTime = timeScore = 0f;
        gameLevel = "normal";   //���� ���̵� normal�� ����
        repetitionCount = 0;    //�� �ߺ�Ƚ�� �ʱ�ȭ
        chaseCount = 0;
        isFinished = isPass = false;
        agility = accuracy = predictability = 'F';  //��� F�� �ʱ�ȭ

        average = 0f;   //��� �ʱ�ȭ

    }

    void Update()
    {
        //Ÿ�̸� ����
        playTime += Time.deltaTime;
    }
}
