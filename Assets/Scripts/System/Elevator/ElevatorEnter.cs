using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnter : MonoBehaviour
{
    public List<GameObject> monsters;
    bool isInElevator, isPlayingCoroutine, isDie; //플레이어가 엘리베이터에 입장했는지, 코루틴 실행중
    bool InFrontOfDoor; //괴물이 문 앞에 있는지

    public AudioClip quake;
    AudioSource quakeAudio;
    DoorManager doorState;

    void Start()
    {
        quakeAudio = this.gameObject.AddComponent<AudioSource>();
        quakeAudio.clip = quake;

        doorState = GameObject.Find("GameSystem").GetComponent<DoorManager>();
    }

    void Update()
    {
        if (isInElevator) //플레이어가 엘리베이터에 탑승했는데
        {
            bool checkEnemy = false;
            foreach (GameObject obj in monsters)
            {
                Debug.Log(Vector3.Distance(this.transform.position, obj.transform.position));
                if (Vector3.Distance(this.transform.position, obj.transform.position) < 8.0f) //엘리베이터 문 앞에 괴물이 다가서면
                {
                    if (!doorState.isCloseDoor && !doorState.isMoving) //문이 활짝 열려있는 경우 게임오버
                    {
                        if (isDie) //씬 전환 함수 호출 여러 번 하는 것 방지
                            return;

                        isDie = true;
                        GameObject.Find("GameSystem").GetComponent<SceneChange>().ChangeGameOverScene();
                        return; //괴물이 엘리베이터를 때리는 소리 방지
                    }

                    //문이 닫히고 있거나 닫혀있을 때
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    obj.transform.position = this.transform.position + this.transform.forward * 5;
                    InFrontOfDoor = true;
                    if (!isPlayingCoroutine)
                    {
                        isPlayingCoroutine = true;
                        StartCoroutine(AttackDoor()); //괴물이 엘리베이터 공격 시작
                    }
                    checkEnemy = true;
                }
            }
            if(!checkEnemy) //문 근처에 적이 하나도 없으면 (엘리베이터가 올라가면)
                InFrontOfDoor = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player") //플레이어가 엘리베이터에 입장하면
        {
            if (!isInElevator)
            {
                isInElevator = true;
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster")) //모든 괴물이 플레이어를 추격
                {
                    monsters.Add(obj);
                    obj.GetComponent<Enemy>().ChasePlayer();
                }
            }
        }
    }

    IEnumerator AttackDoor()
    {
        int count = 0;
        GameObject elevator = this.transform.parent.gameObject;
        while (true)
        {
            if (!InFrontOfDoor) //엘리베이터가 올라가며 괴물과 멀어지면
                break; //코루틴 종료

            if (++count >= 5) //5번 충돌하면 게임오버
                GameObject.Find("GameSystem").GetComponent<SceneChange>().ChangeGameOverScene();
            StartCoroutine(QuakeDoor(elevator));

            yield return new WaitForSeconds(2f); //2초 대기
        }
    }
    IEnumerator QuakeDoor(GameObject gameObject) //엘리베이터 흔들기
    {
        quakeAudio.Play();
        bool even = false;
        for (int i = 0; i < 4; i++)
        {
            if (even)
                gameObject.transform.Translate(Vector3.forward/2);
            else
                gameObject.transform.Translate(-Vector3.forward/2);
            even = !even;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
