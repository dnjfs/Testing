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

        Debug.Log("�̸�: " + PlayerPrefs.GetString("Name"));
        Debug.Log("����: " + PlayerPrefs.GetInt("Score"));
        Debug.Log("�ð�: " + PlayerPrefs.GetInt("Time"));
    }

    void Update()
    {
        if (isLoaded)
            PageUpdate();
    }

    private void DataLoad() //���̾�̽��� ��ŷ �ҷ�����
    {
        //reference�� �ڽ�(RANK)�� task�� ����
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

            //if (task.IsCompleted) //task�� �������̸�
            DataSnapshot snapshot = task.Result; //DataSnapshot ������ �����Ͽ� task�� ��� ���� ����
            Debug.Log("����� ������ ��: " + snapshot.ChildrenCount);
            foreach (DataSnapshot data in snapshot.Children) //�����ͺ��̽����� ������ �ҷ��� UserRank ����Ʈ�� ����
                UserRank.Add((IDictionary)data.Value);

            //������ �ð����� ����
            UserRank.Sort(delegate (IDictionary x, IDictionary y)
            {
                string X = x["score"].ToString();
                string Y = y["score"].ToString();
                int xScore = int.Parse(X);
                int yScore = int.Parse(Y);

                if (xScore == yScore) //������ ���� ���
                {
                    if (xScore == 0) //Ż�� ���� ��
                        return int.Parse(x["time"].ToString()) < int.Parse(y["time"].ToString()) ? 1 : -1; //������������ ���� (�ð��� �� �������)
                    return int.Parse(x["time"].ToString()) < int.Parse(y["time"].ToString()) ? -1 : 1; //������������ ���� (�ð��� ª�� �������)
                }

                return int.Parse(X) < int.Parse(Y) ? 1 : -1; //������������ ���� (������ ���� �������)
            });

            int rank = 1;
            foreach (IDictionary temp in UserRank)
                Debug.Log(rank++ + "�� name: " + temp["username"] + ", score: " + temp["score"] + ", time: " + temp["time"]);

            isLoaded = true; //�����尡 ������ Ÿ�̹��� �÷��׷� ����
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
            GameObject row = this.transform.GetChild(i).gameObject; //start���� ����
            int rank = (Page - 1) * 5 + i;
            if (UserRank.Count - 1 < rank) //�������� ���� �Ѿ�� ���
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
            row.transform.Find("Time").gameObject.GetComponent<Text>().text = (time/60).ToString("00")+":"+(time%60).ToString("00"); //�ð��� 00:00 �������� ǥ��

            //�ֱ� ��� ���̶���Ʈ
            if (PlayerPrefs.HasKey("Score") && PlayerPrefs.HasKey("Time")) //���� �̷��� �ִ� ��츸
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
