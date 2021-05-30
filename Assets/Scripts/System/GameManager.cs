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
        nickName = mazeType = null;
        playTime = timeScore = 0f;
        gameLevel = "normal";   //���� ���̵� normal�� ����
        repetitionCount = 0;    //�� �ߺ�Ƚ�� �ʱ�ȭ

    }

    void Update()
    {
        //Ÿ�̸� ����
        playTime += Time.deltaTime;
    }
}
