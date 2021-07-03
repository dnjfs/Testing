using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_TheWhiteRoom : MonoBehaviour
{
    int dialogNum;  //Ż�� ��ȭâ ��� Ƚ��
    public GameObject joyStick;

    void Start()
    {
        dialogNum = 0;  //�ʱ�ȭ
    }

    void OnTriggerEnter(Collider other)
    {
        //���� �÷��̾�� �ε����ٸ�
        if (other.gameObject.tag == "Player")
        {
            if (dialogNum == 0) //��ȭâ�� �� ���� ��µ��� �ʾҴٸ�
            {
                //���̽�ƽ ����
                joyStick.gameObject.SetActive(false);   //�÷��̾� ���� ��Ȱ��ȭ
                GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //�÷��̾� ����
                GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(93.4f, 7.8f, 584.6f);    //�÷��̾� ��ġ �ű�
                GameObject.FindWithTag("Player").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f); //�÷��̾� ȸ�� �ʱ�ȭ

                //�Ͼ� ���� ���� ����
                GameObject.FindWithTag("WhiteDoorOpenBlock").GetComponent<OpenTheWhiteDoor>().CloseTheWhiteDoor();

                //������ ��ȭâ ���
                GameObject.FindWithTag("GameSystem").GetComponent<DialogManager>().EnterWhiteRoom();
                dialogNum++;
            }
        }
    }
}
