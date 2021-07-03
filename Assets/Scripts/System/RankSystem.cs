using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class RankSystem : MonoBehaviour
{
    //json ���Ϸ� ����� ���� class ����
    public class User
    {
        public string username;
        public int score;
        public int time;

        public User(string inName, int inScore, int inTime)
        {
            this.username = inName;
            this.score = inScore;
            this.time = inTime;
        }
    }

    DatabaseReference reference;
    void Start()
    {
        //�����͸� ������ DatabaseReference�� �ν��Ͻ��� �ʿ�
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void DataWrite(string name, int score, int time)
    {
        //�ֱ� ����� ���ÿ� ����
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Time", time);

        //count�� �ҷ����� �����ϱ� ���� �ٸ� ����ڰ� ���� �����Ͽ�
        //������� �� �����Ƿ� �ҷ����� �����ϴ� ���� �� ���� ó��
        reference.Child("RANK").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("���� ���� �Ұ�");
            }
            else if (task.IsCompleted) //task�� �������̸�
            {
                int count = (int)task.Result.ChildrenCount;
                Debug.Log("�ҷ��� ������ ��: " + count);

                AddDatabase(name, score, time, count + 1);
            }
        });
    }
    private void AddDatabase(string name, int score, int time, int turn) //���̾�̽��� ���
    {
        Debug.Log("???");
        User user = new User(name, score, time);
        string json = JsonUtility.ToJson(user);
        Debug.Log("����: " + turn);
        reference.Child("RANK").Child(turn.ToString()).SetRawJsonValueAsync(json);
    }
}
