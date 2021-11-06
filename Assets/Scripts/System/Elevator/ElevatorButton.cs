using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween을 사용하기 위해 임포트

public class ElevatorButton : MonoBehaviour
{
    GameObject system;
    GameObject upButton;    //엘리베이터 위 버튼(엘리베이터 여는 버튼)
    //GameObject firstFloorButton;    //엘리베이터 1층 버튼
    //GameObject closeButton; //엘리베이터 닫힘 버튼

    bool isPushing;   //버튼을 눌리고 있는지 여부 
    public int elevator;    //탈출구 엘리베이터 인덱스

    void Start()
    {
        isPushing = false;  //버튼 눌리고 있지 않은 상태
        system = GameObject.FindWithTag("GameSystem");  //DoorManager 스크립트를 가지고 있는 GameSystem 오브젝트를 가져옴
        elevator = GameManager.instance.elevatorIndex;  //탈출구 엘리베이터 인덱스를 가져옴

        //버튼들을 가져옴
        upButton = this.transform.GetChild(3).GetChild(0).gameObject;   //위로 누르는 버튼(엘리베이터 여는 버튼)
        
    }

    void Update()
    {
        //버튼 눌림 이벤트 매 프레임마다 체크
        PushOpenButton(); 
        PushCloseButton();
        PushFirstFloorButton();
    }

    //엘리베이터 버튼을 누르면 문이 열리는 함수
    public void PushOpenButton()
    {
        //테스트를 위한 pc용
        //if (Input.GetMouseButtonDown(0))    //엘리베이터 버튼을 터치하면(테스트용)
        if (Input.GetMouseButtonDown(0) && GameManager.instance.isFinished)    //모든 미로를 다 돌았고 엘리베이터 버튼을 터치하면
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //터치한 지점을 가져옴

            if (Physics.Raycast(ray, out hit, 5f))          //Mathf.Infinity를 사용하면 무한대 거리에서 측정(멀리서 눌러도 인식됨) -> 3f 거리에서 측정되도록 설정
            {
                if (hit.collider.tag == "ElevatorOpenButton")   //터치한 오브젝트가 엘리베이터 버튼이라면
                {
                    StartCoroutine(ButtonDown());   //버튼 들어갔다 나오는 움직임
                    system.GetComponent<DoorManager>().OpenDoor(elevator, elevator);  //탈출구 엘리베이터 열리기

                    /*
                    for (int i = 0; i < 12; i++)
                    {
                        system.GetComponent<DoorManager>().OpenDoor(i, i);  //모든 탈출구 엘리베이터 열리기(문 제대로 열리는지 확인용)
                    }
                    */
                }
            }

        }        
    }
    
    //1층 버튼을 누르고 문이 닫히면 엘리베이터가 위로 올라가도록 설정
    public void PushFirstFloorButton()
    {
        //테스트를 위한 pc용(테스트를 위해 진행률 100프로 조건 없앰)
        if (Input.GetMouseButtonDown(0))    //엘리베이터 버튼을 터치하면
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //터치한 지점을 가져옴

            if (Physics.Raycast(ray, out hit, 5f))
            {
                bool isDoorClose = system.GetComponent<DoorManager>().isDoorClose();

                if (hit.collider.tag == "FirstFloorButton" && isDoorClose)   //터치한 오브젝트가 1층 버튼이고 엘리베이터 문이 닫힌 상태라면
                {
                    //StartCoroutine(ButtonDown());   //버튼 들어갔다 나오는 움직임
                    system.GetComponent<ElevatorUp>().UpToFirstFloor();  //1층으로 올라가기
                }
            }
        }
        
    }

    
    //엘리베이터 안의 닫힘 버튼을 누르면 문이 닫히는 함수
    public void PushCloseButton()
    { 
        
        //엘리베이터 안의 버튼이라 진행률 100프로 조건 없음.
        if (Input.GetMouseButtonDown(0))    //엘리베이터 버튼을 터치하면
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //터치한 지점을 가져옴

            if (Physics.Raycast(ray, out hit, 5f))
            {
                if (hit.collider.tag == "ElevatorCloseButton")   //터치한 오브젝트가 엘리베이터 버튼이라면
                {
                    //StartCoroutine(ButtonDown());   //버튼 들어갔다 나오는 움직임
                    system.GetComponent<DoorManager>().CloseDoor(elevator, elevator);  //탈출구 엘리베이터 닫기
                }
            }
        }
        
        
    }
    

    //버튼이 눌러졌다 나오는 모션
    IEnumerator ButtonDown()
    {
        if (!isPushing)
        {
            isPushing = true;   //버튼 눌리고 있음 설정

            if (elevator == 3 || elevator == 8)
            {
                upButton.transform.DOLocalMoveX(-0.02f, 0.5f).SetRelative();  //Z축 값 0.02 만큼 감소(상대적 값)
                yield return new WaitForSeconds(0.5f);    //0.5초 뒤
                upButton.transform.DOLocalMoveX(0.02f, 0.5f).SetRelative();  //Z축 값 0.02 만큼 증가(상대적 값)
            }
            else
            {
                upButton.transform.DOLocalMoveZ(-0.02f, 0.5f).SetRelative();  //Z축 값 0.02 만큼 감소(상대적 값)
                yield return new WaitForSeconds(0.5f);    //0.5초 뒤
                upButton.transform.DOLocalMoveZ(0.02f, 0.5f).SetRelative();  //Z축 값 0.02 만큼 증가(상대적 값)
            }

            yield return new WaitForSeconds(0.5f);    //0.5초 뒤
            isPushing = false;          //버튼 눌리고 있지 않음 설정
        }

        yield return null;
    }
}
