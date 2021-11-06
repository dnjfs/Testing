using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  //Max()�� ����ϱ� ���� ���

public class Repetition : MonoBehaviour
{
    public int[] Count;   //��Ϻ� �ߺ� Ƚ���� ������ �迭

    //�������� ���� ���� �����س��� �������� ���ִ� ������� ���
    //�������� ���� ���� ������ ����Ʈ
    public List<int> leftBlock = new List<int>();

    public int MaxValue;    //�ִ� �ߺ���
    public double ProgressRate;  //�����
    public int EnemyCount;  //Enemy�� ��

    public bool isCreateElevator;   //���������� ���� ����

    //T���� �ߺ��� üũ ��� 47��
    //E, S���� �ߺ��� üũ ��� 65��
    int NumberOfBlock;  //�ߺ��� üũ ����� ����

    void Start()
    {
        MaxValue = 0; //�ִ� �ʱ�ȭ
        ProgressRate = 0f;  //����� �ʱ�ȭ
        EnemyCount = 0; //Enemy�� �ʱ�ȭ
        isCreateElevator = false;   //���������� ���� ���� �ʱ�ȭ

        if (GameManager.instance.mazeType == "T")
            NumberOfBlock = 47;
        else
            NumberOfBlock = 65;

        Count = new int[NumberOfBlock]; //��� ���� �ʱ�ȭ

        for (int i = 0; i < NumberOfBlock; i++)    //�������� ���� �� ����Ʈ �߰�(�ʱ�ȭ)
            leftBlock.Add(i);
    }

    void Update()
    {
        MaxValue = Count.Max(); //�ִ� ��ȯ
        Progress(); //����� ���
        ProgressEvent();    //������� ���� �̺�Ʈ
    }

    public void addCount(int index)
    {
        //�ε�����°�� ī��Ʈ �迭 ���� ������Ű�� �Լ�
        //����� �浹ó�� �Ǹ� �ڽ��� �ε����� �� �Լ� ȣ��
        Count[index]++; //�� ������ Ƚ�� ī��Ʈ
        
        if (leftBlock.Contains(index))  //���� �������� ���� �� ����Ʈ�� index�� �ִٸ�
        {
            leftBlock.Remove(index);    //index��°�� �ƴ϶� index���� ����
        }

    }

    //���� ������� ��ȯ�ϴ� �Լ�
    public double Progress()
    {
        //Count �迭���� �� ���̶� ������ �� �ۼ�Ʈ ���

        int left = leftBlock.Count();   //���� ���� ��
        
        double temp1 = NumberOfBlock - left;
        double temp2 = temp1 / NumberOfBlock;
        double temp3 = temp2 * 100;
        ProgressRate = temp3; //���� ����� ���
        
        //ProgressRate = ((79 - left) / 79) * 100;  //�� ���� ����ϸ� ����� �ȵǰ� ���� ������ ����ϸ� ��

        return ProgressRate;
    }

    public void ProgressEvent()
    {
        //������� ���� �̺�Ʈ �߻�(���� ����, ���������� ���� ��)

        //���� ���� ������� 10, 20, 40, 60�ۼ�Ʈ �̻��̶��
        if (ProgressRate >= 10 && EnemyCount == 0)
        {
            //Enemy ����
            this.gameObject.GetComponent<EnemyStart>().CreateEnemy();
            EnemyCount++;
        }

        if (ProgressRate >= 20 && EnemyCount == 1)
        {
            //Enemy ����
            this.gameObject.GetComponent<EnemyStart>().CreateEnemy();
            EnemyCount++;
        }

        if (ProgressRate >= 40 && EnemyCount == 2)
        {
            //Enemy ����
            this.gameObject.GetComponent<EnemyStart>().CreateEnemy();
            EnemyCount++;
        }

        if (ProgressRate >= 60 && EnemyCount == 3)
        {
            //Enemy ����
            this.gameObject.GetComponent<EnemyStart>().CreateEnemy();
            EnemyCount++;
        }


        if (ProgressRate == 100 && isCreateElevator == false)    //���� ���� ������� 100�ۼ�Ʈ��� == ��� ���� �� ���Ҵٸ�
        {
            //GameManager�� isFinished�� true�� ����
            GameManager.instance.isFinished = true;
            GameManager.instance.repetitionCount = MaxValue;
            GameManager.instance.timeScore = GameManager.instance.playTime; //�÷���Ÿ�� ����

            //���������� ���� �޽��� ���
            this.gameObject.GetComponent<Dialog_Maze>().CreateElevatorMessage();

            //���������� ���� 10�� �� ����
            int elevatorIndex = GameManager.instance.elevatorIndex; //�÷��̾� ���������� �ε����� ������
            this.GetComponent<DoorManager>().OpenDoor(elevatorIndex, elevatorIndex);    //���������� ���� ���� 10�� �� �ݴ´�.

            isCreateElevator = true; //���������� ���� �Ϸ�

            //�÷��̾�� ������ �ӵ� ����
            GameObject.FindWithTag("Player").GetComponent<Player>().SpeedBoostPlayer();
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Monster"))
                obj.GetComponent<Enemy>().SpeedBoostEnemy();
        }
    }

}
