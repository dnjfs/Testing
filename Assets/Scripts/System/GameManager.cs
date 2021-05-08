using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�̱��� �Լ�
    //�г���, ����, ���̵�, Ÿ�̸�, �ߺ���, ���� �� ����

    //�̱��� ������ ����ϱ� ���� ���� ����
    public static GameManager instance;

    public string nickName; //�÷��̾� �г���

    public float playTime;  //�÷��� �ð�
    public float timeScore; //���� �÷��� �ð�

    public string gameLevel; //���� ���̵�
    public float sound_BGLevel; //������� ���� ũ��
    public float sound_EFLevel; //ȿ���� ���� ũ��

    public int repetitionCount;   //�� �ߺ���(���� ���� �ߺ� Ƚ��)
    public bool isFinished; //��� ���� �� ���Ҵ��� ����

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
        nickName = null;
        playTime = timeScore = sound_BGLevel = sound_EFLevel  = 0f;
        gameLevel = "normal";   //���� ���̵� normal�� ����
        repetitionCount = 0;    //�� �ߺ�Ƚ�� �ʱ�ȭ
        isFinished = false; //��� ���� �� ���� �ʾ���
    }

    void Update()
    {
        //Ÿ�̸� ����
        playTime += Time.deltaTime;

        if (isFinished) //��� ���� �� ���Ҵٸ�
        {

        }
    }
}
