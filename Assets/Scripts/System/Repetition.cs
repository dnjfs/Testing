using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  //Max()를 사용하기 위해 명시

public class Repetition : MonoBehaviour
{
    public int[] Count = new int[79];   //블록별 중복 횟수를 저장할 배열

    //배열의 처음부터 끝까지 계산하는 것을 반복하는 것은 비효율적인 것 같아, 지나가지 않은 길을 저장해놓고 지나가면 없애는 방식으로 계산
    //지나가지 않은 길을 저장할 리스트
    public List<int> leftBlock = new List<int>();

    public int MaxValue;    //최대 중복값
    public double ProgressRate;  //진행률
    public int EnemyCount;  //Enemy의 수

    public bool isCreateElevator;   //엘리베이터 생성 여부


    void Start()
    {
        MaxValue = 0; //최댓값 초기화
        ProgressRate = 0f;  //진행률 초기화
        EnemyCount = 0; //Enemy수 초기화
        isCreateElevator = false;   //엘리베이터 생성 여부 초기화
        
        for (int i = 0; i < 79; i++)    //지나가지 않은 길 리스트 추가(초기화)
            leftBlock.Add(i);
    }

    void Update()
    {
        MaxValue = Count.Max(); //최댓값 반환
        Progress(); //진행률 계산
        ProgressEvent();    //진행률에 따라 이벤트
    }

    public void addCount(int index)
    {
        //인덱스번째의 카운트 배열 값을 증가시키는 함수
        //블록이 충돌처리 되면 자신의 인덱스로 이 함수 호출
        Count[index]++; //길 지나간 횟수 카운트
        
        if (leftBlock.Contains(index))  //만약 지나가지 않은 길 리스트에 index가 있다면
        {
            leftBlock.Remove(index);    //index번째가 아니라 index값을 삭제
        }

    }

    public int MaxCount()
    {
        //가장 많은 중복 카운트값을 구하는 함수
        //배열 중 가장 높은 값을 구하여 반환
        return MaxValue;

        //실시간으로 GameManager 싱글톤 값 변경하는게 나을까? 아니면 죽거나 게임 끝났을 때 값 변경하는게 나을까?
    }

    //현재 진행률을 반환하는 함수
    public double Progress()
    {
        //Count 배열에서 한 번이라도 지나간 길 퍼센트 계산

        int left = leftBlock.Count();   //남은 길의 수
        
        double temp1 = 79 - left;
        double temp2 = temp1 / 79;
        double temp3 = temp2 * 100;
        ProgressRate = temp3; //현재 진행률 계산
        
        //ProgressRate = ((79 - left) / 79) * 100;  //한 번에 계산하면 계산이 안되고 따로 나눠서 계산하면 됨.. 왜지?

        return ProgressRate;
    }

    public void ProgressEvent()
    {
        //진행률에 따라 이벤트 발생(몬스터 생성, 엘리베이터 생성 등)

        //만약 현재 진행률이 10, 20, 40, 60퍼센트 이상이라면
        if (ProgressRate >= 10 && EnemyCount == 0)
        {
            //Enemy 생성
            this.gameObject.GetComponent<EnemyStart>().CreateEnemy();
            EnemyCount++;
        }

        if (ProgressRate >= 20 && EnemyCount == 1)
        {
            //Enemy 생성
            this.gameObject.GetComponent<EnemyStart>().CreateEnemy();
            EnemyCount++;
        }

        if (ProgressRate >= 40 && EnemyCount == 2)
        {
            //Enemy 생성
            this.gameObject.GetComponent<EnemyStart>().CreateEnemy();
            EnemyCount++;
        }

        if (ProgressRate >= 60 && EnemyCount == 3)
        {
            //Enemy 생성
            this.gameObject.GetComponent<EnemyStart>().CreateEnemy();
            EnemyCount++;
        }


        if (ProgressRate == 100 && isCreateElevator == false)    //만약 현재 진행률이 100퍼센트라면 == 모든 길을 다 돌았다면
        { 
            //GameManager의 isFinished를 true로 설정
            //GameManager.instance.isFinished = true;

            //엘리베이터 생성 메시지 출력
            this.gameObject.GetComponent<DialogManager>().CreateElevatorMessage();
            isCreateElevator = true; //엘리베이터 생성 완료
        }
    }

}
