using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_TheWhiteRoom : MonoBehaviour
{
    public GameObject joyStick;

    void OnTriggerEnter(Collider other)
    {
        //���� �÷��̾�� �ε����ٸ�
        if (other.gameObject.tag == "Player")
        {
            //���̽�ƽ ����
            joyStick.gameObject.SetActive(false);   //�÷��̾� ���� ��Ȱ��ȭ
            GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //�÷��̾� ����
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(-106f, 7.46f, 87f);    //�÷��̾� ��ġ �ű�
            GameObject.FindWithTag("Player").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f); //�÷��̾� ȸ�� �ʱ�ȭ

            //�Ͼ� ���� ���� ����
            GameObject.FindWithTag("WhiteDoorOpenBlock").GetComponent<OpenTheWhiteDoor>().CloseTheWhiteDoor();

            //������ ��ȭâ ���
            GameObject.FindWithTag("GameSystem").GetComponent<Dialog_Corridor>().EnterWhiteRoomMessage();
        }
    }
}
