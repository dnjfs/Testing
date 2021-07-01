using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class FirebaseTest : MonoBehaviour
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

    public void OnClickSave()
    {
        //count�� �ҷ����� �����ϱ� ���� �ٸ� ����ڰ� ���� �����Ͽ�
        //������� �� �����Ƿ� �ҷ����� �����ϴ� ���� �� ���� ó��
        //ContinueWith�� �����ϴ� �ͺ��� runTransaction() ����� ������?
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

                System.Random random = new System.Random(); //����Ƽ Random�� ����ϸ� �����尡 �׾����
                DataWrite("User", random.Next(101), random.Next(1000), count+1);
            }
        });
    }
    public void OnClickLoad()
    {
        DataLoad();
    }

    private void DataWrite(string name, int score, int time, int turn) //���̾�̽��� ���
    {
        User user = new User(name+turn, score, time);
        string json = JsonUtility.ToJson(user);
        Debug.Log("����: " + turn);
        reference.Child("RANK").Child(turn.ToString()).SetRawJsonValueAsync(json);
    }
    private void DataLoad() //���̾�̽��� ��ŷ �ҷ�����
    {
        //reference�� �ڽ�(RANK)�� task�� ����
        reference.Child("RANK").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted) //task�� �������̸�
            {
                DataSnapshot snapshot = task.Result; //DataSnapshot ������ �����Ͽ� task�� ��� ���� ����

                Debug.Log("����� ������ ��: " + snapshot.ChildrenCount);

                List<IDictionary> personInfo = new List<IDictionary>();
                foreach (DataSnapshot data in snapshot.Children) //�����ͺ��̽����� ������ �ҷ��� personInfo�� ����
                    personInfo.Add((IDictionary)data.Value);

                //������ �ð����� ����
                personInfo.Sort(delegate (IDictionary x, IDictionary y)
                {
                    string X = x["score"].ToString();
                    string Y = y["score"].ToString();
                    int xScore = int.Parse(X);
                    int yScore = int.Parse(Y);

                    if (xScore == yScore) //������ ���� ���
                    {
                        if(xScore == 0) //Ż�� ���� ��
                            return int.Parse(x["time"].ToString()) < int.Parse(y["time"].ToString()) ? 1 : -1; //������������ ���� (�ð��� �� �������)
                        return int.Parse(x["time"].ToString()) < int.Parse(y["time"].ToString()) ? -1 : 1; //������������ ���� (�ð��� ª�� �������)
                    }

                    return int.Parse(X) < int.Parse(Y) ? 1 : -1; //������������ ���� (������ ���� �������)
                });

                int rank = 1;
                foreach(IDictionary temp in personInfo)
                    Debug.Log(rank++ + "�� name: " + temp["username"] + ", score: " + temp["score"] + ", time: " + temp["time"]);
            }
        });
    }
}
