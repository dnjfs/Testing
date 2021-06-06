using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween�� ����ϱ� ���� ����Ʈ

public class ElevatorButton : MonoBehaviour
{
    GameObject system;
    GameObject upButton;    //���������� �� ��ư(���������� ���� ��ư)
    //GameObject firstFloorButton;    //���������� 1�� ��ư
    //GameObject closeButton; //���������� ���� ��ư

    bool isPushing;   //��ư�� ������ �ִ��� ���� 
    public int elevator;    //Ż�ⱸ ���������� �ε���

    void Start()
    {
        isPushing = false;  //��ư ������ ���� ���� ����
        system = GameObject.FindWithTag("GameSystem");  //DoorManager ��ũ��Ʈ�� ������ �ִ� GameSystem ������Ʈ�� ������
        elevator = GameManager.instance.elevatorIndex;  //Ż�ⱸ ���������� �ε����� ������

        //��ư���� ������
        upButton = this.transform.GetChild(3).GetChild(0).gameObject;   //���� ������ ��ư(���������� ���� ��ư)
        
    }

    void Update()
    {
        PushOpenButton();   //��ư ���� �̺�Ʈ �׻� Ȯ��
    }

    //���������� ��ư�� ������ ���� ������ �Լ�
    public void PushOpenButton()
    {
        //pc���� GetMouseButtonDown�� �ᵵ ����Ϸ� ��ġ ������ �ɷ� ������ Ȥ�� �𸣴� �� �� �����غ�. Ȯ�� �ʿ�

        //PC��
        /*
        if (GameManager.instance.isFinished)    //�̷θ� �� ���Ƽ� ���������Ͱ� �����Ǿ��ٸ�
        {
            if (Input.GetMouseButtonDown(0))    //���������� ��ư�� ��ġ�ϸ�
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //��ġ�� ������ ������

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "ElevatorButton")   //��ġ�� ������Ʈ�� ���������� ��ư�̶��
                    {
                        StartCoroutine(ButtonDown());   //��ư ���� ������ ������
                        system.GetComponent<DoorManager>().OpenDoor(elevator, elevator);  //Ż�ⱸ ���������� ����
                    }
                }
            }
        }
        */


        //����Ͽ�
        /*
        if (GameManager.instance.isFinished)    //�̷θ� �� ���Ƽ� ���������Ͱ� �����Ǿ��ٸ�
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)    //���������� ��ư�� ��ġ�ϸ�
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); //��ġ�� ������ ������

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "ElevatorButton")   //��ġ�� ������Ʈ�� ���������� ��ư�̶��
                    {
                        StartCoroutine(ButtonDown());   //��ư ���� ������ ������
                        system.GetComponent<DoorManager>().OpenDoor(elevator, elevator);  //Ż�ⱸ ���������� ����
                    }
                }
            }
        }
        */
        

        //�׽�Ʈ�� ���� pc��(�׽�Ʈ�� ���� ����� 100���� ���� ����) -���������� �����
        if (Input.GetMouseButtonDown(0))    //���������� ��ư�� ��ġ�ϸ�
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //��ġ�� ������ ������

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "ElevatorOpenButton")   //��ġ�� ������Ʈ�� ���������� ��ư�̶��
                {
                    StartCoroutine(ButtonDown(upButton));   //��ư ���� ������ ������
                    system.GetComponent<DoorManager>().OpenDoor(elevator, elevator);  //Ż�ⱸ ���������� ����
                }
            }
        }

        //�׽�Ʈ�� ���� ����Ͽ� -> �����ؼ� Ȯ�� �ʿ�(��罺������)
        
    }
    
    //1�� ��ư�� ������ 1�� ��ư�� ���������� Ȯ���ϴ� �Լ�
    public void PushFirstFloorButton()
    {

        /*
        //�׽�Ʈ�� ���� pc��(�׽�Ʈ�� ���� ����� 100���� ���� ����)
        if (Input.GetMouseButtonDown(0))    //���������� ��ư�� ��ġ�ϸ�
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //��ġ�� ������ ������

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "FirstFloorButton")   //��ġ�� ������Ʈ�� 1�� ��ư�̶��
                {
                    StartCoroutine(ButtonDown());   //��ư ���� ������ ������
                    system.GetComponent<DoorManager>().OpenDoor(elevator, elevator);  //Ż�ⱸ ���������� ����
                }
            }
        }
        */
    }

    /*
    //���������� ���� ���� ��ư�� ������ ���� ������ �Լ�
    public void PushCloseButton()
    {

        //���������� ���� ��ư�̶� ����� 100���� ���� ����.
        if (Input.GetMouseButtonDown(0))    //���������� ��ư�� ��ġ�ϸ�
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //��ġ�� ������ ������

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "ElevatorCloseButton")   //��ġ�� ������Ʈ�� ���������� ��ư�̶��
                {
                    upButton = hit;
                    StartCoroutine(ButtonDown(closeButton));   //��ư ���� ������ ������
                    system.GetComponent<DoorManager>().OpenDoor(elevator, elevator);  //Ż�ⱸ ���������� ����
                }
            }
        }
    }
    */

    //��ư�� �������� ������ ���
    IEnumerator ButtonDown(GameObject button)
    {
        if (!isPushing)
        {
            isPushing = true;   //��ư ������ ���� ����

            if (elevator == 3 || elevator == 8)
            {
                button.transform.DOLocalMoveX(-0.02f, 0.5f).SetRelative();  //Z�� �� 0.02 ��ŭ ����(����� ��)
                yield return new WaitForSeconds(0.5f);    //0.5�� ��
                button.transform.DOLocalMoveX(0.02f, 0.5f).SetRelative();  //Z�� �� 0.02 ��ŭ ����(����� ��)
            }
            else
            {
                button.transform.DOLocalMoveZ(-0.02f, 0.5f).SetRelative();  //Z�� �� 0.02 ��ŭ ����(����� ��)
                yield return new WaitForSeconds(0.5f);    //0.5�� ��
                button.transform.DOLocalMoveZ(0.02f, 0.5f).SetRelative();  //Z�� �� 0.02 ��ŭ ����(����� ��)
            }

            yield return new WaitForSeconds(0.5f);    //0.5�� ��
            isPushing = false;          //��ư ������ ���� ���� ����
        }

        yield return null;
    }
}
