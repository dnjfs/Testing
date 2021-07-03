using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class RankSystem : MonoBehaviour
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

    public void DataWrite(string name, int score, int time)
    {
        //최근 기록을 로컬에 저장
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Time", time);

        //count를 불러오고 저장하기 전에 다른 사용자가 먼저 저장하여
        //덮어씌워질 수 있으므로 불러오고 저장하는 것을 한 번에 처리
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

                AddDatabase(name, score, time, count + 1);
            }
        });
    }
    private void AddDatabase(string name, int score, int time, int turn) //파이어베이스에 기록
    {
        Debug.Log("???");
        User user = new User(name, score, time);
        string json = JsonUtility.ToJson(user);
        Debug.Log("순번: " + turn);
        reference.Child("RANK").Child(turn.ToString()).SetRawJsonValueAsync(json);
    }
}
