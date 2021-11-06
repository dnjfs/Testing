using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  //Max()를 사용하기 위해 명시

public class Repetition : MonoBehaviour
{
    public int[] Count;   //블록별 중복 횟수를 저장할 배열

    //지나가지 않은 길을 저장해놓고 지나가면 없애는 방식으로 계산
    //지나가지 않은 길을 저장할 리스트
    public List<int> leftBlock = new List<int>();

    public int MaxValue;    //최대 중복값
    public double ProgressRate;  //진행률
    public int EnemyCount;  //Enemy의 수

    public bool isCreateElevator;   //엘리베이터 생성 여부

    //T맵은 중복성 체크 블록 47개
    //E, S맵은 중복성 체크 블록 65개
    int NumberOfBlock;  //중복성 체크 블록의 개수

    void Start()
    {
        MaxValue = 0; //최댓값 초기화
        ProgressRate = 0f;  //진행률 초기화
        EnemyCount = 0; //Enemy수 초기화
        isCreateElevator = false;   //엘리베이터 생성 여부 초기화

        if (GameManager.instance.mazeType == "T")
            NumberOfBlock = 47;
        else
            NumberOfBlock = 65;

        Count = new int[NumberOfBlock]; //블록 개수 초기화

        for (int i = 0; i < NumberOfBlock; i++)    //지나가지 않은 길 리스트 추가(초기화)
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

    //현재 진행률을 반환하는 함수
    public double Progress()
    {
        //Count 배열에서 한 번이라도 지나간 길 퍼센트 계산

        int left = leftBlock.Count();   //남은 길의 수
        
        double temp1 = NumberOfBlock - left;
        double temp2 = temp1 / NumberOfBlock;
        double temp3 = temp2 * 100;
        ProgressRate = temp3; //현재 진행률 계산
        
        //ProgressRate = ((79 - left) / 79) * 100;  //한 번에 계산하면 계산이 안되고 따로 나눠서 계산하면 됨

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
            GameManager.instance.isFinished = true;
            GameManager.instance.repetitionCount = MaxValue;
            GameManager.instance.timeScore = GameManager.instance.playTime; //플레이타임 저장

            //엘리베이터 생성 메시지 출력
            this.gameObject.GetComponent<Dialog_Maze>().CreateElevatorMessage();

            //엘리베이터 열고 10초 뒤 닫음
            int elevatorIndex = GameManager.instance.elevatorIndex; //플레이어 엘리베이터 인덱스를 가져옴
            this.GetComponent<DoorManager>().OpenDoor(elevatorIndex, elevatorIndex);    //엘리베이터 문을 열고 10초 뒤 닫는다.

            isCreateElevator = true; //엘리베이터 생성 완료

            //플레이어와 괴물의 속도 증가
            GameObject.FindWithTag("Player").GetComponent<Player>().SpeedBoostPlayer();
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Monster"))
                obj.GetComponent<Enemy>().SpeedBoostEnemy();
        }
    }

}
