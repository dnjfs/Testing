using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenTheWhiteDoor : MonoBehaviour
{
    public GameObject whiteLeftDoor;    //�Ͼ� ���� ���� ��
    public GameObject whiteRightDoor;   //�Ͼ� ���� ������ ��

    void OnTriggerEnter(Collider other)
    {
        //���� �÷��̾�� �ε����ٸ�
        if (other.gameObject.tag == "Player")
        {
            //�Ͼ� ���� �� ����
            whiteLeftDoor.transform.DOLocalMoveX(5f, 3f).SetRelative();  //3�ʰ� X �������� 5��ŭ �̵�
            whiteRightDoor.transform.DOLocalMoveX(-6.5f, 3f).SetRelative();  //3�ʰ� X �������� -5��ŭ �̵�
        }
    }

    //�Ͼ� ���� ���� �ݴ� �Լ�
    public void CloseTheWhiteDoor()
    {
        whiteLeftDoor.transform.DOLocalMoveX(-5f, 3f).SetRelative();  //3�ʰ� X �������� -5��ŭ �̵�
        whiteRightDoor.transform.DOLocalMoveX(6.5f, 3f).SetRelative();  //3�ʰ� X �������� 5��ŭ �̵�
    }
}
