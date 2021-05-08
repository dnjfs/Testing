using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  //Max()�� ����ϱ� ���� ���

public class Repetition : MonoBehaviour
{
    //public GameObject[] Block = new GameObject[78]; //�ߺ� �˻��� ��� �迭
    public int[] Count = new int[78];   //��Ϻ� �ߺ� Ƚ���� ������ �迭

    public int MaxValue;

    void Start()
    {
        /*
        //ī��Ʈ�� ��� ������
        for (int i = 0; i < 78; i++)
        {
            Block[i] = GameObject.Find(("Cube (" + i + ")").ToString()); 
        }*/

        MaxValue = 0; //�ִ� �ʱ�ȭ
    }

    void Update()
    {
        MaxValue = Count.Max(); //�ִ� ��ȯ
    }

    public void addCount(int index)
    {
        //�ε�����°�� ī��Ʈ �迭 ���� ������Ű�� �Լ�
        //����� �浹ó�� �Ǹ� �ڽ��� �ε����� �� �Լ� ȣ��
        Count[index]++;
    }

    public int MaxCount()
    {
        //���� ���� �ߺ� ī��Ʈ���� ���ϴ� �Լ�
        //�迭 �� ���� ���� ���� ���Ͽ� ��ȯ
        return MaxValue;

        //�ǽð����� GameManager �̱��� �� �����ϴ°� ������? �ƴϸ� �װų� ���� ������ �� �� �����ϴ°� ������?
    }
}
