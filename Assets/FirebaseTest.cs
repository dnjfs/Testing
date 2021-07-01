using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class FirebaseTest : MonoBehaviour
{
    //json 파일로 만들기 위해 class 정의
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
        //데이터를 쓰려면 DatabaseReference의 인스턴스가 필요
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSave()
    {
        //count를 불러오고 저장하기 전에 다른 사용자가 먼저 저장하여
        //덮어씌워질 수 있으므로 불러오고 저장하는 것을 한 번에 처리
        //ContinueWith를 연결하는 것보다 runTransaction() 사용이 좋을까?
        reference.Child("RANK").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("서버 접속 불가");
            }
            else if (task.IsCompleted) //task가 성공적이면
            {
                int count = (int)task.Result.ChildrenCount;
                Debug.Log("불러온 데이터 수: " + count);

                System.Random random = new System.Random(); //유니티 Random을 사용하면 쓰레드가 죽어버림
                DataWrite("User", random.Next(101), random.Next(1000), count+1);
            }
        });
    }
    public void OnClickLoad()
    {
        DataLoad();
    }

    private void DataWrite(string name, int score, int time, int turn) //파이어베이스에 기록
    {
        User user = new User(name+turn, score, time);
        string json = JsonUtility.ToJson(user);
        Debug.Log("순번: " + turn);
        reference.Child("RANK").Child(turn.ToString()).SetRawJsonValueAsync(json);
    }
    private void DataLoad() //파이어베이스의 랭킹 불러오기
    {
        //reference의 자식(RANK)를 task로 받음
        reference.Child("RANK").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted) //task가 성공적이면
            {
                DataSnapshot snapshot = task.Result; //DataSnapshot 변수를 선언하여 task의 결과 값을 받음

                Debug.Log("저장된 데이터 수: " + snapshot.ChildrenCount);

                List<IDictionary> personInfo = new List<IDictionary>();
                foreach (DataSnapshot data in snapshot.Children) //데이터베이스에서 값들을 불러와 personInfo에 저장
                    personInfo.Add((IDictionary)data.Value);

                //점수와 시간으로 정렬
                personInfo.Sort(delegate (IDictionary x, IDictionary y)
                {
                    string X = x["score"].ToString();
                    string Y = y["score"].ToString();
                    int xScore = int.Parse(X);
                    int yScore = int.Parse(Y);

                    if (xScore == yScore) //점수가 같은 경우
                    {
                        if(xScore == 0) //탈출 실패 시
                            return int.Parse(x["time"].ToString()) < int.Parse(y["time"].ToString()) ? 1 : -1; //내림차순으로 정렬 (시간이 긴 순서대로)
                        return int.Parse(x["time"].ToString()) < int.Parse(y["time"].ToString()) ? -1 : 1; //오름차순으로 정렬 (시간이 짧은 순서대로)
                    }

                    return int.Parse(X) < int.Parse(Y) ? 1 : -1; //내림차순으로 정렬 (점수가 높은 순서대로)
                });

                int rank = 1;
                foreach(IDictionary temp in personInfo)
                    Debug.Log(rank++ + "등 name: " + temp["username"] + ", score: " + temp["score"] + ", time: " + temp["time"]);
            }
        });
    }
}
