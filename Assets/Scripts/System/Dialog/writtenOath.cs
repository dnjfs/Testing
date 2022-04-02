using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//날짜 클래스
public class OathAndReportDate
{
    public int year;
    public int month;
    public int day;
}

public class writtenOath : MonoBehaviour
{
    //서약서, 성적표 관련 기능을 수행하는 스크립트

    public Image theOath;  //서약서 이미지
    public Image reportCard;    //성적표 이미지

    public GameObject oathContent;  //서약서 플레이어 이름과 날짜 텍스트
    public GameObject reportContent;    //성적표 스크롤뷰 컨텐츠
    public GameObject dialog;   //대화창

    int openNumber = 0; //성적표 열린 횟수

    void Start()
    {
        openNumber = 0;
    }

    //연구원과의 대화를 끝내면 서약서를 페이드 효과로 띄우는 함수(대화창 터치 이벤트)
    public void OpenOath()
    {
        //대화창 비활성화
        dialog.gameObject.SetActive(false);

        if (openNumber == 0)    //성적표가 처음 열리는 거라면
        {
            //대화창을 터치하면 대화창을 닫는다.
            this.GetComponent<Dialog_Corridor>().CloseWhiteDialog();

            //서약서의 날짜와 이름 변경
            OathAndReportDate writeDate;    //요일 객체 생성
            writeDate = GetDate();  //요일 계산해서 가져옴

            //서약서에 값 입력(이름과 날짜를 사용자의 이름과 플레이한 날짜의 일주일 전으로 설정)
            oathContent.transform.GetChild(7).GetComponent<Text>().text = "날짜: " + writeDate.year + "년 " + writeDate.month + "월" + writeDate.day + "일\n\n" +
                "성명 : " + GameManager.instance.nickName;

            //서약서가 페이드 효과로 나온다.
            theOath.gameObject.SetActive(true);  //서약서 이미지 활성화
            theOath.DOFade(1f, 1f); //서약서 페이드 효과

            openNumber++;
        }
    }

    //(스크롤을 모두 내리거나 스킵 버튼을 누르면) 서약서를 페이드 효과로 닫는 함수(스크롤 이벤트, 스킵 버튼 이벤트)
    public void CloasOath()
    {
        bool isOathActive = theOath.gameObject.activeSelf; //서약서가 활성화 되어있는지 여부

        if (isOathActive)   //서약서가 활성화 되어있다면
        {
            //서약서를 페이드 효과로 닫는다.
            Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성
            seq.Append(theOath.DOFade(0f, 0.5f)); //서약서 페이드 효과
            
            //서약서 글자 페이드
            for (int i = 0; i < 8; i++)
            {
                seq.Join(oathContent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().DOFade(0f, 0.5f));    //서약서 글자 페이드
            }

            seq.OnComplete(() => {
                theOath.gameObject.SetActive(false); //서약서 이미지 비활성화
            });
        }
    }

    //(스크롤을 모두 내리거나 스킵 버튼을 누르면) 성적표를 페이드 효과로 띄우는 함수(스크롤 이벤트, 스킵 버튼 이벤트)
    public void OpenReportCard()
    {

        //성적 계산(GameManager 싱글톤에 저장된 점수들을 가져옴)
        OathAndReportDate writeDate;    //요일 객체 생성
        writeDate = GetDate();  //요일 계산해서 가져옴
        

        //점수 계산
        this.GetComponent<ScoreCalculate>().GetAverage();
        GameObject.Find("GameSystem").GetComponent<RankSystem>().DataWrite(PlayerPrefs.GetString("Name"), (int)GameManager.instance.average, (int)GameManager.instance.timeScore);
        bool isPass = GameManager.instance.average >= 70 ? true : false;    //70점 이상이면 합격
        GameManager.instance.isPass = isPass;

        //성적표에 값 입력(이름과 날짜를 사용자의 이름과 플레이한 날짜의 일주일 전으로 설정)
        reportContent.transform.GetChild(2).GetComponent<Text>().text = "성명 : " + GameManager.instance.nickName + "\n\n" +
            "날짜: " + writeDate.year + "년 " + writeDate.month + "월" + writeDate.day + "일\n";

        reportContent.gameObject.transform.GetChild(3).GetComponent<Text>().text = "민첩성: " + GameManager.instance.agility;   //민첩성 적용
        reportContent.gameObject.transform.GetChild(4).GetComponent<Text>().text = "정확성: " + GameManager.instance.accuracy;   //정확성 적용
        reportContent.gameObject.transform.GetChild(5).GetComponent<Text>().text = "예측성: " + GameManager.instance.predictability;   //예측성 적용
        reportContent.gameObject.transform.GetChild(6).GetComponent<Text>().text = "평균: " + GameManager.instance.average.ToString("N1");   //평균 적용(소수점 한자리까지 표현)

        string passOrFail = isPass == true ? "합격" : "불합격"; //합격, 불합격 여부 가져옴
        Debug.Log(passOrFail);
        reportContent.gameObject.transform.GetChild(7).GetComponent<Text>().text = passOrFail;   //합격여부 적용

        //성적표를 페이드 효과로 띄운다(딜레이 2초)
        Sequence reportSeq = DOTween.Sequence();
        reportSeq.SetDelay(2f);
        reportCard.gameObject.SetActive(true);

        //reportSeq.Append(reportCard.gameObject.SetActive(true));  //서약서 이미지 활성화
        reportSeq.Append(reportCard.DOFade(1f, 1f)); //서약서 페이드 효과
    }

    //(스크롤을 모두 내리면) 성적표를 페이드 효과로 닫는 함수
    public void CloseReportCard()
    {
        //성적표를 페이드 효과로 닫는다.
        Sequence seq = DOTween.Sequence();  //DOTween Sequence 생성
        seq.Append(reportCard.DOFade(0f, 0.5f)); //서약서 페이드 효과

        //성적표 글자 페이드
        for (int i = 0; i < 8; i++)
        {
            seq.Join(reportContent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().DOFade(0f, 0.5f));    //서약서 글자 페이드
        }

        seq.OnComplete(() => {
            reportCard.gameObject.SetActive(false); //서약서 이미지 비활성화
            
            //엔딩
            this.GetComponent<Ending>().FailOrPass();
        });
    }

    OathAndReportDate GetDate()
    {
        OathAndReportDate todayDate = new OathAndReportDate();

        //오늘 날짜를 가져옴
        string year = System.DateTime.Now.ToString("yyyy");
        string month = System.DateTime.Now.ToString("MM");
        string day = System.DateTime.Now.ToString("dd");

        //string을 int로 변환
        int reportyear = int.Parse(year);
        int reportMonth = int.Parse(month);
        int reportDay = int.Parse(day);

        //만약 1~7일이라면 달 변경, 8일 이상이면 달은 그대로 두고 일만 변경
        if (reportDay < 8)
        {
            //년, 달 변경
            if (reportMonth == 1)   //이번달이 1월이라면
            {
                reportyear -= 1;    //년도 1 감소
                reportMonth = 12; //12월로 변경
            }
            else
            {
                reportMonth -= 1;   //지난달로 변경
            }

            //일 변경
            if (reportMonth < 8)    //1~7월 달이고 
            {
                if (reportMonth % 2 == 1) //홀수 달이라면(31일까지 있는 달이라면)
                {
                    reportDay = reportDay + 31 - 7; //31일 적용
                }
                else //짝수 달이라면(30일까지 있는 달이라면)
                {
                    if (reportMonth == 2)   //2월이라면
                    {
                        //윤년이라면 29일 적용
                        if (reportyear % 4 == 0)    //4로 나누어 떨어지면
                        {
                            if (reportyear % 100 != 0)  //100으로 나누어 떨어지지 않으면
                            {
                                if (reportyear % 400 == 0)  //400으로 나누어 떨어지면 윤년
                                {
                                    reportDay = reportDay + 29 - 7; //29일 적용
                                }
                                else //400으로 나누어 떨어지지 않으면 평년
                                {
                                    reportDay = reportDay + 28 - 7; //28일 적용
                                }
                            }
                            else    //100으로 나누어 떨어지면 평년
                            {
                                reportDay = reportDay + 28 - 7; //28일 적용
                            }
                        }
                        //아니라면 28일 적용
                        else
                        {
                            reportDay = reportDay + 28 - 7; //28일 적용
                        }
                    }
                    else //2월이 아니라면
                    {
                        reportDay = reportDay + 30 - 7; //30일 적용
                    }
                }
            }
            else //8~10월 달이라면
            {
                if (reportMonth % 2 == 0) //짝수 달이라면(31일까지 있는 달이라면)
                {
                    reportDay = reportDay + 31 - 7; //31일 적용
                }
                else //홀수 달이라면(30일까지 있는 달이라면)
                {
                    reportDay = reportDay + 30 - 7; //30일 적용
                }
            }
        }
        else
        {
            reportDay -= 7;
        }

        //클래스로 반환
        todayDate.year = reportyear;
        todayDate.month = reportMonth;
        todayDate.day = reportDay;

        return todayDate;
    }
}
