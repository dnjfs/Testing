using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnter : MonoBehaviour
{
    public List<GameObject> monsters;
    bool isInElevator, isPlayingCoroutine, isDie; //�÷��̾ ���������Ϳ� �����ߴ���, �ڷ�ƾ ������
    bool InFrontOfDoor; //������ �� �տ� �ִ���

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
        if (isInElevator) //�÷��̾ ���������Ϳ� ž���ߴµ�
        {
            bool checkEnemy = false;
            foreach (GameObject obj in monsters)
            {
                Debug.Log(Vector3.Distance(this.transform.position, obj.transform.position));
                if (Vector3.Distance(this.transform.position, obj.transform.position) < 8.0f) //���������� �� �տ� ������ �ٰ�����
                {
                    if (!doorState.isCloseDoor && !doorState.isMoving) //���� Ȱ¦ �����ִ� ��� ���ӿ���
                    {
                        if (isDie) //�� ��ȯ �Լ� ȣ�� ���� �� �ϴ� �� ����
                            return;

                        isDie = true;
                        GameObject.Find("GameSystem").GetComponent<SceneChange>().ChangeGameOverScene();
                        return; //������ ���������͸� ������ �Ҹ� ����
                    }

                    //���� ������ �ְų� �������� ��
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    obj.transform.position = this.transform.position + this.transform.forward * 5;
                    InFrontOfDoor = true;
                    if (!isPlayingCoroutine)
                    {
                        isPlayingCoroutine = true;
                        StartCoroutine(AttackDoor()); //������ ���������� ���� ����
                    }
                    checkEnemy = true;
                }
            }
            if(!checkEnemy) //�� ��ó�� ���� �ϳ��� ������ (���������Ͱ� �ö󰡸�)
                InFrontOfDoor = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player") //�÷��̾ ���������Ϳ� �����ϸ�
        {
            if (!isInElevator)
            {
                isInElevator = true;
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster")) //��� ������ �÷��̾ �߰�
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
            if (!InFrontOfDoor) //���������Ͱ� �ö󰡸� ������ �־�����
                break; //�ڷ�ƾ ����

            if (++count >= 5) //5�� �浹�ϸ� ���ӿ���
                GameObject.Find("GameSystem").GetComponent<SceneChange>().ChangeGameOverScene();
            StartCoroutine(QuakeDoor(elevator));

            yield return new WaitForSeconds(2f); //2�� ���
        }
    }
    IEnumerator QuakeDoor(GameObject gameObject) //���������� ����
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
