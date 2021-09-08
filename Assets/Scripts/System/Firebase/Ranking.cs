using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Database;
using Firebase.Unity;


public class Ranking : MonoBehaviour
{
    public Text PageText;

    DatabaseReference reference;
    List<IDictionary> UserRank = new List<IDictionary>();
    int Page = 1;
    bool isLoaded;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        DataLoad();

        Debug.Log("이름: " + PlayerPrefs.GetString("Name"));
        Debug.Log("점수: " + PlayerPrefs.GetInt("Score"));
        Debug.Log("시간: " + PlayerPrefs.GetInt("Time"));
    }

    void Update()
    {
        if (isLoaded)
            PageUpdate();
    }

    private void DataLoad() //파이어베이스의 랭킹 불러오기
    {
        //reference의 자식(RANK)를 task로 받음
        reference.Child("RANK").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("DataLoad was canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Load error");
                return;
            }

            //if (task.IsCompleted) //task가 성공적이면
            DataSnapshot snapshot = task.Result; //DataSnapshot 변수를 선언하여 task의 결과 값을 받음
            Debug.Log("저장된 데이터 수: " + snapshot.ChildrenCount);
            foreach (DataSnapshot data in snapshot.Children) //데이터베이스에서 값들을 불러와 UserRank 리스트에 저장
                UserRank.Add((IDictionary)data.Value);

            //점수와 시간으로 정렬
            UserRank.Sort(delegate (IDictionary x, IDictionary y)
            {
                string X = x["score"].ToString();
                string Y = y["score"].ToString();
                int xScore = int.Parse(X);
                int yScore = int.Parse(Y);

                if (xScore == yScore) //점수가 같은 경우
                {
                    if (xScore == 0) //탈출 실패 시
                        return int.Parse(x["time"].ToString()) < int.Parse(y["time"].ToString()) ? 1 : -1; //내림차순으로 정렬 (시간이 긴 순서대로)
                    return int.Parse(x["time"].ToString()) < int.Parse(y["time"].ToString()) ? -1 : 1; //오름차순으로 정렬 (시간이 짧은 순서대로)
                }

                return int.Parse(X) < int.Parse(Y) ? 1 : -1; //내림차순으로 정렬 (점수가 높은 순서대로)
            });

            int rank = 1;
            foreach (IDictionary temp in UserRank)
                Debug.Log(rank++ + "등 name: " + temp["username"] + ", score: " + temp["score"] + ", time: " + temp["time"]);

            isLoaded = true; //쓰레드가 끝나는 타이밍을 플래그로 설정
        });
    }

    public void OnClickLeft()
    {
        if (Page <= 1)
            return;

        Page--;
        PageText.text = Page.ToString();
    }
    public void OnClickRight()
    {
        if (Page > (UserRank.Count-1) / 5)
            return;

        Page++;
        PageText.text = Page.ToString();
    }

    private void PageUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject row = this.transform.GetChild(i).gameObject; //start에서 에러
            int rank = (Page - 1) * 5 + i;
            if (UserRank.Count - 1 < rank) //데이터의 수를 넘어가는 경우
            {
                row.SetActive(false);
                continue;
            }

            row.SetActive(true);
            if (i < 3)
            {
                if (Page == 1)
                {
                    row.transform.Find("Icon").gameObject.SetActive(true);
                    row.transform.Find("Rank").gameObject.SetActive(false);
                }
                else
                {
                    row.transform.Find("Icon").gameObject.SetActive(false);
                    row.transform.Find("Rank").gameObject.SetActive(true);
                }
            }
            row.transform.Find("Rank").gameObject.GetComponent<Text>().text = (rank+1).ToString();
            row.transform.Find("UserName").gameObject.GetComponent<Text>().text = UserRank[rank]["username"].ToString();
            row.transform.Find("Score").gameObject.GetComponent<Text>().text = UserRank[rank]["score"].ToString();

            int time = int.Parse(UserRank[rank]["time"].ToString());
            row.transform.Find("Time").gameObject.GetComponent<Text>().text = (time/60).ToString("00")+":"+(time%60).ToString("00"); //시간을 00:00 형식으로 표현

            //최근 기록 하이라이트
            if (PlayerPrefs.HasKey("Score") && PlayerPrefs.HasKey("Time")) //저장 이력이 있는 경우만
            {
                if (PlayerPrefs.GetString("Name") == UserRank[rank]["username"].ToString() &&
                    PlayerPrefs.GetInt("Score") == int.Parse(UserRank[rank]["score"].ToString()) &&
                    PlayerPrefs.GetInt("Time") == int.Parse(UserRank[rank]["time"].ToString()))
                {
                    row.transform.Find("Rank").gameObject.GetComponent<Text>().color = new Color(1, 1, 0);
                    row.transform.Find("UserName").gameObject.GetComponent<Text>().color = new Color(1, 1, 0);
                    row.transform.Find("Score").gameObject.GetComponent<Text>().color = new Color(1, 1, 0);
                    row.transform.Find("Time").gameObject.GetComponent<Text>().color = new Color(1, 1, 0);

                    row.transform.Find("Rank").gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
                    row.transform.Find("UserName").gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
                    row.transform.Find("Score").gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
                    row.transform.Find("Time").gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
                }
                else
                {
                    row.transform.Find("Rank").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
                    row.transform.Find("UserName").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
                    row.transform.Find("Score").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
                    row.transform.Find("Time").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);

                    row.transform.Find("Rank").gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
                    row.transform.Find("UserName").gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
                    row.transform.Find("Score").gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
                    row.transform.Find("Time").gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
                }
            }
        }
    }
}
