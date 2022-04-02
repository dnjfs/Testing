using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//��¥ Ŭ����
public class OathAndReportDate
{
    public int year;
    public int month;
    public int day;
}

public class writtenOath : MonoBehaviour
{
    //���༭, ����ǥ ���� ����� �����ϴ� ��ũ��Ʈ

    public Image theOath;  //���༭ �̹���
    public Image reportCard;    //����ǥ �̹���

    public GameObject oathContent;  //���༭ �÷��̾� �̸��� ��¥ �ؽ�Ʈ
    public GameObject reportContent;    //����ǥ ��ũ�Ѻ� ������
    public GameObject dialog;   //��ȭâ

    int openNumber = 0; //����ǥ ���� Ƚ��

    void Start()
    {
        openNumber = 0;
    }

    //���������� ��ȭ�� ������ ���༭�� ���̵� ȿ���� ���� �Լ�(��ȭâ ��ġ �̺�Ʈ)
    public void OpenOath()
    {
        //��ȭâ ��Ȱ��ȭ
        dialog.gameObject.SetActive(false);

        if (openNumber == 0)    //����ǥ�� ó�� ������ �Ŷ��
        {
            //��ȭâ�� ��ġ�ϸ� ��ȭâ�� �ݴ´�.
            this.GetComponent<Dialog_Corridor>().CloseWhiteDialog();

            //���༭�� ��¥�� �̸� ����
            OathAndReportDate writeDate;    //���� ��ü ����
            writeDate = GetDate();  //���� ����ؼ� ������

            //���༭�� �� �Է�(�̸��� ��¥�� ������� �̸��� �÷����� ��¥�� ������ ������ ����)
            oathContent.transform.GetChild(7).GetComponent<Text>().text = "��¥: " + writeDate.year + "�� " + writeDate.month + "��" + writeDate.day + "��\n\n" +
                "���� : " + GameManager.instance.nickName;

            //���༭�� ���̵� ȿ���� ���´�.
            theOath.gameObject.SetActive(true);  //���༭ �̹��� Ȱ��ȭ
            theOath.DOFade(1f, 1f); //���༭ ���̵� ȿ��

            openNumber++;
        }
    }

    //(��ũ���� ��� �����ų� ��ŵ ��ư�� ������) ���༭�� ���̵� ȿ���� �ݴ� �Լ�(��ũ�� �̺�Ʈ, ��ŵ ��ư �̺�Ʈ)
    public void CloasOath()
    {
        bool isOathActive = theOath.gameObject.activeSelf; //���༭�� Ȱ��ȭ �Ǿ��ִ��� ����

        if (isOathActive)   //���༭�� Ȱ��ȭ �Ǿ��ִٸ�
        {
            //���༭�� ���̵� ȿ���� �ݴ´�.
            Sequence seq = DOTween.Sequence();  //DOTween Sequence ����
            seq.Append(theOath.DOFade(0f, 0.5f)); //���༭ ���̵� ȿ��
            
            //���༭ ���� ���̵�
            for (int i = 0; i < 8; i++)
            {
                seq.Join(oathContent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().DOFade(0f, 0.5f));    //���༭ ���� ���̵�
            }

            seq.OnComplete(() => {
                theOath.gameObject.SetActive(false); //���༭ �̹��� ��Ȱ��ȭ
            });
        }
    }

    //(��ũ���� ��� �����ų� ��ŵ ��ư�� ������) ����ǥ�� ���̵� ȿ���� ���� �Լ�(��ũ�� �̺�Ʈ, ��ŵ ��ư �̺�Ʈ)
    public void OpenReportCard()
    {

        //���� ���(GameManager �̱��濡 ����� �������� ������)
        OathAndReportDate writeDate;    //���� ��ü ����
        writeDate = GetDate();  //���� ����ؼ� ������
        

        //���� ���
        this.GetComponent<ScoreCalculate>().GetAverage();
        GameObject.Find("GameSystem").GetComponent<RankSystem>().DataWrite(PlayerPrefs.GetString("Name"), (int)GameManager.instance.average, (int)GameManager.instance.timeScore);
        bool isPass = GameManager.instance.average >= 70 ? true : false;    //70�� �̻��̸� �հ�
        GameManager.instance.isPass = isPass;

        //����ǥ�� �� �Է�(�̸��� ��¥�� ������� �̸��� �÷����� ��¥�� ������ ������ ����)
        reportContent.transform.GetChild(2).GetComponent<Text>().text = "���� : " + GameManager.instance.nickName + "\n\n" +
            "��¥: " + writeDate.year + "�� " + writeDate.month + "��" + writeDate.day + "��\n";

        reportContent.gameObject.transform.GetChild(3).GetComponent<Text>().text = "��ø��: " + GameManager.instance.agility;   //��ø�� ����
        reportContent.gameObject.transform.GetChild(4).GetComponent<Text>().text = "��Ȯ��: " + GameManager.instance.accuracy;   //��Ȯ�� ����
        reportContent.gameObject.transform.GetChild(5).GetComponent<Text>().text = "������: " + GameManager.instance.predictability;   //������ ����
        reportContent.gameObject.transform.GetChild(6).GetComponent<Text>().text = "���: " + GameManager.instance.average.ToString("N1");   //��� ����(�Ҽ��� ���ڸ����� ǥ��)

        string passOrFail = isPass == true ? "�հ�" : "���հ�"; //�հ�, ���հ� ���� ������
        Debug.Log(passOrFail);
        reportContent.gameObject.transform.GetChild(7).GetComponent<Text>().text = passOrFail;   //�հݿ��� ����

        //����ǥ�� ���̵� ȿ���� ����(������ 2��)
        Sequence reportSeq = DOTween.Sequence();
        reportSeq.SetDelay(2f);
        reportCard.gameObject.SetActive(true);

        //reportSeq.Append(reportCard.gameObject.SetActive(true));  //���༭ �̹��� Ȱ��ȭ
        reportSeq.Append(reportCard.DOFade(1f, 1f)); //���༭ ���̵� ȿ��
    }

    //(��ũ���� ��� ������) ����ǥ�� ���̵� ȿ���� �ݴ� �Լ�
    public void CloseReportCard()
    {
        //����ǥ�� ���̵� ȿ���� �ݴ´�.
        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����
        seq.Append(reportCard.DOFade(0f, 0.5f)); //���༭ ���̵� ȿ��

        //����ǥ ���� ���̵�
        for (int i = 0; i < 8; i++)
        {
            seq.Join(reportContent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().DOFade(0f, 0.5f));    //���༭ ���� ���̵�
        }

        seq.OnComplete(() => {
            reportCard.gameObject.SetActive(false); //���༭ �̹��� ��Ȱ��ȭ
            
            //����
            this.GetComponent<Ending>().FailOrPass();
        });
    }

    OathAndReportDate GetDate()
    {
        OathAndReportDate todayDate = new OathAndReportDate();

        //���� ��¥�� ������
        string year = System.DateTime.Now.ToString("yyyy");
        string month = System.DateTime.Now.ToString("MM");
        string day = System.DateTime.Now.ToString("dd");

        //string�� int�� ��ȯ
        int reportyear = int.Parse(year);
        int reportMonth = int.Parse(month);
        int reportDay = int.Parse(day);

        //���� 1~7���̶�� �� ����, 8�� �̻��̸� ���� �״�� �ΰ� �ϸ� ����
        if (reportDay < 8)
        {
            //��, �� ����
            if (reportMonth == 1)   //�̹����� 1���̶��
            {
                reportyear -= 1;    //�⵵ 1 ����
                reportMonth = 12; //12���� ����
            }
            else
            {
                reportMonth -= 1;   //�����޷� ����
            }

            //�� ����
            if (reportMonth < 8)    //1~7�� ���̰� 
            {
                if (reportMonth % 2 == 1) //Ȧ�� ���̶��(31�ϱ��� �ִ� ���̶��)
                {
                    reportDay = reportDay + 31 - 7; //31�� ����
                }
                else //¦�� ���̶��(30�ϱ��� �ִ� ���̶��)
                {
                    if (reportMonth == 2)   //2���̶��
                    {
                        //�����̶�� 29�� ����
                        if (reportyear % 4 == 0)    //4�� ������ ��������
                        {
                            if (reportyear % 100 != 0)  //100���� ������ �������� ������
                            {
                                if (reportyear % 400 == 0)  //400���� ������ �������� ����
                                {
                                    reportDay = reportDay + 29 - 7; //29�� ����
                                }
                                else //400���� ������ �������� ������ ���
                                {
                                    reportDay = reportDay + 28 - 7; //28�� ����
                                }
                            }
                            else    //100���� ������ �������� ���
                            {
                                reportDay = reportDay + 28 - 7; //28�� ����
                            }
                        }
                        //�ƴ϶�� 28�� ����
                        else
                        {
                            reportDay = reportDay + 28 - 7; //28�� ����
                        }
                    }
                    else //2���� �ƴ϶��
                    {
                        reportDay = reportDay + 30 - 7; //30�� ����
                    }
                }
            }
            else //8~10�� ���̶��
            {
                if (reportMonth % 2 == 0) //¦�� ���̶��(31�ϱ��� �ִ� ���̶��)
                {
                    reportDay = reportDay + 31 - 7; //31�� ����
                }
                else //Ȧ�� ���̶��(30�ϱ��� �ִ� ���̶��)
                {
                    reportDay = reportDay + 30 - 7; //30�� ����
                }
            }
        }
        else
        {
            reportDay -= 7;
        }

        //Ŭ������ ��ȯ
        todayDate.year = reportyear;
        todayDate.month = reportMonth;
        todayDate.day = reportDay;

        return todayDate;
    }
}
