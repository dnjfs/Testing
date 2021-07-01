using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_TheWhiteRoom : MonoBehaviour
{
    int dialogNum;  //Ż�� ��ȭâ ��� Ƚ��

    //�Ͼ�� �޽���
    private string[] theWhiteRoomDialogText = { "Ż���� �����մϴ�. �׷��� ���� ���� �ƴմϴ�. " +
            "����� ���ϴ� �ش��� ���⼭ ã�� �� ���� ���Դϴ�."};

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
                //�Ͼ� ���� ���� ����
                GameObject.FindWithTag("WhiteDoorOpenBlock").GetComponent<OpenTheWhiteDoor>().CloseTheWhiteDoor();

                //������ ��ȭâ ���
                GameObject.FindWithTag("GameSystem").GetComponent<DialogManager>().EnterWhiteRoom();
            }
        }
    }
}
